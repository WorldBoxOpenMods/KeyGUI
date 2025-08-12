using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using KeyGeneralPurposeLibrary.Assets;
using KeyGeneralPurposeLibrary.Classes;
using KeyGUI.MenuArchitecture;
using UnityEngine;

namespace KeyGUI.Patches {
  public class CustomTraitsSpriteOverwrite : KeyGuiPatch {
    public override Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> Patches => new Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> {
      {AccessTools.Method(typeof(ActorTrait), nameof(ActorTrait.getSprite)), AsHarmonyMethods(prefix: AccessTools.Method(typeof(CustomTraitsSpriteOverwrite), nameof(GetCustomSpriteForCustomTrait)))}
    };
    
    private static void GetCustomSpriteForCustomTrait(ActorTrait __instance) {
      if (__instance is CustomTrait customTrait) {
        if (customTrait.cached_sprite == null) {
          Sprite sprite = KeyGenLibFileAssetManager.CreateSprite(customTrait.Author, customTrait.Sprite);
          customTrait.cached_sprite = sprite;
        }
      }
    }
  }
}
