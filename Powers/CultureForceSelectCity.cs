using KeyGUI.MenuArchitecture;

namespace KeyGUI.Powers {
  public class CultureForceSelectCity : KeyGuiPower {
    public CultureForceSelectCity() : base(new GodPower() {
      id = "culture_force_select_city_keygui",
      name = "Culture Force Select City",
      force_map_mode = MetaType.City
    }) {
    }

    protected override bool PowerButtonPress(string pPower) {
      WorldTip.showNow("KGPLL_CultureForceConversion_SelectCity", true, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      City cityToForceCultureUpon = pTile.zone.city;
      if (cityToForceCultureUpon != null) {
        if (CultureForceSelectCulture.CultureToForceUponCity != null) {
          ForceCultureOnCity(CultureForceSelectCulture.CultureToForceUponCity, cityToForceCultureUpon);
          WorldTip.showNow("KGPLL_CultureForceConversion_Success", true, "top");
          return true;
        }
        WorldTip.showNow("KGPLL_CultureForceConversion_NoCultureSelectedError", true, "top");
        return false;
      }
      WorldTip.showNow("KGPLL_CultureForceConversion_NoCitySelectedError", true, "top");
      return false;
    }

    public void ForceCultureOnCity(Culture citySelectionTargetCulture, City city) {
      Culture currentCityCulture = city.getCulture();
      currentCityCulture?.cities.Remove(city);

      city.setCulture(citySelectionTargetCulture);
      citySelectionTargetCulture.cities.Add(city);

      foreach (Actor actor in city.units) {
        actor.setCulture(citySelectionTargetCulture);
      }
    }
  }
}
