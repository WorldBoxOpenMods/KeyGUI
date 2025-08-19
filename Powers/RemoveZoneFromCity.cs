using KeyGUI.MenuArchitecture;

namespace KeyGUI.Powers {
  public class RemoveZoneFromCity : KeyGuiPower {
    public RemoveZoneFromCity() : base(new GodPower() {
      id = "remove_zone_from_city_keygui",
      name = "Remove Zone From City",
      force_map_mode = MetaType.City
    }) {
    }
    private static City _cityToRemoveZoneFrom;

    protected override bool PowerButtonPress(string pPower) {
      _cityToRemoveZoneFrom = null;
      WorldTip.showNow("KGPLL_CityZoneRemoval_SelectCity", true, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      if (_cityToRemoveZoneFrom == null) {
        _cityToRemoveZoneFrom = pTile.zone.city;
        if (_cityToRemoveZoneFrom != null) {
          WorldTip.showNow("KGPLL_CityZoneRemoval_SelectZones", true, "top");
          return false;
        }
        WorldTip.showNow("KGPLL_CityZoneRemoval_NoCitySelectedError", true, "top");
        return false;
      }
      if (pTile.zone.city == _cityToRemoveZoneFrom) {
        _cityToRemoveZoneFrom.removeZone(pTile.zone);
        return true;
      }
      WorldTip.showNow("KGPLL_CityZoneRemoval_ZoneOwnershipConflict", true, "top");
      return false;
    }
  }
}
