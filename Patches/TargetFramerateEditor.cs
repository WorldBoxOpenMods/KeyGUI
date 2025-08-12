using System.Reflection;
using HarmonyLib;
using KeyGUI.MenuArchitecture;
using UnityEngine;

namespace KeyGUI.Patches {
  public class TargetFramerateEditor : KeyGuiPatch {
    public override MethodInfo TargetMethod => AccessTools.PropertySetter(typeof(Application), nameof(Application.targetFrameRate));

    public override MethodInfo Prefix => AccessTools.Method(typeof(TargetFramerateEditor), nameof(SetTargetFramerateToOwnValue));

    internal static int TargetFramerate { get; set; } = 60;
    private static void SetTargetFramerateToOwnValue(ref int value) {
      value = TargetFramerate;
    }
  }
}
