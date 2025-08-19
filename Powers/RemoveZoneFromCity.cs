using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;

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
      WorldTip.showNow(Locales.KeyGui.Powers.CityZoneRemoval.KGPLL_CityZoneRemoval_SelectCity, false, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      if (_cityToRemoveZoneFrom == null) {
        _cityToRemoveZoneFrom = pTile.zone.city;
        if (_cityToRemoveZoneFrom != null) {
          WorldTip.showNow(Locales.KeyGui.Powers.CityZoneRemoval.KGPLL_CityZoneRemoval_SelectZones, false, "top");
          return false;
        }
        WorldTip.showNow(Locales.KeyGui.Powers.CityZoneRemoval.KGPLL_CityZoneRemoval_NoCitySelectedError, false, "top");
        return false;
      }
      if (pTile.zone.city == _cityToRemoveZoneFrom) {
        _cityToRemoveZoneFrom.removeZone(pTile.zone);
        return true;
      }
      WorldTip.showNow(Locales.KeyGui.Powers.CityZoneRemoval.KGPLL_CityZoneRemoval_ZoneOwnershipConflict, false, "top");
      return false;
    }
  }
}
