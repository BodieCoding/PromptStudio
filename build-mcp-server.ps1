# PromptStudio MCP Server - Build Self-Contained Executable
# This script creates a single-file executable that can be used directly with Claude Desktop

param(
    [ValidateSet("win-x64", "win-x86", "win-arm64", "linux-x64", "osx-x64", "osx-arm64")]
    [string]$Runtime = "win-x64",
    
    [ValidateSet("Release", "Debug")]
    [string]$Configuration = "Release",
    
    [string]$OutputPath = ".\dist"
)

Write-Host "🚀 Building PromptStudio MCP Server" -ForegroundColor Green
Write-Host "   Runtime: $Runtime" -ForegroundColor Cyan
Write-Host "   Configuration: $Configuration" -ForegroundColor Cyan
Write-Host "   Output Path: $OutputPath" -ForegroundColor Cyan
Write-Host ""

# Ensure we're in the correct directory
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $scriptDir

# Create output directory
if (Test-Path $OutputPath) {
    Remove-Item $OutputPath -Recurse -Force
}
New-Item -ItemType Directory -Path $OutputPath -Force | Out-Null

# Build the self-contained executable
Write-Host "Publishing self-contained executable..." -ForegroundColor Yellow

$publishArgs = @(
    "publish"
    "PromptStudioMcpServer/PromptStudioMcpServer.csproj"
    "--configuration", $Configuration
    "--runtime", $Runtime
    "--self-contained", "true"
    "--output", $OutputPath
    "/p:PublishSingleFile=true"
    "/p:PublishTrimmed=true"
    "/p:PublishReadyToRun=true"
    "--verbosity", "minimal"
)

& dotnet @publishArgs

if ($LASTEXITCODE -eq 0) {
    Write-Host "Build completed successfully!" -ForegroundColor Green
      # Show the created executable
    $exeName = if ($Runtime.StartsWith("win")) { "PromptStudioMcpServer.exe" } else { "PromptStudioMcpServer" }
    $exePath = Join-Path $OutputPath $exeName
    
    if (Test-Path $exePath) {
        $fileInfo = Get-Item $exePath
        Write-Host ""
        Write-Host "Executable created:" -ForegroundColor Cyan
        Write-Host "   Path: $($fileInfo.FullName)" -ForegroundColor White
        Write-Host "   Size: $([math]::Round($fileInfo.Length / 1MB, 2)) MB" -ForegroundColor White
        Write-Host ""
        
        # Create Claude Desktop configuration
        $claudeConfig = @{
            mcpServers = @{
                promptstudio = @{
                    command = $fileInfo.FullName
                    args = @()
                }
            }
        } | ConvertTo-Json -Depth 4
        
        $claudeConfigPath = Join-Path $OutputPath "claude_desktop_config.json"
        $claudeConfig | Set-Content -Path $claudeConfigPath -Encoding UTF8
        
        Write-Host "Claude Desktop configuration created:" -ForegroundColor Cyan
        Write-Host "   Path: $claudeConfigPath" -ForegroundColor White
        Write-Host ""
        Write-Host "To use with Claude Desktop:" -ForegroundColor Yellow
        Write-Host "   1. Copy the contents of $claudeConfigPath" -ForegroundColor White
        Write-Host "   2. Paste into %APPDATA%\Claude\claude_desktop_config.json" -ForegroundColor White
        Write-Host "   3. Restart Claude Desktop" -ForegroundColor White
        Write-Host ""
        Write-Host "To test manually:" -ForegroundColor Yellow
        Write-Host "   $($fileInfo.FullName)" -ForegroundColor White
    }
} else {
    Write-Host "Build failed!" -ForegroundColor Red
    exit 1
}
