using KeyGUI.Menus.Localizations.Declarations.Buildings.BuildingPlacementBuildingSelector;

namespace KeyGUI.Menus.Localizations.Declarations.Buildings {
  public class KeyGuiBuildings : LocalesContainer {
    public readonly KeyGuiBuildingsBuildingPlacementBuildingSelector BuildingPlacementBuildingSelector = new KeyGuiBuildingsBuildingPlacementBuildingSelector();
    public readonly LocaleDeclaration BuildingPlacementBuildingSelectorSubmenuName = "Building Placement Building Selector";
    public readonly LocaleDeclaration BuildingPlacementPlaceSelectedBuildingButton = "Place Selected Building";
    public readonly LocaleDeclaration BuildingPlacementNoBuildingSelectedError = "No building selected to place!";
    internal KeyGuiBuildings() { }
  }
}
