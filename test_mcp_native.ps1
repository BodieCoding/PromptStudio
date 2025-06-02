#!/usr/bin/env pwsh

# Test script for the native C# MCP server
Write-Host "Testing Native C# MCP Server" -ForegroundColor Green

# Build the MCP server first
Write-Host "Building MCP server..." -ForegroundColor Yellow
Set-Location "c:\Code\Promptlet\PromptStudioMcpServer"
$buildResult = dotnet build --verbosity quiet
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed!" -ForegroundColor Red
    exit 1
}

Write-Host "Build successful!" -ForegroundColor Green

# Test tools/list
Write-Host "Testing tools/list..." -ForegroundColor Yellow

$toolsListRequest = @'
{"jsonrpc":"2.0","id":1,"method":"tools/list","params":{}}
'@

# Create a temporary file for the request
$requestFile = [System.IO.Path]::GetTempFileName()
Set-Content -Path $requestFile -Value $toolsListRequest

# Run the MCP server with input redirection
try {
    $result = Get-Content $requestFile | dotnet run 2>&1
    
    # Filter out just the JSON response (lines that start with {)
    $jsonResponse = $result | Where-Object { $_ -match '^\s*\{' }
    
    if ($jsonResponse) {
        Write-Host "Tools/list response:" -ForegroundColor Green
        $jsonResponse | ForEach-Object { Write-Host $_ -ForegroundColor Cyan }
        
        # Try to parse and format the JSON
        try {
            $parsed = $jsonResponse | ConvertFrom-Json
            Write-Host "`nParsed response:" -ForegroundColor Green
            $parsed | ConvertTo-Json -Depth 10 | Write-Host -ForegroundColor Cyan
        } catch {
            Write-Host "Could not parse JSON response: $_" -ForegroundColor Yellow
        }
    } else {
        Write-Host "No JSON response found in output:" -ForegroundColor Red
        $result | ForEach-Object { Write-Host $_ -ForegroundColor Gray }
    }
} finally {
    Remove-Item $requestFile -ErrorAction SilentlyContinue
}

Write-Host "`nTest completed!" -ForegroundColor Green
