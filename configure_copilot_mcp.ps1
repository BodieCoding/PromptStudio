#!/usr/bin/env pwsh
# Configure PromptStudio MCP Server for GitHub Copilot
# This script sets up the MCP server to work with GitHub Copilot in VS Code

param(
    [switch]$Install,
    [switch]$Test,
    [switch]$Uninstall
)

$ErrorActionPreference = "Stop"

# Paths
$mcpServerPath = "c:\Code\Promptlet\mcp-server"
$vscodeSettingsPath = "$env:APPDATA\Code\User\settings.json"
$promptStudioUrl = "http://localhost:5131"

Write-Host "🤖 PromptStudio MCP Server Configuration for GitHub Copilot" -ForegroundColor Cyan
Write-Host "=" * 60 -ForegroundColor Gray

function Test-PromptStudioRunning {
    try {
        $response = Invoke-RestMethod -Uri "$promptStudioUrl/api/prompts/collections" -Method GET -TimeoutSec 5
        return $true
    } catch {
        return $false
    }
}

function Test-McpServer {
    Write-Host "`n🧪 Testing MCP Server..." -ForegroundColor Yellow
    
    # Check if PromptStudio is running
    if (-not (Test-PromptStudioRunning)) {
        Write-Host "❌ PromptStudio is not running on $promptStudioUrl" -ForegroundColor Red
        Write-Host "   Please start PromptStudio first using: dotnet run --project c:\Code\Promptlet\PromptStudio\PromptStudio.csproj" -ForegroundColor Yellow
        return $false
    }
    
    Write-Host "✅ PromptStudio is running and accessible" -ForegroundColor Green
    
    # Test MCP server build
    Push-Location $mcpServerPath
    try {
        Write-Host "   Building MCP server..." -ForegroundColor Cyan
        npm run build | Out-Null
        if ($LASTEXITCODE -ne 0) {
            Write-Host "❌ MCP server build failed" -ForegroundColor Red
            return $false
        }
        Write-Host "✅ MCP server builds successfully" -ForegroundColor Green
        
        # Test if dist/index.js exists
        if (-not (Test-Path "dist/index.js")) {
            Write-Host "❌ Built MCP server file not found at dist/index.js" -ForegroundColor Red
            return $false
        }
        Write-Host "✅ MCP server executable found" -ForegroundColor Green
        
    } finally {
        Pop-Location
    }
    
    return $true
}

function Install-McpConfiguration {
    Write-Host "`n📝 Installing MCP Configuration..." -ForegroundColor Yellow
    
    # Ensure VS Code settings exist
    if (-not (Test-Path $vscodeSettingsPath)) {
        Write-Host "   Creating VS Code settings.json..." -ForegroundColor Cyan
        New-Item -Path (Split-Path $vscodeSettingsPath) -ItemType Directory -Force | Out-Null
        '{}' | Out-File -FilePath $vscodeSettingsPath -Encoding utf8
    }
    
    # Read current settings
    $settings = Get-Content $vscodeSettingsPath -Raw | ConvertFrom-Json -AsHashtable -ErrorAction SilentlyContinue
    if (-not $settings) {
        $settings = @{}
    }
    
    # Add MCP configuration for GitHub Copilot
    if (-not $settings.ContainsKey("github.copilot.experimental")) {
        $settings["github.copilot.experimental"] = @{}
    }
    
    if (-not $settings["github.copilot.experimental"].ContainsKey("mcpServers")) {
        $settings["github.copilot.experimental"]["mcpServers"] = @{}
    }
    
    # Configure PromptStudio MCP server
    $settings["github.copilot.experimental"]["mcpServers"]["promptstudio"] = @{
        "command" = "node"
        "args" = @("$mcpServerPath\dist\index.js")
        "env" = @{
            "PROMPTSTUDIO_URL" = $promptStudioUrl
        }
    }
    
    # Write back settings
    $settings | ConvertTo-Json -Depth 10 | Out-File -FilePath $vscodeSettingsPath -Encoding utf8
    
    Write-Host "✅ MCP configuration added to VS Code settings" -ForegroundColor Green
    Write-Host "   Path: $vscodeSettingsPath" -ForegroundColor Gray
}

