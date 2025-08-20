using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Powers;

namespace KeyGUI.Powers {
  public class AddZoneToCity : KeyGuiPower {
    public readonly KeyGuiLocale SelectCity = "Select the city to add zones to.";
    public readonly KeyGuiLocale SelectZones = "Select zones to add to the city.";
    public readonly KeyGuiLocale NoCitySelectedError = "No city to add zone to here, try again!";
    public readonly KeyGuiLocale ZoneOwnershipConflict = "The zone you're trying to add already belongs to another city, please remove it from that city first!";
    
    public AddZoneToCity() : base(new GodPower() {
      id = "add_zone_to_city_keygui",
      name = "Add Zone To City",
      force_map_mode = MetaType.City
    }) {
    }
    private static City _cityToAddZoneTo;

    protected override bool PowerButtonPress(string _) {
      _cityToAddZoneTo = null;
      WorldTip.showNow(SelectCity, false, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      if (_cityToAddZoneTo == null) {
        _cityToAddZoneTo = pTile.zone.city;
        if (_cityToAddZoneTo != null) {
          WorldTip.showNow(SelectZones, false, "top");
          return false;
        }
        WorldTip.showNow(NoCitySelectedError, false, "top");
        return false;
      }
      if (pTile.zone.city == null) {
        _cityToAddZoneTo.addZone(pTile.zone);
        return true;
      }
      WorldTip.showNow(ZoneOwnershipConflict, false, "top");
      return false;
    }
  }
}
