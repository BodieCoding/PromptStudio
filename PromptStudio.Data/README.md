# PromptStudio.Data

This project contains the data access layer for PromptStudio, including:

## Contents

- **Database Context**: `PromptStudioDbContext.cs` - Entity Framework Core context
- **Database File**: `promptstudio.db` - SQLite database file
- **Migrations**: `Migrations/` - Entity Framework migrations
- **Services**: `Services/` - Data access services implementing core interfaces
- **Design-time Factory**: `PromptStudioDbContextFactory.cs` - For EF tooling

## Architecture Benefits

This separation provides several architectural benefits:

1. **Independence**: The MCP server can access data without depending on the web application
2. **Separation of Concerns**: Data access logic is isolated from presentation and business logic
3. **Reusability**: Multiple applications (web app, MCP server, CLI tools) can share the same data layer
4. **Maintainability**: Database schema changes are managed in one place

## Database Location

The SQLite database file (`promptstudio.db`) is stored in this project directory, making it:
- Easily accessible to all consuming projects
- Independent of any specific application
- Simple to backup and version control (if needed)

## Usage

### From Web Application
```csharp
// In PromptStudio/Program.cs
builder.Services.AddDbContext<PromptStudioDbContext>(options =>
    options.UseSqlite("Data Source=../PromptStudio.Data/promptstudio.db"));
```

### From MCP Server
```csharp
// In PromptStudio.Mcp/Program.cs
builder.Services.AddDbContext<PromptStudioDbContext>(options =>
    options.UseSqlite("Data Source=../PromptStudio.Data/promptstudio.db"));
```

## Migrations

To add a new migration:
```bash
cd PromptStudio.Data
dotnet ef migrations add MigrationName
```

To update the database:
```bash
cd PromptStudio.Data
dotnet ef database update
```

The design-time factory (`PromptStudioDbContextFactory`) ensures migrations work correctly even without a running application.
