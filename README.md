# KeyGUI

KeyGUI is a QoL mod made for WorldBox using BepInEx as its mod loader. It doesn't have any sort of consietent theme or coherent feature set, as I just add whatever features I feel like to it.

## Building

The project, as uploaded on this repo, cannot be built out of the box. It assumes that all of it's two mod project dependencies, [KeyGeneralPurposeLibrary](https://github.com/WorldBoxOpenMods/KeyGeneralPurposeLibrary) and [BepInExModCompatibilityLayer](https://github.com/WorldBoxOpenMods/BepInExModCompatibilityLayer), are located in the parent directory of the KeyGUI project, and that further dependency DLLs like BepInEx and a publicised Assembly-CSharp.dll file are located within a Libraries directory in the parent directory of KeyGUI.
To be able to build it, you need to either replicate this file structure or edit the .csproj file to contain dependency paths which work on your device.
