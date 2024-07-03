# Book Subscription System

## Overview
The Book Subscription System is an ASP.NET Core API application designed to facilitate the subscription and management of books through a RESTful API. It allows users to subscribe to books from an online catalog and manage their subscriptions. Additionally, third-party resellers can access the system via a dedicated API to provide similar functionalities.

## Features
- **User Subscription**: Users can subscribe to books available in the catalog.
- **Subscription Management**: Users can manage their subscriptions, including adding and removing books.
- **Third-Party API**: Provides API endpoints for third-party resellers to access subscription functionalities.
- **Book Catalog**: Includes functionalities to list, add, update, and delete books from the catalog.
- **Authentication and Authorization**: Uses JWT tokens for authentication and role-based access control (RBAC) for authorization.

## Technology Stack
- **ASP.NET Core**: Backend framework for building APIs and web applications.
- **C#**: Primary programming language.
- **Entity Framework Core**: ORM for database access and management.
- **Database Management System** Microsoft SQL Server
- **Testing** XUnit, Moq
- **Swagger / NSwag**: API documentation and testing tools.
- **React**: Frontend framework for building the user interface.

## Project Structure
The project follows a modular architecture with the following main directories:
```sh
Book_subscription/
├── client/                  # Client-side (if applicable)
│   ├── public/              # Public assets
│   ├── src/                 # Source code for client application
│   └── package.json         # Client-side dependencies and scripts
├── server/                  # Server-side ASP.NET Core API project
│   ├── API/                 # API layer
│   │   ├── Controllers/     # API controllers
│   │   ├── Models/          # Models for API requests and responses
│   │   ├── Services/        # Business logic services
│   │   └── appsettings.json # Configuration settings
│   ├── Core/                # Core layer
│   │   ├── Entities/        # Domain entities (e.g., Book.cs, User.cs)
│   │   ├── Services/        # Domain services implementing business logic
│   │   └── DbContext.cs     # Entity Framework DbContext
│   └── Infrastructure/      # Infrastructure layer
│       ├── Data/            # DbContext and database-related files
│       │   ├── ApplicationDbContext.cs  # Entity Framework DbContext
│       │   └── Migrations/   # Database migrations
│       ├── Repositories/    # Data access repositories
│       
└── tests/                   # Unit tests and integration tests
    ├── Unit/                # Unit tests
    └── Integration/        # Integration tests
```

## Setup Instructions
1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-repository-url.git
   cd BookSubscriptionSystem
2. **Restore dependencies**:
   ```bash
   dotnet restore
3. **Database migration**:
   ```bash
   cd Book_subscription.Server
   dotnet ef migrations add DatabaseCreate
   dotnet ef database update
4. **Run Application**:
   - Start the server
     ``` sh
     cd Book_subscription.Server
     dotnet run
   - Start the client
     ``` sh
     cd client
     npm run dev
   
5. **Clone the repository**:
   ```bash
   git clone https://github.com/your-repository-url.git
   cd BookSubscriptionSystem
   
## Configuration
- **appsettings.json**: Contains configuration settings including database connection strings, JWT token settings, and other application-specific settings.

## Testing
- Unit Tests: Located in the Tests directory, covering individual components using XUnit and Moq.
- Integration Tests: InMemory databases and WebApplicationFactory for testing API endpoints.
## License
his project is licensed under the MIT License - see the LICENSE.md file for details.

