param (
    [string]$KeyGuiBinPath,
    [string]$KeyGuiBuildAssetsPath,
    [string]$BepInExPluginsFolderPath,
    [string]$DistributableArchivePath
)

# Define basic info about mod to distribute
$MOD_NAME = "KeyGUI"
$DISTRIBUTION_KIND = "binary"  # Always binary for KeyGUI

# Init mod related path variables
if (-not $KeyGuiBinPath) {
    Write-Host "No KeyGUI bin path provided, using default path. To see available paths, run with -h or --help."
    $KEYGUI_BIN_PATH = "$env:USERPROFILE\RiderProjects\Modding\KeyMods\KeyGUI\bin\Release"
} else {
    $KEYGUI_BIN_PATH = $KeyGuiBinPath
}
if (-not $KeyGuiBuildAssetsPath) {
    Write-Host "No KeyGUI build assets path provided, using default path. To see available paths, run with -h or --help."
    $KEYGUI_BUILD_ASSETS_PATH = "$env:USERPROFILE\RiderProjects\Modding\KeyMods\KeyGUI\BuildAssets"
} else {
    $KEYGUI_BUILD_ASSETS_PATH = $KeyGuiBuildAssetsPath
}
if (-not $BepInExPluginsFolderPath) {
    Write-Host "No BepInEx plugins folder path provided, using default path. To see available paths, run with -h or --help."
    $BEPINEX_PLUGINS_FOLDER_PATH = "$env:USERPROFILE\AppData\LocalLow\Steam\steamapps\common\worldbox\BepInEx\plugins"
} else {
    $BEPINEX_PLUGINS_FOLDER_PATH = $BepInExPluginsFolderPath
}
if (-not $DistributableArchivePath) {
    Write-Host "No distributable archive path provided, using default path. To see available paths, run with -h or --help."
    $DISTRIBUTABLE_ARCHIVE_PATH = "$BEPINEX_PLUGINS_FOLDER_PATH\$MOD_NAME.zip"
} else {
    $DISTRIBUTABLE_ARCHIVE_PATH = $DistributableArchivePath
}
if ($args[0] -eq "-h" -or $args[0] -eq "--help") {
    Write-Host "Usage: .\assemble_mod_distributable.ps1 [KeyGuiBinPath] [KeyGuiBuildAssetsPath] [BepInExPluginsFolderPath] [(optional) DistributableArchivePath]"
    Write-Host "If no arguments are supplied, default paths will be used."
    exit 0
}

# Print out paths for debugging
Write-Host "KeyGUI Bin Path: $KEYGUI_BIN_PATH"
Write-Host "KeyGUI Build Assets Path: $KEYGUI_BUILD_ASSETS_PATH"
Write-Host "BepInEx Plugins Folder Path: $BEPINEX_PLUGINS_FOLDER_PATH"
Write-Host "Distributable Archive Path: $DISTRIBUTABLE_ARCHIVE_PATH"

# Check if BEPINEX_PLUGINS_FOLDER_PATH is correct (is absolute path)
if (-not (Test-Path $BEPINEX_PLUGINS_FOLDER_PATH)) {
    Write-Host "The BepInEx plugins folder path is incorrect or doesn't exist."
    exit 1
}

# Init distributable folder structure
Set-Location -Path $BEPINEX_PLUGINS_FOLDER_PATH
if (Test-Path "$MOD_NAME.old") {
    Remove-Item -Recurse -Force "$MOD_NAME.old"
}
if (Test-Path "$MOD_NAME.zip") {
    Remove-Item -Force "$MOD_NAME.zip"
}
if (Test-Path "$MOD_NAME") {
    Rename-Item "$MOD_NAME" "$MOD_NAME.old"
}
New-Item -ItemType Directory -Path "$MOD_NAME" | Out-Null
New-Item -ItemType Directory -Path "$MOD_NAME\KeyGUI-Plugin-Dependencies" | Out-Null

# Copy built files into distributable folder
Copy-Item "$KEYGUI_BIN_PATH\KeyGUI.dll" "$BEPINEX_PLUGINS_FOLDER_PATH\$MOD_NAME\KeyGUI.dll"
Copy-Item "$KEYGUI_BIN_PATH\KeyGeneralPurposeLibrary.dll" "$BEPINEX_PLUGINS_FOLDER_PATH\$MOD_NAME\KeyGUI-Plugin-Dependencies\KeyGeneralPurposeLibrary.dll"
Copy-Item "$KEYGUI_BIN_PATH\BepInExModCompatibilityLayer.dll" "$BEPINEX_PLUGINS_FOLDER_PATH\$MOD_NAME\KeyGUI-Plugin-Dependencies\BepInExModCompatibilityLayer.dll"
Copy-Item "$KEYGUI_BIN_PATH\NcmsModCompatibilityLayer.dll" "$BEPINEX_PLUGINS_FOLDER_PATH\$MOD_NAME\KeyGUI-Plugin-Dependencies\NcmsModCompatibilityLayer.dll"

# Copy build assets into distributable folder
Copy-Item "$KEYGUI_BUILD_ASSETS_PATH\KeyGUI-DLL-Dependencies" "$BEPINEX_PLUGINS_FOLDER_PATH\$MOD_NAME\" -Recurse
Copy-Item "$KEYGUI_BUILD_ASSETS_PATH\DefaultAssets" "$BEPINEX_PLUGINS_FOLDER_PATH\$MOD_NAME\" -Recurse
Copy-Item "$KEYGUI_BUILD_ASSETS_PATH\Locales" "$BEPINEX_PLUGINS_FOLDER_PATH\$MOD_NAME\" -Recurse

# Compress distributable folder into zip file
if (Test-Path $DISTRIBUTABLE_ARCHIVE_PATH) {
    Remove-Item $DISTRIBUTABLE_ARCHIVE_PATH -Force
}
Compress-Archive -Path "$MOD_NAME\*" -DestinationPath "$DISTRIBUTABLE_ARCHIVE_PATH"
Write-Host "Archive created at $DISTRIBUTABLE_ARCHIVE_PATH"

# Copy PDB files into distributable folder
Copy-Item "$KEYGUI_BIN_PATH\KeyGUI.pdb" "$BEPINEX_PLUGINS_FOLDER_PATH\$MOD_NAME\KeyGUI.pdb"
Copy-Item "$KEYGUI_BIN_PATH\KeyGeneralPurposeLibrary.pdb" "$BEPINEX_PLUGINS_FOLDER_PATH\$MOD_NAME\KeyGUI-Plugin-Dependencies\KeyGeneralPurposeLibrary.pdb"
Copy-Item "$KEYGUI_BIN_PATH\BepInExModCompatibilityLayer.pdb" "$BEPINEX_PLUGINS_FOLDER_PATH\$MOD_NAME\KeyGUI-Plugin-Dependencies\BepInExModCompatibilityLayer.pdb"
Copy-Item "$KEYGUI_BIN_PATH\NcmsModCompatibilityLayer.pdb" "$BEPINEX_PLUGINS_FOLDER_PATH\$MOD_NAME\KeyGUI-Plugin-Dependencies\NcmsModCompatibilityLayer.pdb"
