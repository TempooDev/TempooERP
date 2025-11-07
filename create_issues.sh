#!/usr/bin/env bash
set -e

echo "Creando issues Semana 3 — Eventos + Outbox..."

echo "🔧 Creando labels necesarias (si no existen)..."
gh label create "backend (.NET)" --color "#0366d6" --description "Cambios o features del backend .NET" || true
gh label create "infra / aspire" --color "#7c3aed" --description "Infraestructura o configuración Aspire" || true
gh label create "observabilidad" --color "#d97706" --description "Logs, tracing, métricas" || true
gh label create "testing" --color "#2563eb" --description "Pruebas" || true
gh label create "refactor técnico" --color "#6b7280" --description "Refactors internos" || true
gh label create "week-3" --color "#8b5cf6" --description "Tareas Semana 3 — Eventos + Outbox" || true
gh label create "enhancement" --color "#a2eeef" --description "Feature o mejora" || true

# 1) Dominio: eventos de dominio base
gh issue create \
  --title "Definir eventos de dominio para Orders e Invoices" \
  --label "backend (.NET)" --label "week-3" --label "enhancement" \
  --body "$(cat << 'EOF'
### Resumen
Introducir eventos de dominio en los módulos de Orders e Invoices para reflejar cambios relevantes de negocio.

### Motivación
Preparar la arquitectura para integraciones, outbox, proyecciones y auditoría sin acoplar casos de uso entre sí.

### Propuesta de implementación
- Definir interfaz/base `IDomainEvent`.
- Añadir eventos:
  - `OrderCreatedDomainEvent`
  - `OrderConfirmedDomainEvent`
  - `InvoiceCreatedDomainEvent`
- Ajustar entidades para registrar eventos durante operaciones de dominio.
- Añadir helper para recoger eventos pendientes desde el DbContext.

### Área del proyecto
Backend (.NET)

### Fase del proyecto
Semana 3 — Eventos + Outbox

### Criteria de aceptación
- [ ] Eventos definidos en módulos correspondientes.
- [ ] Entidades disparan eventos cuando cambian estado.
- [ ] Existe forma centralizada de obtener eventos desde el contexto (ej: `GetDomainEvents()`).
- [ ] Documentado con uno o dos ejemplos.

### Notas adicionales
Sin publicar aún hacia fuera; solo eventos internos de dominio.
EOF
)"

# 2) Infra: Outbox table + mapping
gh issue create \
  --title "Crear tabla Outbox e infraestructura básica para eventos pendientes" \
  --label "backend (.NET)" --label "week-3" --label "enhancement" \
  --body "$(cat << 'EOF'
### Resumen
Añadir una tabla `OutboxMessages` y la infraestructura necesaria para almacenar eventos de integración pendientes.

### Motivación
Poder publicar eventos de negocio de forma confiable sin acoplar la transacción de negocio con la entrega externa.

### Propuesta de implementación
- Entidad `OutboxMessage` (Id, Type, Payload, OccurredOn, ProcessedOn, Attempts, etc.).
- Configuración EF Core + migración para la tabla Outbox.
- Hook en `SaveChanges` o UoW:
  - Guardar en Outbox los eventos de dominio que deban salir como eventos de integración.
- Mantener diseño simple, extensible.

### Área del proyecto
Backend (.NET)

### Fase del proyecto
Semana 3 — Eventos + Outbox

### Criteria de aceptación
- [ ] Tabla Outbox creada en BD.
- [ ] Mensajes se guardan al persistir cambios relevantes.
- [ ] Sin lógica de envío aún (siguiente issue).

### Notas adicionales
Pensado para futuros brokers/servicios, pero usable también para proyecciones internas.
EOF
)"

# 3) Worker: procesador Outbox (in-process)
gh issue create \
  --title "Implementar procesador de Outbox en background (in-process)" \
  --label "backend (.NET)" --label "week-3" --label "enhancement" \
  --body "$(cat << 'EOF'
### Resumen
Crear un background service que lea mensajes de Outbox y los marque como procesados.

### Motivación
Completar el patrón Outbox permitiendo:
- Lanzar eventos internos (ej: logging, auditoría, proyecciones).
- Servir como ejemplo de integración futura con colas sin acoplar la lógica.

### Propuesta de implementación
- `OutboxProcessor` como `BackgroundService`.
- Uso de `IDbContextFactory<ErpDbContext>`.
- Estrategia:
  - Leer mensajes pendientes (ej: batch de N).
  - Deserializar payload.
  - Invocar manejadores internos (ej: `IIntegrationEventHandler` o simple `switch` inicial).
  - Marcar como procesados.
