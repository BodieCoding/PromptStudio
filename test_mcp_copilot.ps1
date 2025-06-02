#!/usr/bin/env pwsh
# Test PromptStudio MCP Server functionality
param([switch]$Verbose)

$ErrorActionPreference = "Stop"
$mcpServerPath = "c:\Code\Promptlet\mcp-server\dist\index.js"
$promptStudioUrl = "http://localhost:5131"

Write-Host "🧪 Testing PromptStudio MCP Server" -ForegroundColor Cyan

# Test 1: Check if PromptStudio is running
Write-Host "`n1. Checking PromptStudio API..." -ForegroundColor Yellow
try {
    $collections = Invoke-RestMethod -Uri "$promptStudioUrl/api/prompts/collections" -Method GET -TimeoutSec 5
    Write-Host "✅ PromptStudio API responding ($($collections.Count) collections)" -ForegroundColor Green
} catch {
    Write-Host "❌ PromptStudio not responding on $promptStudioUrl" -ForegroundColor Red
    Write-Host "   Start with: dotnet run --project c:\Code\Promptlet\PromptStudio\PromptStudio.csproj" -ForegroundColor Yellow
    exit 1
}

# Test 2: Check MCP server build
Write-Host "`n2. Checking MCP server build..." -ForegroundColor Yellow
if (Test-Path $mcpServerPath) {
    Write-Host "✅ MCP server executable found" -ForegroundColor Green
} else {
    Write-Host "❌ MCP server not built. Run: cd c:\Code\Promptlet\mcp-server ; npm run build" -ForegroundColor Red
    exit 1
}

# Test 3: Test MCP server startup (quick test)
Write-Host "`n3. Testing MCP server startup..." -ForegroundColor Yellow
try {
    $env:PROMPTSTUDIO_URL = $promptStudioUrl
    $mcpProcess = Start-Process -FilePath "node" -ArgumentList $mcpServerPath -PassThru -WindowStyle Hidden
    Start-Sleep -Seconds 2
    
    if (-not $mcpProcess.HasExited) {
        Write-Host "✅ MCP server starts successfully" -ForegroundColor Green
        $mcpProcess.Kill()
    } else {
        Write-Host "❌ MCP server failed to start" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "❌ Error testing MCP server: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Test 4: Sample API calls
Write-Host "`n4. Testing API endpoints..." -ForegroundColor Yellow
$endpoints = @(
    @{ Name = "Collections"; Url = "$promptStudioUrl/api/prompts/collections" },
    @{ Name = "Prompts"; Url = "$promptStudioUrl/api/prompts/prompts" },
    @{ Name = "Executions"; Url = "$promptStudioUrl/api/prompts/executions" }
)

foreach ($endpoint in $endpoints) {
    try {
        $response = Invoke-RestMethod -Uri $endpoint.Url -Method GET -TimeoutSec 5
        Write-Host "✅ $($endpoint.Name): $($response.Count) items" -ForegroundColor Green
    } catch {
        Write-Host "⚠️  $($endpoint.Name): $($_.Exception.Message)" -ForegroundColor Yellow
    }
}

Write-Host "`n🎉 MCP server is ready for GitHub Copilot integration!" -ForegroundColor Green
Write-Host "`nNext steps:" -ForegroundColor Yellow
Write-Host "1. Run: .\configure_copilot_mcp.ps1 -Install" -ForegroundColor Cyan
Write-Host "2. Restart VS Code" -ForegroundColor Cyan
Write-Host "3. Use @promptstudio in Copilot Chat" -ForegroundColor Cyan
