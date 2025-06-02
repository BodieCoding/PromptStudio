# Simple CSV functionality test
Write-Host "Testing Enhanced CSV Functionality via MCP API" -ForegroundColor Green

$baseUrl = "http://localhost:5131/api/mcp"
$headers = @{ "Content-Type" = "application/json" }

Write-Host "`nStep 1: Get Collections" -ForegroundColor Yellow
try {
    $collections = Invoke-RestMethod -Uri "$baseUrl/collections" -Method GET -Headers $headers
    Write-Host "Found $($collections.Count) collections"
    $collections | Format-Table -AutoSize
    
    if ($collections.Count -eq 0) {
        Write-Host "No collections found. Please create some test data first." -ForegroundColor Red
        exit 1
    }
    
    $collectionId = $collections[0].id
} catch {
    Write-Host "Failed to get collections: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

Write-Host "`nStep 2: Get Prompts in First Collection" -ForegroundColor Yellow
try {
    $prompts = Invoke-RestMethod -Uri "$baseUrl/prompts?collectionId=$collectionId" -Method GET -Headers $headers
    Write-Host "Found $($prompts.Count) prompts in collection"
    $prompts | Select-Object id, name, variableCount | Format-Table -AutoSize
    
    if ($prompts.Count -eq 0) {
        Write-Host "No prompts found in collection. Please create some test prompts first." -ForegroundColor Red
        exit 1
    }
    
    $promptId = $prompts[0].id
    $promptName = $prompts[0].name
} catch {
    Write-Host "Failed to get prompts: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

Write-Host "`nStep 3: Generate CSV Template for Prompt '$promptName'" -ForegroundColor Yellow
try {
    $csvTemplate = Invoke-RestMethod -Uri "$baseUrl/prompt-templates/$promptId/csv-template" -Method GET -Headers $headers
    Write-Host "Generated CSV template:"
    Write-Host $csvTemplate -ForegroundColor Cyan
} catch {
    Write-Host "Failed to generate CSV template: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

Write-Host "`nStep 4: Create Sample CSV Data" -ForegroundColor Yellow
$templateLines = $csvTemplate -split "`n"
$headers = $templateLines[0] -split ","

$sampleData = @()
$sampleData += $templateLines[0]  # Add header row

for ($i = 1; $i -le 3; $i++) {
    $row = @()
    foreach ($header in $headers) {
        $cleanHeader = $header.Trim('"')
        if ($cleanHeader -match "language|lang") {
            $languages = @("javascript", "python", "java")
            $row += "`"$($languages[($i-1) % $languages.Count])`""
        } elseif ($cleanHeader -match "code|script") {
            $codes = @(
                "console.log('Hello World');",
                "print('Hello World')",
                "System.out.println(`"Hello World`");"
            )
            $row += "`"$($codes[($i-1) % $codes.Count])`""
        } elseif ($cleanHeader -match "name") {
            $row += "`"Test Name $i`""
        } else {
            $row += "`"Sample Value $i for $cleanHeader`""
        }
    }
    $sampleData += $row -join ","
}

$csvData = $sampleData -join "`n"
Write-Host "Created sample CSV data:"
Write-Host $csvData -ForegroundColor Cyan

Write-Host "`nStep 5: Create Variable Collection from CSV" -ForegroundColor Yellow
try {
    $createRequest = @{
        promptId = $promptId
        name = "Test CSV Collection $(Get-Date -Format 'HHmmss')"
        description = "Created via MCP API test"
        csvData = $csvData
    } | ConvertTo-Json -Depth 3

    $newCollection = Invoke-RestMethod -Uri "$baseUrl/variable-collections" -Method POST -Body $createRequest -Headers $headers
    Write-Host "Created variable collection:"
    $newCollection | Format-List
    
    $variableCollectionId = $newCollection.id
} catch {
    Write-Host "Failed to create variable collection: $($_.Exception.Message)" -ForegroundColor Red
    if ($_.Exception.Response) {
        $reader = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
        $responseBody = $reader.ReadToEnd()
        Write-Host "Response: $responseBody" -ForegroundColor Red
    }
    exit 1
}

Write-Host "`nStep 6: Execute Batch Processing" -ForegroundColor Yellow
try {
    $batchRequest = @{
        promptId = $promptId
    } | ConvertTo-Json

    $batchResult = Invoke-RestMethod -Uri "$baseUrl/variable-collections/$variableCollectionId/execute" -Method POST -Body $batchRequest -Headers $headers
    Write-Host "Batch execution completed:"
    Write-Host "  Total Sets: $($batchResult.totalSets)"
    Write-Host "  Successful: $($batchResult.successfulExecutions)"
    Write-Host "  Failed: $($batchResult.failedExecutions)"
    
    Write-Host "`nExecution Results:" -ForegroundColor Cyan
    foreach ($result in $batchResult.results) {
        Write-Host "  Set $($result.setIndex + 1): Success=$($result.success)"
        if ($result.success) {
            Write-Host "    Variables: $($result.variables | ConvertTo-Json -Compress)"
            Write-Host "    Resolved Prompt (first 100 chars): $($result.resolvedPrompt.Substring(0, [Math]::Min(100, $result.resolvedPrompt.Length)))..."
        } else {
            Write-Host "    Error: $($result.error)" -ForegroundColor Red
        }
    }
} catch {
    Write-Host "Failed to execute batch: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

Write-Host "`nCSV Functionality Test Complete!" -ForegroundColor Green
Write-Host "The enhanced MCP API successfully:" -ForegroundColor Green
Write-Host "  - Generated CSV templates from prompt variables" -ForegroundColor Green
Write-Host "  - Parsed CSV data into variable collections" -ForegroundColor Green
Write-Host "  - Executed batch processing with CSV data" -ForegroundColor Green
Write-Host "  - Provided comprehensive results and error handling" -ForegroundColor Green
