using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.PowersLib;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Buildings.BuildingPlacementBuildingSelector;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Buildings {
  public class KeyGuiBuildings : KeyGuiMenuManager {
    internal override void AddSubMenus() {
      Load<KeyGuiBuildingsBuildingPlacementBuildingSelector>(Locales.KeyGui.Buildings.BuildingPlacementBuildingSelectorSubmenuName);
    }

    protected override void LoadGUI(int windowID) {
      LoadSubMenuToggles();
      if (KeyGuiBuildingsBuildingPlacementBuildingSelector.SelectedBuilding != null) {
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Buildings.BuildingPlacementPlaceSelectedBuildingButton))) {
          GodPower power = KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.PlaceBuildingIndex];
          PowerButton button = KeyLib.Get<KeyGenLibGodPowerButtonLibrary>()[KeyGenLibGodPowerButtonLibrary.PlaceBuildingButtonIndex];
          if (button != null) {
            power.select_button_action(power.id);
            power.drop_id = KeyGuiBuildingsBuildingPlacementBuildingSelector.SelectedBuilding.id;
            PowerButtonSelector.instance.unselectAll();
            PowerButtonSelector.instance.setPower(button);
          } else {
            Debug.LogError("Something went wrong with the Place Building! Please report this to the mod author!");
          }
        }
      } else {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Buildings.BuildingPlacementNoBuildingSelectedError));
      }
    }
  }
}
