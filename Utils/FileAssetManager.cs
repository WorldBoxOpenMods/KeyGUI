using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace KeyGUI.Utils {
  public static class FileAssetManager {
    public static Sprite CreateSprite(string modName, string spriteName) {
      if (modName == "KeyGeneralPurposeLibrary") {
        modName = "DefaultAssets";
      }

      if (!Directory.Exists(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Sprites"))) {
        if (modName != "DefaultAssets") {
          Directory.CreateDirectory(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Sprites"));
        }
      }

      string path = Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Sprites/" + spriteName + ".png");
      if (modName == "DefaultAssets") {
        path = Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/" + spriteName + ".png");
      }
      Texture2D spriteTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
      if (File.Exists(path)) {
        byte[] imageBytes = File.ReadAllBytes(path);
        try {
          spriteTexture.LoadImage(imageBytes);
        } catch (Exception) {
          Debug.Log("Invalid image provided for sprite " + spriteName + " in mod " + modName + ", defaulting to the default sprite.");
          try {
            byte[] defaultImageBytes = File.ReadAllBytes(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/DefaultAssets/DefaultSprite.png"));
            spriteTexture.LoadImage(defaultImageBytes);
          } catch (Exception) {
            Debug.Log("Default sprite not found, attempting to create Sprite without sprite.");
          }
        }
      } else {
        Debug.Log("Invalid image provided for sprite " + spriteName + " in mod " + modName + ", defaulting to the default sprite.");
        try {
          byte[] defaultImageBytes = File.ReadAllBytes(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/DefaultAssets/DefaultSprite.png"));
          spriteTexture.LoadImage(defaultImageBytes);
        } catch (Exception) {
          Debug.Log("Default sprite not found, attempting to create Sprite without sprite.");
        }
      }

      Sprite sprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0.5f, 0.5f));
      return sprite;
    }

    public static void InitializeLocalFolders(string modName) {
      DirectoryInfo keyGuiRootDir = new FileInfo(Assembly.GetCallingAssembly().Location).Directory;
      if (keyGuiRootDir == null) {
        Debug.LogError("Something went wrong when trying to find DefaultAssets, file asset related functionality might not work correctly.");
        return;
      }
      string defaultPath = Path.GetFullPath(keyGuiRootDir.FullName + "/DefaultAssets");
      if (!Directory.Exists(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Traits"))) {
        Directory.CreateDirectory(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Traits"));
      }

      if (!Directory.Exists(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Items"))) {
        Directory.CreateDirectory(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Items"));
      }

      if (!Directory.Exists(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Sprites"))) {
        Directory.CreateDirectory(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Sprites"));
      }

      if (Directory.Exists(defaultPath)) {
        DirectoryInfo defaultDirectory = new DirectoryInfo(defaultPath);
        string destinationPath = Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/DefaultAssets");
        if (Directory.Exists(destinationPath)) {
          Directory.Delete(destinationPath, true);
        }

        defaultDirectory.MoveTo(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/DefaultAssets"));
      }
    }

    public static void MoveTraitsToFolder(string modName) {
      string pathToCheck = Path.GetFullPath(BepInEx.Paths.PluginPath);
      foreach (string file in Directory.GetFiles(pathToCheck).Where(file => file.Contains("traits")).Where(file => file.Contains(".txt"))) {
        int number;
        for (number = 0; File.Exists(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Traits/" + Path.GetFileName(file).Replace(".txt", "") + "_" + number + ".txt")); ++number) { }
        try {
          File.Move(file, Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Traits/" + Path.GetFileName(file).Replace(".txt", "") + "_" + number + ".txt"));
        } catch (Exception) {
          // ignored
        }
      }
    }

    public static void MoveItemsToFolder(string modName) {
      string pathToCheck = Path.GetFullPath(BepInEx.Paths.PluginPath);
      foreach (string file in Directory.GetFiles(pathToCheck).Where(file => file.Contains("items")).Where(file => file.Contains(".txt"))) {
        int number;
        for (number = 0; File.Exists(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Items/" + Path.GetFileName(file).Replace(".txt", "") + "_" + number + ".txt")); ++number) { }
        try {
          File.Move(file, Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Items/" + Path.GetFileName(file).Replace(".txt", "") + "_" + number + ".txt"));
        } catch (Exception) {
          // ignored
        }
      }
    }

    public static void MoveSpritesToFolder(string modName) {
      string pathToCheck = Path.GetFullPath(BepInEx.Paths.PluginPath);
      foreach (string file in Directory.GetFiles(pathToCheck).Where(file => file.Contains(".png"))) {
        try {
          if (!File.Exists(Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Sprites/" + Path.GetFileName(file)))) {
            File.Move(file, Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + modName + "/Sprites/" + Path.GetFileName(file)));
          }
        } catch (Exception) {
          // ignored
        }
      }
    }

    // TODO: WHY IS THIS BUGGED. WHICH PART OF THIS IS NOT WORKING. PAIN.
    public static void MoveModFilesToOtherMod(string original, string newMod) {
      string pathToCheck = Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + original);
      if (Directory.Exists(pathToCheck)) {
        foreach (string folderPath in Directory.GetDirectories(pathToCheck)) {
          foreach (string filePath in Directory.GetFiles(folderPath)) {
            try {
              Debug.Log("Original file location: " + filePath);
              Debug.Log("New path: " + Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + newMod + "/" + folderPath.Split('/')[folderPath.Split('/').Length - 1] + "/" + Path.GetFileName(filePath)));
              File.Move(filePath, Path.GetFullPath(Application.dataPath + "/KeyLibraryModsData/" + newMod + "/" + folderPath.Split('/')[folderPath.Split('/').Length - 1] + "/" + Path.GetFileName(filePath)));
            } catch (Exception) {
              // ignored
            }
          }
        }
      }
    }
  }
}
