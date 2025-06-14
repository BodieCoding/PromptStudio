# PromptStudio MCP Server - Advanced Installer Builder
# This creates a Windows Installer (MSI) package using WiX Toolset

param(
    [string]$Version = "1.0.0",
    [string]$OutputPath = ".\installer"
)

Write-Host "🔧 PromptStudio MCP Server - Advanced Installer" -ForegroundColor Green
Write-Host "   Version: $Version" -ForegroundColor Cyan
Write-Host ""

# Check if WiX is available
$wixPath = Get-Command "heat.exe" -ErrorAction SilentlyContinue
if (-not $wixPath) {
    Write-Host "⚠️  WiX Toolset not found. Installing via winget..." -ForegroundColor Yellow
    try {
        winget install --id WiXToolset.WiX --silent
        Write-Host "✅ WiX Toolset installed successfully" -ForegroundColor Green
    } catch {
        Write-Host "❌ Failed to install WiX. Please install manually from: https://wixtoolset.org/" -ForegroundColor Red
        Write-Host "   Alternative: Use the simple executable builder instead" -ForegroundColor Yellow
        exit 1
    }
}

# First, build the self-contained executable
Write-Host "📦 Building self-contained executable..." -ForegroundColor Yellow
& .\build-mcp-server.ps1 -Configuration Release -OutputPath ".\dist"

if ($LASTEXITCODE -ne 0) {
    Write-Host "❌ Failed to build executable" -ForegroundColor Red
    exit 1
}

# Create installer directory structure
$installerDir = New-Item -ItemType Directory -Path $OutputPath -Force
$sourceDir = Join-Path $installerDir "source"
$wixDir = Join-Path $installerDir "wix"

New-Item -ItemType Directory -Path $sourceDir -Force | Out-Null
New-Item -ItemType Directory -Path $wixDir -Force | Out-Null

# Copy executable to installer source
Copy-Item ".\dist\PromptStudio.Mcp.exe" -Destination $sourceDir

# Create WiX source file
$wixSource = @"
<?xml version="1.0" encoding="UTF-8"?>
<Wix xmlns="http://schemas.microsoft.com/wix/2006/wi">
  <Product Id="*" 
           Name="PromptStudio MCP Server" 
           Language="1033" 
           Version="$Version" 
           Manufacturer="PromptStudio" 
           UpgradeCode="12345678-1234-1234-1234-123456789012">
    
    <Package InstallerVersion="200" Compressed="yes" InstallScope="perMachine" />
    
    <MajorUpgrade DowngradeErrorMessage="A newer version of [ProductName] is already installed." />
    <MediaTemplate EmbedCab="yes" />
    
    <Feature Id="ProductFeature" Title="PromptStudio MCP Server" Level="1">
      <ComponentGroupRef Id="ProductComponents" />
    </Feature>
    
    <Directory Id="TARGETDIR" Name="SourceDir">
      <Directory Id="ProgramFilesFolder">
        <Directory Id="INSTALLFOLDER" Name="PromptStudio MCP Server" />
      </Directory>
      <Directory Id="ProgramMenuFolder">
        <Directory Id="ApplicationProgramsFolder" Name="PromptStudio MCP Server"/>
      </Directory>
    </Directory>
    
    <ComponentGroup Id="ProductComponents" Directory="INSTALLFOLDER">
      <Component Id="MainExecutable">
        <File Id="PromptStudioMcpExe" 
              Source="$($sourceDir)\PromptStudio.Mcp.exe" 
              KeyPath="yes" />
      </Component>
      
      <!-- Add to PATH environment variable -->
      <Component Id="PathEnvironment">
        <Environment Id="PATH" Name="PATH" Value="[INSTALLFOLDER]" Permanent="no" Part="last" Action="set" System="yes" />
      </Component>
      
      <!-- Create Start Menu shortcut -->
      <Component Id="ApplicationShortcut" Directory="ApplicationProgramsFolder">
        <Shortcut Id="ApplicationStartMenuShortcut"
                  Name="PromptStudio MCP Server"
                  Description="PromptStudio MCP Server"
                  Target="[INSTALLFOLDER]PromptStudio.Mcp.exe"
                  WorkingDirectory="INSTALLFOLDER"/>
        <RemoveFolder Id="ApplicationProgramsFolder" On="uninstall"/>
        <RegistryValue Root="HKCU" Key="Software\Microsoft\PromptStudio MCP Server" Name="installed" Type="integer" Value="1" KeyPath="yes"/>
      </Component>
    </ComponentGroup>
    
    <!-- Custom action to configure Claude Desktop -->
    <InstallExecuteSequence>
      <Custom Action="ConfigureClaudeDesktop" After="InstallFinalize">NOT Installed</Custom>
    </InstallExecuteSequence>
    
    <CustomAction Id="ConfigureClaudeDesktop" 
                  ExeCommand='cmd.exe /c "echo Configuring Claude Desktop... && powershell.exe -WindowStyle Hidden -ExecutionPolicy Bypass -Command "& { \`$config = @{ mcpServers = @{ promptstudio = @{ command = \\"[INSTALLFOLDER]PromptStudio.Mcp.exe\\"; args = @() } } } | ConvertTo-Json -Depth 4; \`$configPath = \\"$env:APPDATA\\Claude\\claude_desktop_config.json\\"; if (-not (Test-Path (Split-Path \`$configPath))) { New-Item -ItemType Directory -Path (Split-Path \`$configPath) -Force }; \`$config | Set-Content -Path \`$configPath -Encoding UTF8; Write-Host \\"Claude Desktop configured successfully\\" }"'
                  Directory="INSTALLFOLDER" 
                  Return="ignore" />
  </Product>
