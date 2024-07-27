using System.Collections.Generic;
using System.Linq;
using KeyGUI.Menus.ModConfig;
using KeyGUI.Menus.ModConfig.ConfigOptions;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KeyGUI.Utils {
  public static class KeyGuiNetworkingResponseParsingHelper {

    internal static (string, string)[] ParseModsResponse(JArray criticalMods) {
      List<(string name, string reason)> criticalModsList = new List<(string, string)>();
      foreach (JToken criticalModToken in criticalMods) {
        if (criticalModToken is JObject criticalMod) {
          string mName = null;
          string reason = null;
          if (criticalMod.GetValue("mod") is JValue modName) {
            if (modName.Value is string modNameString) {
              mName = modNameString;
            }
          }

          if (criticalMod.GetValue("reason") is JValue modReason) {
            if (modReason.Value is string modReasonString) {
              reason = modReasonString;
            }
          }

          if (mName != null && reason != null) {
            criticalModsList.Add((mName, reason));
          }
        }
      }

      return criticalModsList.ToArray();
    }
    internal static void RemoveWhitelistedCriticalModsFromListOfModsToDelete((string, string)[] criticalMods) {
      string[] criticalModsToIgnore = KeyGuiModConfig.Get(Internal.WhitelistedMods);

      foreach (string mod in criticalModsToIgnore) {
        for (int i = 0; i < criticalMods.Length; i++) {
          char[] charsFromConfigMod = mod.ToLowerInvariant().ToCharArray();
          char[] charsFromSentMod = criticalMods[i].Item1.ToLowerInvariant().ToCharArray();
          if (charsFromConfigMod.Length != charsFromSentMod.Length) {
            continue;
          }

          bool match = !charsFromConfigMod.Where((t, j) => t != charsFromSentMod[j]).Any();
          if (!match) {
            continue;
          }

          Debug.Log("Removing the mod " + criticalMods[i].Item1 + " from the list of critical mods!");
          criticalMods = criticalMods.Where((_, idx) => idx != i).ToArray();
        }
      }
    }
  }
}