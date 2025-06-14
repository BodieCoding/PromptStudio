@echo off
REM PromptStudio MCP Server - Build Self-Contained Executable (Batch Version)
REM This creates a single executable that doesn't require .NET runtime

echo 🚀 Building PromptStudio MCP Server...
echo.

REM Get current directory
set "BUILD_DIR=%CD%"
set "OUTPUT_DIR=%BUILD_DIR%\dist"

REM Clean output directory
if exist "%OUTPUT_DIR%" (
    echo 🧹 Cleaning previous build...
    rmdir /s /q "%OUTPUT_DIR%"
)
mkdir "%OUTPUT_DIR%"

echo 📦 Publishing self-contained executable...
dotnet publish PromptStudio.Mcp/PromptStudio.Mcp.csproj ^
    --configuration Release ^
    --runtime win-x64 ^
    --self-contained true ^
    --output "%OUTPUT_DIR%" ^
    /p:PublishSingleFile=true ^
    /p:PublishTrimmed=true ^
    /p:PublishReadyToRun=true ^
    --verbosity minimal

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ✅ Build completed successfully!
    echo 📄 Executable created: %OUTPUT_DIR%\PromptStudio.Mcp.exe
    
    REM Get file size
    for %%I in ("%OUTPUT_DIR%\PromptStudio.Mcp.exe") do (
        set "FILE_SIZE=%%~zI"
    )
    set /a "FILE_SIZE_MB=%FILE_SIZE% / 1048576"
    echo 📊 Size: %FILE_SIZE_MB% MB
    echo.
    
    REM Create Claude Desktop configuration
    echo 📝 Creating Claude Desktop configuration...
    (
        echo {
        echo   "mcpServers": {
        echo     "promptstudio": {
        echo       "command": "%OUTPUT_DIR%\\PromptStudio.Mcp.exe",
        echo       "args": []
        echo     }
        echo   }
        echo }
    ) > "%OUTPUT_DIR%\claude_desktop_config.json"
    
    echo ✅ Configuration created: %OUTPUT_DIR%\claude_desktop_config.json
    echo.
    echo 🔧 To use with Claude Desktop:
    echo    1. Copy contents of claude_desktop_config.json
    echo    2. Paste into %%APPDATA%%\Claude\claude_desktop_config.json
    echo    3. Restart Claude Desktop
    echo.
    echo 🧪 To test manually:
    echo    "%OUTPUT_DIR%\PromptStudio.Mcp.exe"
    echo.
) else (
    echo.
    echo ❌ Build failed!
    exit /b 1
)

pause
