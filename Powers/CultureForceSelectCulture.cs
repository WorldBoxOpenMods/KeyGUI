using UnityEngine;
using KeyGUI.MenuArchitecture;

namespace KeyGUI.Powers {
  public class CultureForceSelectCulture : KeyGuiPower {
    public CultureForceSelectCulture() : base(new GodPower() {
      id = "culture_force_select_culture_keygui",
      name = "Culture Force Select Culture",
      force_map_mode = MetaType.Culture
    }) {
    }

    protected override bool PowerButtonPress(string pPower) {
      CultureToForceUponCity = null;
      WorldTip.showNow("KGPLL_CultureForceConversion_SelectCulture", true, "top");
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
        WorldTip.showNow("KGPLL_CultureForceConversion_SelectCity", true, "top");
        power.select_button_action(power.id);
        PowerButtonSelector.instance.unselectAll();
        PowerButtonSelector.instance.setPower(button);
        return true;
      }
      WorldTip.showNow("KGPLL_CultureForceConversion_NoCultureSelectedError", true, "top");
      return false;
    }
  }
}
