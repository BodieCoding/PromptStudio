#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Start the PromptStudio Docker stack with proper sequencing and health monitoring.

.DESCRIPTION
    This script starts the PromptStudio stack services in the correct order:
    1. SQL Server with health checks
    2. PromptStudio web application
    3. MCP Server
    
    Supports both production and development modes.

.PARAMETER Mode
    Deployment mode: 'production' (default) or 'development'

.PARAMETER Clean
    Clean build (rebuild images from scratch)

.PARAMETER Logs
    Follow logs after startup

.PARAMETER Stop
    Stop all services

.EXAMPLE
    .\start-stack.ps1
    Start in production mode

.EXAMPLE
    .\start-stack.ps1 -Mode development -Logs
    Start in development mode and follow logs

.EXAMPLE
    .\start-stack.ps1 -Clean
    Clean build and start in production mode

.EXAMPLE
    .\start-stack.ps1 -Stop
    Stop all services
#>

param(
    [ValidateSet('production', 'development')]
    [string]$Mode = 'production',
    
    [switch]$Clean,
    [switch]$Logs,
    [switch]$Stop
)

# Colors for output
$ErrorColor = 'Red'
$SuccessColor = 'Green'
$InfoColor = 'Cyan'
$WarningColor = 'Yellow'

function Write-ColorOutput {
    param([string]$Message, [string]$Color = 'White')
    Write-Host $Message -ForegroundColor $Color
}

function Test-DockerRunning {
    try {
        docker info | Out-Null
        return $true
    } catch {
        return $false
    }
}

function Wait-ForService {
    param(
        [string]$ServiceName,
        [int]$MaxAttempts = 30,
        [int]$SleepSeconds = 10
    )
    
    Write-ColorOutput "Waiting for $ServiceName to be healthy..." $InfoColor
    
    for ($i = 1; $i -le $MaxAttempts; $i++) {
        $health = docker-compose ps --format json | ConvertFrom-Json | Where-Object { $_.Service -eq $ServiceName } | Select-Object -ExpandProperty Health -ErrorAction SilentlyContinue
        
        if ($health -eq 'healthy') {
            Write-ColorOutput "✓ $ServiceName is healthy" $SuccessColor
            return $true
        }
        
        Write-ColorOutput "  Attempt $i/$MaxAttempts - $ServiceName status: $health" $WarningColor
        Start-Sleep $SleepSeconds
    }
    
    Write-ColorOutput "✗ $ServiceName failed to become healthy after $($MaxAttempts * $SleepSeconds) seconds" $ErrorColor
    return $false
}

function Show-ServiceStatus {
    Write-ColorOutput "`n=== Service Status ===" $InfoColor
    docker-compose ps
    
    Write-ColorOutput "`n=== Service Health ===" $InfoColor
    docker-compose ps --format "table {{.Service}}\t{{.State}}\t{{.Health}}"
}

function Show-Urls {
    Write-ColorOutput "`n=== Access URLs ===" $SuccessColor
    Write-ColorOutput "PromptStudio Web App: http://localhost:5000" $SuccessColor
    Write-ColorOutput "MCP Server:           http://localhost:3001" $SuccessColor
    Write-ColorOutput "SQL Server:           Server=localhost,1433;User Id=sa;Password=Two3RobotDuckTag!" $SuccessColor
}

# Main execution
try {
    Write-ColorOutput "=== PromptStudio Docker Stack Manager ===" $InfoColor
    
    # Check if Docker is running
    if (-not (Test-DockerRunning)) {
        Write-ColorOutput "✗ Docker is not running. Please start Docker Desktop and try again." $ErrorColor
        exit 1
    }
    
    # Handle stop command
    if ($Stop) {
        Write-ColorOutput "Stopping all services..." $InfoColor
        docker-compose down --remove-orphans
        Write-ColorOutput "✓ All services stopped" $SuccessColor
        exit 0
    }
    
    # Prepare compose files
    $composeFiles = @('docker-compose.yml')
    if ($Mode -eq 'development') {
        $composeFiles += 'docker-compose.dev.yml'
        Write-ColorOutput "Starting in DEVELOPMENT mode..." $WarningColor
    } else {
        Write-ColorOutput "Starting in PRODUCTION mode..." $InfoColor
    }
    
    # Build options
    $buildArgs = @()
    if ($Clean) {
        Write-ColorOutput "Clean build requested - rebuilding all images..." $WarningColor
        $buildArgs += '--build', '--force-recreate'
    }
    
    # Compose command arguments
    $composeArgs = @()
    foreach ($file in $composeFiles) {
        $composeArgs += '-f', $file
    }
    
    Write-ColorOutput "Starting services with proper dependency order..." $InfoColor
    
    # Start SQL Server first
    Write-ColorOutput "Step 1: Starting SQL Server..." $InfoColor
    & docker-compose @composeArgs up -d sqlserver @buildArgs
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to start SQL Server"
    }
    
    if (-not (Wait-ForService -ServiceName 'sqlserver')) {
        throw "SQL Server failed to start properly"
    }
    
    # Start PromptStudio
    Write-ColorOutput "Step 2: Starting PromptStudio..." $InfoColor
    & docker-compose @composeArgs up -d promptstudio @buildArgs
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to start PromptStudio"
    }
    
    if (-not (Wait-ForService -ServiceName 'promptstudio')) {
        throw "PromptStudio failed to start properly"
    }
    
    # Start MCP Server
    Write-ColorOutput "Step 3: Starting MCP Server..." $InfoColor
    & docker-compose @composeArgs up -d mcp-server @buildArgs
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to start MCP Server"
    }
    
    if (-not (Wait-ForService -ServiceName 'mcp-server')) {
        Write-ColorOutput "⚠ MCP Server health check failed, but this may be normal for stdio-based MCP servers" $WarningColor
    }
    
    Write-ColorOutput "`n🎉 Stack startup completed!" $SuccessColor
    Show-ServiceStatus
    Show-Urls
    
    # Follow logs if requested
    if ($Logs) {
        Write-ColorOutput "`nFollowing logs (Ctrl+C to exit)..." $InfoColor
        & docker-compose @composeArgs logs -f
    }
    
} catch {
    Write-ColorOutput "✗ Error: $($_.Exception.Message)" $ErrorColor
    Write-ColorOutput "`nTroubleshooting steps:" $WarningColor
    Write-ColorOutput "1. Check Docker Desktop is running" $WarningColor
    Write-ColorOutput "2. Ensure ports 1433, 5000, 3001 are available" $WarningColor
    Write-ColorOutput "3. Try running with -Clean flag for a fresh build" $WarningColor
    Write-ColorOutput "4. Check logs: docker-compose logs [service-name]" $WarningColor
    
    # Show current status for debugging
    Write-ColorOutput "`nCurrent service status:" $InfoColor
    try {
        docker-compose ps
    } catch {
        Write-ColorOutput "Could not get service status" $ErrorColor
    }
    
    exit 1
}
