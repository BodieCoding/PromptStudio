#!/usr/bin/env pwsh
# Test GitHub Copilot MCP Integration
# This script validates that the PromptStudio MCP server is properly configured for GitHub Copilot

$ErrorActionPreference = "Stop"

Write-Host "🤖 Testing GitHub Copilot MCP Integration" -ForegroundColor Cyan
Write-Host "=" * 50 -ForegroundColor Gray

# Test 1: PromptStudio API Access
Write-Host "`n1️⃣ Testing PromptStudio API Access..." -ForegroundColor Yellow
try {
    $response = Invoke-RestMethod -Uri "http://localhost:5131/api/prompts/collections" -Method GET -TimeoutSec 10
    Write-Host "✅ PromptStudio API accessible - Found $($response.Count) collections" -ForegroundColor Green
    
    # Show collection info
    $response | ForEach-Object {
        Write-Host "   📁 Collection: $($_.name) ($($_.promptTemplatesCount) prompts)" -ForegroundColor Cyan
    }
} catch {
    Write-Host "❌ PromptStudio API not accessible: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "   Make sure PromptStudio is running on http://localhost:5131" -ForegroundColor Yellow
    exit 1
}

# Test 2: MCP Server Build
Write-Host "`n2️⃣ Testing MCP Server Build..." -ForegroundColor Yellow
$mcpServerPath = "c:\Code\Promptlet\mcp-server"
$distPath = "$mcpServerPath\dist\index.js"

if (Test-Path $distPath) {
    Write-Host "✅ MCP server executable found at $distPath" -ForegroundColor Green
} else {
    Write-Host "❌ MCP server executable not found, building..." -ForegroundColor Yellow
    Push-Location $mcpServerPath
    try {
        npm run build
        if (Test-Path $distPath) {
            Write-Host "✅ MCP server built successfully" -ForegroundColor Green
        } else {
            Write-Host "❌ MCP server build failed" -ForegroundColor Red
            exit 1
        }
    } finally {
        Pop-Location
    }
}

# Test 3: VS Code Settings
Write-Host "`n3️⃣ Testing VS Code Settings..." -ForegroundColor Yellow
$vscodeSettingsPath = "$env:APPDATA\Code\User\settings.json"

if (Test-Path $vscodeSettingsPath) {
    $settings = Get-Content $vscodeSettingsPath -Raw | ConvertFrom-Json
    if ($settings.'github.copilot.experimental'.'mcpServers'.promptstudio) {
        Write-Host "✅ GitHub Copilot MCP configuration found" -ForegroundColor Green
        $mcpConfig = $settings.'github.copilot.experimental'.'mcpServers'.promptstudio
        Write-Host "   📄 Command: $($mcpConfig.command)" -ForegroundColor Cyan
        Write-Host "   📁 Script: $($mcpConfig.args[0])" -ForegroundColor Cyan
        Write-Host "   🔗 URL: $($mcpConfig.env.PROMPTSTUDIO_URL)" -ForegroundColor Cyan
    } else {
        Write-Host "❌ GitHub Copilot MCP configuration not found in VS Code settings" -ForegroundColor Red
        exit 1
    }
} else {
    Write-Host "❌ VS Code settings file not found at $vscodeSettingsPath" -ForegroundColor Red
    exit 1
}

# Test 4: GitHub Copilot Extension
Write-Host "`n4️⃣ Testing GitHub Copilot Extension..." -ForegroundColor Yellow
try {
    $extensions = code --list-extensions 2>$null
    if ($extensions -contains "GitHub.copilot") {
        Write-Host "✅ GitHub Copilot extension is installed" -ForegroundColor Green
    } else {
        Write-Host "⚠️  GitHub Copilot extension not found" -ForegroundColor Yellow
        Write-Host "   Install from: https://marketplace.visualstudio.com/items?itemName=GitHub.copilot" -ForegroundColor Gray
    }
} catch {
    Write-Host "⚠️  Could not check VS Code extensions (VS Code might not be in PATH)" -ForegroundColor Yellow
}

# Test 5: MCP Tools Available
Write-Host "`n5️⃣ Available MCP Tools:" -ForegroundColor Yellow
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
    Write-Host "   🛠️  $tool" -ForegroundColor Cyan
}

Write-Host "`n🎉 Configuration Test Complete!" -ForegroundColor Green
Write-Host "`n📋 Next Steps:" -ForegroundColor Yellow
Write-Host "1. Restart VS Code to load the new MCP configuration" -ForegroundColor Cyan
Write-Host "2. Open a file in your PromptStudio workspace" -ForegroundColor Cyan
Write-Host "3. Use @promptstudio in GitHub Copilot Chat to access MCP tools" -ForegroundColor Cyan
Write-Host "`n💡 Example prompts:" -ForegroundColor Gray
Write-Host "   @promptstudio List all my prompt collections" -ForegroundColor White
Write-Host "   @promptstudio Execute the 'Code Review' prompt with some JavaScript code" -ForegroundColor White
Write-Host "   @promptstudio Generate a CSV template for prompt ID 1" -ForegroundColor White
Write-Host "   @promptstudio Create a new prompt for API documentation" -ForegroundColor White
