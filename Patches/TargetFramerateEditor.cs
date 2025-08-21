using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using KeyGUI.Framework.Patches;
using UnityEngine;

namespace KeyGUI.Patches {
  public class TargetFramerateEditor : KeyGuiPatch {
    public override Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> Patches => new Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> {
      {AccessTools.PropertySetter(typeof(Application), nameof(Application.targetFrameRate)), AsHarmonyMethods(prefix: AccessTools.Method(typeof(TargetFramerateEditor), nameof(SetTargetFramerateToOwnValue)))}
    };

    internal static int TargetFramerate { get; set; } = 60;
    private static void SetTargetFramerateToOwnValue(ref int value) {
      value = TargetFramerate;
    }
  }
}
