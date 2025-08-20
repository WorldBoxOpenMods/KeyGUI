using System.Collections.Generic;
using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Powers;

namespace KeyGUI.Powers {
  public class CultureReset : KeyGuiPower {
    public readonly KeyGuiLocale SelectCulture = "Select the culture to reset.";
    public readonly KeyGuiLocale Success = "Culture reset!";
    public readonly KeyGuiLocale NoCultureSelectedError = "No culture to reset here, try again!";
    
    public CultureReset() : base(new GodPower() {
      id = "culture_reset_keygui",
      name = "Culture Reset",
      force_map_mode = MetaType.Culture
    }) {
    }

    protected override bool PowerButtonPress(string pPower) {
      WorldTip.showNow(SelectCulture, false, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      Culture cultureToReset = pTile.zone.city?.culture;
      if (cultureToReset != null) {
        ResetCulture(cultureToReset);
        WorldTip.showNow(Success, false, "top");
        return true;
      }

      WorldTip.showNow(NoCultureSelectedError, false, "top");
      return false;
    }

    public void ResetCulture(Culture culture) {
      List<City> cities = culture.cities.ToList();
      Actor newFounder = culture.units.First();
      if (KeyGui.Instance.TryGetPower(out CultureDeletion power)) {
        power.DeleteCulture(culture);
      } else {
        // TODO: error message
      }
      Culture newCulture = World.world.cultures.newCulture(newFounder, true);
      foreach (City t in cities) {
        t.setCulture(newCulture);
      }
      foreach (Actor unit in culture.units) {
        unit.setCulture(newCulture);
      }
    }
  }
}