- Configuración solo activa en entorno Development por ahora (si aplica).

### Área del proyecto
Backend (.NET)

### Fase del proyecto
Semana 3 — Eventos + Outbox

### Criteria de aceptación
- [ ] Procesador se ejecuta sin bloquear la API.
- [ ] Mensajes se marcan como procesados correctamente.
- [ ] Errores controlados (reintentos básicos o incrementos de Attempts).
- [ ] Logs mínimos para trazar qué se procesa.

### Notas adicionales
Mantener implementación sencilla y didáctica para usar en directos.
EOF
)"

# 4) Proyección: OrderDetails read model
gh issue create \
  --title "Crear proyección OrderDetails como read model optimizado" \
  --label "backend (.NET)" --label "week-3" --label "enhancement" \
  --body "$(cat << 'EOF'
### Resumen
Crear una proyección `OrderDetails` para consultas rápidas combinando Order + User + Invoice.

### Motivación
Mostrar el beneficio del patrón Outbox + proyecciones:
- Lecturas optimizadas sin sobrecargar el modelo de dominio.
- Patrón reutilizable para otros casos.

### Propuesta de implementación
- Crear tabla o vista `OrderDetailsReadModel`.
- Poblado a través de:
  - Eventos procesados desde Outbox (OrderCreated, OrderConfirmed, InvoiceCreated).
- Crear `OrderDetailsReadOnlyRepository`:
  - Consulta por `OrderId`.
- Endpoint `GET /api/orders/{id}/details` que use el read model.

### Área del proyecto
Backend (.NET)

### Fase del proyecto
Semana 3 — Eventos + Outbox

### Criteria de aceptación
- [ ] Proyección o tabla de lectura creada.
- [ ] Se alimenta desde el procesador de Outbox.
- [ ] Endpoint devuelve datos consolidados sin joins complejos ad-hoc.
- [ ] Documentado como patrón para futuros read models.

### Notas adicionales
Primera versión sencilla; se puede refinar después.
EOF
)"

# 5) Read-only Repositories + separación de lecturas
gh issue create \
  --title "Introducir ReadOnlyRepositories para consultas (CQRS ligero)" \
  --label "backend (.NET)" --label "refactor técnico" --label "week-3" \
  --body "$(cat << 'EOF'
### Resumen
Separar lecturas de escrituras usando repositorios de solo lectura para queries.

### Motivación
Alinear el código con CQRS ligero:
- Servicios de consulta no necesitan tracking.
- Consultas pueden usar proyecciones específicas.

### Propuesta de implementación
- Crear interfaz genérica `IReadOnlyRepository<TReadModel>` o específicas por módulo.
- Implementaciones para:
  - Productos (list/filters).
  - Orders (listas o dashboards).
  - OrderDetails (read model).
- Usar `AsNoTracking` y `select` directo a DTO/VM.

### Área del proyecto
Backend (.NET)

### Fase del proyecto
Semana 3 — Eventos + Outbox

### Criteria de aceptación
- [ ] Nuevos handlers de queries usan repos read-only.
- [ ] No se exponen entidades de dominio directamente en lecturas.
- [ ] Código más claro entre commands (write) y queries (read).

### Notas adicionales
Solo refactor mínimo donde aporte claridad; sin sobrecomplicar.
EOF
)"

# 6) Tests: Outbox + Proyecciones
gh issue create \
  --title "Tests de integración: flujo Outbox + OrderDetails" \
  --label "testing" --label "backend (.NET)" --label "week-3" \
  --body "$(cat << 'EOF'
### Resumen
Cubrir con tests de integración el flujo:
1) Crear/confirmar pedido.
2) Generar factura.
3) Registrar eventos en Outbox.
4) Procesar Outbox.
5) Actualizar proyección `OrderDetails`.

### Motivación
Asegurar que el patrón Outbox + proyección funciona y es demostrable en directos.

### Propuesta de implementación
- Usar BD de test (Postgres Aspire o sqlite in-memory si aplica).
- Escenario:
  - Crear pedido.
  - Confirmar pedido.
  - Crear factura asociada.
  - Ejecutar procesador de Outbox (forzado en test).
  - Verificar que `OrderDetails` contiene datos esperados.

### Área del proyecto
Testing

### Fase del proyecto
Semana 3 — Eventos + Outbox

### Criteria de aceptación
- [ ] Test verde ejecutable vía CI.
- [ ] Documentado brevemente en README/tests.

### Notas adicionales
Mantener tests rápidos y estables.
EOF
)"

echo "Issues Semana 3 creadas ✅"
