# Build and Install PromptStudio VS Code Extension

Write-Host "Building PromptStudio VS Code Extension" -ForegroundColor Green
Write-Host ""

# Ensure we're in the extension directory
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $scriptDir

# Check if Node.js is installed
try {
    $nodeVersion = & node --version
    Write-Host "Node.js version: $nodeVersion" -ForegroundColor Green
} catch {
    Write-Host "Node.js is not installed or not in PATH" -ForegroundColor Red
    Write-Host "Please install Node.js from https://nodejs.org/" -ForegroundColor Yellow
    exit 1
}

# Check if npm is available
try {
    $npmVersion = & npm --version
    Write-Host "npm version: $npmVersion" -ForegroundColor Green
} catch {
    Write-Host "npm is not available" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Installing dependencies..." -ForegroundColor Yellow
& npm install

if ($LASTEXITCODE -ne 0) {
    Write-Host "Failed to install dependencies" -ForegroundColor Red
    exit 1
}

Write-Host "Dependencies installed successfully" -ForegroundColor Green
Write-Host ""

Write-Host "Compiling TypeScript..." -ForegroundColor Yellow
& npm run compile

if ($LASTEXITCODE -ne 0) {
    Write-Host "Compilation failed" -ForegroundColor Red
    exit 1
}

Write-Host "Compilation successful" -ForegroundColor Green
Write-Host ""

Write-Host "Packaging extension..." -ForegroundColor Yellow
& npm run package

if ($LASTEXITCODE -ne 0) {
    Write-Host "Packaging failed" -ForegroundColor Red
    exit 1
}

# Find the generated VSIX file
$vsixFile = Get-ChildItem -Filter "*.vsix" | Sort-Object LastWriteTime -Descending | Select-Object -First 1

if ($vsixFile) {
    Write-Host "Extension packaged successfully!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Package created:" -ForegroundColor Cyan
    Write-Host "   File: $($vsixFile.FullName)" -ForegroundColor White
    Write-Host "   Size: $([math]::Round($vsixFile.Length / 1KB, 2)) KB" -ForegroundColor White
    Write-Host ""
    
    # Ask if user wants to install the extension
    $install = Read-Host "Do you want to install the extension in VS Code? (y/n)"
    
    if ($install -eq 'y' -or $install -eq 'Y' -or $install -eq 'yes') {
        Write-Host "Installing extension in VS Code..." -ForegroundColor Yellow
        
        try {
            & code --install-extension $vsixFile.FullName
            
            if ($LASTEXITCODE -eq 0) {
                Write-Host "Extension installed successfully!" -ForegroundColor Green
                Write-Host ""
                Write-Host "Setup complete! Next steps:" -ForegroundColor Cyan
                Write-Host "   1. Start PromptStudio server: dotnet run (in PromptStudio directory)" -ForegroundColor White
                Write-Host "   2. Restart VS Code" -ForegroundColor White
                Write-Host "   3. Look for 'PromptStudio' in the Activity Bar" -ForegroundColor White
                Write-Host "   4. Test Copilot integration with: @promptstudio list" -ForegroundColor White
            } else {
                Write-Host "Extension packaging completed, but installation failed" -ForegroundColor Yellow
                Write-Host "You can manually install using: code --install-extension $($vsixFile.Name)" -ForegroundColor White
            }
        } catch {
            Write-Host "Could not install automatically. Please install manually:" -ForegroundColor Yellow
            Write-Host "   1. Open VS Code" -ForegroundColor White
            Write-Host "   2. Press Ctrl+Shift+P" -ForegroundColor White
            Write-Host "   3. Run 'Extensions: Install from VSIX...'" -ForegroundColor White
            Write-Host "   4. Select: $($vsixFile.FullName)" -ForegroundColor White
        }
    } else {
        Write-Host "To install later, use:" -ForegroundColor Cyan
        Write-Host "   code --install-extension $($vsixFile.Name)" -ForegroundColor White
    }
} else {
    Write-Host "No VSIX file found after packaging" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Build completed!" -ForegroundColor Green
