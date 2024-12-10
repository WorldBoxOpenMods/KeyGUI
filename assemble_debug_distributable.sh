#!/bin/sh
# Init KeyGUI related path variables
KEYGUI_BIN_PATH=~/RiderProjects/Modding/KeyMods/KeyGUI/bin/Debug
KEYGUI_BUILD_ASSETS_PATH=~/RiderProjects/Modding/KeyMods/KeyGUI/BuildAssets
BEPINEX_PLUGINS_FOLDER_PATH=~/Library/Application\ Support/Steam/steamapps/common/worldbox/BepInEx/plugins

# Init KeyGUI distributable folder structure
cd "$BEPINEX_PLUGINS_FOLDER_PATH" || exit
rm -rf "./KeyGUI"
rm -rf "./KeyGUI.old"
rm "./KeyGUI.zip"
rm -rf "./KeyGUI_Debug.old"
rm "./KeyGUI_Debug.zip"
mv "./KeyGUI_Debug" "./KeyGUI_Debug.old"
mkdir "./KeyGUI_Debug"
mkdir "./KeyGUI_Debug/KeyGUI-Plugin-Dependencies"

# Copy built files into distributable folder
cd "$KEYGUI_BIN_PATH" || exit
mv "./KeyGUI.dll" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI_Debug/KeyGUI.dll"
mv "./KeyGeneralPurposeLibrary.dll" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI_Debug/KeyGUI-Plugin-Dependencies/KeyGeneralPurposeLibrary.dll"
mv "./BepInExModCompatibilityLayer.dll" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI_Debug/KeyGUI-Plugin-Dependencies/BepInExModCompatibilityLayer.dll"
mv "./NcmsModCompatibilityLayer.dll" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI_Debug/KeyGUI-Plugin-Dependencies/NcmsModCompatibilityLayer.dll"

# Copy build assets into distributable folder
cd "$KEYGUI_BUILD_ASSETS_PATH" || exit
cp -R "./KeyGUI-DLL-Dependencies" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI_Debug/"
cp -R "./DefaultAssets" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI_Debug/"
cp -R "./Locales" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI_Debug/"

# Copy PDB files into distributable folder
cd "$KEYGUI_BIN_PATH" || exit
mv "./KeyGUI.pdb" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI_Debug/KeyGUI.pdb"
mv "./KeyGeneralPurposeLibrary.pdb" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI_Debug/KeyGUI-Plugin-Dependencies/KeyGeneralPurposeLibrary.pdb"
mv "./BepInExModCompatibilityLayer.pdb" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI_Debug/KeyGUI-Plugin-Dependencies/BepInExModCompatibilityLayer.pdb"
mv "./NcmsModCompatibilityLayer.pdb" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI_Debug/KeyGUI-Plugin-Dependencies/NcmsModCompatibilityLayer.pdb"
