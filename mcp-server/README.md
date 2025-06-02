# PromptStudio MCP Server

Model Context Protocol (MCP) server for PromptStudio integration with AI agents like GitHub Copilot and Claude Desktop.

## 🚀 Quick Start

### Prerequisites

- [Node.js 18+](https://nodejs.org/)
- PromptStudio application running on http://localhost:5131

### Installation

```bash
# Install dependencies
npm install

# Build TypeScript
npm run build

# Start the server
npm start
```

### Development

```bash
# Start with auto-reload for development
npm run dev
```

## 🔧 Configuration

Set the base URL for your PromptStudio instance:

```bash
export PROMPTSTUDIO_BASE_URL=http://localhost:5131
```

Or create a `.env` file:
```env
PROMPTSTUDIO_BASE_URL=http://localhost:5131
```

## 🤖 AI Agent Integration

### GitHub Copilot (VS Code)

Add to your VS Code `settings.json`:

```json
{
  "mcp.servers": {
    "promptstudio": {
      "command": "node",
      "args": ["/absolute/path/to/mcp-server/dist/index.js"],
      "env": {
        "PROMPTSTUDIO_BASE_URL": "http://localhost:5131"
      }
    }
  }
}
```

### Claude Desktop

Add to your Claude Desktop configuration:

**macOS**: `~/Library/Application Support/Claude/claude_desktop_config.json`
**Windows**: `%APPDATA%\Claude\claude_desktop_config.json`

```json
{
  "mcpServers": {
    "promptstudio": {
      "command": "node", 
      "args": ["/absolute/path/to/mcp-server/dist/index.js"],
      "env": {
        "PROMPTSTUDIO_BASE_URL": "http://localhost:5131"
      }
    }
  }
}
```

## 🛠️ Available Tools

### `csv_template_generate`
Generate CSV templates for prompt variables.

**Input:**
```json
{
  "template_id": 1
}
```

**Example Usage:**
> "Generate a CSV template for prompt template 1"

### `variable_collection_create_from_csv`
Create variable collections from CSV data.

**Input:**
```json
{
  "prompt_id": 1,
  "name": "Test Collection",
  "description": "Optional description",
  "csv_data": "language,code\npython,print('hello')\njavascript,console.log('hello')"
}
```

**Example Usage:**
> "Create a variable collection from this CSV data for prompt 1"

### `variable_collections_list`
List variable collections for a prompt.

**Input:**
```json
{
  "prompt_id": 1
}
```

**Example Usage:**
> "List all variable collections for prompt 1"

### `prompt_execute`
Execute a prompt with variables.

**Input:**
```json
{
  "prompt_id": 1,
  "variables": {
    "language": "python",
    "code": "print('hello world')"
  }
}
```

**Example Usage:**
> "Execute prompt 1 with language=python and code=print('hello')"

### `batch_execute`
Execute prompt with a variable collection.

**Input:**
```json
{
  "prompt_id": 1,
  "collection_id": 1
}
```

**Example Usage:**
> "Batch execute prompt 1 using variable collection 1"

## 🔍 Example AI Agent Interactions

Once configured, you can interact with PromptStudio through your AI agent:

### Generate CSV Template
```
User: "I need a CSV template for my code review prompt (ID 1)"
AI: Uses csv_template_generate tool → Returns formatted CSV template
```

### Create Variable Collection
```
User: "Create a test collection with this CSV data: language,code\npython,print('test')\njs,console.log('test')"
AI: Uses variable_collection_create_from_csv tool → Creates collection and returns ID
```

### Batch Execute
```
User: "Run prompt 1 against all variables in collection 2"
AI: Uses batch_execute tool → Returns execution results for all variable sets
```

### List Collections
```
User: "What variable collections exist for prompt 3?"
AI: Uses variable_collections_list tool → Returns list of available collections
```

## 🐛 Troubleshooting

### Common Issues

1. **"Connection refused" errors**
   - Ensure PromptStudio is running on http://localhost:5131
   - Check if the port is accessible: `curl http://localhost:5131/api/mcp/prompts`

2. **"Module not found" errors**
   - Run `npm install` to install dependencies
   - Ensure Node.js version is 18 or higher: `node --version`

3. **"Command not found" in AI agent**
   - Verify the path to `dist/index.js` is correct in your configuration
   - Run `npm run build` to ensure TypeScript is compiled

4. **TypeScript compilation errors**
   - Install @types/node: `npm install --save-dev @types/node`
   - Check `tsconfig.json` configuration

### Debugging

Enable verbose logging by setting:
```bash
export DEBUG=mcp:*
```

Or add to your environment configuration:
```json
{
  "env": {
    "DEBUG": "mcp:*",
    "PROMPTSTUDIO_BASE_URL": "http://localhost:5131"
  }
}
```

### Test API Connection

Verify PromptStudio API is accessible:

```bash
# Test basic connectivity
curl http://localhost:5131/api/mcp/prompts

# Test collections endpoint
curl http://localhost:5131/api/mcp/collections

# Test CSV template generation
curl http://localhost:5131/api/mcp/prompt-templates/1/csv-template
```

## 📁 Project Structure

```
mcp-server/
├── src/
│   └── index.ts          # Main MCP server implementation
├── dist/                 # Compiled JavaScript (after npm run build)
├── package.json         # Dependencies and scripts
├── tsconfig.json        # TypeScript configuration
└── README.md           # This file
```

## 🔗 Dependencies

- **@modelcontextprotocol/sdk**: Core MCP protocol implementation
- **axios**: HTTP client for PromptStudio API calls
- **zod**: Runtime type validation
- **typescript**: TypeScript compiler
- **tsx**: TypeScript execution for development

## 🚀 Development

### Building

```bash
npm run build
```

Compiles TypeScript to JavaScript in the `dist/` directory.

### Running in Development

```bash
npm run dev
```

Uses `tsx` to run TypeScript directly with auto-reload on file changes.

### Production

```bash
npm start
```

Runs the compiled JavaScript from `dist/index.js`.

## 📄 License

This MCP server is part of the PromptStudio project and follows the same MIT License.

---

**For more information about PromptStudio, see the [main README](../PromptStudio/README.md).**
