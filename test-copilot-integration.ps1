# Test PromptStudio API and Copilot Integration
# This script tests the API connection and adds sample data if needed

Write-Host "🔍 Testing PromptStudio API Connection..." -ForegroundColor Cyan

$baseUrl = "http://localhost:5131"

try {
    # Test API connection
    $response = Invoke-RestMethod -Uri "$baseUrl/api/prompts/prompts" -Method Get -ErrorAction Stop
    Write-Host "✅ API Connection successful!" -ForegroundColor Green
    Write-Host "📊 Found $($response.Count) existing prompts" -ForegroundColor White
    
    if ($response.Count -eq 0) {
        Write-Host "🚀 Adding sample prompts for testing..." -ForegroundColor Yellow
        
        # Create sample prompts
        $samplePrompts = @(
            @{
                name = "Code Review Assistant"
                description = "Reviews code for best practices and suggests improvements"
                content = "Please review the following code for:\n- Code quality and best practices\n- Potential bugs or issues\n- Performance optimizations\n- Readability improvements\n\nCode:\n{{code}}\n\nLanguage: {{language}}"
            },
            @{
                name = "Technical Documentation Writer"
                description = "Creates comprehensive technical documentation"
                content = "Create detailed technical documentation for:\n\nTitle: {{title}}\nDescription: {{description}}\nTarget Audience: {{audience}}\n\nPlease include:\n1. Overview\n2. Prerequisites\n3. Step-by-step instructions\n4. Code examples\n5. Troubleshooting section"
            },
            @{
                name = "API Design Assistant"
                description = "Helps design RESTful API endpoints and specifications"
                content = "Design a RESTful API for: {{feature_name}}\n\nRequirements:\n- {{requirements}}\n- Authentication: {{auth_type}}\n- Data format: {{data_format}}\n\nPlease provide:\n1. Endpoint definitions\n2. Request/response schemas\n3. HTTP status codes\n4. Error handling\n5. Example requests/responses"
            }
        )
        
        foreach ($prompt in $samplePrompts) {            try {
                $json = $prompt | ConvertTo-Json -Depth 3
                $result = Invoke-RestMethod -Uri "$baseUrl/api/prompts/prompts" -Method Post -Body $json -ContentType "application/json"
                Write-Host "  ✅ Created: $($prompt.name)" -ForegroundColor Green
            }
            catch {
                Write-Host "  ❌ Failed to create: $($prompt.name) - $($_.Exception.Message)" -ForegroundColor Red
            }        }
        
        # Refresh the prompt list
        $response = Invoke-RestMethod -Uri "$baseUrl/api/prompts/prompts" -Method Get
        Write-Host "📊 Total prompts after creation: $($response.Count)" -ForegroundColor White
    }
    
    # Display current prompts
    if ($response.Count -gt 0) {
        Write-Host "`n📋 Available Prompts:" -ForegroundColor Cyan
        $response | Select-Object id, name, description | Format-Table -AutoSize
    }
    
    Write-Host "`n🎯 Next Steps:" -ForegroundColor Yellow
    Write-Host "1. Open VS Code" -ForegroundColor White
    Write-Host "2. Open the Command Palette (Ctrl+Shift+P)" -ForegroundColor White
    Write-Host "3. Type 'GitHub Copilot: Open Chat'" -ForegroundColor White
    Write-Host "4. In the chat, type: @promptstudio list" -ForegroundColor White
    Write-Host "5. Or try: @promptstudio search code review" -ForegroundColor White
    
}
catch {
    Write-Host "❌ API Connection failed: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "Make sure PromptStudio is running on port 5131" -ForegroundColor Yellow
}
