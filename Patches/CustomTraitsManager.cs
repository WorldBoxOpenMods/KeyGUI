using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using KeyGeneralPurposeLibrary.Assets;
using KeyGeneralPurposeLibrary.Classes;
using KeyGUI.MenuArchitecture;
using UnityEngine;

namespace KeyGUI.Patches {
  public class CustomTraitsManager : KeyGuiPatch {
    public override Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> Patches => new Dictionary<MethodInfo, (HarmonyMethod Prefix, HarmonyMethod Postfix, HarmonyMethod Transpiler, HarmonyMethod Finalizer, HarmonyMethod IlManipulator)> {
      {AccessTools.Method(typeof(ActorTrait), nameof(ActorTrait.getSprite)), AsHarmonyMethods(prefix: AccessTools.Method(typeof(CustomTraitsManager), nameof(GetCustomSpriteForCustomTrait)))},
      {AccessTools.Method(typeof(Actor), nameof(Actor.addTrait), new[] {typeof(ActorTrait), typeof(bool)}), AsHarmonyMethods(postfix: AccessTools.Method(typeof(CustomTraitsManager), nameof(AddPartnerTraitsOfTrait)))},
      {AccessTools.Method(typeof(Actor), nameof(Actor.removeTrait), new[] {typeof(ActorTrait)}), AsHarmonyMethods(postfix: AccessTools.Method(typeof(CustomTraitsManager), nameof(RemovePartnerTraitsOfTrait)))},
      {AccessTools.Method(typeof(Actor), nameof(Actor.removeTraits)), AsHarmonyMethods(postfix: AccessTools.Method(typeof(CustomTraitsManager), nameof(RemovePartnerTraitsOfTraits)))},
      {AccessTools.Method(typeof(Actor), nameof(Actor.die)), AsHarmonyMethods(prefix: AccessTools.Method(typeof(CustomTraitsManager), nameof(RemoveActorFromPartnerTraitsCache)))}
  };
    
    private static void GetCustomSpriteForCustomTrait(ActorTrait __instance) {
      if (__instance is CustomTrait customTrait) {
        if (customTrait.cached_sprite == null) {
          Sprite sprite = KeyGenLibFileAssetManager.CreateSprite(customTrait.Author, customTrait.Sprite);
          customTrait.cached_sprite = sprite;
        }
      }
    }
    
    private static void AddPartnerTraitsOfTrait(Actor __instance, ActorTrait pTrait) {
      if (pTrait is CustomTrait customTrait) {
        foreach (ActorTrait partnerTrait in from partnerTraitId in customTrait.PartnerTraits let partnerTrait = AssetManager.traits.get(partnerTraitId) where partnerTrait != null where !__instance.hasTrait(partnerTraitId) select partnerTrait) {
          __instance.removeOppositeTraits(partnerTrait);
          __instance.traits.Add(partnerTrait);
          __instance.setStatsDirty();
          if (!customTrait.PartnerTraitCache.ContainsKey(__instance.data)) {
            customTrait.PartnerTraitCache.Add(__instance.data, new List<string>());
          }
          customTrait.PartnerTraitCache[__instance.data].Add(partnerTrait.id);
        }
      }
    }

    private static void RemovePartnerTraitsOfTrait(Actor __instance, ActorTrait pTrait) {
      if (pTrait is CustomTrait customTrait) {
        if (customTrait.PartnerTraitCache.ContainsKey(__instance.data)) {
          foreach (string partnerTraitId in customTrait.PartnerTraitCache[__instance.data]) {
            __instance.removeTrait(partnerTraitId);
          }
          customTrait.PartnerTraitCache.Remove(__instance.data);
        }
      }
    }

    private static void RemovePartnerTraitsOfTraits(Actor __instance, ICollection<ActorTrait> pTraits) {
      foreach (ActorTrait trait in pTraits) {
        RemovePartnerTraitsOfTrait(__instance, trait);
      }
    }

    private static void RemoveActorFromPartnerTraitsCache(Actor __instance) {
      foreach (CustomTrait trait in __instance.traits.Where(trait => trait.GetType() == typeof(CustomTrait)).Cast<CustomTrait>().Where(trait => trait.PartnerTraitCache.ContainsKey(__instance.data))) {
        trait.PartnerTraitCache.Remove(__instance.data);
      }
    }
  }
}
