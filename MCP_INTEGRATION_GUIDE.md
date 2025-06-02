# PromptStudio MCP Server Integration Guide

## Overview
This MCP server provides access to PromptStudio functionality through the Model Context Protocol, allowing AI assistants to manage prompt templates, execute prompts, and handle variable collections.

## Available Tools

### Prompt Template Management
- `prompt_templates_list` - List all prompt templates
- `prompt_templates_get` - Get specific template details
- `prompt_templates_create` - Create new templates
- `prompt_templates_execute` - Execute templates with variables

### Collection Management
- `collections_list` - List all collections
- `collections_get` - Get collection details

### Variable Collections
- `variable_collections_list` - List variable collections for a prompt
- `variable_collection_create_from_csv` - Create collections from CSV data
- `variable_collections_execute` - Batch execute with variable sets
- `csv_template_generate` - Generate CSV templates

### Execution History
- `execution_history_list` - View execution history

## Integration Steps

### 1. For Claude Desktop (Anthropic)
Add to your Claude Desktop config (`%APPDATA%\Claude\claude_desktop_config.json`):

```json
{
  "mcpServers": {
    "promptstudio": {
      "command": "dotnet",
      "args": ["run", "--project", "PromptStudio.Mcp"],
      "cwd": "c:\\Code\\Promptlet"
    }
  }
}
```

**Automated Setup**: Run the setup script:
```powershell
.\setup-claude-mcp-new.ps1
```

### 2. For VS Code with MCP Extension
1. Install the MCP extension for VS Code
2. Add this configuration to your VS Code settings.json:

```json
{
  "mcp.servers": {
    "promptstudio": {
      "command": "dotnet",
      "args": ["run", "--project", "PromptStudio.Mcp"],
      "cwd": "c:\\Code\\Promptlet"
    }
  }
}
```

**Alternative**: Use the provided mcp-config.json:
```json
{
  "mcpServers": {
    "promptstudio": {
      "command": "dotnet",
      "args": ["run", "--project", "PromptStudio.Mcp"],
      "cwd": "c:\\Code\\Promptlet"
    }
  }
}
```
```

### 3. For GitHub Copilot (Future Integration)
GitHub Copilot doesn't natively support MCP yet, but you can use it through:
- Custom VS Code extensions that bridge MCP to Copilot
- Direct API integration in your development workflow

## Usage Examples

Once integrated, you can ask your AI assistant:

- "List all my prompt templates"
- "Create a new prompt template for email generation"
- "Execute the customer service template with name=John and issue=billing"
- "Generate a CSV template for my marketing prompt"
- "Show me the execution history for template ID 5"

## Prerequisites

1. .NET 8.0 SDK must be installed
2. PromptStudio web application running on http://localhost:5131
3. PromptStudio.Mcp standalone server (built with the modular architecture)

## Architecture

The PromptStudio MCP integration uses a modular .NET architecture:

- **PromptStudio.Core** - Shared domain models and interfaces
- **PromptStudio.Data** - Data access layer with Entity Framework
- **PromptStudio.Mcp** - Standalone MCP server console application

This design allows:
- Independent operation of web app and MCP server
- No build conflicts between services
- Clean separation of concerns
- Better maintainability

## Troubleshooting

- Ensure PromptStudio is running before starting the MCP server
- Check that the port (5131) matches your PromptStudio configuration
- Verify Node.js can execute TypeScript files (you may need ts-node)
