using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KeyGUI.Utils {
  public static class KeyGuiModManager {
    internal static void TryToRemoveModFromPluginsFolder(string modToRemove) {
      string pluginsFolderPath = Paths.PluginPath;
      string parentFolderPath = Directory.GetParent(pluginsFolderPath)?.FullName;
      foreach (string modPath in Directory.GetFiles(pluginsFolderPath)) {
        string mod = Path.GetFileName(modPath);
        if (mod.ToLowerInvariant().Split(".".ToCharArray())[0].Trim().Equals(modToRemove.ToLowerInvariant().Trim())) {
          if (parentFolderPath != null) {
            File.Move(modPath, Path.Combine(parentFolderPath, mod));
          } else {
            File.Delete(modPath);
          }
        }
      }
    }

    internal static void TryToRemoveModFromModsFolder(string modToRemove) {
      string modsFolderPath = Paths.PluginPath + "/../../Mods";
      string parentFolderPath = Paths.PluginPath + "/../..";
      foreach (string modPath in Directory.GetFiles(modsFolderPath)) {
        string mod = Path.GetFileName(modPath);
        if (mod.EndsWith(".zip") || mod.EndsWith(".mod")) {
          if (mod.ToLowerInvariant().Split(".".ToCharArray())[0].Split("_".ToCharArray())[0].Trim().Equals(modToRemove.ToLowerInvariant().Trim())) {
            File.Move(modPath, Path.Combine(parentFolderPath, mod));
          }
        }
      }

      foreach (string modPath in Directory.GetDirectories(modsFolderPath)) {
        string mod = new DirectoryInfo(modPath).Name;
        if (mod.ToLowerInvariant().Split("_".ToCharArray())[0].Trim().Equals(modToRemove.ToLowerInvariant().Trim())) {
          new DirectoryInfo(modPath).MoveTo(parentFolderPath + "/" + mod);
        }
      }
    }

    internal static ((string, string)[] nonNcmsMods, JArray ncmsMods) FindMods() {
      JArray ncmsMods = new JArray();
      List<(string, string)> nonNcmsMods = new List<(string, string)>();
      string ncmsModsFolderPath = Paths.PluginPath + "/../../Mods";
      if (Directory.Exists(ncmsModsFolderPath)) {
        foreach (string mod in Directory.GetDirectories(ncmsModsFolderPath)) {
          if (Directory.Exists(mod)) {
            foreach (FileInfo file in new DirectoryInfo(mod).GetFiles().Where(file => file.Name == "mod.json")) {
              ncmsMods.Add(JObject.Parse(File.ReadAllText(file.FullName)));
              ncmsMods[ncmsMods.Count - 1]["path"] = mod;
            }
          }
        }

        foreach (string path in Directory.GetFiles(ncmsModsFolderPath)) {
          if (path.Split('.').Last().Equals("zip")) {
            string newPath = "";
            for (int i = 0; i < path.Split('.').Length - 1; ++i) {
              newPath += path.Split('.')[i];
              if (i < path.Split('.').Length - 2) {
                newPath += ".";
              }
            }

            try {
              System.IO.Compression.ZipFile.ExtractToDirectory(path, newPath);
            } catch (IOException) {
              Debug.Log("Failed to extract " + path + " to " + newPath + "!");
              continue;
            }

            foreach (FileInfo file in new DirectoryInfo(newPath).GetFiles().Where(file => file.Name == "mod.json")) {
              ncmsMods.Add(JObject.Parse(File.ReadAllText(file.FullName)));
              ncmsMods[ncmsMods.Count - 1]["path"] = path;
            }

            if (Directory.Exists(newPath)) {
              Directory.Delete(newPath, true);
            }
          }
        }
      }

      string bepInExModsFolderPath = Paths.PluginPath;
      if (Directory.Exists(bepInExModsFolderPath)) {
        nonNcmsMods.AddRange(Directory.GetFiles(bepInExModsFolderPath).Select(mod => Path.GetFileName(mod).Split('.').Where((_, index) => index != Path.GetFileName(mod).Split('.').Length - 1 || index == 0).Append("").Aggregate((current, next) => current + (next != "" ? "." : "") + next)).Select((modName, i) => (modName, Directory.GetFiles(bepInExModsFolderPath)[i])));
      }

      string nativeModsFolderPath = Application.streamingAssetsPath + "/mods";
      if (Directory.Exists(nativeModsFolderPath)) {
        nonNcmsMods.AddRange(Directory.GetFiles(nativeModsFolderPath).Where(mod => !Path.GetFileName(mod).Contains(".json")).Select(mod => Path.GetFileName(mod).Split('.').Where((_, index) => index != Path.GetFileName(mod).Split('.').Length - 1 || index == 0).Append("").Aggregate((current, next) => current + (next != "" ? "." : "") + next)).Select((modName, i) => (modName, Directory.GetFiles(nativeModsFolderPath)[i])));
      }

      Debug.Log("Mods: " + string.Join(", ", nonNcmsMods));
      return (nonNcmsMods.ToArray(), ncmsMods);
    }

    internal static void RemoveMod(string modToRemove) {
      TryToRemoveModFromPluginsFolder(modToRemove);
      TryToRemoveModFromModsFolder(modToRemove);
    }
  }
}