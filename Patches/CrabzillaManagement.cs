using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using KeyGUI.Framework.Patches;
using strings;
using UnityEngine;

namespace KeyGUI.Patches {
  public class CrabzillaManagement : KeyGuiPatch {
    public override Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> Patches => new Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> {
      {
        AccessTools.Method(typeof(Actor), nameof(Actor.checkSpriteToRender)),
        AsHarmonyMethods(prefix: AccessTools.Method(typeof(CrabzillaManagement), nameof(FixCrabzillaSpriteRenderIssues)))
      },
      {
        AccessTools.Method(typeof(Actor), nameof(Actor.die)),
        AsHarmonyMethods(prefix: AccessTools.Method(typeof(CrabzillaManagement), nameof(CheckIfCrabzillaCanDie)))
      },
      {
        AccessTools.Method(typeof(Crabzilla), nameof(Crabzilla.update)),
        AsHarmonyMethods(postfix: AccessTools.Method(typeof(CrabzillaManagement), nameof(SetCrabzillaAsSpawned)))
      },
      {
        AccessTools.Method(typeof(CrabArm), nameof(CrabArm.damageWorld)),
        AsHarmonyMethods(transpiler: AccessTools.Method(typeof(CrabzillaManagement), nameof(MakeCrabArmRadiusModifiable)))
      }
    };
    
    public static bool CrabzillaIsSpawned { get; set; }
    public static bool IsCrabzillaKillable { get; set; } = true;
    public static int CrabzillaArmExplosionRadius { get; set; } = 4;
    private static bool FixCrabzillaSpriteRenderIssues(ref Sprite __result, Actor __instance) {
      if (__instance.asset.id == SA.crabzilla) {
        __result = SpriteTextureLoader.getSprite("ui/icons/iconcrabzilla");
        return false;
      }

      return true;
    }

    private static bool CheckIfCrabzillaCanDie(Actor __instance) {
      if (IsCrabzillaKillable) {
        return true;
      }
      return __instance.asset.id != SA.crabzilla;
    }
    
    private static void SetCrabzillaAsSpawned() {
      CrabzillaIsSpawned = true;
    }

    private static IEnumerable<CodeInstruction> MakeCrabArmRadiusModifiable(IEnumerable<CodeInstruction> instructions, ILGenerator generator) {
      LocalBuilder builder = generator.DeclareLocal(typeof(int));
      foreach (CodeInstruction instruction in instructions) {
        if (instruction.opcode == OpCodes.Ldc_I4_4 || instruction.opcode == OpCodes.Ldc_I4 && (int)instruction.operand == 4) {
          yield return new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeof(CrabzillaManagement), nameof(CrabzillaArmExplosionRadius)));
          yield return new CodeInstruction(OpCodes.Stloc_S, builder.LocalIndex);
          yield return new CodeInstruction(OpCodes.Ldloc_S, builder.LocalIndex);
        } else {
          yield return instruction;
        }
      }
    }
  }
}
