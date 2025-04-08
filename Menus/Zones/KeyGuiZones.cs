using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.PowersLib;
using KeyGeneralPurposeLibrary.PowersLib.Powers;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Zones {
  public class KeyGuiZones : KeyGuiMenu {
    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Zones.AddZoneToCityButton))) {
        (GodPower power, PowerButton button) = KeyLib.Get<KeyGenLibGodPowerLibrary>().GetPower<AddZoneToCity>();
        if (button != null) {
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        } else {
          Debug.LogError("Something went wrong with the Add Zone To City! Please report this to the mod author!");
        }
      }
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Zones.RemoveZoneFromCityButton))) {
        (GodPower power, PowerButton button) = KeyLib.Get<KeyGenLibGodPowerLibrary>().GetPower<RemoveZoneFromCity>();
        if (button != null) {
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        } else {
          Debug.LogError("Something went wrong with the Remove Zone From City! Please report this to the mod author!");
        }
      }
    }
  }
}
