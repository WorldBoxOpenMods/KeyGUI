#!/bin/sh
# Init KeyGUI related path variables
if test $# -eq 0; then
  KEYGUI_BIN_PATH=~/RiderProjects/Modding/KeyMods/KeyGUI/bin/Release
  KEYGUI_BUILD_ASSETS_PATH=~/RiderProjects/Modding/KeyMods/KeyGUI/BuildAssets
  BEPINEX_PLUGINS_FOLDER_PATH=~/Library/Application\ Support/Steam/steamapps/common/worldbox/BepInEx/plugins
else
  if test "$1" = "-h" || test "$1" = "--help"; then
    echo "Usage: ./assemble_mod_distributable.sh [KeyGUIBinPath] [KeyGUIBuildAssetsPath] [BepInExPluginsFolderPath]"
    echo "If no arguments are supplied, default paths will be used."
    exit 0
  else
    KEYGUI_BIN_PATH=$1
    KEYGUI_BUILD_ASSETS_PATH=$2
    BEPINEX_PLUGINS_FOLDER_PATH=$3
  fi
fi

# Init KeyGUI distributable folder structure
cd "$BEPINEX_PLUGINS_FOLDER_PATH" || exit
rm -rf "./KeyGUI.old"
rm "./KeyGUI.zip"
mv "./KeyGUI" "./KeyGUI.old"
mkdir "./KeyGUI"
mkdir "./KeyGUI/Plugin-Dependencies"

# Copy built files into distributable folder
cd "$KEYGUI_BIN_PATH" || exit
mv "./KeyGUI.dll" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/KeyGUI.dll"
mv "./KeyGeneralPurposeLibrary.dll" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/Plugin-Dependencies/KeyGeneralPurposeLibrary.dll"
mv "./BepInExModCompatibilityLayer.dll" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/Plugin-Dependencies/BepInExModCompatibilityLayer.dll"

# Copy build assets into distributable folder
cd "$KEYGUI_BUILD_ASSETS_PATH" || exit
cp -R "./DefaultAssets" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/"
cp -R "./Locales" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/"

# Compress distributable folder into zip file
cd "$BEPINEX_PLUGINS_FOLDER_PATH" || exit
zip -r "./KeyGUI.zip" "./KeyGUI"

# Copy PDB files into distributable folder
cd "$KEYGUI_BIN_PATH" || exit
mv "./KeyGUI.pdb" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/KeyGUI.pdb"
mv "./KeyGeneralPurposeLibrary.pdb" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/Plugin-Dependencies/KeyGeneralPurposeLibrary.pdb"
mv "./BepInExModCompatibilityLayer.pdb" "$BEPINEX_PLUGINS_FOLDER_PATH/KeyGUI/Plugin-Dependencies/BepInExModCompatibilityLayer.pdb"
