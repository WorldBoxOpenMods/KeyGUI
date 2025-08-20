using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using KeyGUI.Framework.Patches;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KeyGUI.Patches {
  public class CultureBirthTraitsManagement : KeyGuiPatch {
    public override Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> Patches => new Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> {
      {AccessTools.Method(typeof(Actor), nameof(Actor.setCulture)), AsHarmonyMethods(prefix: AccessTools.Method(typeof(CultureBirthTraitsManagement), nameof(AssignCultureBirthTraits)))}
    };
    
    private static void AssignCultureBirthTraits(Actor __instance, Culture pCulture) {
      if (pCulture.data.custom_data_string != null) {
        bool cultureTraitsSet = pCulture.data.custom_data_string.TryGetValue("CultureTraits", out string cultureTraitsJson);
        if (cultureTraitsSet) {
          JToken[] cultureTraitsArray = JsonConvert.DeserializeObject<JArray>(cultureTraitsJson).ToArray();
          cultureTraitsArray.Shuffle();
          foreach (string traitId in cultureTraitsArray.Select(token => token.Value<string>()).ToArray()) {
            __instance.addTrait(traitId);
          }
        }
      }
    }
  }
}
