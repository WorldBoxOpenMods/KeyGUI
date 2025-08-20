namespace KeyGUI.Menus.Localizations.Declarations.Powers.CityZoneAddition {
  public class KeyGuiPowersCityZoneAddition : LocalesContainer {
    public readonly LocaleDeclaration SelectCity = new LocaleDeclaration("Select the city to add zones to.");
    public readonly LocaleDeclaration SelectZones = new LocaleDeclaration("Select zones to add to the city.");
    public readonly LocaleDeclaration NoCitySelectedError = new LocaleDeclaration("No city to add zone to here, try again!");
    public readonly LocaleDeclaration ZoneOwnershipConflict = new LocaleDeclaration("The zone you're trying to add already belongs to another city, please remove it from that city first!");
    internal KeyGuiPowersCityZoneAddition() { }
  }
}
