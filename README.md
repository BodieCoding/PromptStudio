# Promptlet

This repository contains the Promptlet project, a suite of tools and applications for working with prompts.

## Project Structure

The project is organized into several main components:

*   **`PromptStudio/`**: The main ASP.NET Core web application for managing and experimenting with prompts.
*   **`PromptStudio.Core/`**: Contains the core business logic, domain models, and interfaces for the Promptlet application.
*   **`PromptStudio.Data/`**: Handles data access, including the Entity Framework Core setup, database context, and migrations.
*   **`PromptStudio.Mcp/`**: An ASP.NET Core project implementing a Model Context Protocol (MCP) server.
*   **`PromptStudioMcpServer/`**: Another ASP.NET Core project, likely related to MCP server functionalities.
*   **`mcp-server/`**: A Node.js-based MCP server implementation.

The solution is managed by `PromptStudio.sln` and can be opened in Visual Studio. For VS Code users, the `PromptStudio.code-workspace` file is provided.

## Getting Started

1.  **Clone the repository.**
2.  **Environment Setup**:
    *   Several PowerShell scripts (`.ps1`) are available for configuration, such as `setup-claude-mcp.ps1`, `configure_claude_desktop.ps1`, etc. Review and run the appropriate scripts for your setup.
3.  **Build the solution**:
    *   Open `PromptStudio.sln` in Visual Studio and build the solution.
    *   Alternatively, use the .NET CLI: `dotnet build PromptStudio.sln`

## Running the Applications

You can run the applications using Visual Studio, the .NET CLI, or the provided VS Code tasks.

### Using VS Code Tasks

This workspace is configured with several tasks to help you run and debug the applications. You can access these tasks from the Command Palette (Ctrl+Shift+P) by typing "Tasks: Run Task". Some key tasks include:

*   **`Run PromptStudio`**: Starts the main PromptStudio web application.
*   **`Run MCP Server`**: Starts the `PromptStudio.Mcp` server.
*   **`build`**: Builds the entire solution.

### Using .NET CLI

*   To run the PromptStudio web application:
    ```bash
    dotnet run --project PromptStudio/PromptStudio.csproj
    ```
*   To run the PromptStudio.Mcp server:
    ```bash
    dotnet run --project PromptStudio.Mcp/PromptStudio.Mcp.csproj
    ```
*   To run the Node.js mcp-server:
    ```bash
    cd mcp-server
    npm install # If you haven't already
    npm start   # Or the relevant script from its package.json
    ```

## Database Migrations

If you need to apply Entity Framework migrations for `PromptStudio.Data`:

1.  Ensure you have the .NET EF tools installed (`dotnet tool install --global dotnet-ef`).
2.  Navigate to the `PromptStudio.Data` directory or specify the startup project and project containing migrations.
3.  Run: `dotnet ef database update --startup-project ../PromptStudio/PromptStudio.csproj --project PromptStudio.Data.csproj` (Adjust paths as necessary if running from a different directory).

## Testing

Various test scripts (`.ps1`) are available in the root directory to test different functionalities, such as `test_mcp_integration.ps1`.

---


