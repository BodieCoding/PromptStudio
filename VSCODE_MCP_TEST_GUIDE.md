# VS Code Extension MCP Integration Test Guide

## Overview
This guide helps you test the VS Code extension's integration with the MCP server.

## Prerequisites
1. **Built MCP Server**: `c:\Code\Promptlet\dist\PromptStudioMcpServer.exe`
2. **VS Code Extension**: Built and packaged as `PromptStudio.VSCodeExt\promptstudio-vscode-1.0.2.vsix`
3. **Database**: SQL Server database with PromptStudio schema

## Installation Steps

### 1. Install the VS Code Extension
```bash
# In VS Code, open Command Palette (Ctrl+Shift+P)
# Type: "Extensions: Install from VSIX..."
# Select: c:\Code\Promptlet\PromptStudio.VSCodeExt\promptstudio-vscode-1.0.2.vsix
```

### 2. Configure Extension Settings
Open VS Code Settings (Ctrl+,) and search for "promptstudio":

- **Connection Type**: Set to `mcp` (default)
- **MCP Server Path**: `c:\Code\Promptlet\dist\PromptStudioMcpServer.exe`
- **Server URL**: `http://localhost:5131` (fallback for API mode)

## Testing Steps

### 1. Extension Activation
1. Open any workspace in VS Code
2. Look for the PromptStudio icon in the Activity Bar (left sidebar)
3. Click on it to open the PromptStudio panel

### 2. MCP Connection Test
1. In the PromptStudio panel, you should see a tree view labeled "Prompts"
2. If MCP connection is working, it will show prompt collections and templates
3. If there's an error, check VS Code's Developer Console (Help → Toggle Developer Tools → Console)

### 3. Copilot Integration Test
1. Open VS Code Chat (Ctrl+Shift+I)
2. Type: `@promptstudio list`
3. The extension should respond with available prompts from the MCP server

### 4. Manual MCP Server Test
```powershell
# Test if MCP server can connect to database
cd c:\Code\Promptlet
.\dist\PromptStudioMcpServer.exe
# Server should start without errors and wait for JSON-RPC input
```

## Troubleshooting

### Connection Issues
- **Error: "Failed to connect to MCP server"**
  - Check if `PromptStudioMcpServer.exe` exists and is executable
  - Verify SQL Server connection string in `PromptStudioMcpServer\appsettings.json`
  - Check VS Code Developer Console for detailed error messages

### Database Issues
- **Error: "Database connection failed"**
  - Ensure SQL Server is running
  - Verify connection string in MCP server configuration
  - Test database connectivity with web application first

### Extension Issues
- **PromptStudio panel not showing**
  - Check if extension is installed and enabled
  - Restart VS Code
  - Check Extensions panel for any error messages

## Configuration Switching

### Switch to API Mode (Fallback)
1. Open VS Code Settings
2. Set "promptstudio.connectionType" to `api`
3. Ensure PromptStudio web application is running on `http://localhost:5131`
4. Reload VS Code window (Ctrl+Shift+P → "Developer: Reload Window")

### Switch Back to MCP Mode
1. Set "promptstudio.connectionType" to `mcp`
2. Ensure MCP server path is correct
3. Reload VS Code window

## Expected Behavior

### MCP Mode (Default)
- Extension connects directly to standalone MCP server
- No web application required
- Uses JSON-RPC protocol over stdio
- Faster response times
- Better resource isolation

### API Mode (Fallback)  
- Extension connects to web application API
- Requires web application to be running
- Uses HTTP REST API
- Compatible with existing infrastructure

## Success Indicators
- ✅ PromptStudio panel shows in VS Code Activity Bar
- ✅ Prompts tree view loads without errors
- ✅ `@promptstudio` commands work in VS Code Chat
- ✅ No errors in VS Code Developer Console
- ✅ MCP server runs without crashing
- ✅ Database queries execute successfully

## File Locations
- **MCP Server**: `c:\Code\Promptlet\dist\PromptStudioMcpServer.exe`
- **VS Code Extension**: `c:\Code\Promptlet\PromptStudio.VSCodeExt\promptstudio-vscode-1.0.2.vsix`  
- **Configuration**: VS Code Settings (`promptstudio.*`)
- **Logs**: VS Code Developer Console

## Next Steps After Testing
1. If MCP mode works: Update documentation and deployment scripts
2. If API mode needed: Keep both modes available for flexibility
3. Performance optimization based on usage patterns
4. Error handling improvements based on test results
