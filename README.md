# 🚀 .NET To-Do - RESTful API with Hexagonal Architecture

This project serves as a practical example and a learning environment for implementing a RESTful API using **Hexagonal Architecture** (also known as Ports & Adapters). The main goal is to demonstrate a clear separation of concerns, promoting clean, testable, and maintainable code.

## 🌟 Technologies Used

- **.NET 8:** Robust framework for application development.
- **ASP.NET Core Web API:** For building RESTful endpoints.
- **Hexagonal Architecture:** Design principles for isolating the domain from infrastructure.
- **Entity Framework Core:** ORM (Object-Relational Mapper) for database interaction.
- **PostgreSQL:** Relational database.
- **Dependency Injection:** Managing dependencies between layers.

## 🏗️ Project Structure

The project is divided into multiple layers (.NET Class Library projects) to reflect the Hexagonal Architecture:

- **`Todo.Domain`**: The **core** of the application. Contains pure business entities, domain rules, and interfaces (Ports) that define how the domain interacts with the outside world (infrastructure, application). **Has no dependencies on other layers.**
- **`Todo.Infrastructure`**: The **Infrastructure** layer. Contains the concrete implementations of domain interfaces (Adapters), such as repositories that use Entity Framework Core to persist data in PostgreSQL.
- **`Todo.Api`**: The **Primary Adapter** (Driving Adapter). This is the RESTful API that exposes system functionalities, receiving HTTP requests and translating them into Commands or Queries for the Application layer.

## 🚀 How to Run the Project

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) (installed locally or via Docker)
- [Docker](https://www.docker.com/products/docker-desktop) (optional, for running PostgreSQL)
- [dotnet-ef tool](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) (global installation: `dotnet tool install --global dotnet-ef --version 8.0.0`)

### Steps

1. **Clone the repository:**

   ```bash
   git clone [https://github.com/lucasbazev/dotnet-todo.git](https://github.com/lucasbazev/dotnet-todo.git)
   cd dotnet-todo
   ```

2. **Configure the database:**

   - Create a PostgreSQL database.
   - In the `src/Todo.Api/appsettings.json` file, update the `ConnectionStrings:DefaultConnection` with your database credentials. Example:

     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=TodoDb;Username=postgres;Password=your_password"
     }
     ```

   - **Optional: Run PostgreSQL with Docker**

     ```bash
     docker run --name TodoDb -e POSTGRES_PASSWORD=your_password -p 5432:5432 -d postgres
     ```

3. **Run Entity Framework Core Migrations:**
   This will create the database schema (tables) based on your entities.

   ```bash
   dotnet ef migrations add InitialCreate --project src/Todo.Infrastructure --startup-project src/Todo.Api
   dotnet ef database update --project src/Todo.Infrastructure --startup-project src/Todo.Api
   ```

4. **Start the API application:**

   ```bash
   dotnet run --project src/Todo.Api
   ```

5. **Access the Swagger UI:**
   With the API running, open your browser and navigate to `https://localhost:8080/swagger`. You'll be able to interact with the API using the generated interface.

---
