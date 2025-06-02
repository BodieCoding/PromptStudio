# Test PromptStudio MCP Server Integration
Write-Host "Testing PromptStudio MCP Server Integration..." -ForegroundColor Green

# Check prerequisites
Write-Host "`n=== Checking Prerequisites ===" -ForegroundColor Cyan

# Check if Node.js is installed
try {
    $nodeVersion = node --version
    Write-Host "OK Node.js: $nodeVersion" -ForegroundColor Green
} catch {
    Write-Host "ERROR Node.js not found. Please install Node.js" -ForegroundColor Red
    exit 1
}

# Check if PromptStudio is running
try {
    $response = Invoke-WebRequest -Uri "http://localhost:5000" -TimeoutSec 5 -ErrorAction Stop
    Write-Host "OK PromptStudio is running on localhost:5000" -ForegroundColor Green
} catch {
    Write-Host "ERROR PromptStudio not accessible on localhost:5000" -ForegroundColor Red
    Write-Host "   Please start PromptStudio first: docker-compose up" -ForegroundColor Yellow
    exit 1
}

# Check MCP server dependencies
Set-Location "c:\Code\Promptlet\mcp-server"
if (!(Test-Path "node_modules")) {
    Write-Host "Installing MCP server dependencies..." -ForegroundColor Yellow
    npm install
}

# Build MCP server
Write-Host "Building MCP server..." -ForegroundColor Yellow
npm run build

if (!(Test-Path "dist\index.js")) {
    Write-Host "ERROR MCP server build failed" -ForegroundColor Red
    exit 1
}

Write-Host "OK MCP server built successfully" -ForegroundColor Green

# Test API endpoints that MCP server uses
Write-Host "`n=== Testing API Endpoints ===" -ForegroundColor Cyan

$testEndpoints = @(
    "http://localhost:5000/api/collections",
    "http://localhost:5000/api/mcp/prompts"
)

foreach ($endpoint in $testEndpoints) {
    try {
        $response = Invoke-WebRequest -Uri $endpoint -TimeoutSec 5 -ErrorAction Stop
        Write-Host "OK API endpoint working: $endpoint" -ForegroundColor Green
    } catch {
        Write-Host "WARNING API endpoint issue: $endpoint - $($_.Exception.Message)" -ForegroundColor Yellow
    }
}

# Test MCP server startup
Write-Host "`n=== Testing MCP Server ===" -ForegroundColor Cyan

$env:PROMPTSTUDIO_URL = "http://localhost:5000"
try {
    $mcpProcess = Start-Process -FilePath "node" -ArgumentList "dist\index.js" -PassThru -WindowStyle Hidden
    Start-Sleep 2
    
    if ($mcpProcess.HasExited) {
        Write-Host "ERROR MCP server failed to start" -ForegroundColor Red
    } else {
        Write-Host "OK MCP server started (PID: $($mcpProcess.Id))" -ForegroundColor Green
        Stop-Process -Id $mcpProcess.Id -Force
        Write-Host "Cleaned up test MCP server process" -ForegroundColor Gray
    }
} catch {
    Write-Host "ERROR Failed to start MCP server: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "`n=== Usage Instructions ===" -ForegroundColor Cyan
Write-Host "After setting up MCP integration, you can ask your AI assistant:" -ForegroundColor White
Write-Host "- 'List all my prompt templates'" -ForegroundColor Gray
Write-Host "- 'Create a new prompt template for customer service'" -ForegroundColor Gray
Write-Host "- 'Execute prompt template 1 with customer_name=John and issue=billing'" -ForegroundColor Gray
Write-Host "- 'Show me execution history for the last 10 runs'" -ForegroundColor Gray
Write-Host "- 'Generate a CSV template for prompt ID 2'" -ForegroundColor Gray

Write-Host "`nOK MCP Integration Test Complete!" -ForegroundColor Green
Set-Location "c:\Code\Promptlet"
