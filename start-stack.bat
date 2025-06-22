@echo off
REM =====================================================
REM PromptStudio Docker Stack Quick Start
REM =====================================================

echo === PromptStudio Docker Stack Manager ===

REM Check if Docker is running
docker info >nul 2>&1
if errorlevel 1 (
    echo ERROR: Docker is not running. Please start Docker Desktop and try again.
    pause
    exit /b 1
)

echo Docker is running...

REM Parse command line arguments
set MODE=production
set CLEAN=false
set LOGS=false
set STOP=false

:parse_args
if "%1"=="" goto start_services
if "%1"=="--dev" set MODE=development
if "%1"=="--development" set MODE=development
if "%1"=="--clean" set CLEAN=true
if "%1"=="--logs" set LOGS=true
if "%1"=="--stop" set STOP=true
if "%1"=="-h" goto show_help
if "%1"=="--help" goto show_help
shift
goto parse_args

:show_help
echo.
echo Usage: start-stack.bat [options]
echo.
echo Options:
echo   --dev, --development    Start in development mode
echo   --clean                 Clean build (rebuild images)
echo   --logs                  Follow logs after startup
echo   --stop                  Stop all services
echo   -h, --help             Show this help
echo.
echo Examples:
echo   start-stack.bat                    Start in production mode
echo   start-stack.bat --dev --logs      Start in development mode and follow logs
echo   start-stack.bat --clean           Clean build and start
echo   start-stack.bat --stop            Stop all services
echo.
pause
exit /b 0

:start_services

if "%STOP%"=="true" (
    echo Stopping all services...
    docker-compose down --remove-orphans
    echo All services stopped.
    goto end
)

REM Prepare compose files
if "%MODE%"=="development" (
    echo Starting in DEVELOPMENT mode...
    set COMPOSE_FILES=-f docker-compose.yml -f docker-compose.dev.yml
) else (
    echo Starting in PRODUCTION mode...
    set COMPOSE_FILES=-f docker-compose.yml
)

REM Build options
if "%CLEAN%"=="true" (
    echo Clean build requested - rebuilding all images...
    set BUILD_ARGS=--build --force-recreate
) else (
    set BUILD_ARGS=
)

echo.
echo Starting services in correct order...

echo Step 1: Starting SQL Server...
docker-compose %COMPOSE_FILES% up -d sqlserver %BUILD_ARGS%
if errorlevel 1 (
    echo ERROR: Failed to start SQL Server
    goto error_exit
)

echo Waiting for SQL Server to be healthy...
:wait_sql
timeout /t 5 /nobreak >nul
docker-compose %COMPOSE_FILES% ps --format json | findstr "sqlserver" | findstr "healthy" >nul
if errorlevel 1 goto wait_sql
echo SQL Server is healthy!

echo Step 2: Starting PromptStudio...
docker-compose %COMPOSE_FILES% up -d promptstudio %BUILD_ARGS%
if errorlevel 1 (
    echo ERROR: Failed to start PromptStudio
    goto error_exit
)

echo Waiting for PromptStudio to be healthy...
:wait_promptstudio
timeout /t 5 /nobreak >nul
docker-compose %COMPOSE_FILES% ps --format json | findstr "promptstudio" | findstr "healthy" >nul
if errorlevel 1 goto wait_promptstudio
echo PromptStudio is healthy!

echo Step 3: Starting MCP Server...
docker-compose %COMPOSE_FILES% up -d mcp-server %BUILD_ARGS%
if errorlevel 1 (
    echo ERROR: Failed to start MCP Server
    goto error_exit
)

echo Waiting for MCP Server...
timeout /t 10 /nobreak >nul

echo.
echo =========================================
echo Stack startup completed successfully!
echo =========================================
echo.
echo Access URLs:
echo   PromptStudio: http://localhost:5000
echo   MCP Server:   http://localhost:3001/health
echo   SQL Server:   localhost,1433 (sa/Two3RobotDuckTag!)
echo.

REM Show service status
echo Current service status:
docker-compose %COMPOSE_FILES% ps

if "%LOGS%"=="true" (
    echo.
    echo Following logs... (Press Ctrl+C to exit)
    docker-compose %COMPOSE_FILES% logs -f
)

goto end

:error_exit
echo.
echo Troubleshooting tips:
echo 1. Ensure Docker Desktop is running
echo 2. Check if ports 1433, 5000, 3001 are available
echo 3. Try running with --clean flag for fresh build
echo 4. Check logs: docker-compose logs [service-name]
echo.
echo Current service status:
docker-compose ps
echo.
pause
exit /b 1

:end
if not "%LOGS%"=="true" pause
exit /b 0
