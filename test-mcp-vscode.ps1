# Test MCP Server VS Code Integration
Write-Host "Testing MCP Server VS Code Integration..." -ForegroundColor Green

# Set environment variable
$env:PROMPTSTUDIO_URL = "http://localhost:5000"

# Test if PromptStudio is accessible
try {
    $response = Invoke-WebRequest -Uri "http://localhost:5000/api/mcp/collections" -TimeoutSec 5
    Write-Host "✓ PromptStudio API accessible" -ForegroundColor Green
} catch {
    Write-Host "✗ PromptStudio API not accessible: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Test MCP server initialization
Set-Location "c:\Code\Promptlet\mcp-server"

Write-Host "Testing MCP server initialization..." -ForegroundColor Yellow

$initRequest = @'
{"jsonrpc":"2.0","id":1,"method":"initialize","params":{"protocolVersion":"2024-11-05","capabilities":{},"clientInfo":{"name":"test","version":"1.0.0"}}}
'@

try {
    $output = $initRequest | node dist/index.js 2>&1
    if ($output -match '"result"') {
        Write-Host "✓ MCP server initializes correctly" -ForegroundColor Green
        Write-Host "Server response: $($output -split "`n" | Where-Object { $_ -match '"result"' })" -ForegroundColor Gray
    } else {
        Write-Host "✗ MCP server initialization failed" -ForegroundColor Red
        Write-Host "Output: $output" -ForegroundColor Gray
    }
} catch {
    Write-Host "✗ Error testing MCP server: $($_.Exception.Message)" -ForegroundColor Red
}

# Test tool listing
Write-Host "Testing tool listing..." -ForegroundColor Yellow

$toolsRequest = @'
{"jsonrpc":"2.0","id":2,"method":"tools/list","params":{}}
'@

try {
    $output = $toolsRequest | node dist/index.js 2>&1
    if ($output -match '"tools"') {
        Write-Host "✓ MCP server lists tools correctly" -ForegroundColor Green
        # Count tools
        $toolCount = ($output | ConvertFrom-Json).result.tools.Count
        Write-Host "Found $toolCount tools available" -ForegroundColor Gray
    } else {
        Write-Host "✗ Tool listing failed" -ForegroundColor Red
        Write-Host "Output: $output" -ForegroundColor Gray
    }
} catch {
    Write-Host "✗ Error testing tool listing: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "`n=== VS Code MCP Configuration ===" -ForegroundColor Cyan
Write-Host "Your VS Code settings should include:" -ForegroundColor White
Write-Host @'
"mcp": {
    "servers": {
        "promptstudio": {
            "command": "node",
            "args": ["c:\\Code\\Promptlet\\mcp-server\\dist\\index.js"],
            "env": {
                "PROMPTSTUDIO_URL": "http://localhost:5000"
            }
        }
    }
}
'@ -ForegroundColor Gray

Write-Host "`n=== Troubleshooting ===" -ForegroundColor Cyan
Write-Host "If VS Code MCP is stuck on initialize:" -ForegroundColor White
Write-Host "1. Restart VS Code completely" -ForegroundColor Gray
Write-Host "2. Check VS Code Developer Console (Ctrl+Shift+I) for errors" -ForegroundColor Gray
Write-Host "3. Ensure PromptStudio Docker container is running" -ForegroundColor Gray
Write-Host "4. Try disabling/re-enabling the MCP extension" -ForegroundColor Gray

Set-Location "c:\Code\Promptlet"
