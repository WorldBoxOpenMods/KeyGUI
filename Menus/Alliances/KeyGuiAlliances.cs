using KeyGUI.Powers;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Alliances {
  public class KeyGuiAlliances : KeyGuiMenu {
    internal override void RegisterPowers() {
      KeyGui.Instance.RegisterPower<WhisperOfAlliance>();
    }
    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Alliances.WhisperOfAllianceButton))) {
        if (!KeyGui.Instance.TryGetPower<WhisperOfAlliance>(out (GodPower, PowerButton) powerAndButton)) {
          Debug.LogError("Something went wrong with the Whisper Of Alliance! Please report this to the mod author!");
        } else {
          (GodPower power, PowerButton button) = powerAndButton;
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        }
      }
    }
  }
}
