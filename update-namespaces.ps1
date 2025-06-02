# PowerShell script to update namespace references in PromptStudio project
$projectPath = "c:\Code\Promptlet\PromptStudio"

Write-Host "Updating namespace references in PromptStudio project..." -ForegroundColor Green

# Get all C# files in the project
$files = Get-ChildItem -Path $projectPath -Recurse -Filter "*.cs" | Where-Object { $_.FullName -notlike "*\bin\*" -and $_.FullName -notlike "*\obj\*" }

foreach ($file in $files) {
    Write-Host "Processing: $($file.Name)" -ForegroundColor Yellow
    
    $content = Get-Content -Path $file.FullName -Raw
    $originalContent = $content
    
    # Update namespace references
    $content = $content -replace "using PromptStudio\.Domain;", "using PromptStudio.Core.Domain;"
    $content = $content -replace "using PromptStudio\.Services;", "using PromptStudio.Core.Interfaces;`r`nusing PromptStudio.Data.Services;"
    
    # Only write if content changed
    if ($content -ne $originalContent) {
        Set-Content -Path $file.FullName -Value $content -NoNewline
        Write-Host "  Updated namespace references" -ForegroundColor Green
    } else {
        Write-Host "  No changes needed" -ForegroundColor Gray
    }
}

Write-Host "`nNamespace update complete!" -ForegroundColor Green
