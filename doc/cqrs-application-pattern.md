# Patrón de Queries y Commands (CQRS) en TempooERP

Este documento resume el patrón que usamos para separar Lecturas (Queries) y Escrituras (Commands) por módulo, los motivos detrás de la estructura, y cómo añadir nuevas queries desde cero.

## Objetivo y motivos

- Independencia por módulo: cada módulo (p. ej., Catalog, Billing) mantiene su propia lógica de aplicación y acceso a datos sin acoplamientos directos a la infraestructura de otros módulos.
- Separación de responsabilidades: Queries no modifican estado y se optimizan para lectura; Commands modifican estado y usan Unit of Work (UoW) para persistir cambios atómicamente.
- Evitar dependencias circulares: los módulos consumen contratos publicados (Published Language) y repositorios de su propio módulo; la infraestructura implementa los puentes.
- Testabilidad y evolución: proyecciones de lectura con DTOs inmutables y handlers sin EF directo facilitan tests y cambios seguros.

## Estructura de carpetas (por módulo)

```text
src/Modules/<Modulo>/
  Application/
    Abstractions/           # Interfaces de aplicación (repositorios/ctx de lectura)
    <Feature>/
      Queries/
        <QueryName>/
          <QueryName>Query.cs
          <QueryName>Handler.cs
          <QueryName>Dto.cs (opcional)
      Commands/             # (reservado para comandos de escritura)
  Infrastructure/
    DependencyInjection.cs  # Registro de handlers del módulo

src/Infrastructure/
  Extensions/ServiceCollectionExtensions.cs   # Registro cross-cutting (DbContext, UoW, adapters)
  Data/ ErpDbContext.cs, ErpReadDbContextAdapter.cs, UnitOfWork.cs
  Repositories/ (implementaciones EF de Abstractions)

src/Modules/<Modulo>/Contracts/                # Published Language (consultas y eventos compartidos entre módulos)
```

## Queries (Lecturas)

Building blocks usados:

- `IQueryEntity` (marcador para queries)
- `IQueryHandler<TQuery, TResult>` para ejecutar la query

Patrones clave:

- Los handlers inyectan interfaces de aplicación (p. ej., `IProductReadRepository`) y nunca el `DbContext` directamente.
- Las implementaciones EF viven en `Infrastructure` y proyectan a DTOs (`AsNoTracking()`, `Select(...)`).
- Para lecturas cross-módulo, usamos contratos publicados (p. ej., `Catalog.Contracts.IProductsQuery`).

Ejemplo (Catálogo):

- Query: `GetProductsListQuery : IQueryEntity`
- Handler: `GetProductsListHandler : IQueryHandler<GetProductsListQuery, IEnumerable<ProductListDto>>`
- Abstracciones: `IErpReadDbContext`, `IProductReadRepository`
- Infra: `ErpReadDbContextAdapter` y `ProductReadRepository` (proyección a `ProductListDto`)
- Registro DI: en `Catalog/Infrastructure/DependencyInjection.cs` se registra el handler; en `Infrastructure/Extensions/ServiceCollectionExtensions.cs` se registran DbContext, adapters y repos.

## Commands (Escrituras)

Intención:

- Los comandos cambian estado del dominio del módulo. Deben validar reglas y persistir con `IUnitOfWork`.
- Los comandos publican eventos de integración (outbox) cuando el cambio es relevante para otros módulos.

Piezas:

- `IUnitOfWork` (ya disponible) con implementación en `Infrastructure/Data/UnitOfWork.cs`.
- Interfaces `ICommand`/`ICommandHandler` (pendientes de introducir de forma simétrica a queries). Mientras tanto, los services de aplicación pueden usar `IUnitOfWork` directamente.

Estructura esperada (cuando se añadan):

```plaintext
Application/<Feature>/Commands/<CommandName>/<CommandName>Command.cs
Application/<Feature>/Commands/<CommandName>/<CommandName>Handler.cs
```

En el handler se inyectan repositorios de escritura del propio módulo y `IUnitOfWork` para `SaveChangesAsync()`.

## Registro de dependencias

- Infraestructura global (`AddInfrastructure`):
  - `ErpDbContext` (con `UseNpgsql`, `QueryTrackingBehavior.NoTracking` por defecto)
  - Adaptadores de lectura: `IErpReadDbContext -> ErpReadDbContextAdapter`
  - Repositorios de lectura: `IProductReadRepository -> ProductReadRepository`
  - Published queries: `Catalog.Contracts.IProductsQuery -> Infrastructure.Repositories.ProductReadRepository`
  - Unit of Work: `IUnitOfWork -> UnitOfWork`

- Módulo (`Modules/<Modulo>/Infrastructure/DependencyInjection.cs`):
  - Registro de `IQueryHandler<,>` (y en el futuro `ICommandHandler<>`) del módulo.

