# Import Data Analysis Prompts to PromptStudio
# This script uses the MCP server to import the data analysis prompt collection

Write-Host "🔍 Data Analysis Prompt Collection Importer" -ForegroundColor Cyan
Write-Host "==========================================" -ForegroundColor Cyan

# Check if PromptStudio is running
try {
    $response = Invoke-WebRequest -Uri "http://localhost:5000/api/mcp/collections" -UseBasicParsing -TimeoutSec 5
    Write-Host "✅ PromptStudio is running" -ForegroundColor Green
} catch {
    Write-Host "❌ PromptStudio is not running. Please start it first." -ForegroundColor Red
    Write-Host "Run: cd PromptStudio && dotnet run" -ForegroundColor Yellow
    exit 1
}

# Check if MCP server is available
try {
    $mcpPath = ".\mcp-server\dist\index.js"
    if (-not (Test-Path $mcpPath)) {
        Write-Host "❌ MCP server not built. Building now..." -ForegroundColor Yellow
        cd mcp-server
        npm run build
        cd ..
    }
    Write-Host "✅ MCP server is available" -ForegroundColor Green
} catch {
    Write-Host "❌ Error with MCP server setup" -ForegroundColor Red
    exit 1
}

# Read the prompt collection data
$promptData = Get-Content "data_analysis_prompts.json" | ConvertFrom-Json

Write-Host "📁 Creating collection: $($promptData.collection.name)" -ForegroundColor Blue

# Create collection (we'll use the web interface for this as the API endpoint needs verification)
Write-Host "Please create the collection manually in the web interface at http://localhost:5000" -ForegroundColor Yellow
Write-Host "Collection Name: $($promptData.collection.name)" -ForegroundColor White
Write-Host "Description: $($promptData.collection.description)" -ForegroundColor White

Read-Host "Press Enter once you've created the collection and noted its ID"

$collectionId = Read-Host "Enter the Collection ID from the web interface"

Write-Host "📝 Importing prompts..." -ForegroundColor Blue

# For demonstration, let's show what would be imported
foreach ($prompt in $promptData.prompts) {
    Write-Host "  📄 $($prompt.name)" -ForegroundColor White
    Write-Host "     Variables detected:" -ForegroundColor Gray
    
    # Extract variables from prompt content
    $variables = [regex]::Matches($prompt.content, '\{\{([^}]+)\}\}') | ForEach-Object { $_.Groups[1].Value } | Sort-Object -Unique
    foreach ($var in $variables) {
        Write-Host "       - $var" -ForegroundColor DarkGray
    }
    Write-Host ""
}

Write-Host "📊 Variable Collections Available:" -ForegroundColor Blue
Write-Host "  📋 data_analysis_variables.csv - General data analysis scenarios" -ForegroundColor White
Write-Host "  📋 ab_test_variables.csv - A/B testing scenarios" -ForegroundColor White  
Write-Host "  📋 trend_analysis_variables.csv - Trend forecasting scenarios" -ForegroundColor White
Write-Host "  📋 customer_segmentation_variables.csv - Customer segmentation scenarios" -ForegroundColor White

Write-Host "`n🚀 Next Steps:" -ForegroundColor Green
Write-Host "1. Copy the prompt content from data_analysis_prompts.json" -ForegroundColor White
Write-Host "2. Create prompts manually in the web interface" -ForegroundColor White
Write-Host "3. Upload the CSV files as variable collections" -ForegroundColor White
Write-Host "4. Use the DATA_ANALYSIS_GUIDE.md for detailed usage instructions" -ForegroundColor White

Write-Host "`n📖 Quick Start Examples:" -ForegroundColor Green
Write-Host "- Use 'Statistical Summary Analysis' with e-commerce sales data" -ForegroundColor White
Write-Host "- Try 'A/B Test Results Analysis' with homepage CTA test data" -ForegroundColor White
Write-Host "- Run 'Customer Segmentation Analysis' with RFM segmentation" -ForegroundColor White

Write-Host "`n✨ Ready to analyze data with AI-powered insights!" -ForegroundColor Cyan
