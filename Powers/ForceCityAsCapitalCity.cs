using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Powers;

namespace KeyGUI.Powers {
  public class ForceCityAsCapitalCity : KeyGuiPower {
    public readonly KeyGuiLocale SelectCity = "Select the city to force as the capital city.";
    public readonly KeyGuiLocale Success = "City forced as capital city!";
    public readonly KeyGuiLocale NoCitySelectedError = "No city to force as capital city here, try again!";
    
    public ForceCityAsCapitalCity() : base(new GodPower() {
      id = "force_city_as_capital_city_keygui",
      name = "Force City As Capital City",
      force_map_mode = MetaType.City
    }) {
    }

    protected override bool PowerButtonPress(string pPower) {
      WorldTip.showNow(SelectCity, false, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      City cityToForceAsCapitalCity = pTile.zone.city;
      if (cityToForceAsCapitalCity != null) {
        cityToForceAsCapitalCity.kingdom.capital = cityToForceAsCapitalCity;
        cityToForceAsCapitalCity.kingdom.data.capitalID = cityToForceAsCapitalCity.kingdom.capital.data.id;
        cityToForceAsCapitalCity.kingdom.location = cityToForceAsCapitalCity.kingdom.capital.city_center;
        WorldTip.showNow(Success, false, "top");
        return true;
      }
      WorldTip.showNow(NoCitySelectedError, false, "top");
      return false;
    }
  }
}
