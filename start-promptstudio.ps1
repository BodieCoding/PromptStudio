# PromptStudio Docker Startup Script
param(
    [switch]$Development,
    [switch]$Production,
    [switch]$Stop,
    [switch]$Status,
    [switch]$Logs,
    [string]$Service = ""
)

$ErrorActionPreference = "Stop"

function Write-Banner {
    Write-Host "🚀 PromptStudio Docker Management" -ForegroundColor Cyan
    Write-Host "=================================" -ForegroundColor Cyan
    Write-Host ""
}

function Show-Usage {
    Write-Host "Usage:" -ForegroundColor Yellow
    Write-Host "  .\start-promptstudio.ps1 -Development    # Start in development mode" -ForegroundColor Gray
    Write-Host "  .\start-promptstudio.ps1 -Production     # Start in production mode" -ForegroundColor Gray
    Write-Host "  .\start-promptstudio.ps1 -Stop           # Stop all services" -ForegroundColor Gray
    Write-Host "  .\start-promptstudio.ps1 -Status         # Show service status" -ForegroundColor Gray
    Write-Host "  .\start-promptstudio.ps1 -Logs           # Show logs for all services" -ForegroundColor Gray
    Write-Host "  .\start-promptstudio.ps1 -Logs -Service mcp-server  # Show logs for specific service" -ForegroundColor Gray
    Write-Host ""
}

Write-Banner

if ($Stop) {
    Write-Host "🛑 Stopping PromptStudio services..." -ForegroundColor Red
    docker-compose down
    Write-Host "✅ Services stopped" -ForegroundColor Green
    exit 0
}

if ($Status) {
    Write-Host "📊 Service Status:" -ForegroundColor Yellow
    docker-compose ps
    Write-Host ""
    Write-Host "🔗 Service URLs:" -ForegroundColor Yellow
    Write-Host "  PromptStudio Web UI: http://localhost:5000" -ForegroundColor Gray
    Write-Host "  MCP Server: http://localhost:3001" -ForegroundColor Gray
    Write-Host "  SQL Server: localhost:1433 (sa/Two3RobotDuckTag!)" -ForegroundColor Gray
    exit 0
}

if ($Logs) {
    Write-Host "📋 Viewing logs..." -ForegroundColor Yellow
    if ($Service) {
        docker-compose logs -f $Service
    } else {
        docker-compose logs -f
    }
    exit 0
}

if ($Development) {
    Write-Host "🔧 Starting PromptStudio in DEVELOPMENT mode..." -ForegroundColor Yellow
    Write-Host "Features: Hot reload, debug logging, development environment" -ForegroundColor Gray
    Write-Host ""
    docker-compose -f docker-compose.yml -f docker-compose.dev.yml up -d
    
    Write-Host ""
    Write-Host "✅ Development environment started!" -ForegroundColor Green
    Write-Host ""
    Write-Host "🔗 Access your services:" -ForegroundColor Cyan
    Write-Host "  📱 PromptStudio Web UI: http://localhost:5000" -ForegroundColor White
    Write-Host "  🔌 MCP Server: http://localhost:3001" -ForegroundColor White
    Write-Host "  💾 SQL Server: localhost:1433" -ForegroundColor White
    Write-Host ""
    Write-Host "📋 Useful commands:" -ForegroundColor Yellow
    Write-Host "  View logs: .\start-promptstudio.ps1 -Logs" -ForegroundColor Gray
    Write-Host "  Check status: .\start-promptstudio.ps1 -Status" -ForegroundColor Gray
    Write-Host "  Stop services: .\start-promptstudio.ps1 -Stop" -ForegroundColor Gray
    
} elseif ($Production) {
    Write-Host "🚀 Starting PromptStudio in PRODUCTION mode..." -ForegroundColor Green
    Write-Host "Features: Optimized performance, production logging" -ForegroundColor Gray
    Write-Host ""
    docker-compose up -d
    
    Write-Host ""
    Write-Host "✅ Production environment started!" -ForegroundColor Green
    Write-Host ""
    Write-Host "🔗 Access your services:" -ForegroundColor Cyan
    Write-Host "  📱 PromptStudio Web UI: http://localhost:5000" -ForegroundColor White
    Write-Host "  🔌 MCP Server: http://localhost:3001" -ForegroundColor White
    Write-Host "  💾 SQL Server: localhost:1433" -ForegroundColor White
    
} else {
    Write-Host "❌ Please specify either -Development or -Production mode" -ForegroundColor Red
    Write-Host ""
    Show-Usage
    exit 1
}

Write-Host ""
Write-Host "⏳ Services are starting up... This may take a few moments." -ForegroundColor Yellow
Write-Host "💡 Use '.\start-promptstudio.ps1 -Status' to check when all services are ready." -ForegroundColor Yellow
