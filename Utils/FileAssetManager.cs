using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace KeyGUI.Utils {
  public static class FileAssetManager {
    public static Sprite CreateSprite(string spriteName) {
      return CreateSprite("DefaultAssets", spriteName);
    }
    public static Sprite CreateSprite(string authorName, string spriteName) {
      if (Directory.Exists(Path.GetFullPath($"{Application.dataPath}/KeyLibraryModsData"))) {
        Directory.Move(Path.GetFullPath($"{Application.dataPath}/KeyLibraryModsData"), Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets"));
      }
      if (!Directory.Exists(Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/{authorName}/Sprites"))) {
        if (authorName != "DefaultAssets") {
          Directory.CreateDirectory(Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/{authorName}/Sprites"));
        }
      }

      string path = Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/{authorName}/Sprites/{spriteName}.png");
      if (authorName == "DefaultAssets") {
        path = Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/{authorName}/{spriteName}.png");
      }
      Texture2D spriteTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
      if (File.Exists(path)) {
        byte[] imageBytes = File.ReadAllBytes(path);
        try {
          spriteTexture.LoadImage(imageBytes);
        } catch (Exception) {
          Debug.Log($"Invalid image provided for sprite {spriteName} in mod {authorName}, defaulting to the default sprite.");
          try {
            byte[] defaultImageBytes = File.ReadAllBytes(Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/DefaultAssets/DefaultSprite.png"));
            spriteTexture.LoadImage(defaultImageBytes);
          } catch (Exception) {
            Debug.Log("Default sprite not found, attempting to create Sprite without sprite.");
          }
        }
      } else {
        Debug.Log($"Invalid image provided for sprite {spriteName} in mod {authorName}, defaulting to the default sprite.");
        try {
          byte[] defaultImageBytes = File.ReadAllBytes(Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/DefaultAssets/DefaultSprite.png"));
          spriteTexture.LoadImage(defaultImageBytes);
        } catch (Exception) {
          Debug.Log("Default sprite not found, attempting to create Sprite without sprite.");
        }
      }

      Sprite sprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0.5f, 0.5f));
      return sprite;
    }

    public static string[] GetSpriteNames(string authorName = "DefaultAssets") {
      if (Directory.Exists(Path.GetFullPath($"{Application.dataPath}/KeyLibraryModsData"))) {
        Directory.Move(Path.GetFullPath($"{Application.dataPath}/KeyLibraryModsData"), Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets"));
      }
      if (!Directory.Exists(Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/{authorName}/Sprites"))) {
        return Array.Empty<string>();
      }

      string path = Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/{authorName}/Sprites");
      return Directory.GetFiles(path, "*.png").Select(Path.GetFileNameWithoutExtension).ToArray();
    }

    public static void InitializeLocalFolders() {
      if (Directory.Exists(Path.GetFullPath($"{Application.dataPath}/KeyLibraryModsData"))) {
        Directory.Move(Path.GetFullPath($"{Application.dataPath}/KeyLibraryModsData"), Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets"));
      }
      if (Directory.Exists(Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/KeyGUI/Traits"))) {
        Directory.Move(Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/KeyGUI/Traits"), Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/Traits"));
      }
      DirectoryInfo rootDir = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;
      if (rootDir == null) {
        Debug.LogError("Something went wrong when trying to find DefaultAssets, file asset related functionality might not work correctly.");
        return;
      }
      string defaultPath = Path.GetFullPath($"{rootDir.FullName}/DefaultAssets");
      if (!Directory.Exists(Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/Traits"))) {
        Directory.CreateDirectory(Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/Traits"));
      }

      if (Directory.Exists(defaultPath)) {
        DirectoryInfo defaultDirectory = new DirectoryInfo(defaultPath);
        string destinationPath = Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/DefaultAssets");
        if (Directory.Exists(destinationPath)) {
          Directory.Delete(destinationPath, true);
        }

        defaultDirectory.MoveTo(Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/DefaultAssets"));
      }
    }
  }
}
