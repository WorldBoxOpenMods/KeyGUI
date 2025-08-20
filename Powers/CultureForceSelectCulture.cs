using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Powers;
using UnityEngine;

namespace KeyGUI.Powers {
  public class CultureForceSelectCulture : KeyGuiPower {
    public readonly KeyGuiLocale SelectCulture = "Select the culture to force upon the city.";
    public readonly KeyGuiLocale NoCultureSelectedError = "No culture to force upon city here, try again!";
    public readonly KeyGuiLocale SelectCity = "Select the city to force the culture upon.";
    
    public CultureForceSelectCulture() : base(new GodPower() {
      id = "culture_force_select_culture_keygui",
      name = "Culture Force Select Culture",
      force_map_mode = MetaType.Culture
    }) {
    }

    protected override bool PowerButtonPress(string pPower) {
      CultureToForceUponCity = null;
      WorldTip.showNow(SelectCulture, false, "top");
      return false;
    }

    internal static Culture CultureToForceUponCity;
    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      Culture cultureToForce = pTile.zone.city?.culture;
      CultureToForceUponCity = cultureToForce;
      if (cultureToForce != null) {
        if (!KeyGui.Instance.TryGetPower<CultureForceSelectCity>(out (GodPower, PowerButton) powerAndButton)) {
          Debug.LogError("Something went wrong with the Culture Conversion! Please report this to the mod author!");
          return false;
        }
        (GodPower power, PowerButton button) = powerAndButton;
        WorldTip.showNow(SelectCity, false, "top");
        power.select_button_action(power.id);
        PowerButtonSelector.instance.unselectAll();
        PowerButtonSelector.instance.setPower(button);
        return true;
      }
      WorldTip.showNow(NoCultureSelectedError, false, "top");
      return false;
    }
  }
}
