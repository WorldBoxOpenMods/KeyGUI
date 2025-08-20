using KeyGUI.Framework.Menus;
using KeyGUI.Powers;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Zones {
  public class KeyGuiZones : KeyGuiMenu {
    internal override void RegisterPowers() {
      KeyGui.Instance.RegisterPower<AddZoneToCity>();
      KeyGui.Instance.RegisterPower<RemoveZoneFromCity>();
    }
    
    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Zones.AddZoneToCityButton))) {
        if (!KeyGui.Instance.TryGetPower<AddZoneToCity>(out (GodPower, PowerButton) powerAndButton)) {
          Debug.LogError("Something went wrong with the Add Zone To City! Please report this to the mod author!");
        } else {
          (GodPower power, PowerButton button) = powerAndButton;
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        }
      }
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Zones.RemoveZoneFromCityButton))) {
        if (!KeyGui.Instance.TryGetPower<RemoveZoneFromCity>(out (GodPower, PowerButton) powerAndButton)) {
          Debug.LogError("Something went wrong with the Remove Zone From City! Please report this to the mod author!");
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
