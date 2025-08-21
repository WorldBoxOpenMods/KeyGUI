using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

namespace KeyGUI.Utils {
  public static class CustomActorTraitsManager {
    public static void AddTraitToLocalizedLibrary(string id, string description) {
      LocalizedTextManager.instance._localized_text.Remove($"trait_{id}");
      LocalizedTextManager.instance._localized_text.Remove($"trait_{id}_info");
      LocalizedTextManager.instance._localized_text.Add($"trait_{id}", id);
      LocalizedTextManager.instance._localized_text.Add($"trait_{id}_info", description);
    }

    public static void SaveTraitsLocally(List<CustomActorTrait> traits) {
      if (!Directory.Exists(Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/Traits"))) {
        Directory.CreateDirectory(Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/Traits"));
      }

      string path = Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/Traits/traits.txt");
      File.Create(path).Dispose();
      using (StreamWriter writer = new StreamWriter(path)) {
        foreach (CustomActorTrait trait in traits) {
          writer.WriteLine(trait + "|||");
        }
      }
    }

    public static List<CustomActorTrait> LoadTraits() {
      List<CustomActorTrait> traits = new List<CustomActorTrait>();
      string path = Path.GetFullPath($"{Application.dataPath}/KeyGuiAssets/Traits/");
      if (Directory.Exists(path)) {
        foreach (string filePath in Directory.GetFiles(path)) {
          Debug.Log(filePath);
          string traitData = "";
          using (StreamReader reader = new StreamReader(filePath)) {
            string line;
            while ((line = reader.ReadLine()) != null) {
              traitData += line;
            }
          }

          string[] traitDataArray = traitData.Split(new[] {"|||"}, StringSplitOptions.RemoveEmptyEntries);
          foreach (string traitDataString in traitDataArray) {
            try {
              CustomActorTrait trait = new CustomActorTrait();
              traits.Add(trait);
              trait.LoadTrait(traitDataString);
            } catch (Exception e) {
              Debug.LogError($"Error loading trait: {traitDataString}, error: {e}");
            }
          }
        }
      }

      return traits;
    }

    public static Dictionary<string, float> ConvertTraitStats(Dictionary<string, string> statDictionary) {
      Dictionary<string, float> stats = new Dictionary<string, float>();
      for (int i = 0; i < statDictionary.Count; ++i) {
        string statName = statDictionary.Keys.ElementAt(i);
        string statValue = statDictionary.Values.ElementAt(i);
        if (statValue.Contains('%')) {
          statValue = statValue.Replace("%", "");
          statValue = statValue.Replace(",", ".");
          try {
            stats.Add(statName, float.Parse(statValue, CultureInfo.InvariantCulture) / 100);
          } catch (Exception) {
            Debug.Log($"Error parsing stat: {statName} with value: {statValue}, trying again differently.");
            stats.Add(statName, float.Parse(statValue.Replace(".", ",")) / 100);
          }
        } else {
          statValue = statValue.Replace(",", ".");
          try {
            stats.Add(statName, float.Parse(statValue, CultureInfo.InvariantCulture));
          } catch (Exception) {
            Debug.Log($"Error parsing stat: {statName} with value: {statValue}, trying again differently.");
            stats.Add(statName, float.Parse(statValue.Replace(".", ",")));
          }
        }
      }

      return stats;
    }
  }
}
