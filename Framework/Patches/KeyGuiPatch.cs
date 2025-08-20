using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace KeyGUI.Framework.Patches {
  public abstract class KeyGuiPatch {
    public abstract Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> Patches { get; }
    protected (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator) AsHarmonyMethods(
      MethodInfo prefix = null,
      MethodInfo postfix = null,
      MethodInfo transpiler = null,
      MethodInfo finalizer = null,
      MethodInfo ilManipulator = null
    ) {
      return (
        Prefix: prefix == null ? null : new HarmonyMethod(prefix),
        Postfix: postfix == null ? null : new HarmonyMethod(postfix),
        Transpiler: transpiler == null ? null : new HarmonyMethod(transpiler),
        Finalizer: finalizer == null ? null : new HarmonyMethod(finalizer),
        IlManipulator: ilManipulator == null ? null : new HarmonyMethod(ilManipulator)
      );
    }
  }
}