</Wix>
"@

$wixSourcePath = Join-Path $wixDir "Product.wxs"
$wixSource | Set-Content -Path $wixSourcePath -Encoding UTF8

Write-Host "📝 Created WiX source file: $wixSourcePath" -ForegroundColor Cyan

# Compile the installer
Write-Host "🔨 Compiling Windows Installer..." -ForegroundColor Yellow

$msiPath = Join-Path $installerDir "PromptStudio.MCP.Server.v$Version.msi"

try {
    # Compile WiX object file
    $wixObjPath = Join-Path $wixDir "Product.wixobj"
    & candle.exe -out $wixObjPath $wixSourcePath
    
    if ($LASTEXITCODE -eq 0) {
        # Link to create MSI
        & light.exe -out $msiPath $wixObjPath
        
        if ($LASTEXITCODE -eq 0) {
            Write-Host "✅ Windows Installer created successfully!" -ForegroundColor Green
            Write-Host "📦 Installer: $msiPath" -ForegroundColor Cyan
            
            $fileInfo = Get-Item $msiPath
            Write-Host "📊 Size: $([math]::Round($fileInfo.Length / 1MB, 2)) MB" -ForegroundColor White
            
            Write-Host ""
            Write-Host "🔧 Installation Instructions:" -ForegroundColor Yellow
            Write-Host "   1. Run the MSI installer as Administrator" -ForegroundColor White
            Write-Host "   2. Follow the installation wizard" -ForegroundColor White
            Write-Host "   3. The installer will automatically configure Claude Desktop" -ForegroundColor White
            Write-Host "   4. Restart Claude Desktop" -ForegroundColor White
        } else {
            throw "Light.exe failed"
        }
    } else {
        throw "Candle.exe failed"
    }
} catch {
    Write-Host "❌ Failed to create installer: $_" -ForegroundColor Red
    Write-Host "💡 Fallback: Use the simple executable from .\dist\PromptStudio.Mcp.exe" -ForegroundColor Yellow
    exit 1
}
"@

$installerScript | Set-Content -Path "c:\Code\Promptlet\create-installer.ps1" -Encoding UTF8
