using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Powers;

namespace KeyGUI.Powers {
  public class RemoveZoneFromCity : KeyGuiPower {
    public readonly KeyGuiLocale SelectCity = "Select the city to remove zones from.";
    public readonly KeyGuiLocale SelectZones = "Select zones to remove.";
    public readonly KeyGuiLocale NoCitySelectedError = "No city to remove zone from here, try again!";
    public readonly KeyGuiLocale ZoneOwnershipConflict = "The zone you're trying to remove doesn't belong to the city you selected, please select the correct city!";
    
    public RemoveZoneFromCity() : base(new GodPower() {
      id = "remove_zone_from_city_keygui",
      name = "Remove Zone From City",
      force_map_mode = MetaType.City
    }) {
    }
    private static City _cityToRemoveZoneFrom;

    protected override bool PowerButtonPress(string pPower) {
      _cityToRemoveZoneFrom = null;
      WorldTip.showNow(SelectCity, false, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      if (_cityToRemoveZoneFrom == null) {
        _cityToRemoveZoneFrom = pTile.zone.city;
        if (_cityToRemoveZoneFrom != null) {
          WorldTip.showNow(SelectZones, false, "top");
          return false;
        }
        WorldTip.showNow(NoCitySelectedError, false, "top");
        return false;
      }
      if (pTile.zone.city == _cityToRemoveZoneFrom) {
        _cityToRemoveZoneFrom.removeZone(pTile.zone);
        return true;
      }
      WorldTip.showNow(ZoneOwnershipConflict, false, "top");
      return false;
    }
  }
}