function Show-McpTools {
    Write-Host "`n🛠️  Available MCP Tools:" -ForegroundColor Yellow
    $tools = @(
        "collections_list - List all prompt collections",
        "collection_get - Get a specific collection with prompts",
        "prompts_list - List all prompt templates",
        "prompt_get - Get a specific prompt template",
        "prompt_execute - Execute a prompt with variables",
        "prompt_create - Create a new prompt template",
        "csv_template_generate - Generate CSV template for variables",
        "variable_collection_create_from_csv - Create variable collection from CSV",
        "variable_collections_list - List variable collections",
        "batch_execute - Execute prompt with variable collection",
        "execution_history_list - Get execution history"
    )
    
    foreach ($tool in $tools) {
        Write-Host "   • $tool" -ForegroundColor Cyan
    }
}

function Test-CopilotIntegration {
    Write-Host "`n🔍 Testing Copilot Integration..." -ForegroundColor Yellow
    
    # Check if Copilot extension is installed
    try {
        $extensions = code --list-extensions 2>$null
        if ($extensions -contains "GitHub.copilot") {
            Write-Host "✅ GitHub Copilot extension is installed" -ForegroundColor Green
        } else {
            Write-Host "⚠️  GitHub Copilot extension not found" -ForegroundColor Yellow
            Write-Host "   Install it from: https://marketplace.visualstudio.com/items?itemName=GitHub.copilot" -ForegroundColor Gray
        }
    } catch {
        Write-Host "⚠️  Could not check VS Code extensions" -ForegroundColor Yellow
    }
    
    # Verify settings
    if (Test-Path $vscodeSettingsPath) {
        $settings = Get-Content $vscodeSettingsPath -Raw | ConvertFrom-Json -ErrorAction SilentlyContinue
        if ($settings."github.copilot.experimental"."mcpServers"."promptstudio") {
            Write-Host "✅ MCP server configuration found in VS Code settings" -ForegroundColor Green
        } else {
            Write-Host "❌ MCP server configuration not found in settings" -ForegroundColor Red
        }
    }
}

function Remove-McpConfiguration {
    Write-Host "`n🗑️  Removing MCP Configuration..." -ForegroundColor Yellow
    
    if (Test-Path $vscodeSettingsPath) {
        $settings = Get-Content $vscodeSettingsPath -Raw | ConvertFrom-Json -AsHashtable -ErrorAction SilentlyContinue
        if ($settings -and $settings["github.copilot.experimental"]."mcpServers"."promptstudio") {
            $settings["github.copilot.experimental"]["mcpServers"].Remove("promptstudio")
            $settings | ConvertTo-Json -Depth 10 | Out-File -FilePath $vscodeSettingsPath -Encoding utf8
            Write-Host "✅ MCP configuration removed from VS Code settings" -ForegroundColor Green
        } else {
            Write-Host "⚠️  MCP configuration not found" -ForegroundColor Yellow
        }
    }
}

function Show-Usage {
    Write-Host "`n📖 Usage Instructions:" -ForegroundColor Yellow
    Write-Host "1. Start PromptStudio: dotnet run --project c:\Code\Promptlet\PromptStudio\PromptStudio.csproj" -ForegroundColor Cyan
    Write-Host "2. Install MCP config: .\configure_copilot_mcp.ps1 -Install" -ForegroundColor Cyan
    Write-Host "3. Restart VS Code to apply changes" -ForegroundColor Cyan
    Write-Host "4. Use @promptstudio in Copilot Chat to access MCP tools" -ForegroundColor Cyan
    Write-Host "`nExample prompts:" -ForegroundColor Gray
    Write-Host "   @promptstudio List all my prompt collections" -ForegroundColor Gray
    Write-Host "   @promptstudio Execute the 'Code Review' prompt with some JavaScript code" -ForegroundColor Gray
    Write-Host "   @promptstudio Generate a CSV template for prompt ID 1" -ForegroundColor Gray
}

# Main execution
switch ($true) {
    $Install {
        if (Test-McpServer) {
            Install-McpConfiguration
            Test-CopilotIntegration
            Show-McpTools
            Write-Host "`n🎉 Installation complete! Restart VS Code to use MCP tools." -ForegroundColor Green
        }
    }
    $Test {
        Test-McpServer
        Test-CopilotIntegration
        Show-McpTools
    }
    $Uninstall {
        Remove-McpConfiguration
        Write-Host "✅ MCP configuration removed" -ForegroundColor Green
    }
    default {
        Show-Usage
        Write-Host "`nOptions:" -ForegroundColor Yellow
        Write-Host "  -Install    Install MCP configuration" -ForegroundColor Cyan
        Write-Host "  -Test       Test current setup" -ForegroundColor Cyan
        Write-Host "  -Uninstall  Remove MCP configuration" -ForegroundColor Cyan
    }
}
