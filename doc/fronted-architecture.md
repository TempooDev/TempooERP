# TempooERP – Estructura Frontend (Angular 20)

> Angular 20 · Standalone · Rutas por features · Nueva sintaxis `@if / @for / @switch`  
> Alineado con los módulos backend: Identity, Catalog, Sales, Billing, etc.

## 📂 Raíz (`src/app`)

| Elemento            | Contenido                                      | Notas |
|---------------------|-----------------------------------------------|-------|
| `app.config.ts`     | `provideRouter`, `provideHttpClient`, interceptores globales, etc. | Punto central de configuración. |
| `app.routes.ts`     | Rutas raíz: monta `ShellComponent` y carga `features/*`. | Similar a `MapXEndpoints` del backend. |
| `app.ts`            | `AppComponent` standalone con `<router-outlet>`. | Bootstrap UI. |
| `app.css`           | Estilos globales / Tailwind.                  | Opcional pero recomendado. |
| `main.ts`           | `bootstrapApplication(AppComponent, appConfig)`. | Entry point. |

---

## 🧱 `core/` — Infraestructura de la app (cross-cutting)

Equivalente a `Infrastructure` + parte de `BuildingBlocks` en backend.

**Nunca** lógica específica de negocio aquí.

```text
src/app/core/
  api/
    api-client.service.ts      # Wrapper HttpClient → siempre `/api/...`
    /health/health.service.ts          # Usa /api/health para monitorizar backend
    api.interceptor.ts         # (Opc) añade headers comunes, tracing, etc.
  auth/
    auth.service.ts
    auth.guard.ts
    auth.store.ts              # Signals/Rx para sesión
  layout/
    shell.component.ts         # Layout principal (header + router-outlet)
    shell.component.html       # Usa @switch para estado backend
  config/
    app-config.service.ts      # Carga config runtime si hace falta
  services/
    notification.service.ts
    logger.service.ts
  core.providers.ts            # Agrupa providers globales (opcional)
```

## Estructura de `features`
 
```text
features/catalog/
  pages/
    product-list/
      product-list.page.ts
      product-list.page.html
    product-edit/
      product-edit.page.ts
      product-edit.page.html
  components/
    product-table/
      product-table.component.ts
  services/
    products.api.ts            # Llama a /api/catalog/products...
  catalog.routes.ts            # Rutas propias del módulo
```

## Estructura de `shared`

```text
src/app/shared/
  ui/
    button/
    card/
    table/
  pipes/
    currency.pipe.ts
    date.pipe.ts
  directives/
    autofocus.directive.ts
  models/
    pagination.ts
    api-response.ts
```

## ✅ Resumen rápido

- core/ → Infraestructura global del front (como Infrastructure backend).

- features/ → Contextos de negocio (como módulos backend).

- shared/ → Componentes/pipes/directivas UI genéricas.

- Standalone components + nueva sintaxis Angular 20 en todos lados.

- /api/... como contrato estable entre front y back (Aspire/gateway hacen el wiring).