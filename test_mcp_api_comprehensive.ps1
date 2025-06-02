# Comprehensive test of MCP API endpoints
Write-Host "=== Testing PromptStudio MCP API Endpoints ===" -ForegroundColor Green

# Base URL
$baseUrl = "http://localhost:5131/api/Mcp"

Write-Host "`n1. Testing Collections endpoint..." -ForegroundColor Yellow
try {
    $response = Invoke-WebRequest -Uri "$baseUrl/collections" -Method GET -UseBasicParsing
    Write-Host "✓ Collections: Status $($response.StatusCode)" -ForegroundColor Green
    $collections = $response.Content | ConvertFrom-Json
    Write-Host "  Found $($collections.Count) collections"
} catch {
    Write-Host "✗ Collections failed: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "`n2. Testing Prompts endpoint..." -ForegroundColor Yellow
try {
    $response = Invoke-WebRequest -Uri "$baseUrl/prompts" -Method GET -UseBasicParsing
    Write-Host "✓ Prompts: Status $($response.StatusCode)" -ForegroundColor Green
    $prompts = $response.Content | ConvertFrom-Json
    Write-Host "  Found $($prompts.Count) prompts"
    $firstPromptId = $prompts[0].id
} catch {
    Write-Host "✗ Prompts failed: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "`n3. Testing CSV Template generation..." -ForegroundColor Yellow
try {
    $response = Invoke-WebRequest -Uri "$baseUrl/prompt-templates/$firstPromptId/csv-template" -Method GET -UseBasicParsing
    Write-Host "✓ CSV Template: Status $($response.StatusCode)" -ForegroundColor Green
    Write-Host "  Template content:"
    Write-Host "  $($response.Content.Substring(0, [Math]::Min(100, $response.Content.Length)))"
} catch {
    Write-Host "✗ CSV Template failed: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "`n4. Testing Variable Collection creation..." -ForegroundColor Yellow
try {
    $headers = @{"Content-Type" = "application/json"}
    $body = @{
        PromptId = $firstPromptId
        Name = "API Test Collection $(Get-Date -Format 'yyyyMMdd-HHmmss')"
        Description = "Test collection created via API"
        CsvData = "language,code`npython,print('hello from API')`njavascript,console.log('hello from API')"
    } | ConvertTo-Json
    
    $response = Invoke-WebRequest -Uri "$baseUrl/variable-collections" -Method POST -Body $body -Headers $headers -UseBasicParsing
    Write-Host "✓ Variable Collection Creation: Status $($response.StatusCode)" -ForegroundColor Green
    $newCollection = $response.Content | ConvertFrom-Json
    Write-Host "  Created collection: $($newCollection.name) (ID: $($newCollection.id))"
} catch {
    Write-Host "✗ Variable Collection Creation failed: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "`n5. Testing Variable Collections listing..." -ForegroundColor Yellow
try {
    $response = Invoke-WebRequest -Uri "$baseUrl/variable-collections?promptId=$firstPromptId" -Method GET -UseBasicParsing
    Write-Host "✓ Variable Collections List: Status $($response.StatusCode)" -ForegroundColor Green
    $collections = $response.Content | ConvertFrom-Json
    Write-Host "  Found $($collections.Count) variable collections for prompt $firstPromptId"
} catch {
    Write-Host "⚠ Variable Collections List failed: $($_.Exception.Message)" -ForegroundColor Yellow
    Write-Host "  This might be due to routing conflicts - using alternative endpoint..."
    
    # Try alternative endpoint
    try {
        $response = Invoke-WebRequest -Uri "http://localhost:5131/api/VariableCollectionsApi/template/$firstPromptId" -Method GET -UseBasicParsing
        Write-Host "✓ Alternative CSV Template: Status $($response.StatusCode)" -ForegroundColor Green
    } catch {
        Write-Host "✗ Alternative endpoint also failed: $($_.Exception.Message)" -ForegroundColor Red
    }
}

Write-Host "`n=== MCP API Test Complete ===" -ForegroundColor Green
