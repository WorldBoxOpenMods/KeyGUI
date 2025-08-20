using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using KeyGUI.Powers;
using UnityEngine;

namespace KeyGUI.Menus.Zones {
  public class KeyGuiZones : KeyGuiMenu {
    public readonly KeyGuiLocale AddZoneToCityButton = "Add Zone To City";
    public readonly KeyGuiLocale RemoveZoneFromCityButton = "Remove Zone From City";
    
    internal override void RegisterPowers() {
      KeyGui.Instance.RegisterPower<AddZoneToCity>();
      KeyGui.Instance.RegisterPower<RemoveZoneFromCity>();
    }
    
    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(AddZoneToCityButton)) {
        if (!KeyGui.Instance.TryGetPower<AddZoneToCity>(out (GodPower, PowerButton) powerAndButton)) {
          Debug.LogError("Something went wrong with the Add Zone To City! Please report this to the mod author!");
        } else {
          (GodPower power, PowerButton button) = powerAndButton;
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        }
      }
      if (GUILayout.Button(RemoveZoneFromCityButton)) {
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
