# Promptlet

Promptlet is a sophisticated AI-powered prompt management and workflow orchestration platform. It provides a comprehensive suite of tools for creating, managing, and executing advanced AI agent workflows, with specialized prompt templates for enterprise automation, code analysis, debugging, content generation, and quality assurance.

## 🚀 Key Features

- **AI Agent Workflows**: Advanced prompt templates designed for complex multi-step AI automation
- **Enterprise-Grade Code Analysis**: Comprehensive code review and refactoring capabilities
- **Intelligent Debugging**: Systematic problem-solving frameworks for technical issues
- **Multi-Modal Content Generation**: Creative content creation for multi-platform campaigns
- **Workflow Orchestration**: Complex AI agent workflow management with error handling and monitoring
- **Test Generation & QA Automation**: Intelligent testing strategies with CI/CD integration
- **Interactive Web Interface**: Modern ASP.NET Core application for prompt management
- **RESTful API**: Comprehensive API for integration and automation
- **Model Context Protocol (MCP) Support**: Multiple MCP server implementations

## Project Structure

The project is organized into several main components:

*   **`PromptStudio/`**: The main ASP.NET Core web application for managing and experimenting with prompts.
*   **`PromptStudio.Core/`**: Contains the core business logic, domain models, and interfaces for the Promptlet application.
*   **`PromptStudio.Data/`**: Handles data access, including the Entity Framework Core setup, database context, and migrations.
*   **`mcp-server/`**: A Node.js-based MCP server implementation.

The solution is managed by `PromptStudio.sln` and can be opened in Visual Studio. For VS Code users, the `PromptStudio.code-workspace` file is provided.

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- SQL Server (LocalDB, SQL Server Express, or full SQL Server)
- Node.js (for MCP server components)
- Visual Studio 2022 or VS Code

### Quick Start

1.  **Clone the repository.**
    ```bash
    git clone <repository-url>
    cd Promptlet
    ```

2.  **Database Setup**:
    The application includes comprehensive seed data with enterprise-grade AI agent prompt templates:
    ```bash
    dotnet ef database update --startup-project PromptStudio/PromptStudio.csproj --project PromptStudio.Data/PromptStudio.Data.csproj
    ```

3.  **Environment Setup**:
    *   Several PowerShell scripts (`.ps1`) are available for configuration, such as `setup-claude-mcp.ps1`, `configure_claude_desktop.ps1`, etc. Review and run the appropriate scripts for your setup.

4.  **Build the solution**:
    *   Open `PromptStudio.sln` in Visual Studio and build the solution.
    *   Alternatively, use the .NET CLI: `dotnet build PromptStudio.sln`

5.  **Run PromptStudio**:
    ```bash
    dotnet run --project PromptStudio/PromptStudio.csproj
    ```
    Access the application at `http://localhost:5131`

## 🤖 AI Agent Workflows

PromptStudio comes pre-configured with sophisticated AI agent prompt templates designed for enterprise automation:

### Available Collections

#### **AI Agent Workflows** 🧠
Advanced prompt templates for complex multi-step AI automation:

1. **Advanced Code Analysis & Refactoring** (21 variables)
   - Comprehensive code quality assessment with business context
   - Architecture review and performance optimization
   - Refactoring recommendations with impact analysis
   - Risk assessment and implementation planning

2. **AI-Powered Debugging & Problem Solving** (40+ variables)
   - Systematic issue diagnosis and resolution framework
   - Root cause analysis with environmental context
   - Solution validation and testing strategies
   - Performance impact assessment

3. **Multi-Modal Creative Content Generator** (60+ variables)
   - Multi-platform content creation and optimization
   - A/B testing framework integration
   - Brand consistency and compliance checking
   - Performance metrics and ROI tracking

4. **AI Agent Workflow Orchestration** (80+ variables)
   - Complex multi-step workflow design and management
   - Error handling and fallback strategies
   - Monitoring and alerting configuration
   - Scalability and performance optimization

5. **Intelligent Test Generation & QA Automation** (80+ variables)
   - Comprehensive testing strategy development
   - CI/CD pipeline integration
   - Quality metrics and compliance reporting
   - Risk assessment and mitigation planning

