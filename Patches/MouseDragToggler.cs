using System.Reflection;
using HarmonyLib;
using KeyGUI.MenuArchitecture;

namespace KeyGUI.Patches {
  public class MouseDragToggler : KeyGuiPatch {
    public override MethodInfo TargetMethod => AccessTools.Method(typeof(MoveCamera), nameof(MoveCamera.updateMouseCameraDrag));
    public override MethodInfo Prefix => AccessTools.Method(typeof(MouseDragToggler), nameof(CheckIfMouseDragAllowed));

    internal static bool AllowMouseDrag { get; set; } = true;
    private static bool CheckIfMouseDragAllowed() {
      return AllowMouseDrag;
    }
  }
}
