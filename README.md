# 📦 TempooERP
### *De monolito a arquitectura moderna paso a paso*  

> Proyecto educativo para aprender desarrollo profesional migrando una aplicación real desde un **monolito simple** hasta un **sistema modular orquestado con .NET Aspire**, Angular y buenas prácticas.

---

## 🎯 Objetivo del proyecto

Construir una aplicación real para autónomos / pequeñas empresas:

- 📁 Clientes y productos  
- 🛒 Pedidos  
- 🧾 Facturación + PDF  
- 💰 Pagos  
- 📊 Dashboard de métricas  
- ✉️ Emails transaccionales  
- ✅ (Más adelante) Integración fiscal / Verifactu  

Y aprender a:

- Diseñar desde **monolito → modular monolith → servicios**
- Implementar **CQRS Light + Domain Events**
- Aplicar **Outbox Pattern** desde el principio
- Adoptar **observabilidad real**: logs, métricas, traces
- Usar **.NET Aspire** para orquestar infra y apps
- Mantener **buenas prácticas y evolución guiada**

🔥 *Construimos software real para aprender arquitectura de verdad.*

---

## 🧱 Stack Inicial

| Área | Tecnología |
|---|---|
Backend | .NET 10 — Minimal API |
Orquestación | **.NET Aspire** |
Frontend | Angular + Tailwind |
Infra Dev | PostgreSQL + Redis + Mailpit + Seq |
DB | EF Core |
Testing | xUnit (Playwright después) |
Build/CD | GitHub Actions |
Estilo | Clean, incremental, didáctico |

---

## 🏗️ Filosofía del Proyecto

✅ Empieza simple → mejora por iteraciones  
✅ Cada paso explicado y justificado  
✅ Software ejecutable en todo momento  
✅ Documentación clara en cada fase  
🚫 Nada de microservicios prematuros  
🚫 Nada sin una necesidad clara  

---

## 🗺️ Fases del proyecto

### **Fase 0 — Monolito MVP**
- Minimal API + EF Core + Angular
- Persistencia, seed, salud, logs a Seq
- Primera UI navegable

### **Fase 1 — Modular Monolith**
- Módulos: Catalog, Sales, Billing, Identity
- CQRS Light
- Domain Events (in-process)
- Outbox Pattern + Worker local

### **Fase 2 — Observabilidad & Robustez**
- OpenTelemetry: traces, metrics, logs
- Health checks
- Polly (reintentos / circuit breakers)
- Feature flags

### **Fase 3 — Integraciones**
- Email transaccional (Mailpit → proveedor real)
- PDF completo
- Import/export CSV
- Background jobs
- Stripe Sandbox
- Verifactu (compliance)

### **Fase 4 — Ready to Break Out**
- Mensajería externa
- Gateway/API boundary
- Extraer primer servicio **si tiene sentido**
- IaC + despliegue cloud

---

## 📅 Plan Semana 1 — “Bases y primer módulo”

### 🎬 **Día 1 — Kickoff + Orquestación**
**Objetivo:** un solo comando para levantar todo

- Crear solución con **.NET Aspire**
- Configurar recursos:
  - API
  - Angular dev server
  - PostgreSQL
  - Redis
  - Mailpit
  - Seq
- Comprobación entorno y enlaces

✅ Resultado:  
`dotnet run` en AppHost → todo vivo  
Swagger accesible  
Angular sirviendo y leyendo `API_URL`

---

### 🧩 **Día 2 — Catálogo: migraciones + seed + endpoint**
**Objetivo:** primer módulo funcional

- Entidad `Product`
- EF Core + migración inicial
- Seeder inicial
- `GET /api/catalog/products`
- Angular: tabla de productos

✅ Resultado:  
UI lista de productos desde Postgres

---

### 🛠 **Día 3 — CRUD + UI + instrumentación**
**Objetivo:** crear, editar, borrar + trazas iniciales

- Formularios Angular
- Validaciones básicas
- Endpoints `POST`, `PUT`, `DELETE`
- OpenTelemetry traces
- Logs estructurados a Seq

✅ Resultado:  
CRUD completo + observabilidad básica

---

### 🧪 **Día 4 — Testing + limpieza + acuerdos**
**Objetivo:** base de calidad y disciplina

- Tests simples dominio + 1 endpoint
- Reglas de commits y ramas
- Documentación de decisiones
- Limpiar estructura
- Comentarios y próximos pasos

✅ Resultado:  
CI Build + Tests OK  
Primera versión estable

---

## ✅ Definition of Done

- Funciona **end-to-end** (UI → API → DB)
- Validaciones mínimas
- 1 test mínimo
- Logs + trazas mínimas
- Documentación breve
- Demo en stream

---

## 📌 Roadmap (simplificado)

| Semana | Tema |
|---|---|
1 | Base Aspire + Catálogo |
2 | Pedidos + Facturación MVP |
3 | Eventos + Outbox + Email |
4 | Observabilidad + Flags + CSV |
5+ | Integraciones reales y despliegue |

---

## 🤝 Contribuir

- Proyecto educativo abierto
- Issues etiquetadas: `good first issue`
- PRs bienvenidos

---

## 🌐 Sígueme y acompaña el desarrollo

📺 Twitch: _[tu enlace]_  
▶️ YouTube: _[tu enlace]_  
🐙 GitHub: _[repo]_  
💬 Twitter/X: _[tu enlace]_  

---

## 📎 Licencia
MIT

---

> ✨ *Aprendemos arquitectura construyendo software real, paso a paso y sin humo.*

