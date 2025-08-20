using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Powers;

namespace KeyGUI.Powers {
  public class CultureForceSelectCity : KeyGuiPower {
    public readonly KeyGuiLocale NoCultureSelectedError = "No culture to force upon city here, try again!";
    public readonly KeyGuiLocale SelectCity = "Select the city to force the culture upon.";
    public readonly KeyGuiLocale Success = "Culture forced upon city!";
    public readonly KeyGuiLocale NoCitySelectedError = "No city to force culture upon here, try again!";
    public CultureForceSelectCity() : base(new GodPower() {
      id = "culture_force_select_city_keygui",
      name = "Culture Force Select City",
      force_map_mode = MetaType.City
    }) {
    }

    protected override bool PowerButtonPress(string pPower) {
      WorldTip.showNow(SelectCity, false, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      City cityToForceCultureUpon = pTile.zone.city;
      if (cityToForceCultureUpon != null) {
        if (CultureForceSelectCulture.CultureToForceUponCity != null) {
          ForceCultureOnCity(CultureForceSelectCulture.CultureToForceUponCity, cityToForceCultureUpon);
          WorldTip.showNow(Success, false, "top");
          return true;
        }
        WorldTip.showNow(NoCultureSelectedError, false, "top");
        return false;
      }
      WorldTip.showNow(NoCitySelectedError, false, "top");
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
