using System.Reflection;
using HarmonyLib;
using KeyGUI.MenuArchitecture;

namespace KeyGUI.Patches {
  public class IceBoatMovementToggler : KeyGuiPatch {
    public override MethodInfo TargetMethod => AccessTools.Method(typeof(WorldTile), nameof(WorldTile.isGoodForBoat));
    public override MethodInfo Postfix => AccessTools.Method(typeof(IceBoatMovementToggler), nameof(MarkIceAsNotGoodForBoatIfNeeded));

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
