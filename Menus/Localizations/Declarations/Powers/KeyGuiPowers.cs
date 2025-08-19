using KeyGUI.Menus.Localizations.Declarations.Powers.AllianceCreation;
using KeyGUI.Menus.Localizations.Declarations.Powers.ChangeCityKingdom;
using KeyGUI.Menus.Localizations.Declarations.Powers.CityZoneAddition;
using KeyGUI.Menus.Localizations.Declarations.Powers.CityZoneRemoval;
using KeyGUI.Menus.Localizations.Declarations.Powers.CultureCreation;
using KeyGUI.Menus.Localizations.Declarations.Powers.CultureDeletion;
using KeyGUI.Menus.Localizations.Declarations.Powers.CultureForceConversion;
using KeyGUI.Menus.Localizations.Declarations.Powers.CultureFullReset;
using KeyGUI.Menus.Localizations.Declarations.Powers.ForceCapital;
using KeyGUI.Menus.Localizations.Declarations.Powers.PlaceBuilding;
using KeyGUI.Menus.Localizations.Declarations.Powers.SetKing;

namespace KeyGUI.Menus.Localizations.Declarations.Powers {
  public class KeyGuiPowers : LocalesContainer {
    public readonly KeyGuiPowersAllianceCreation AllianceCreation = new KeyGuiPowersAllianceCreation();
    public readonly KeyGuiPowersChangeCityKingdom ChangeCityKingdom = new KeyGuiPowersChangeCityKingdom();
    public readonly KeyGuiPowersCityZoneAddition CityZoneAddition = new KeyGuiPowersCityZoneAddition();
    public readonly KeyGuiPowersCityZoneRemoval CityZoneRemoval = new KeyGuiPowersCityZoneRemoval();
    public readonly KeyGuiPowersCultureCreation CultureCreation = new KeyGuiPowersCultureCreation();
    public readonly KeyGuiPowersCultureDeletion CultureDeletion = new KeyGuiPowersCultureDeletion();
    public readonly KeyGuiPowersCultureForceConversion CultureForceConversion = new KeyGuiPowersCultureForceConversion();
    public readonly KeyGuiPowersCultureFullReset CultureFullReset = new KeyGuiPowersCultureFullReset();
    public readonly KeyGuiPowersForceCapital ForceCapital = new KeyGuiPowersForceCapital();
    public readonly KeyGuiPowersPlaceBuilding PlaceBuilding = new KeyGuiPowersPlaceBuilding();
    public readonly KeyGuiPowersSetKing SetKing = new KeyGuiPowersSetKing();
  }
}
