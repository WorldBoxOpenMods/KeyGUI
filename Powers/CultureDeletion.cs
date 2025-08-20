using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Powers;

namespace KeyGUI.Powers {
  public class CultureDeletion : KeyGuiPower {
    public readonly KeyGuiLocale SelectCulture = "Select the culture to delete.";
    public readonly KeyGuiLocale Success = "Culture deleted!";
    public readonly KeyGuiLocale NoCultureSelectedError = "No culture to delete here, try again!";
    
    public CultureDeletion() : base(new GodPower() {
      id = "culture_wipe_keygui",
      name = "Culture Wipe",
      force_map_mode = MetaType.Culture
    }) {
    }

    protected override bool PowerButtonPress(string pPower) {
      WorldTip.showNow(SelectCulture, false, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      Culture cultureToWipe = pTile.zone.city?.culture;
      if (cultureToWipe != null) {
        DeleteCulture(cultureToWipe);
        WorldTip.showNow(Success, false, "top");
        return true;
      }

      WorldTip.showNow(NoCultureSelectedError, false, "top");
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
