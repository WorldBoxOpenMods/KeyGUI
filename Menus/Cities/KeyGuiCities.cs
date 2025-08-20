using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using KeyGUI.Powers;
using KeyGUI.Menus.Cities.ResourceEditor;
using UnityEngine;

namespace KeyGUI.Menus.Cities {
  public class KeyGuiCities : KeyGuiMenuManager {
    public readonly KeyGuiLocale ResourceEditorSubmenuName = "Resource Editor";
    public readonly KeyGuiLocale ForceCityAsCapitalCityButton = "Force City As Capital City";
    public readonly KeyGuiLocale ForceCityIntoOtherKingdomButton = "Force City Into Other Kingdom";
    public readonly KeyGuiLocale SelectCityToEditResourcesOfListHeader = "Select City to edit resources of:";
    
    internal override void RegisterPowers() {
      KeyGui.Instance.RegisterPower<ForceCityAsCapitalCity>();
      KeyGui.Instance.RegisterPower<ForceCityIntoOtherKingdom>();
    }
    
    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(ForceCityAsCapitalCityButton)) {
        if (!KeyGui.Instance.TryGetPower<ForceCityAsCapitalCity>(out (GodPower, PowerButton) powerAndButton)) {
          Debug.LogError("Something went wrong with the Force City As Capital City! Please report this to the mod author!");
        } else {
          (GodPower power, PowerButton button) = powerAndButton;
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        }
      }
      if (GUILayout.Button(ForceCityIntoOtherKingdomButton)) {
        if (!KeyGui.Instance.TryGetPower<ForceCityIntoOtherKingdom>(out (GodPower, PowerButton) powerAndButton)) {
          Debug.LogError("Something went wrong with the Force City Into Other Kingdom! Please report this to the mod author!");
        } else {
          (GodPower power, PowerButton button) = powerAndButton;
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        }
      }
      GUILayout.Label(SelectCityToEditResourcesOfListHeader);
      foreach (City city in World.world.cities.list.Where(city => GUILayout.Button(city.name))) {
        KeyGuiCitiesResourceEditor.SelectedCity = city;
      }
      LoadSubMenuToggles();
    }
    internal override void AddSubMenus() {
      Load<KeyGuiCitiesResourceEditor>(ResourceEditorSubmenuName);
    }
  }
}