## Cómo añadir una nueva Query desde cero

Supongamos que queremos listar categorías (Categories) en el módulo Catalog.

1) Definir el DTO (opcional si proyectas a un tipo existente)

```csharp
src/Modules/Catalog/Application/Categories/Queries/GetCategoriesList/CategoryListDto.cs

namespace TempooERP.Modules.Catalog.Application.Categories.Queries.GetCategoriesList;

public sealed record CategoryListDto(Guid Id, string Name);
```

1) Crear la query

```csharp
src/Modules/Catalog/Application/Categories/Queries/GetCategoriesList/GetCategoriesListQuery.cs

using TempooERP.BuildingBlocks.Application;

namespace TempooERP.Modules.Catalog.Application.Categories.Queries.GetCategoriesList;

public sealed record GetCategoriesListQuery : IQueryEntity;
```

1) Declarar una abstracción de lectura si no existe

```csharp
src/Modules/Catalog/Application/Abstractions/ICategoryReadRepository.cs

using System.Linq;

namespace TempooERP.Modules.Catalog.Application.Abstractions;

public interface ICategoryReadRepository
{
    IQueryable<CategoryListDto> QueryProjection();
    Task<List<CategoryListDto>> GetAllAsync(CancellationToken ct);
}
```

1) Implementar la abstracción en Infrastructure

```csharp
src/Infrastructure/Repositories/CategoryReadRepository.cs

using Microsoft.EntityFrameworkCore;
using TempooERP.Infrastructure.Data;
using TempooERP.Modules.Catalog.Application.Abstractions;
using TempooERP.Modules.Catalog.Application.Categories.Queries.GetCategoriesList;

namespace TempooERP.Infrastructure.Repositories;

public sealed class CategoryReadRepository(IErpReadDbContext db) : ICategoryReadRepository
{
    private readonly IErpReadDbContext _db = db;

    public IQueryable<CategoryListDto> QueryProjection() =>
        _db.Categories.Select(c => new CategoryListDto(c.Id, c.Name));

    public Task<List<CategoryListDto>> GetAllAsync(CancellationToken ct) =>
        QueryProjection().OrderBy(c => c.Name).ToListAsync(ct);
}
```

1) Crear el handler

```csharp
src/Modules/Catalog/Application/Categories/Queries/GetCategoriesList/GetCategoriesListHandler.cs

using TempooERP.BuildingBlocks.Application;
using TempooERP.Modules.Catalog.Application.Abstractions;

namespace TempooERP.Modules.Catalog.Application.Categories.Queries.GetCategoriesList;

public sealed class GetCategoriesListHandler(ICategoryReadRepository repo) :
    IQueryHandler<GetCategoriesListQuery, IEnumerable<CategoryListDto>>
{
    private readonly ICategoryReadRepository _repo = repo;

    public Task<IEnumerable<CategoryListDto>> Handle(GetCategoriesListQuery q, CancellationToken ct) =>
        _repo.GetAllAsync(ct).ContinueWith(t => (IEnumerable<CategoryListDto>)t.Result, ct);
}
```

1) Registrar en DI

- En `Modules/Catalog/Infrastructure/DependencyInjection.cs` añade:

```csharp
services.AddScoped<IQueryHandler<GetCategoriesListQuery, IEnumerable<CategoryListDto>>, GetCategoriesListHandler>();
```

- En `Infrastructure/Extensions/ServiceCollectionExtensions.cs` añade:

```csharp
services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
```

1) Exponer endpoint (opcional)

En `Api/Program.cs`:

```csharp
catalogApi.MapGet("/categories", async (
    IQueryHandler<GetCategoriesListQuery, IEnumerable<CategoryListDto>> handler,
    CancellationToken ct) =>
{
    var items = await handler.Handle(new GetCategoriesListQuery(), ct);
    return Results.Ok(items);
});
```

1) Consideraciones

- Usa `AsNoTracking()` y proyecciones (`Select`) en repositorios de lectura.
- Usa `CancellationToken` en todas las operaciones asíncronas.
- Mantén DTOs inmutables (records) y namespaces alineados con la estructura de carpetas.
- Para lecturas cross-módulo, define interfaces en `Contracts` y provee implementación en `Infrastructure` apuntando a esa interfaz.

## Apéndice: Commands (cuando se añadan interfaces)

Pasos similares, pero:

1) `CreateOrderCommand : ICommand`
2) `CreateOrderHandler : ICommandHandler<CreateOrderCommand>`
3) Inyectar repos de dominio de escritura de tu módulo
4) Validar reglas, aplicar cambios al agregado
5) `await _uow.SaveChangesAsync(ct)`
6) Publicar eventos de integración (outbox) si procede

Esto mantiene la simetría con Queries pero con UoW y consistencia eventual entre módulos.
