@echo off
echo Setting up PromptStudio MCP Server for Claude Desktop...

:: Check if Claude config directory exists
if not exist "%APPDATA%\Claude" (
    echo Creating Claude config directory...
    mkdir "%APPDATA%\Claude"
)

:: Create or update Claude Desktop config
echo Creating Claude Desktop configuration...
echo {> "%APPDATA%\Claude\claude_desktop_config.json"
echo   "mcpServers": {>> "%APPDATA%\Claude\claude_desktop_config.json"
echo     "promptstudio": {>> "%APPDATA%\Claude\claude_desktop_config.json"
echo       "command": "node",>> "%APPDATA%\Claude\claude_desktop_config.json"
echo       "args": ["c:\\Code\\Promptlet\\mcp-server\\dist\\index.js"],>> "%APPDATA%\Claude\claude_desktop_config.json"
echo       "env": {>> "%APPDATA%\Claude\claude_desktop_config.json"
echo         "PROMPTSTUDIO_URL": "http://localhost:5131">> "%APPDATA%\Claude\claude_desktop_config.json"
echo       }>> "%APPDATA%\Claude\claude_desktop_config.json"
echo     }>> "%APPDATA%\Claude\claude_desktop_config.json"
echo   }>> "%APPDATA%\Claude\claude_desktop_config.json"
echo }>> "%APPDATA%\Claude\claude_desktop_config.json"

echo.
echo Configuration complete! 
echo.
echo Next steps:
echo 1. Build the MCP server: cd mcp-server && npm run build
echo 2. Start PromptStudio: dotnet run (in PromptStudio directory)
echo 3. Restart Claude Desktop
echo 4. You can now ask Claude to "list prompt templates" or "create a new prompt"
echo.
pause
