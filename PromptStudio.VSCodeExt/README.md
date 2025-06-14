# PromptStudio VS Code Extension

AI Prompt Management extension for Visual Studio Code with GitHub Copilot integration.

## Features

- 📋 **Prompt Management**: Create, edit, and organize AI prompt templates
- 🤖 **Copilot Integration**: Execute prompts directly in GitHub Copilot Chat using `@promptstudio`
- 📁 **Collections**: Organize prompts into collections for better management
- 🔧 **Variables**: Support for dynamic variables in prompts with `{{variableName}}` syntax
- 🌐 **Web Interface**: Integrated access to PromptStudio web application
- 🔍 **Search**: Find prompts by name, description, or content

## Installation

### From VSIX Package
1. Download the `.vsix` file from releases
2. Open VS Code
3. Run `Extensions: Install from VSIX...` command
4. Select the downloaded `.vsix` file

### From Source
```bash
npm install
npm run compile
npm run package
```

## Setup

1. **Start PromptStudio Server**:
   ```bash
   cd PromptStudio
   dotnet run
   ```

2. **Configure Extension** (if server is not on default port):
   - Open VS Code Settings
   - Search for "PromptStudio"
   - Set `promptstudio.serverUrl` to your server URL

3. **Test Connection**:
   - Open PromptStudio panel in VS Code
   - Click "Test Connection"

## Usage

### Tree View
- Browse prompts and collections in the PromptStudio panel
- Click prompts to execute them
- Right-click for context menu options

### Copilot Chat Integration
Use these commands in Copilot Chat:

```
@promptstudio list                    # List all available prompts
@promptstudio execute "Prompt Name"   # Execute a specific prompt
@promptstudio search keyword          # Search for prompts
```

### Commands
- `PromptStudio: Open PromptStudio` - Open web interface
- `PromptStudio: Create New Prompt` - Create a new prompt template
- `PromptStudio: Refresh Prompts` - Refresh the prompts tree

### Variable Substitution
Prompts support variables using `{{variableName}}` syntax:

```
Write a {{language}} function that {{description}}.
The function should:
- {{requirement1}}
- {{requirement2}}
```

When executed, you'll be prompted to provide values for each variable.

## Configuration

| Setting | Default | Description |
|---------|---------|-------------|
| `promptstudio.serverUrl` | `http://localhost:5131` | URL of the PromptStudio server |
| `promptstudio.enableCopilotIntegration` | `true` | Enable GitHub Copilot integration |
| `promptstudio.autoRefresh` | `true` | Automatically refresh prompts |

## Development

### Prerequisites
- Node.js 18+
- npm or yarn
- VS Code

### Build
```bash
npm install
npm run compile
```

### Package
```bash
npm run package
```

### Publish
```bash
npm run publish
```

## Troubleshooting

### Server Connection Issues
1. Ensure PromptStudio server is running: `dotnet run`
2. Check server URL in settings
3. Verify firewall/network settings
4. Test connection using the "Test Connection" button

### Copilot Integration Not Working
1. Ensure GitHub Copilot extension is installed and active
2. Check that `promptstudio.enableCopilotIntegration` is enabled
3. Restart VS Code after installing the extension

### Prompts Not Loading
1. Refresh the prompts view
2. Check server connection
3. Verify server is responding at `/api/prompt-templates`

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

MIT License - see LICENSE file for details.

## Support

- GitHub Issues: Report bugs and feature requests
- Documentation: See the PromptStudio main documentation
- Server API: Refer to the PromptStudio API documentation
