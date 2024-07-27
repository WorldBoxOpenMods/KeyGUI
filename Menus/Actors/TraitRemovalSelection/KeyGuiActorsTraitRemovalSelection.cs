using KeyGeneralPurposeLibrary.Classes;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Actors.TraitRemovalSelection {
  public class KeyGuiActorsTraitRemovalSelection : KeyGuiMenu {
    protected override float MenuMaxHeight => 900;
    protected override float MenuMinHeight => 400;
    private Vector2 _scrollPos;
    protected override void LoadGUI(int windowID) {
      _scrollPos = GUILayout.BeginScrollView(_scrollPos);
      if (!(AssetManager.drops.get("trait_mass_removal_rain_keygui") is CustomDropAsset)) {
        GUILayout.Label("Something went wrong with the Mass Trait Removal Rain! Please report this to the mod author!");
        return;
      }
      CustomDropAsset traitMassRemovalRain = (CustomDropAsset) AssetManager.drops.get("trait_mass_removal_rain_keygui");
      foreach (ActorTrait trait in AssetManager.traits.list) {
        if (traitMassRemovalRain.CustomStringStorage.Contains(trait.id)) {
          if (GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Actors.TraitRemovalSelection.DeselectTraitButton), trait.id))) {
            traitMassRemovalRain.CustomStringStorage.Remove(trait.id);
          }
        } else {
          if (GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Actors.TraitRemovalSelection.SelectTraitButton), trait.id))) {
            traitMassRemovalRain.CustomStringStorage.Add(trait.id);
          }
        }
      }
      GUILayout.EndScrollView();
      GUILayout.Label("");
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Actors.TraitRemovalSelection.CloseMenuButton))) {
        Enabled = false;
      }
    }
  }
}