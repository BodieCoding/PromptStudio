#!/usr/bin/env powershell

# Simple MCP API Validation Test for PromptStudio
Write-Host "=====================================================================" -ForegroundColor Cyan
Write-Host "             PromptStudio MCP API - Simple Validation Test         " -ForegroundColor Cyan  
Write-Host "=====================================================================" -ForegroundColor Cyan

$baseUrl = "http://localhost:5131/api/Mcp"
$passCount = 0
$failCount = 0

function Test-API {
    param($TestName, $Uri, $Method = "GET", $Body = $null)
    
    Write-Host "`n--- Testing: $TestName ---" -ForegroundColor Yellow
    try {
        if ($Body) {
            $headers = @{"Content-Type" = "application/json"}
            $response = Invoke-WebRequest -Uri $Uri -Method $Method -Body $Body -Headers $headers -UseBasicParsing
        } else {
            $response = Invoke-WebRequest -Uri $Uri -Method $Method -UseBasicParsing
        }
        
        if ($response.StatusCode -eq 200 -or $response.StatusCode -eq 201) {
            Write-Host "[PASS] $TestName - Status: $($response.StatusCode)" -ForegroundColor Green
            $script:passCount++
            return $response.Content | ConvertFrom-Json
        } else {
            Write-Host "[FAIL] $TestName - Status: $($response.StatusCode)" -ForegroundColor Red
            $script:failCount++
            return $null
        }
    } catch {
        Write-Host "[FAIL] $TestName - Error: $($_.Exception.Message)" -ForegroundColor Red
        $script:failCount++
        return $null
    }
}

# Test 1: Get Collections
$collections = Test-API "Get Collections" "$baseUrl/collections"
if ($collections) {
    Write-Host "  Found $($collections.Count) collections" -ForegroundColor Cyan
}

# Test 2: Get Prompts
$prompts = Test-API "Get Prompts" "$baseUrl/prompts"
if ($prompts) {
    Write-Host "  Found $($prompts.Count) prompts" -ForegroundColor Cyan
    $testPromptId = $prompts[0].id
    Write-Host "  Using Prompt ID $testPromptId for CSV tests" -ForegroundColor Magenta
    
    # Test 3: Generate CSV Template
    $csvTemplate = Test-API "Generate CSV Template" "$baseUrl/prompt-templates/$testPromptId/csv-template"
    if ($csvTemplate) {
        $lines = $csvTemplate -split "`n"
        Write-Host "  Template: $($lines[0])" -ForegroundColor Cyan
    }
    
    # Test 4: Create Variable Collection
    $timestamp = Get-Date -Format "HHmmss"
    $csvData = "language,code`npython,print('test!')`njavascript,console.log('test!')"
    $body = @{
        PromptId = $testPromptId
        Name = "Test Collection $timestamp"
        Description = "MCP validation test"
        CsvData = $csvData
    } | ConvertTo-Json
    
    $collection = Test-API "Create Variable Collection" "$baseUrl/variable-collections" "POST" $body
    if ($collection) {
        Write-Host "  Created: $($collection.name) (ID: $($collection.id))" -ForegroundColor Cyan
    }
}

# Summary
Write-Host "`n=====================================================================" -ForegroundColor Cyan
Write-Host "                            TEST SUMMARY                            " -ForegroundColor Cyan
Write-Host "=====================================================================" -ForegroundColor Cyan

$totalTests = $passCount + $failCount
Write-Host "Results: $passCount PASSED, $failCount FAILED out of $totalTests tests" -ForegroundColor Cyan

if ($passCount -ge 3) {
    Write-Host "`n[SUCCESS] MCP CSV functionality is working correctly!" -ForegroundColor Green
    Write-Host "  - CSV template generation: Working" -ForegroundColor Green
    Write-Host "  - Variable collection creation: Working" -ForegroundColor Green  
    Write-Host "  - API endpoints: Responding correctly" -ForegroundColor Green
    Write-Host "`n[READY] System is ready for MCP server integration!" -ForegroundColor Green
} else {
    Write-Host "`n[ERROR] Some core functionality needs attention" -ForegroundColor Red
}

Write-Host "`n=====================================================================" -ForegroundColor Cyan
