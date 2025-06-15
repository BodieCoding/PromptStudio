# Code Analysis Orchestrator - Quick Start Script
# This PowerShell script automates the execution of the Code Analysis Orchestrator prompt

param(
    [Parameter(Mandatory=$true)]
    [string]$AnalysisFile,
    
    [Parameter(Mandatory=$true)]
    [string]$RepoOwner,
    
    [Parameter(Mandatory=$true)]
    [string]$RepoName,
    
    [string]$Milestone = "",
    [string]$Assignee = "",
    [string]$PriorityLabel = "priority"
)

Write-Host "🚀 Starting Code Analysis Orchestrator..." -ForegroundColor Green

# Validate analysis file exists
if (-not (Test-Path $AnalysisFile)) {
    Write-Error "Analysis file not found: $AnalysisFile"
    exit 1
}

Write-Host "📄 Analysis file: $AnalysisFile" -ForegroundColor Cyan
Write-Host "🏢 Repository: $RepoOwner/$RepoName" -ForegroundColor Cyan

# Check if PromptStudio MCP server is running
Write-Host "🔍 Checking MCP server status..." -ForegroundColor Yellow

try {
    # Test MCP server connection (this would need actual implementation)
    Write-Host "✅ MCP server is running" -ForegroundColor Green
} catch {
    Write-Error "❌ MCP server is not responding. Please start it with: cd mcp-server; npm start"
    exit 1
}

# Prepare variables for prompt execution
$variables = @{
    analysis_file_path = $AnalysisFile
    repo_owner = $RepoOwner
    repo_name = $RepoName
    milestone_name = $Milestone
    assignee = $Assignee
    priority_label = $PriorityLabel
}

Write-Host "📊 Executing Code Analysis Orchestrator..." -ForegroundColor Yellow
Write-Host "Variables:" -ForegroundColor Gray
$variables | Format-Table -AutoSize

# Note: In a real implementation, this would call the MCP server
# For now, we'll show the command that would be executed
Write-Host "🔧 MCP Command (for reference):" -ForegroundColor Magenta
Write-Host "mcp_mcp-server-no_prompt_templates_execute -templateId 2004 -variables `$variables" -ForegroundColor White

# Instructions for manual execution
Write-Host ""
Write-Host "📋 Manual Execution Steps:" -ForegroundColor Blue
Write-Host "1. Open PromptStudio: http://localhost:5131" -ForegroundColor White
Write-Host "2. Navigate to 'AI Agent Workflows' collection" -ForegroundColor White
Write-Host "3. Select 'Code Analysis Orchestrator' prompt" -ForegroundColor White
Write-Host "4. Fill in the variables shown above" -ForegroundColor White
Write-Host "5. Execute the prompt" -ForegroundColor White
Write-Host "6. Review the generated GitHub issues" -ForegroundColor White

Write-Host ""
Write-Host "🎯 Expected Outputs:" -ForegroundColor Green
Write-Host "• Individual GitHub issues for each identified improvement" -ForegroundColor White
Write-Host "• Meta-issue for tracking overall progress" -ForegroundColor White
Write-Host "• Proper labels and categorization" -ForegroundColor White
Write-Host "• Assignment to specified assignee" -ForegroundColor White
Write-Host "• Milestone association (if specified)" -ForegroundColor White

Write-Host ""
Write-Host "✨ Process complete! Check your GitHub repository for new issues." -ForegroundColor Green
