# Test script to verify VS Code extension API connectivity
Write-Host "Testing PromptStudio VS Code Extension API Connectivity..." -ForegroundColor Green

# Test if PromptStudio server is running
try {
    $response = Invoke-RestMethod -Uri "http://localhost:5131/api/prompts/prompts" -Method GET -ContentType "application/json"
    Write-Host "✓ PromptStudio server is running on localhost:5131" -ForegroundColor Green
    Write-Host "✓ Found $($response.Count) prompts in the database" -ForegroundColor Green
} catch {
    Write-Host "✗ Cannot connect to PromptStudio server" -ForegroundColor Red
    Write-Host "Please make sure the PromptStudio web app is running" -ForegroundColor Yellow
    exit 1
}

# Test collections endpoint
try {
    $collections = Invoke-RestMethod -Uri "http://localhost:5131/api/prompts/collections" -Method GET -ContentType "application/json"
    Write-Host "✓ Collections endpoint working - found $($collections.Count) collections" -ForegroundColor Green
} catch {
    Write-Host "✗ Collections endpoint not working" -ForegroundColor Red
}

Write-Host "`nAPI Endpoints Summary:" -ForegroundColor Cyan
Write-Host "- Prompts: http://localhost:5131/api/prompts/prompts" -ForegroundColor White
Write-Host "- Collections: http://localhost:5131/api/prompts/collections" -ForegroundColor White
Write-Host "- Execute: http://localhost:5131/api/prompts/prompts/{id}/execute" -ForegroundColor White

Write-Host "`nNext Steps:" -ForegroundColor Cyan
Write-Host "1. The VS Code extension has been updated with correct API endpoints" -ForegroundColor White
Write-Host "2. Open VS Code and try using @promptstudio in GitHub Copilot Chat" -ForegroundColor White
Write-Host "3. Available commands:" -ForegroundColor White
Write-Host "   - @promptstudio list - List all available prompts" -ForegroundColor Gray
Write-Host "   - @promptstudio search <term> - Search for prompts" -ForegroundColor Gray
Write-Host "   - @promptstudio execute <id> - Execute a prompt with variables" -ForegroundColor Gray

Write-Host "`nExample usage:" -ForegroundColor Cyan
Write-Host "@promptstudio list" -ForegroundColor Yellow
Write-Host "@promptstudio execute 1" -ForegroundColor Yellow
