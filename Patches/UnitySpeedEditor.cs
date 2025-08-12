using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using KeyGUI.MenuArchitecture;
using UnityEngine;

namespace KeyGUI.Patches {
  public class UnitySpeedEditor : KeyGuiPatch {
    public override Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> Patches => new Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> {
      {AccessTools.PropertyGetter(typeof(Time), nameof(Time.deltaTime)), AsHarmonyMethods(prefix: AccessTools.Method(typeof(UnitySpeedEditor), nameof(ForceDeltaTimeGetToOwnValue)))}
    };

    internal static float DeltaTime { get; set; } = 1.0f;
    private static void ForceDeltaTimeGetToOwnValue(ref float __result) {
      __result *= DeltaTime;
    }
  }
}
