using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.PowersLib;
using KeyGeneralPurposeLibrary.PowersLib.Powers;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Alliances {
  public class KeyGuiAlliances : KeyGuiMenu {
    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Alliances.WhisperOfAllianceButton))) {
        (GodPower power, PowerButton button) = KeyLib.Get<KeyGenLibGodPowerLibrary>().GetPower<WhisperOfAlliance>();
        if (button != null) {
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        } else {
          Debug.LogError("Something went wrong with the Whisper Of Alliance! Please report this to the mod author!");
        }
      }
    }
  }
}
