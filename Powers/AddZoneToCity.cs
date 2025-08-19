using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;

namespace KeyGUI.Powers {
  public class AddZoneToCity : KeyGuiPower {
    public AddZoneToCity() : base(new GodPower() {
      id = "add_zone_to_city_keygui",
      name = "Add Zone To City",
      force_map_mode = MetaType.City
    }) {
    }
    private static City _cityToAddZoneTo;

    protected override bool PowerButtonPress(string _) {
      _cityToAddZoneTo = null;
      WorldTip.showNow(Locales.KeyGui.Powers.CityZoneAddition.SelectCity, false, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      if (_cityToAddZoneTo == null) {
        _cityToAddZoneTo = pTile.zone.city;
        if (_cityToAddZoneTo != null) {
          WorldTip.showNow(Locales.KeyGui.Powers.CityZoneAddition.SelectZones, false, "top");
          return false;
        }
        WorldTip.showNow(Locales.KeyGui.Powers.CityZoneAddition.NoCitySelectedError, false, "top");
        return false;
      }
      if (pTile.zone.city == null) {
        _cityToAddZoneTo.addZone(pTile.zone);
        return true;
      }
      WorldTip.showNow(Locales.KeyGui.Powers.CityZoneAddition.ZoneOwnershipConflict, false, "top");
      return false;
    }
  }
}
