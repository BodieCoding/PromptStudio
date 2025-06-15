# PromptStudio

<div align="center">

[![.NET](https://img.shields.io/badge/.NET-8.0-purple)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-blue)](https://docs.microsoft.com/en-us/aspnet/core/)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-green)](https://docs.microsoft.com/en-us/ef/core/)
[![MCP](https://img.shields.io/badge/MCP-Compatible-orange)](https://modelcontextprotocol.io/)
[![GitHub Integration](https://img.shields.io/badge/GitHub-Integrated-black)](https://github.com/)

**🚀 Enterprise AI Automation Platform**

*Sophisticated prompt management and workflow orchestration built with modern .NET architecture*

[🌐 Live Demo](http://localhost:5131) • [📚 Documentation](#-documentation--resources) • [🚀 Quick Start](#-getting-started) • [🤖 AI Workflows](#-ai-agent-workflows)

</div>

---

PromptStudio is a sophisticated AI-powered prompt management and workflow orchestration platform built with modern .NET architecture. It provides a comprehensive suite of tools for creating, managing, and executing advanced AI agent workflows, with specialized prompt templates for enterprise automation, code analysis, debugging, content generation, and quality assurance.

## 📋 Table of Contents

- [🚀 Key Features](#-key-features)
- [🏗️ Architecture & Project Structure](#️-architecture--project-structure)
- [🚀 Getting Started](#-getting-started)
- [🤖 AI Agent Workflows](#-ai-agent-workflows)
- [🌐 Running the Applications](#-running-the-applications)
- [🔌 Model Context Protocol (MCP) Integration](#-model-context-protocol-mcp-integration)
- [💾 Database Management](#-database-management)
- [🧪 Testing & Quality Assurance](#-testing--quality-assurance)
- [📚 Documentation & Resources](#-documentation--resources)
- [🤝 Contributing](#-contributing)

## 🚀 Key Features

- **🏗️ Modern .NET Architecture**: Clean layered architecture with separation of concerns
- **🤖 AI Agent Workflows**: Advanced prompt templates designed for complex multi-step AI automation
- **🔍 Enterprise-Grade Code Analysis**: Comprehensive code review and refactoring capabilities
- **🐛 Intelligent Debugging**: Systematic problem-solving frameworks for technical issues
- **🎨 Multi-Modal Content Generation**: Creative content creation for multi-platform campaigns
- **⚡ Workflow Orchestration**: Complex AI agent workflow management with error handling and monitoring
- **🧪 Test Generation & QA Automation**: Intelligent testing strategies with CI/CD integration
- **🌐 Interactive Web Interface**: Modern ASP.NET Core application for prompt management
- **🔌 RESTful API**: Comprehensive API for integration and automation
- **🔗 Model Context Protocol (MCP) Support**: Seamless integration with AI development workflows
- **📊 GitHub Integration**: Direct issue creation and workflow orchestration via MCP servers

## 🏗️ Architecture & Project Structure

PromptStudio follows modern .NET best practices with a clean, layered architecture:

```
PromptStudio/
├── PromptStudio/           # 🌐 ASP.NET Core Web Application (Presentation Layer)
├── PromptStudio.Core/      # 🧠 Business Logic & Domain Models (Core Layer)
├── PromptStudio.Data/      # 💾 Data Access & Entity Framework (Data Layer)
├── PromptStudio.Tests/     # 🧪 Unit & Integration Tests
├── mcp-server/             # 🟢 Node.js MCP Server Implementation
└── PromptStudio.VSCodeExt/ # 📝 VS Code Extension
```

### Key Components

- **Presentation Layer**: Modern ASP.NET Core web application with responsive UI
- **Business Logic**: Clean architecture with services, repositories, and domain models  
- **Data Layer**: Entity Framework Core with SQLite/SQL Server support
- **Testing**: Comprehensive test suite for reliability and maintainability
- **MCP Integration**: Node.js-based MCP server for AI workflow integration
- **VS Code Extension**: Enhanced development experience with prompt management

## 🚀 Getting Started

### Prerequisites
- **.NET 8.0 SDK** or later
- **SQL Server** (LocalDB, SQL Server Express, or full SQL Server)
- **Node.js 18+** (for MCP server components)
- **Visual Studio 2022** or **VS Code** (recommended)

### Quick Start

1. **📂 Clone the repository**
   ```bash
   git clone https://github.com/BodieCoding/promptstudio.git
   cd promptstudio
   ```

2. **🗄️ Database Setup**
   The application includes comprehensive seed data with enterprise-grade AI agent prompt templates:
   ```bash
   dotnet ef database update --startup-project PromptStudio/PromptStudio.csproj --project PromptStudio.Data/PromptStudio.Data.csproj
   ```

3. **⚙️ Environment Setup**
   The application will create and configure the database automatically on first run with comprehensive seed data.

4. **🔨 Build the solution**
   ```bash
   dotnet build PromptStudio.sln
   ```

5. **▶️ Run PromptStudio**
   ```bash
   dotnet run --project PromptStudio/PromptStudio.csproj
   ```
   Access the application at **`http://localhost:5131`**

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

### 🛠️ Development Tools

#### Using VS Code Tasks
The workspace includes pre-configured tasks accessible via Command Palette (`Ctrl+Shift+P` → "Tasks: Run Task"):

- **🌐 `Run PromptStudio`**: Starts the web application at http://localhost:5131
- **🔗 `Run MCP Server`**: Starts the Node.js MCP server
- **🔨 `build`**: Builds the entire solution
- **📦 `publish`**: Creates production-ready builds
- **👀 `watch`**: Runs with hot reload for development

#### Using .NET CLI
```bash
# Run the main web application
dotnet run --project PromptStudio/PromptStudio.csproj

# Run with hot reload (development)
dotnet watch run --project PromptStudio/PromptStudio.csproj

# Run the Node.js MCP server  
cd mcp-server && npm install && npm start
```

## 🔌 Model Context Protocol (MCP) Integration

PromptStudio provides a Node.js-based MCP server for seamless AI workflow integration:

### MCP Server

- ** mcp-server** (Node.js-based): Lightweight, cross-platform implementation with full PromptStudio integration

### Configuration

The MCP server can be integrated with various AI development tools. See the `mcp-server/` directory for configuration examples and setup instructions.

### GitHub Integration

The included GitHub MCP server enables:
- **🎯 Direct issue creation** from analysis results
- **📋 Automated project management** via prompt workflows  
- **🔄 Continuous improvement** through orchestrated analysis cycles

## 💾 Database Management

### Entity Framework Migrations

PromptStudio uses Entity Framework Core with SQLite (default) or SQL Server support.

#### Setup Database
```bash
# Install EF tools (if not already installed)
dotnet tool install --global dotnet-ef

# Apply migrations and seed data
dotnet ef database update --startup-project PromptStudio/PromptStudio.csproj --project PromptStudio.Data/PromptStudio.Data.csproj
```

#### Create New Migrations
```bash
# Add a new migration when making schema changes
dotnet ef migrations add MigrationName --startup-project PromptStudio/PromptStudio.csproj --project PromptStudio.Data/PromptStudio.Data.csproj

# Apply the new migration
dotnet ef database update --startup-project PromptStudio/PromptStudio.csproj --project PromptStudio.Data/PromptStudio.Data.csproj
```

### Seed Data

The application automatically seeds with comprehensive prompt templates:
- **📚 Sample Collection**: Basic templates for getting started
- **🤖 AI Agent Workflows**: 5 enterprise-grade automation templates with 280+ variables total
- **🎯 Code Analysis Orchestrator**: Specialized workflow for continuous code improvement

## 🧪 Testing & Quality Assurance

PromptStudio includes comprehensive testing and validation tools:

### Automated Testing
```bash
# Run all tests
dotnet test PromptStudio.sln

# Run specific test project
dotnet test PromptStudio.Tests/PromptStudio.Tests.csproj
```

### Integration Testing
The project includes testing utilities for validating different functionalities:
- **🔗 `test-mcp-connection.js`**: Test MCP server integration  
- **🧪 API testing utilities**: Validate API endpoints and workflows
- **🤖 MCP integration validation**: Test AI development workflow integration

### Quality Assurance
- **Code Analysis**: Built-in static code analysis
- **Integration Tests**: Comprehensive API and workflow testing
- **MCP Validation**: Automated testing of MCP server implementations

## 📚 Documentation & Resources

### Getting Help
- **🌐 Web Interface**: Access PromptStudio at `http://localhost:5131`
- **📖 API Documentation**: RESTful endpoints with comprehensive examples
- **🔗 MCP Integration Guide**: Node.js server implementation and configuration
- **🎯 Enterprise Templates**: Pre-built sophisticated prompt templates
- **📋 Code Analysis Guide**: Comprehensive workflow orchestration documentation

### Key Resources
- **`CODE_ANALYSIS_ORCHESTRATOR_GUIDE.md`**: Complete guide for automated code analysis workflows
- **`VSCODE_EXTENSION_TEST_GUIDE.md`**: VS Code extension testing and development guide  
- **`mcp-server/README.md`**: MCP server setup and configuration guide
- **API Examples**: Comprehensive cURL and JavaScript examples for all endpoints

## 🚀 Next Steps

1. **🌐 Explore the Web Interface**: Start with the AI Agent Workflows collection
2. **🎯 Try Advanced Templates**: Use the Code Analysis Orchestrator for real-world scenarios
3. **🔌 API Integration**: Integrate PromptStudio with your existing automation workflows
4. **🔗 MCP Setup**: Configure your AI development environment with the included Node.js MCP server
5. **📝 Custom Prompts**: Create your own enterprise-grade prompt templates
6. **🤖 GitHub Integration**: Set up automated issue creation and project management

## 🤝 Contributing

We welcome contributions! Please see our contributing guidelines and:
- Follow the established architecture patterns
- Include comprehensive tests for new features
- Update documentation for any new functionality
- Test MCP integration thoroughly

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

---

**PromptStudio**: *Empowering enterprise AI automation through sophisticated prompt management and workflow orchestration.*




