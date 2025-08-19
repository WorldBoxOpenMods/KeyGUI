namespace KeyGUI.Menus.Localizations.Declarations.Powers.CityZoneRemoval {
  public class KeyGuiPowersCityZoneRemoval : LocalesContainer {
    public readonly LocaleDeclaration SelectCity = new LocaleDeclaration("Select the city to remove zones from.");
    public readonly LocaleDeclaration SelectZones = new LocaleDeclaration("Select zones to remove.");
    public readonly LocaleDeclaration NoCitySelectedError = new LocaleDeclaration("No city to remove zone from here, try again!");
    public readonly LocaleDeclaration ZoneOwnershipConflict = new LocaleDeclaration("The zone you're trying to remove doesn't belong to the city you selected, please select the correct city!");
  }
}
