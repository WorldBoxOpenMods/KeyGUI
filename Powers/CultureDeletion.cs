using System.Linq;
using KeyGUI.MenuArchitecture;

namespace KeyGUI.Powers {
  public class CultureDeletion : KeyGuiPower {
    public CultureDeletion() : base(new GodPower() {
      id = "culture_wipe_keygui",
      name = "Culture Wipe",
      force_map_mode = MetaType.Culture
    }) {
    }

    protected override bool PowerButtonPress(string pPower) {
      WorldTip.showNow("KGPLL_CultureDeletion_SelectCulture", true, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      Culture cultureToWipe = pTile.zone.city?.culture;
      if (cultureToWipe != null) {
        DeleteCulture(cultureToWipe);
        WorldTip.showNow("KGPLL_CultureDeletion_Success", true, "top");
        return true;
      }

      WorldTip.showNow("KGPLL_CultureDeletion_NoCultureSelectedError", true, "top");
      return false;
    }

    public void DeleteCulture(Culture targetCulture) {
      foreach (City city in targetCulture.cities.ToList()) {
        city.setCulture(null);
      }
      for (int i = 0; i < World.world.units.ToList().Count; ++i) {
        if (World.world.units.ToList()[i].culture == targetCulture) {
          World.world.units.ToList()[i].setCulture(null);
        }
      }
      World.world.cultures.removeObject(targetCulture);
    }
  }
}
