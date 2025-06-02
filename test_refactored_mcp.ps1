#!/usr/bin/env pwsh
# Test script to verify the refactored MCP server works with PromptApiController

Write-Host "Testing Refactored MCP Server Integration with PromptApiController" -ForegroundColor Green

# Test 1: Check if PromptStudio is running
Write-Host "`n1. Checking if PromptStudio is running..." -ForegroundColor Yellow
try {
    $response = Invoke-RestMethod -Uri "http://localhost:5131/api/prompts/collections" -Method GET -TimeoutSec 5
    Write-Host "✓ PromptStudio is running and responding" -ForegroundColor Green
    Write-Host "Found $($response.Count) collections" -ForegroundColor Cyan
} catch {
    Write-Host "✗ PromptStudio is not running or not responding" -ForegroundColor Red
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "Please make sure PromptStudio is running on http://localhost:5131" -ForegroundColor Yellow
    exit 1
}

# Test 2: Check MCP server can be built
Write-Host "`n2. Verifying MCP server build..." -ForegroundColor Yellow
Push-Location "c:\Code\Promptlet\mcp-server"
try {
    npm run build 2>&1 | Out-Null
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✓ MCP server TypeScript build successful" -ForegroundColor Green
    } else {
        Write-Host "✗ MCP server build failed" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "✗ Error building MCP server: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
} finally {
    Pop-Location
}

# Test 3: Test specific API endpoints that the MCP server will use
Write-Host "`n3. Testing PromptApiController endpoints..." -ForegroundColor Yellow

$endpoints = @(
    @{ Name = "Get Collections"; Url = "http://localhost:5131/api/prompts/collections" },
    @{ Name = "Get Prompts"; Url = "http://localhost:5131/api/prompts/prompts" },
    @{ Name = "Get Executions"; Url = "http://localhost:5131/api/prompts/executions" },
    @{ Name = "Get Variable Collections"; Url = "http://localhost:5131/api/prompts/variable-collections?promptId=1" }
)

foreach ($endpoint in $endpoints) {
    try {
        $response = Invoke-RestMethod -Uri $endpoint.Url -Method GET -TimeoutSec 5
        Write-Host "✓ $($endpoint.Name): Working" -ForegroundColor Green
    } catch {
        if ($_.Exception.Response.StatusCode -eq 404) {
            Write-Host "⚠ $($endpoint.Name): 404 (expected if no data)" -ForegroundColor Yellow
        } else {
            Write-Host "✗ $($endpoint.Name): Failed - $($_.Exception.Message)" -ForegroundColor Red
        }
    }
}

# Test 4: Test CSV template endpoint (if there are any prompt templates)
Write-Host "`n4. Testing CSV template generation..." -ForegroundColor Yellow
try {
    $prompts = Invoke-RestMethod -Uri "http://localhost:5131/api/prompts/prompts" -Method GET -TimeoutSec 5
    if ($prompts.Count -gt 0) {
        $firstPromptId = $prompts[0].id
        Write-Host "Testing CSV template generation for prompt ID: $firstPromptId" -ForegroundColor Cyan
        
        # Test the CSV template endpoint
        $headers = @{ 'Accept' = 'text/csv' }
        $csvResponse = Invoke-WebRequest -Uri "http://localhost:5131/api/prompts/prompt-templates/$firstPromptId/csv-template" -Method GET -Headers $headers -TimeoutSec 5
        
        if ($csvResponse.StatusCode -eq 200) {
            Write-Host "✓ CSV template generation: Working" -ForegroundColor Green
            Write-Host "Content-Type: $($csvResponse.Headers['Content-Type'])" -ForegroundColor Cyan
            Write-Host "Content-Disposition: $($csvResponse.Headers['Content-Disposition'])" -ForegroundColor Cyan
        } else {
            Write-Host "✗ CSV template generation: Failed with status $($csvResponse.StatusCode)" -ForegroundColor Red
        }
    } else {
        Write-Host "⚠ No prompt templates found to test CSV generation" -ForegroundColor Yellow
    }
} catch {
    Write-Host "✗ CSV template test failed: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "`n=== Test Summary ===" -ForegroundColor Green
Write-Host "The refactored MCP server should now work with PromptApiController endpoints." -ForegroundColor White
Write-Host "Key changes made:" -ForegroundColor White
Write-Host "• Changed base URL from :5000 to :5131" -ForegroundColor Cyan
Write-Host "• Updated all endpoints from /api/mcp/ to /api/prompts/" -ForegroundColor Cyan
Write-Host "• Fixed CSV template generation to handle file responses" -ForegroundColor Cyan
Write-Host "• Migrated VariableCollectionsApiController endpoints to PromptApiController" -ForegroundColor Cyan

Write-Host "`nNext steps:" -ForegroundColor Yellow
Write-Host "1. Test the MCP server with Claude Desktop" -ForegroundColor White
Write-Host "2. Verify all MCP tools work correctly" -ForegroundColor White
Write-Host "3. Remove the old VariableCollectionsApiController file if not already done" -ForegroundColor White
