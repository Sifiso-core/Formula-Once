# 🏎️ FormulaOnce — Modular Monolith Project for F1 (2026 Season)

**FormulaOnce** is a high-performance, backend-focused application built for the **Formula 1 – 2026 season**.  
It is architected as a **Modular Monolith**, deliberately balancing the simplicity of a single deployment with the scalability and discipline typically associated with microservices.

This project is designed as a **portfolio-grade system** that demonstrates real-world backend engineering practices: Domain-Driven Design, Clean Architecture, event-driven integration, and high-throughput commerce logic.

---

## 🏛️ Architecture & Design Philosophy

FormulaOnce follows **Clean Architecture** using folders instead of class libraries, folders are enforced by unit tests using ArchUnitNet to enforce architecture rules and FormulaOnce uses **Domain-Driven Design (DDD)** to keep business rules isolated, explicit, and testable.

Key architectural goals & pillars:
- Vertical Slices (Feature-Based): To minimize coupling and improve maintainability, the project schews a traditional Application Layer in favor of vertical slices. These are housed within the Endpoints directory, leveraging FastEndpoints to group logic by functional requirement rather than technical role.
- Domain-Driven Design (DDD): Business logic is isolated within the Domain layer. This ensures that core rules remain explicit, testable, and entirely independent of external concerns like databases or UI frameworks.
- Automated Governance: To prevent architectural drift, we use ArchUnitNet. These unit tests programmatically enforce dependency rules—for example, ensuring the Domain layer never takes a dependency on Infrastructure.
- Clear separation of concerns
- Explicit module boundaries
- Predictable persistence behavior
- Easy evolution toward distributed systems

> One deployment. Many modules. Zero spaghetti.

---

## 🧩 Modular Monolith Structure

All modules share a **single database**, but enforce **strict internal boundaries** at the code level.  
Each module owns its **domain model**, **DbContext**, and **migrations**.

### 📦 Identity Module
- User registration & authentication
- JWT token issuance
- Role-based access control
- Publishes integration events (e.g. `UserCreated`) (In Progress)

### 🛒 Commerce Module
- Product catalog
- Shopping cart lifecycle
- Checkout flow foundations
- Stock management with concurrency considerations

> ⚠️ **Note**: Order processing is **still a work in progress** and will be completed as part of the event-driven integration phase.

### 🏎️ Teams Module
- F1 teams management
- Organizational structures
- Team-to-commerce relationships

### 🏁 Events Module
- Race & circuit management
- Scheduling of:
  - Grand Prix races
  - Sprint races
  - Qualifying sessions
- Dedicated scheduling service for season planning

---

## 🚀 Key Technical Patterns

### 🔹 REPR Pattern (Request → Endpoint → Response)
Implemented using **FastEndpoints**, providing:
- Vertical slice architecture
- Cleaner alternatives to bloated controllers
- Highly discoverable endpoints

### 🔹 Encapsulated Domain Models
- Aggregate roots protect their own invariants
- Example: `Cart` manages `CartItem` logic internally
- No anemic domain models

### 🔹 Explicit Persistence Mapping
- Explicit foreign keys in EF Core
- Shadow properties resolved intentionally
- Predictable database behavior
- Reduced concurrency surprises

### 🔹 Result Pattern
- Uses **Ardalis.Result**
- Business failures handled without throwing exceptions
- Exceptions reserved for truly exceptional cases
- Centralized global exception handling

---

## 🛠️ Tech Stack

| Layer | Technology |
|------|------------|
| Framework | .NET 10 |
| API | FastEndpoints |
| Database | SQL Server |
| ORM | Entity Framework Core |
| Messaging | MassTransit (RabbitMQ / In-Memory) |
| Caching | Redis (planned) |
| Testing | REST `.http` client & xUnit |
| Containerization | Docker (planned) |
| Cloud | Azure (planned) |

---

## 🗺️ Project Roadmap

FormulaOnce is developed incrementally — starting as a strong modular monolith and evolving toward a **distributed, event-driven architecture**.

### ✅ Completed

- [x] **Identity System**  
  Secure authentication and JWT issuance
- [x] **Commerce Foundations**  
  Cart lifecycle and core domain logic
- [x] **DDD Persistence Strategy**  
  Explicit FK mapping and module-owned DbContexts
- [x] **Teams Module**  
  F1 organizational management
- [x] **Events Module**  
  Races, circuits, sprints, qualifying sessions, and scheduling logic

### 🏗️ In Progress (Work in Progress)

- [ ] **Event-Driven Integration**  
  `UserCreatedIntegrationEvent` via MassTransit & RabbitMQ to decouple Identity and Commerce
- [ ] **Order Processing**  
  Finalizing order creation and post-checkout workflows
- [ ] **Inventory Guard**  
  Optimistic concurrency control for high-traffic checkouts
- [ ] **Redis Caching**  
  Distributed caching for read-heavy endpoints

### 🎯 Future Milestones

- [ ] **Email Module**  
  Transactional emails (welcome, order confirmations) triggered by domain events
- [ ] **Reports Module**  
  Read-optimized analytics
- [ ] **Dockerization**  
  Full containerized development & deployment workflow
- [ ] **Azure Deployment**
  - Azure App Service
  - Azure SQL
  - Azure Service Bus (message broker)

---

## ⚙️ Installation & Setup

### 1️⃣ Requirements

- .NET 10.0 SDK
- SQL Server (LocalDB or Docker)

### 2️⃣ Database Initialization (Per Module)

FormulaOnce uses **multiple DbContexts**, one per module.  
Migrations are applied **explicitly per context**.

```bash
dotnet-ef database update \
  -s .\Host\FormulaOnce.Api\ \
  -c CommerceDbContext
```
### Supported DbContext
- TeamsDbContext
- UserDbContext
- EventsDbContext
- CommerceDbContexts
