using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using KeyGUI.MenuArchitecture;

namespace KeyGUI.Patches {
  public class MouseDragToggler : KeyGuiPatch {
    public override Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> Patches => new Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> {
      {AccessTools.Method(typeof(MoveCamera), nameof(MoveCamera.updateMouseCameraDrag)), AsHarmonyMethods(prefix: AccessTools.Method(typeof(MouseDragToggler), nameof(CheckIfMouseDragAllowed)))}
    };
    
    internal static bool AllowMouseDrag { get; set; } = true;
    private static bool CheckIfMouseDragAllowed() {
      return AllowMouseDrag;
    }
  }
}
