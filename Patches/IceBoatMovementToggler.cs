using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using KeyGUI.Framework.Patches;

namespace KeyGUI.Patches {
  public class IceBoatMovementToggler : KeyGuiPatch {
    public override Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> Patches => new Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> {
      {AccessTools.Method(typeof(WorldTile), nameof(WorldTile.isGoodForBoat)), AsHarmonyMethods(postfix: AccessTools.Method(typeof(IceBoatMovementToggler), nameof(MarkIceAsNotGoodForBoatIfNeeded)))}
    };

    public static bool DisableBoatMovementOnIce { get; set; }
    private static void MarkIceAsNotGoodForBoatIfNeeded(ref bool __result, WorldTile __instance) {
      if (DisableBoatMovementOnIce) {
        if (__result) {
          if (__instance.isFrozen()) {
            __result = false;
          }
        }
      }
    }
  }
}
