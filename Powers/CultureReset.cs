using System.Collections.Generic;
using System.Linq;
using KeyGUI.MenuArchitecture;

namespace KeyGUI.Powers {
  public class CultureReset : KeyGuiPower {
    public CultureReset() : base(new GodPower() {
      id = "culture_reset_keygui",
      name = "Culture Reset",
      force_map_mode = MetaType.Culture
    }) {
    }

    protected override bool PowerButtonPress(string pPower) {
      WorldTip.showNow("KGPLL_CultureFullReset_SelectCulture", true, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      Culture cultureToReset = pTile.zone.city?.culture;
      if (cultureToReset != null) {
        ResetCulture(cultureToReset);
        WorldTip.showNow("KGPLL_CultureFullReset_Success", true, "top");
        return true;
      }

      WorldTip.showNow("KGPLL_CultureFullReset_NoCultureSelectedError", true, "top");
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
