# Test PromptStudio MCP Server Integration
Write-Host "Testing PromptStudio MCP Server Integration..." -ForegroundColor Green

# Check prerequisites
Write-Host "`n=== Checking Prerequisites ===" -ForegroundColor Cyan

# Check if Node.js is installed
try {
    $nodeVersion = node --version
    Write-Host "✅ Node.js: $nodeVersion" -ForegroundColor Green
} catch {
    Write-Host "❌ Node.js not found. Please install Node.js" -ForegroundColor Red
    exit 1
}

# Check if PromptStudio is running
try {
    $response = Invoke-WebRequest -Uri "http://localhost:5131" -TimeoutSec 5 -ErrorAction Stop
    Write-Host "✅ PromptStudio is running on localhost:5131" -ForegroundColor Green
} catch {
    Write-Host "❌ PromptStudio not accessible on localhost:5131" -ForegroundColor Red
    Write-Host "   Please start PromptStudio first: dotnet run" -ForegroundColor Yellow
    exit 1
}

# Check MCP server dependencies
Set-Location "c:\Code\Promptlet\mcp-server"
if (!(Test-Path "node_modules")) {
    Write-Host "📦 Installing MCP server dependencies..." -ForegroundColor Yellow
    npm install
}

# Build MCP server
Write-Host "🔨 Building MCP server..." -ForegroundColor Yellow
npm run build

if (!(Test-Path "dist\index.js")) {
    Write-Host "❌ MCP server build failed" -ForegroundColor Red
    exit 1
}

Write-Host "✅ MCP server built successfully" -ForegroundColor Green

# Test MCP server tools
Write-Host "`n=== Testing MCP Server Tools ===" -ForegroundColor Cyan

# Start MCP server in background for testing
$env:PROMPTSTUDIO_URL = "http://localhost:5131"
$mcpProcess = Start-Process -FilePath "node" -ArgumentList "dist\index.js" -PassThru -WindowStyle Hidden

Start-Sleep 2

if ($mcpProcess.HasExited) {
    Write-Host "❌ MCP server failed to start" -ForegroundColor Red
    exit 1
}

Write-Host "✅ MCP server started (PID: $($mcpProcess.Id))" -ForegroundColor Green

# Test API endpoints that MCP server uses
$testEndpoints = @(
    "http://localhost:5131/api/collections",
    "http://localhost:5131/api/mcp/prompts"
)

foreach ($endpoint in $testEndpoints) {
    try {
        $response = Invoke-WebRequest -Uri $endpoint -TimeoutSec 5 -ErrorAction Stop
        Write-Host "✅ API endpoint working: $endpoint" -ForegroundColor Green
    } catch {
        Write-Host "⚠️  API endpoint issue: $endpoint - $($_.Exception.Message)" -ForegroundColor Yellow
    }
}

# Clean up
if (!$mcpProcess.HasExited) {
    Stop-Process -Id $mcpProcess.Id -Force
    Write-Host "🧹 Cleaned up test MCP server process" -ForegroundColor Gray
}

Write-Host "`n=== Integration Status ===" -ForegroundColor Cyan

# Check Claude Desktop config
$claudeConfig = "$env:APPDATA\Claude\claude_desktop_config.json"
if (Test-Path $claudeConfig) {
    $config = Get-Content $claudeConfig -Raw | ConvertFrom-Json
    if ($config.mcpServers.promptstudio) {
        Write-Host "✅ Claude Desktop MCP configuration found" -ForegroundColor Green
    } else {
        Write-Host "⚠️  Claude Desktop config exists but missing PromptStudio server" -ForegroundColor Yellow
    }
} else {
    Write-Host "❌ Claude Desktop MCP configuration not found" -ForegroundColor Red
    Write-Host "   Run: .\setup-claude-mcp.ps1" -ForegroundColor Yellow
}

Write-Host "`n=== Usage Instructions ===" -ForegroundColor Cyan
Write-Host "After setting up MCP integration, you can ask your AI assistant:" -ForegroundColor White
Write-Host "• 'List all my prompt templates'" -ForegroundColor Gray
Write-Host "• 'Create a new prompt template for customer service'" -ForegroundColor Gray
Write-Host "• 'Execute prompt template 1 with customer_name=John and issue=billing'" -ForegroundColor Gray
Write-Host "• 'Show me execution history for the last 10 runs'" -ForegroundColor Gray
Write-Host "• 'Generate a CSV template for prompt ID 2'" -ForegroundColor Gray

Write-Host "`n✅ MCP Integration Test Complete!" -ForegroundColor Green
Set-Location "c:\Code\Promptlet"
