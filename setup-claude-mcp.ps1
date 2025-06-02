# Setup PromptStudio MCP Server for Claude Desktop
Write-Host "Setting up PromptStudio MCP Server for Claude Desktop..." -ForegroundColor Green

# Create Claude config directory if it doesn't exist
$claudeConfigDir = "$env:APPDATA\Claude"
if (!(Test-Path $claudeConfigDir)) {
    Write-Host "Creating Claude config directory..." -ForegroundColor Yellow
    New-Item -ItemType Directory -Path $claudeConfigDir -Force
}

# Create Claude Desktop config
$configPath = "$claudeConfigDir\claude_desktop_config.json"
$config = @{
    mcpServers = @{
        promptstudio = @{
            command = "node"
            args = @("c:\Code\Promptlet\mcp-server\dist\index.js")
            env = @{
                PROMPTSTUDIO_URL = "http://localhost:5000"
            }
        }
    }
} | ConvertTo-Json -Depth 4

Write-Host "Creating Claude Desktop configuration..." -ForegroundColor Yellow
$config | Out-File -FilePath $configPath -Encoding UTF8

Write-Host ""
Write-Host "✅ Configuration complete!" -ForegroundColor Green
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Cyan
Write-Host "1. Build the MCP server: cd mcp-server && npm run build" -ForegroundColor White
Write-Host "2. Start PromptStudio: dotnet run (in PromptStudio directory)" -ForegroundColor White
Write-Host "3. Restart Claude Desktop" -ForegroundColor White
Write-Host "4. You can now ask Claude to 'list prompt templates' or 'create a new prompt'" -ForegroundColor White
Write-Host ""

# Check if Claude Desktop is running
$claudeProcess = Get-Process -Name "Claude" -ErrorAction SilentlyContinue
if ($claudeProcess) {
    Write-Host "⚠️  Claude Desktop is currently running. Please restart it to load the new configuration." -ForegroundColor Yellow
}

Write-Host "Press any key to continue..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
