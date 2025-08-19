using System.Linq;
using KeyGUI.MenuArchitecture;

namespace KeyGUI.Powers {
  public class ForceCityIntoOtherKingdom : KeyGuiPower {
    public ForceCityIntoOtherKingdom() : base(new GodPower() {
      id = "force_city_into_other_kingdom_keygui",
      name = "Force City Into Other Kingdom",
      force_map_mode = MetaType.City
    }) {
    }
    private static City _cityToForceIntoOtherKingdom;

    protected override bool PowerButtonPress(string pPower) {
      _cityToForceIntoOtherKingdom = null;
      WorldTip.showNow("KGPLL_ChangeCityKingdom_SelectCity", true, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      if (_cityToForceIntoOtherKingdom == null) {
        _cityToForceIntoOtherKingdom = pTile.zone.city;
        if (_cityToForceIntoOtherKingdom != null) {
          WorldTip.showNow("KGPLL_ChangeCityKingdom_SelectKingdom", true, "top");
          return false;
        }
        WorldTip.showNow("KGPLL_ChangeCityKingdom_NoCitySelectedError", true, "top");
        return false;
      }
      if (pTile.zone.city?.kingdom != null) {
        _cityToForceIntoOtherKingdom.kingdom.cities.Remove(_cityToForceIntoOtherKingdom);
        _cityToForceIntoOtherKingdom.kingdom = pTile.zone.city.kingdom;
        _cityToForceIntoOtherKingdom.kingdom.cities.Add(_cityToForceIntoOtherKingdom);
        _cityToForceIntoOtherKingdom.units.ToList().ForEach(a => a.setKingdom(_cityToForceIntoOtherKingdom.kingdom));
        WorldTip.showNow("KGPLL_ChangeCityKingdom_Success", true, "top");
        return true;
      }
      WorldTip.showNow("KGPLL_ChangeCityKingdom_NoKingdomSelectedError", true, "top");
      return false;
    }
  }
}
