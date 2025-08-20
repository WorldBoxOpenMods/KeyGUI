using KeyGUI.Framework.Powers;
using KeyGUI.Menus.Localizations.Declarations;

namespace KeyGUI.Powers {
  public class CreateNewCulture : KeyGuiPower {
    public CreateNewCulture() : base(new GodPower() {
      id = "create_new_culture_keygui",
      name = "Create New Culture",
      force_map_mode = MetaType.City
    }) {
    }

    protected override bool PowerButtonPress(string _) {
      WorldTip.showNow(Locales.KeyGui.Powers.CultureCreation.SelectCity, false, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      City cityToCreateCultureFor = pTile.zone.city;
      if (cityToCreateCultureFor != null) {
        Culture newCulture = World.world.cultures.newCulture(cityToCreateCultureFor.leader, false);
        if (KeyGui.Instance.TryGetPower(out CultureForceSelectCity power)) {
          power.ForceCultureOnCity(newCulture, cityToCreateCultureFor);
        } else {
          // TODO: error message
        }
        WorldTip.showNow(Locales.KeyGui.Powers.CultureCreation.Success, false, "top");
        return true;
      }
      WorldTip.showNow(Locales.KeyGui.Powers.CultureCreation.NoCitySelected, false, "top");
      return false;
    }
  }
}
