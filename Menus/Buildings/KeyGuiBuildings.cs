using KeyGUI.Powers;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Buildings.BuildingPlacementBuildingSelector;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Buildings {
  public class KeyGuiBuildings : KeyGuiMenuManager {
    internal override void AddSubMenus() {
      Load<KeyGuiBuildingsBuildingPlacementBuildingSelector>(Locales.KeyGui.Buildings.BuildingPlacementBuildingSelectorSubmenuName);
    }
    internal override void RegisterPowers() {
      KeyGui.Instance.RegisterPower<PlaceBuilding>();
    }

    protected override void LoadGUI(int windowID) {
      LoadSubMenuToggles();
      if (KeyGuiBuildingsBuildingPlacementBuildingSelector.SelectedBuilding != null) {
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Buildings.BuildingPlacementPlaceSelectedBuildingButton))) {
          if (!KeyGui.Instance.TryGetPower<PlaceBuilding>(out (GodPower, PowerButton) powerAndButton)) {
            Debug.LogError("Something went wrong with the Place Building! Please report this to the mod author!");
          } else {
            (GodPower power, PowerButton button) = powerAndButton;
            power.select_button_action(power.id);
            power.drop_id = KeyGuiBuildingsBuildingPlacementBuildingSelector.SelectedBuilding.id;
            PowerButtonSelector.instance.unselectAll();
            PowerButtonSelector.instance.setPower(button);
          }
        }
      } else {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Buildings.BuildingPlacementNoBuildingSelectedError));
      }
    }
  }
}
