using KeyGUI.MenuArchitecture;

namespace KeyGUI.Powers {
  public class ForceCityAsCapitalCity : KeyGuiPower {
    public ForceCityAsCapitalCity() : base(new GodPower() {
      id = "force_city_as_capital_city_keygui",
      name = "Force City As Capital City",
      force_map_mode = MetaType.City
    }) {
    }

    protected override bool PowerButtonPress(string pPower) {
      WorldTip.showNow("KGPLL_ForceCapital_SelectCity", true, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      City cityToForceAsCapitalCity = pTile.zone.city;
      if (cityToForceAsCapitalCity != null) {
        cityToForceAsCapitalCity.kingdom.capital = cityToForceAsCapitalCity;
        cityToForceAsCapitalCity.kingdom.data.capitalID = cityToForceAsCapitalCity.kingdom.capital.data.id;
        cityToForceAsCapitalCity.kingdom.location = cityToForceAsCapitalCity.kingdom.capital.city_center;
        WorldTip.showNow("KGPLL_ForceCapital_Success", true, "top");
        return true;
      }
      WorldTip.showNow("KGPLL_ForceCapital_NoCitySelectedError", true, "top");
      return false;
    }
  }
}
