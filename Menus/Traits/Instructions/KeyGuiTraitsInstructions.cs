using TraitsC = KeyGUI.Menus.ModConfig.ConfigOptions.Traits;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using KeyGUI.Menus.ModConfig;
using UnityEngine;
namespace KeyGUI.Menus.Traits.Instructions {
  public class KeyGuiTraitsInstructions : KeyGuiMenu {
    protected override float MenuMaxWidth => 300;
    
    public KeyGuiTraitsInstructions() {
      OfferVisibilityToggle = false;
      Enabled = KeyGuiModConfig.Get(TraitsC.ShowTraitsTutorial);
    }
    protected override void LoadGUI(int windowID) {
      GUILayout.Label(Locales.Get(Locales.KeyGui.Traits.Instructions.NecessaryTraitValues));
      GUILayout.Label("");
      GUILayout.Label(Locales.Get(Locales.KeyGui.Traits.Instructions.Tips));
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Traits.Instructions.CloseMenuButton))) {
        Enabled = false;
        KeyGuiModConfig.Set(TraitsC.ShowTraitsTutorial, Enabled);
      }
    }
  }
}