using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using KeyGUI.MenuArchitecture;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KeyGUI.Patches {
  public class ClanBirthTraitsManagement : KeyGuiPatch {
    public override Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> Patches => new Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> {
      {AccessTools.Method(typeof(Actor), nameof(Actor.setClan)), AsHarmonyMethods(prefix: AccessTools.Method(typeof(ClanBirthTraitsManagement), nameof(AssignClanBirthTraits)))}
    };

    private static void AssignClanBirthTraits(Actor __instance, Clan pObject) {
      if (pObject.units.Count >= pObject.getMaxMembers()) return;
      if (pObject.data.custom_data_string != null) {
        bool clanTraitsSet = pObject.data.custom_data_string.TryGetValue("ClanTraits", out string clanTraitsJson);
        if (clanTraitsSet) {
          JToken[] clanTraitsArray = JsonConvert.DeserializeObject<JArray>(clanTraitsJson).ToArray();
          clanTraitsArray.Shuffle();
          foreach (string traitId in clanTraitsArray.Select(token => token.Value<string>()).ToArray()) {
            __instance.addTrait(traitId);
          }
        }
      }
    }
  }
}
