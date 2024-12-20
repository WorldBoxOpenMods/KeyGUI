#!/bin/sh
# Init KeyGUI related path variables
KEYGUI_BIN_PATH=~/RiderProjects/Modding/KeyMods/KeyGUI/bin/Release
KEYGUI_BUILD_ASSETS_PATH=~/RiderProjects/Modding/KeyMods/KeyGUI/BuildAssets
BEPINEX_PLUGINS_FOLDER_PATH=~/Library/Application\ Support/Steam/steamapps/common/worldbox/BepInEx/plugins

# Init KeyGUI distributable folder structure
cd "$BEPINEX_PLUGINS_FOLDER_PATH" || exit
rm -rf "./KeyGUI_Debug"
rm -rf "./KeyGUI_Debug.old"
rm "./KeyGUI_Debug.zip"
rm -rf "./KeyGUI.old"
rm "./KeyGUI.zip"
mv "./KeyGUI" "./KeyGUI.old"
mkdir "./KeyGUI"
mkdir "./KeyGUI/KeyGUI-Plugin-Dependencies"

# Copy built files into distributable folder
cd "$KEYGUI_BIN_PATH" || exit
mv "./KeyGUI.dll" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/KeyGUI.dll"
mv "./KeyGeneralPurposeLibrary.dll" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/KeyGUI-Plugin-Dependencies/KeyGeneralPurposeLibrary.dll"
mv "./BepInExModCompatibilityLayer.dll" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/KeyGUI-Plugin-Dependencies/BepInExModCompatibilityLayer.dll"
mv "./NcmsModCompatibilityLayer.dll" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/KeyGUI-Plugin-Dependencies/NcmsModCompatibilityLayer.dll"

# Copy build assets into distributable folder
cd "$KEYGUI_BUILD_ASSETS_PATH" || exit
cp -R "./KeyGUI-DLL-Dependencies" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/"
cp -R "./DefaultAssets" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/"
cp -R "./Locales" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/"

# Compress distributable folder into zip file
cd "$BEPINEX_PLUGINS_FOLDER_PATH" || exit
zip -r "./KeyGUI.zip" "./KeyGUI"

# Copy PDB files into distributable folder
cd "$KEYGUI_BIN_PATH" || exit
mv "./KeyGUI.pdb" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/KeyGUI.pdb"
mv "./KeyGeneralPurposeLibrary.pdb" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/KeyGUI-Plugin-Dependencies/KeyGeneralPurposeLibrary.pdb"
mv "./BepInExModCompatibilityLayer.pdb" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/KeyGUI-Plugin-Dependencies/BepInExModCompatibilityLayer.pdb"
mv "./NcmsModCompatibilityLayer.pdb" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/KeyGUI-Plugin-Dependencies/NcmsModCompatibilityLayer.pdb"
