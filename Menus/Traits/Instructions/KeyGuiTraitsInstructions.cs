using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using TraitsC = KeyGUI.Menus.ModConfig.ConfigOptions.Traits;
using KeyGUI.Menus.ModConfig;
using UnityEngine;

namespace KeyGUI.Menus.Traits.Instructions {
  public class KeyGuiTraitsInstructions : KeyGuiMenu {
    public readonly KeyGuiLocale NecessaryTraitValues = "Necessary Trait Values:\nTrait Name\nTrait Description\nTrait Group";
    public readonly KeyGuiLocale Tips = "Tips:\nIt's recommended to keep trait sprites 28x28 or smaller. Any size works, but larger ones can experience issues.\nKeyGUI includes a feature called \"Partner Traits\". Any traits that you select as partner traits for your trait will automatically be added to any unit that has your trait.\nThis is useful for combining special trait effects like immortality with your custom trait.";
    public readonly KeyGuiLocale CloseMenuButton = "Close";
    
    protected override float MenuMaxWidth => 300;

    public KeyGuiTraitsInstructions() {
      OfferVisibilityToggle = false;
      Enabled = KeyGuiModConfig.Get(TraitsC.ShowTraitsTutorial);
    }
    protected override void LoadGUI(int windowID) {
      GUILayout.Label(NecessaryTraitValues);
      GUILayout.Label("");
      GUILayout.Label(Tips);
      if (GUILayout.Button(CloseMenuButton)) {
        Enabled = false;
        KeyGuiModConfig.Set(TraitsC.ShowTraitsTutorial, Enabled);
      }
    }
  }
}
