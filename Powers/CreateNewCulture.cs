using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Powers;

namespace KeyGUI.Powers {
  public class CreateNewCulture : KeyGuiPower {
    public readonly KeyGuiLocale SelectCity = "Select the city to create a new culture for.";
    public readonly KeyGuiLocale Success = "New culture created!";
    public readonly KeyGuiLocale NoCitySelected = "No city to create new culture for here, try again!";
    
    public CreateNewCulture() : base(new GodPower() {
      id = "create_new_culture_keygui",
      name = "Create New Culture",
      force_map_mode = MetaType.City
    }) {
    }

    protected override bool PowerButtonPress(string _) {
      WorldTip.showNow(SelectCity, false, "top");
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
        WorldTip.showNow(Success, false, "top");
        return true;
      }
      WorldTip.showNow(NoCitySelected, false, "top");
      return false;
    }
  }
}
