using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using KeyGUI.Menus.ModConfig;
using KeyGUI.Menus.ModConfig.ConfigOptions;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KeyGUI.Utils {
  public static class KeyGuiModManager {
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    public class ModInfo {
      public string Name { get; set; }
      public string DateAdded { get; set; }
      public string DateFoundByMod { get; set; }
      public string DateRemoved { get; set; }
      public string DateLastChanged { get; set; }
    }
    public class NmlModInfo : ModInfo {
      public string Json { get; set; }
      public bool IsWorkshopLoaded { get; set; }
    }
    public class BepinexModInfo : ModInfo {
      public bool IsNmlCompliantFormat { get; set; }
      public bool IsWorkshopLoaded { get; set; }
    }
    public class NativeModInfo : ModInfo {
      public bool IsMemoryLoaded { get; set; }
    }
    // ReSharper restore UnusedAutoPropertyAccessor.Global
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
      string modsFolderPath;
      switch (Application.platform) {
        case RuntimePlatform.LinuxPlayer:
          modsFolderPath = Paths.PluginPath + "/../../../Mods";
          break;
        default:
          modsFolderPath = Paths.PluginPath + "/../../Mods";
          break;
      }
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

    internal static (NativeModInfo[] nativeMods, BepinexModInfo[] bepinexMods, NmlModInfo[] nmlMods) FindMods() {
      List<NativeModInfo> nativeModsList = new List<NativeModInfo>();
      List<BepinexModInfo> bepinexModsList = new List<BepinexModInfo>();
      List<NmlModInfo> nmlModsList = new List<NmlModInfo>();
      string nmlModsFolderPath;
      switch (Application.platform) {
        case RuntimePlatform.LinuxPlayer:
          nmlModsFolderPath = $"{Paths.PluginPath}/../../../Mods";
          break;
        default:
          nmlModsFolderPath = $"{Paths.PluginPath}/../../Mods";
          break;
      }
      if (Directory.Exists(nmlModsFolderPath)) {
        foreach (string path in Directory.GetFiles(nmlModsFolderPath)) {
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
              continue;
            }

            if (Directory.Exists(newPath)) {
              Directory.Delete(newPath, true);
            }
          }
        }
        foreach (string mod in Directory.GetDirectories(nmlModsFolderPath).Where(Directory.Exists)) {
          nmlModsList.AddRange(new DirectoryInfo(mod).GetFiles().
            Where(file => file.Name == "mod.json").
            Select(file => new NmlModInfo {
              Name = JObject.Parse(File.ReadAllText(file.FullName))["name"]?.ToString() ?? "Unknown",
              Json = File.ReadAllText(file.FullName),
              DateAdded = File.GetCreationTimeUtc(file.FullName).ToString("yy/MM/dd"),
              DateFoundByMod = File.GetLastAccessTimeUtc(file.FullName).ToString("yy/MM/dd"),
              DateRemoved = null,
              DateLastChanged = File.GetLastWriteTimeUtc(file.FullName).ToString("yy/MM/dd"),
              IsWorkshopLoaded = false
            }));
        }
        string workshopFolderPath = Paths.PluginPath + "/../../../../workshop/content/1206560";
        if (Directory.Exists(workshopFolderPath)) {
          foreach (string mod in Directory.GetDirectories(workshopFolderPath).Where(Directory.Exists)) {
            FileInfo file = new DirectoryInfo(mod).GetFiles().FirstOrDefault(f => f.Name == "mod.json");
            if (file != null) {
              nmlModsList.Add(new NmlModInfo {
                Name = JObject.Parse(File.ReadAllText(file.FullName))["name"]?.ToString() ?? "Unknown",
                Json = File.ReadAllText(file.FullName),
                DateAdded = File.GetCreationTimeUtc(file.FullName).ToString("yy/MM/dd"),
                DateFoundByMod = File.GetLastAccessTimeUtc(file.FullName).ToString("yy/MM/dd"),
                DateRemoved = null,
                DateLastChanged = File.GetLastWriteTimeUtc(file.FullName).ToString("yy/MM/dd"),
                IsWorkshopLoaded = true
              });
            } else {
              bepinexModsList.Add(new BepinexModInfo {
                Name = new DirectoryInfo(mod).Name,
                DateAdded = Directory.GetCreationTimeUtc(mod).ToString("yy/MM/dd"),
                DateFoundByMod = Directory.GetLastAccessTimeUtc(mod).ToString("yy/MM/dd"),
                DateRemoved = null,
                DateLastChanged = Directory.GetLastWriteTimeUtc(mod).ToString("yy/MM/dd"),
                IsNmlCompliantFormat = true,
                IsWorkshopLoaded = true
              });
            }
          }
        }
      }

      string bepInExModsFolderPath = Paths.PluginPath;
      if (Directory.Exists(bepInExModsFolderPath)) {
        bepinexModsList.AddRange(Directory.GetFiles(bepInExModsFolderPath).
          Where(mod => Path.GetFileName(mod).Contains(".dll")).
          Select(mod => new BepinexModInfo {
            Name = Path.GetFileName(mod).Split('.').Where((_, index) => index != Path.GetFileName(mod).Split('.').Length - 1 || index == 0).Append("").Aggregate((current, next) => current + (next != "" ? "." : "") + next),
            DateAdded = File.GetCreationTimeUtc(mod).ToString("yy/MM/dd"),
            DateFoundByMod = File.GetLastAccessTimeUtc(mod).ToString("yy/MM/dd"),
            DateRemoved = null,
            DateLastChanged = File.GetLastWriteTimeUtc(mod).ToString("yy/MM/dd"),
            IsNmlCompliantFormat = false,
            IsWorkshopLoaded = false
          })
        );

        void CheckForBepinexModules(string directory) {
          foreach (string dir in Directory.GetDirectories(directory)) {
            CheckForBepinexModules(dir);
          }
          if (Directory.GetFiles(directory).Count(file => Path.GetFileName(file).Contains(".dll")) == 1) {
            string mod = Directory.GetFiles(directory).First(file => Path.GetFileName(file).Contains(".dll"));
            bepinexModsList.Add(new BepinexModInfo {
              Name = new DirectoryInfo(directory).Name,
              DateAdded = File.GetCreationTimeUtc(mod).ToString("yy/MM/dd"),
              DateFoundByMod = DateTime.UtcNow.ToString("yy/MM/dd"),
              DateRemoved = null,
              DateLastChanged = File.GetLastWriteTimeUtc(mod).ToString("yy/MM/dd"),
              IsNmlCompliantFormat = true,
              IsWorkshopLoaded = false
            });
          }
        }
        foreach (string dir in Directory.GetDirectories(bepInExModsFolderPath)) {
          CheckForBepinexModules(dir);
        }
      }

      string nativeModsFolderPath = Application.streamingAssetsPath + "/Mods";
      if (Directory.Exists(nativeModsFolderPath)) {
        nativeModsList.AddRange(Directory.GetFiles(nativeModsFolderPath).
          Where(mod => Path.GetFileName(mod).Contains(".dll")).
          Select(mod => new NativeModInfo {
            Name = Path.GetFileName(mod).Split('.').Where((_, index) => index != Path.GetFileName(mod).Split('.').Length - 1 || index == 0).Append("").Aggregate((current, next) => current + (next != "" ? "." : "") + next).Replace("_memload", ""),
            DateAdded = File.GetCreationTimeUtc(mod).ToString("yy/MM/dd"),
            DateFoundByMod = File.GetLastAccessTimeUtc(mod).ToString("yy/MM/dd"),
            DateRemoved = null,
            DateLastChanged = File.GetLastWriteTimeUtc(mod).ToString("yy/MM/dd"),
            IsMemoryLoaded = mod.Contains("_memload")
          })
        );
      }

      return (nativeModsList.ToArray(), bepinexModsList.ToArray(), nmlModsList.ToArray());
    }

    internal static void RemoveMod(string modToRemove) {
      if (KeyGuiModConfig.Get(Internal.WhitelistedMods).Contains(modToRemove)) {
        Debug.Log($"Removal of mod {modToRemove} skipped as it is whitelisted by the user.");
        return;
      }
      TryToRemoveModFromPluginsFolder(modToRemove);
      TryToRemoveModFromModsFolder(modToRemove);
    }
  }
}
