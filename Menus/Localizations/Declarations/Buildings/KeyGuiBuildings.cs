using KeyGUI.Menus.Localizations.Declarations.Buildings.BuildingPlacementBuildingSelector;

namespace KeyGUI.Menus.Localizations.Declarations.Buildings {
  public class KeyGuiBuildings : LocalesContainer {
    public readonly KeyGuiBuildingsBuildingPlacementBuildingSelector BuildingPlacementBuildingSelector = new KeyGuiBuildingsBuildingPlacementBuildingSelector();
    public readonly LocaleDeclaration BuildingPlacementBuildingSelectorSubmenuName = new LocaleDeclaration();
    public readonly LocaleDeclaration BuildingPlacementPlaceSelectedBuildingButton = new LocaleDeclaration();
    public readonly LocaleDeclaration BuildingPlacementNoBuildingSelectedError = new LocaleDeclaration();
    internal KeyGuiBuildings() { }
  }
}
