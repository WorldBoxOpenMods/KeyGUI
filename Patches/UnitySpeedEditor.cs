using System.Reflection;
using HarmonyLib;
using KeyGUI.MenuArchitecture;
using UnityEngine;

namespace KeyGUI.Patches {
  public class UnitySpeedEditor : KeyGuiPatch {
    public override MethodInfo TargetMethod => AccessTools.PropertyGetter(typeof(Time), nameof(Time.deltaTime));
    public override MethodInfo Prefix => AccessTools.Method(typeof(UnitySpeedEditor), nameof(ForceDeltaTimeGetToOwnValue));
    
    internal static float DeltaTime { get; set; } = 1.0f;
    private static void ForceDeltaTimeGetToOwnValue(ref float __result) {
      __result *= DeltaTime;
    }
  }
}
