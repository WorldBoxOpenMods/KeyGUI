using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using KeyGUI.Powers;
using KeyGUI.Menus.Buildings.BuildingPlacementBuildingSelector;
using UnityEngine;

namespace KeyGUI.Menus.Buildings {
  public class KeyGuiBuildings : KeyGuiMenuManager {
    public readonly KeyGuiLocale BuildingPlacementBuildingSelectorSubmenuName = "Building Placement Building Selector";
    public readonly KeyGuiLocale BuildingPlacementPlaceSelectedBuildingButton = "Place Selected Building";
    public readonly KeyGuiLocale BuildingPlacementNoBuildingSelectedError = "No building selected to place!";
    
    internal override void AddSubMenus() {
      Load<KeyGuiBuildingsBuildingPlacementBuildingSelector>(BuildingPlacementBuildingSelectorSubmenuName);
    }
    internal override void RegisterPowers() {
      KeyGui.Instance.RegisterPower<PlaceBuilding>();
    }

    protected override void LoadGUI(int windowID) {
      LoadSubMenuToggles();
      if (KeyGuiBuildingsBuildingPlacementBuildingSelector.SelectedBuilding != null) {
        if (GUILayout.Button(BuildingPlacementPlaceSelectedBuildingButton)) {
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
        GUILayout.Label(BuildingPlacementNoBuildingSelectedError);
      }
    }
  }
}
