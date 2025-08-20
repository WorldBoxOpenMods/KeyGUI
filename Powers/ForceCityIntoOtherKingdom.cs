using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Powers;

namespace KeyGUI.Powers {
  public class ForceCityIntoOtherKingdom : KeyGuiPower {
    public readonly KeyGuiLocale SelectCity = "Select the city to force into another kingdom.";
    public readonly KeyGuiLocale SelectKingdom = "Select the kingdom to force the city into.";
    public readonly KeyGuiLocale NoCitySelectedError = "No city to force into other kingdom here, try again!";
    public readonly KeyGuiLocale Success = "City forced into other kingdom!";
    public readonly KeyGuiLocale NoKingdomSelectedError = "The tile you selected isn't owned by any kingdom!";
    
    public ForceCityIntoOtherKingdom() : base(new GodPower() {
      id = "force_city_into_other_kingdom_keygui",
      name = "Force City Into Other Kingdom",
      force_map_mode = MetaType.City
    }) {
    }
    private static City _cityToForceIntoOtherKingdom;

    protected override bool PowerButtonPress(string pPower) {
      _cityToForceIntoOtherKingdom = null;
      WorldTip.showNow(SelectCity, false, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      if (_cityToForceIntoOtherKingdom == null) {
        _cityToForceIntoOtherKingdom = pTile.zone.city;
        if (_cityToForceIntoOtherKingdom != null) {
          WorldTip.showNow(SelectKingdom, false, "top");
          return false;
        }
        WorldTip.showNow(NoCitySelectedError, false, "top");
        return false;
      }
      if (pTile.zone.city?.kingdom != null) {
        _cityToForceIntoOtherKingdom.kingdom.cities.Remove(_cityToForceIntoOtherKingdom);
        _cityToForceIntoOtherKingdom.kingdom = pTile.zone.city.kingdom;
        _cityToForceIntoOtherKingdom.kingdom.cities.Add(_cityToForceIntoOtherKingdom);
        _cityToForceIntoOtherKingdom.units.ToList().ForEach(a => a.setKingdom(_cityToForceIntoOtherKingdom.kingdom));
        WorldTip.showNow(Success, false, "top");
        return true;
      }
      WorldTip.showNow(NoKingdomSelectedError, false, "top");
      return false;
    }
  }
}
