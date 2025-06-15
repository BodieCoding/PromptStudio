# Repository Rename PowerShell Script
# Run this AFTER completing the GitHub web interface renames

Write-Host "Starting repository migration automation..." -ForegroundColor Cyan

# Step 1: Update local git remote
Write-Host "Updating local git remote..." -ForegroundColor Yellow
try {
    git remote set-url origin https://github.com/BodieCoding/promptstudio.git
    Write-Host "Git remote updated successfully" -ForegroundColor Green
    
    # Verify the change
    Write-Host "Current remotes:" -ForegroundColor Yellow
    git remote -v
}
catch {
    Write-Host "Failed to update git remote: $_" -ForegroundColor Red
}

# Step 2: Test connection to new repository
Write-Host "Testing connection to new repository..." -ForegroundColor Yellow
try {
    git ls-remote origin HEAD
    Write-Host "Connection to new repository successful" -ForegroundColor Green
}
catch {
    Write-Host "Failed to connect to new repository: $_" -ForegroundColor Red
}

# Step 3: Update any local references in documentation
Write-Host "Updating documentation references..." -ForegroundColor Yellow

# Update README.md references
$readmePath = "README.md"
if (Test-Path $readmePath) {
    $content = Get-Content $readmePath -Raw
    $updated = $content -replace "BodieCoding/Promptlet", "BodieCoding/promptstudio"
    Set-Content $readmePath $updated
    Write-Host "Updated README.md references" -ForegroundColor Green
}

Write-Host "Migration automation complete!" -ForegroundColor Green
Write-Host "Manual steps completed:" -ForegroundColor Cyan
Write-Host "  Rename promptstudio to promptstudio-legacy" -ForegroundColor Gray
Write-Host "  Rename Promptlet to promptstudio" -ForegroundColor Gray
Write-Host "Automated steps completed:" -ForegroundColor Cyan
Write-Host "  Updated local git remote" -ForegroundColor Gray
Write-Host "  Updated documentation references" -ForegroundColor Gray
Write-Host ""
Write-Host "New primary repository: https://github.com/BodieCoding/promptstudio" -ForegroundColor Green
Write-Host "Legacy repository: https://github.com/BodieCoding/promptstudio-legacy" -ForegroundColor Yellow
