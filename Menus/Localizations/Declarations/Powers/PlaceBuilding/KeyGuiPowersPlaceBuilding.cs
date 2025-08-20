namespace KeyGUI.Menus.Localizations.Declarations.Powers.PlaceBuilding {
  public class KeyGuiPowersPlaceBuilding : LocalesContainer {
    public readonly LocaleDeclaration SelectCity = new LocaleDeclaration("Select the city to place the building in.");
    public readonly LocaleDeclaration InvalidTileError = new LocaleDeclaration("Can't place building here, try again!");
    public readonly LocaleDeclaration Success = new LocaleDeclaration("Building placed!");
    public readonly LocaleDeclaration NoBuildingSelected = new LocaleDeclaration("No building to place selected, try again!");
    internal KeyGuiPowersPlaceBuilding() { }
  }
}