#### **Sample Collection** 📚
Basic templates to get you started:
- Code Review templates
- Basic prompt structures

## Running the Applications

### Using the Web Interface
1. Start PromptStudio: `dotnet run --project PromptStudio/PromptStudio.csproj`
2. Open your browser to `http://localhost:5131`
3. Explore the AI Agent Workflows collection for advanced automation templates
4. Create and execute sophisticated prompt workflows with extensive variable support

### API Integration
PromptStudio provides a comprehensive REST API for automation and integration:

- **Collections API**: `/api/prompts/collections` - Manage prompt collections
- **Prompts API**: `/api/prompts/prompts` - Create and manage prompt templates  
- **Execution API**: `/api/prompts/execute` - Execute prompts with variables
- **Batch Processing**: `/api/prompts/batch` - Execute prompts with variable collections

Example API usage:
```bash
# Get all collections
curl http://localhost:5131/api/prompts/collections

# Execute a prompt
curl -X POST http://localhost:5131/api/prompts/execute \
  -H "Content-Type: application/json" \
  -d '{"id": 1001, "variables": "{\"code_type\":\"microservice\", \"project_name\":\"PaymentAPI\"}"}'
```

### Development Tools

#### Using VS Code Tasks
This workspace is configured with several tasks to help you run and debug the applications. You can access these tasks from the Command Palette (Ctrl+Shift+P) by typing "Tasks: Run Task". Key tasks include:

*   **`Run PromptStudio`**: Starts the main PromptStudio web application at http://localhost:5131
*   **`Run MCP Server`**: Starts the Node.js MCP server
*   **`build`**: Builds the entire solution

#### Using .NET CLI
*   To run the PromptStudio web application:
    ```bash
    dotnet run --project PromptStudio/PromptStudio.csproj
    ```
*   To run the Node.js mcp-server:
    ```bash
    cd mcp-server
    npm install # If you haven't already
    npm start   # Or the relevant script from its package.json
    ```

## 🔌 Model Context Protocol (MCP) Integration

PromptStudio includes a Node.js-based MCP server for seamless integration with AI development workflows:

- **mcp-server**: Node.js-based MCP server (recommended)

Configure your AI development environment to use these MCP servers for enhanced prompt management capabilities.

## Database Management

### Entity Framework Migrations

If you need to apply Entity Framework migrations for `PromptStudio.Data`:

1.  Ensure you have the .NET EF tools installed:
    ```bash
    dotnet tool install --global dotnet-ef
    ```

2.  Apply migrations to set up the database with seed data:
    ```bash
    dotnet ef database update --startup-project PromptStudio/PromptStudio.csproj --project PromptStudio.Data/PromptStudio.Data.csproj
    ```

3.  Create new migrations when making schema changes:
    ```bash
    dotnet ef migrations add MigrationName --startup-project PromptStudio/PromptStudio.csproj --project PromptStudio.Data/PromptStudio.Data.csproj
    ```

### Seed Data

The database automatically seeds with comprehensive prompt templates including:
- **Sample Collection**: Basic templates for getting started
- **AI Agent Workflows**: Enterprise-grade automation templates with 200+ variables across 5 sophisticated prompt templates

## 🧪 Testing & Validation

Various test scripts (`.ps1`) are available in the root directory to test different functionalities:
- `test_mcp_integration.ps1`: Test MCP server integration
- Additional testing utilities for prompt validation and API testing

## 📚 Documentation & Resources

- **Web Interface**: Access PromptStudio at `http://localhost:5131` after starting the application
- **API Documentation**: RESTful endpoints for prompt management and execution
- **MCP Integration**: Multiple server implementations for AI development workflow integration
- **Enterprise Templates**: Pre-built sophisticated prompt templates for business automation

## 🚀 Next Steps

1. **Explore the Web Interface**: Start with the AI Agent Workflows collection
2. **Try Advanced Templates**: Use the sophisticated prompt templates for real-world scenarios
3. **API Integration**: Integrate PromptStudio with your existing automation workflows
4. **MCP Setup**: Configure your AI development environment with the included MCP servers
5. **Custom Prompts**: Create your own enterprise-grade prompt templates

---

*PromptStudio: Empowering enterprise AI automation through sophisticated prompt management and workflow orchestration.*


