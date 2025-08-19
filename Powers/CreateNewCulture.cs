using KeyGUI.MenuArchitecture;

namespace KeyGUI.Powers {
  public class CreateNewCulture : KeyGuiPower {
    public CreateNewCulture() : base(new GodPower() {
      id = "create_new_culture_keygui",
      name = "Create New Culture",
      force_map_mode = MetaType.City
    }) {
    }

    protected override bool PowerButtonPress(string _) {
      WorldTip.showNow("KGPLL_CultureCreation_SelectCity", true, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      City cityToCreateCultureFor = pTile.zone.city;
      if (cityToCreateCultureFor != null) {
        Culture newCulture = World.world.cultures.newCulture(cityToCreateCultureFor.leader, true);
        if (KeyGui.Instance.TryGetPower(out CultureForceSelectCity power)) {
          power.ForceCultureOnCity(newCulture, cityToCreateCultureFor);
        } else {
          // TODO: error message
        }
        WorldTip.showNow("KGPLL_CultureCreation_Success", true, "top");
        return true;
      }
      WorldTip.showNow("KGPLL_CultureCreation_NoCitySelected", true, "top");
      return false;
    }
  }
}
