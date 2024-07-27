using System.Linq;
using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.Classes;
using KeyGeneralPurposeLibrary.Powers;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Actors.ItemDropper;
using KeyGUI.Menus.Actors.TraitRemovalSelection;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Actors {
  public class KeyGuiActors : KeyGuiMenuManager {
    private CustomDropAsset _traitMassRemovalRain;
    internal override void Initialize() {
      base.Initialize();
      _traitMassRemovalRain = new CustomDropAsset() {
        id = "trait_mass_removal_rain_keygui",
        path_texture = "drops/drop_gamma_rain",
        random_frame = true,
        default_scale = 0.1f,
        action_landed = RemoveTraitsAt,
        material = "mat_world_object_lit",
        sound_drop = "event:/SFX/DROPS/DropRainGamma"
      };
      AssetManager.drops.add(_traitMassRemovalRain);
    }
    internal override void AddSubMenus() {
      Load<KeyGuiActorsTraitRemovalSelection>(Locales.KeyGui.Actors.TraitRemovalSelectionSubmenuName);
      Load<KeyGuiActorsItemDropper>(Locales.KeyGui.Actors.ItemDropperSubmenuName);
    }

    private void RemoveTraitsAt(WorldTile pTile = null, string pDropId = null) {
      foreach (string traitId in _traitMassRemovalRain.CustomStringStorage.ToArray().Where(traitId => pTile != null)) {
        World.world.getObjectsInChunks(pTile, pObjectType: MapObjectType.Actor);
        foreach (Actor a in World.world.temp_map_objects.Select(o => o.a).Where(a => a != null && a.asset.can_edit_traits)) {
          a.removeTrait(traitId);
          a.startShake();
          a.startColorEffect();
        }
      }
    }
    protected override void PostMenuToggleUi() {
      base.PostMenuToggleUi();
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Actors.TraitRemovalRainDropButton))) {
        PowerButton button = KeyLib.Get<KeyGenLibGodPowerButtonLibrary>()[KeyGenLibGodPowerButtonLibrary.MassTraitRemovalRainButtonIndex];
        if (button != null) {
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        } else {
          Debug.LogError("Something went wrong with the Mass Trait Removal Rain! Please report this to the mod author!");
        }
      }
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Actors.MakeActorKingButton))) {
        GodPower power = KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.MakeActorKingIndex];
        PowerButton button = KeyLib.Get<KeyGenLibGodPowerButtonLibrary>()[KeyGenLibGodPowerButtonLibrary.MakeActorKingButtonIndex];
        if (button != null) {
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        } else {
          Debug.LogError("Something went wrong with the Make Actor King Power! Please report this to the mod author!");
        }
      }
    }
  }
}