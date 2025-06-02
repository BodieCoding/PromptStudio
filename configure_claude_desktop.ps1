#!/usr/bin/env pwsh

Write-Host "Updating Claude Desktop Configuration for PromptStudio MCP..." -ForegroundColor Green

# Define paths
$claudeConfigPath = "$env:APPDATA\Claude\claude_desktop_config.json"
$claudeConfigDir = Split-Path $claudeConfigPath -Parent
$promptStudioMcpConfig = @{
    mcpServers = @{
        promptstudio = @{
            command = "dotnet"
            args = @("run", "--project", "PromptStudio.Mcp")
            cwd = "c:\Code\Promptlet"
        }
    }
}

# Create Claude config directory if it doesn't exist
if (!(Test-Path $claudeConfigDir)) {
    Write-Host "Creating Claude config directory: $claudeConfigDir" -ForegroundColor Yellow
    New-Item -ItemType Directory -Path $claudeConfigDir -Force | Out-Null
}

# Load existing config or create new one
$existingConfig = @{}
if (Test-Path $claudeConfigPath) {
    Write-Host "Loading existing Claude Desktop configuration..." -ForegroundColor Cyan
    try {
        $existingConfig = Get-Content $claudeConfigPath | ConvertFrom-Json -AsHashtable
    } catch {
        Write-Host "Warning: Could not parse existing config. Creating new one." -ForegroundColor Yellow
        $existingConfig = @{}
    }
} else {
    Write-Host "Creating new Claude Desktop configuration..." -ForegroundColor Cyan
}

# Ensure mcpServers section exists
if (!$existingConfig.ContainsKey("mcpServers")) {
    $existingConfig["mcpServers"] = @{}
}

# Add or update PromptStudio MCP server configuration
$existingConfig["mcpServers"]["promptstudio"] = $promptStudioMcpConfig["mcpServers"]["promptstudio"]

# Save updated configuration
try {
    $existingConfig | ConvertTo-Json -Depth 10 | Set-Content $claudeConfigPath -Encoding UTF8
    Write-Host "✓ Claude Desktop configuration updated successfully!" -ForegroundColor Green
    Write-Host "Configuration saved to: $claudeConfigPath" -ForegroundColor Gray
} catch {
    Write-Host "✗ Error saving Claude Desktop configuration: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Display the configuration
Write-Host "`nCurrent PromptStudio MCP configuration:" -ForegroundColor Yellow
Write-Host "Command: dotnet" -ForegroundColor White
Write-Host "Args: run --project PromptStudio.Mcp" -ForegroundColor White
Write-Host "Working Directory: c:\Code\Promptlet" -ForegroundColor White

# Verify MCP server is running
Write-Host "`nChecking MCP server status..." -ForegroundColor Yellow
$mcpProcess = Get-Process -Name "dotnet" -ErrorAction SilentlyContinue
if ($mcpProcess) {
    Write-Host "✓ MCP server appears to be running" -ForegroundColor Green
} else {
    Write-Host "! MCP server may not be running. Starting it now..." -ForegroundColor Yellow
    Start-Process -FilePath "powershell" -ArgumentList "-Command", "cd c:\Code\Promptlet; dotnet run --project PromptStudio.Mcp" -WindowStyle Minimized
    Start-Sleep -Seconds 3
    Write-Host "✓ MCP server started" -ForegroundColor Green
}

# Display available tools
Write-Host "`nAvailable PromptStudio MCP Tools:" -ForegroundColor Magenta
Write-Host "1. list_collections - Get all available prompt collections" -ForegroundColor White
Write-Host "2. get_collection - Get detailed information about a specific collection" -ForegroundColor White
Write-Host "3. list_prompts - Get all prompt templates, optionally filtered by collection" -ForegroundColor White
Write-Host "4. get_prompt - Get detailed information about a specific prompt template" -ForegroundColor White
Write-Host "5. execute_prompt - Execute a prompt template with provided variables" -ForegroundColor White
Write-Host "6. get_execution_history - Get execution history for prompts" -ForegroundColor White
Write-Host "7. create_collection - Create a new prompt collection" -ForegroundColor White

Write-Host "`n=== Next Steps ===" -ForegroundColor Green
Write-Host "1. Restart Claude Desktop to load the new MCP configuration" -ForegroundColor Yellow
Write-Host "2. Open Claude Desktop and verify the PromptStudio tools are available" -ForegroundColor Yellow
Write-Host "3. Try using commands like 'List my prompt collections' in Claude" -ForegroundColor Yellow

Write-Host "`n=== Testing MCP Tools ===" -ForegroundColor Green
Write-Host "You can test the following commands in Claude Desktop:" -ForegroundColor Yellow
Write-Host "• 'Show me all my prompt collections'" -ForegroundColor White
Write-Host "• 'Get details for collection 1'" -ForegroundColor White
Write-Host "• 'List all prompts in my collections'" -ForegroundColor White
Write-Host "• 'Execute prompt template 1 with data: sample data'" -ForegroundColor White

Write-Host "`nConfiguration complete! 🚀" -ForegroundColor Green
