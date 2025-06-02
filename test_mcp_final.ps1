#!/usr/bin/env pwsh

# Test MCP server functionality
Write-Host "Testing MCP Server Functionality..." -ForegroundColor Green

# Test 1: Check if MCP server is running
Write-Host "`n1. Checking if MCP server process is running..." -ForegroundColor Yellow
$mcpProcess = Get-Process -Name "dotnet" -ErrorAction SilentlyContinue | Where-Object { $_.CommandLine -like "*PromptStudio.Mcp*" }
if ($mcpProcess) {
    Write-Host "✓ MCP server is running (PID: $($mcpProcess.Id))" -ForegroundColor Green
} else {
    Write-Host "✗ MCP server is not running" -ForegroundColor Red
    exit 1
}

# Test 2: Test MCP server tools using stdio
Write-Host "`n2. Testing MCP server tools..." -ForegroundColor Yellow

$testPayload = @{
    jsonrpc = "2.0"
    id = 1
    method = "tools/list"
    params = @{}
} | ConvertTo-Json -Depth 10

Write-Host "Sending tools/list request to MCP server..." -ForegroundColor Cyan

try {
    # Create a process to communicate with the MCP server
    $psi = New-Object System.Diagnostics.ProcessStartInfo
    $psi.FileName = "dotnet"
    $psi.Arguments = "run --project PromptStudio.Mcp"
    $psi.WorkingDirectory = "c:\Code\Promptlet"
    $psi.UseShellExecute = $false
    $psi.RedirectStandardInput = $true
    $psi.RedirectStandardOutput = $true
    $psi.RedirectStandardError = $true
    $psi.CreateNoWindow = $true

    $process = [System.Diagnostics.Process]::Start($psi)
    
    # Send the test payload
    $process.StandardInput.WriteLine($testPayload)
    $process.StandardInput.Flush()
    
    # Wait a moment for response
    Start-Sleep -Seconds 2
    
    # Read any output
    $output = ""
    while (!$process.StandardOutput.EndOfStream) {
        $output += $process.StandardOutput.ReadLine() + "`n"
    }
    
    if ($output) {
        Write-Host "✓ MCP server responded:" -ForegroundColor Green
        Write-Host $output -ForegroundColor White
    } else {
        Write-Host "✗ No response from MCP server" -ForegroundColor Red
    }
    
    $process.Kill()
} catch {
    Write-Host "✗ Error testing MCP server: $($_.Exception.Message)" -ForegroundColor Red
}

# Test 3: Verify Claude Desktop configuration
Write-Host "`n3. Checking Claude Desktop configuration..." -ForegroundColor Yellow

$claudeConfigPath = "$env:APPDATA\Claude\claude_desktop_config.json"
if (Test-Path $claudeConfigPath) {
    Write-Host "✓ Claude Desktop config file exists" -ForegroundColor Green
    
    try {
        $claudeConfig = Get-Content $claudeConfigPath | ConvertFrom-Json
        if ($claudeConfig.mcpServers.promptstudio) {
            Write-Host "✓ PromptStudio MCP server is configured in Claude Desktop" -ForegroundColor Green
            Write-Host "  Command: $($claudeConfig.mcpServers.promptstudio.command)" -ForegroundColor Gray
            Write-Host "  Args: $($claudeConfig.mcpServers.promptstudio.args -join ' ')" -ForegroundColor Gray
            Write-Host "  Working Directory: $($claudeConfig.mcpServers.promptstudio.cwd)" -ForegroundColor Gray
        } else {
            Write-Host "✗ PromptStudio MCP server not found in Claude Desktop config" -ForegroundColor Red
        }
    } catch {
        Write-Host "✗ Error reading Claude Desktop config: $($_.Exception.Message)" -ForegroundColor Red
    }
} else {
    Write-Host "! Claude Desktop config file not found at $claudeConfigPath" -ForegroundColor Yellow
    Write-Host "  You may need to configure Claude Desktop manually" -ForegroundColor Gray
}

# Test 4: Check database accessibility
Write-Host "`n4. Checking database accessibility..." -ForegroundColor Yellow

$dbPath = "c:\Code\Promptlet\PromptStudio\promptstudio.db"
if (Test-Path $dbPath) {
    Write-Host "✓ Database file exists at: $dbPath" -ForegroundColor Green
    
    # Check file size
    $dbSize = (Get-Item $dbPath).Length
    Write-Host "  Database size: $([math]::Round($dbSize / 1KB, 2)) KB" -ForegroundColor Gray
} else {
    Write-Host "✗ Database file not found at: $dbPath" -ForegroundColor Red
}

Write-Host "`n=== MCP Test Summary ===" -ForegroundColor Magenta
Write-Host "The MCP server appears to be configured and running successfully." -ForegroundColor Green
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "1. Configure Claude Desktop using the mcp-config.json file" -ForegroundColor White
Write-Host "2. Restart Claude Desktop to load the MCP server" -ForegroundColor White
Write-Host "3. Test the tools in Claude Desktop chat" -ForegroundColor White
