using System.Linq;
using KeyGUI.Powers;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Cities.ResourceEditor;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Cities {
  public class KeyGuiCities : KeyGuiMenuManager {
    internal override void RegisterPowers() {
      KeyGui.Instance.RegisterPower<ForceCityAsCapitalCity>();
      KeyGui.Instance.RegisterPower<ForceCityIntoOtherKingdom>();
    }
    
    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cities.ForceCityAsCapitalCityButton))) {
        if (!KeyGui.Instance.TryGetPower<ForceCityAsCapitalCity>(out (GodPower, PowerButton) powerAndButton)) {
          Debug.LogError("Something went wrong with the Force City As Capital City! Please report this to the mod author!");
        } else {
          (GodPower power, PowerButton button) = powerAndButton;
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        }
      }
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cities.ForceCityIntoOtherKingdomButton))) {
        if (!KeyGui.Instance.TryGetPower<ForceCityIntoOtherKingdom>(out (GodPower, PowerButton) powerAndButton)) {
          Debug.LogError("Something went wrong with the Force City Into Other Kingdom! Please report this to the mod author!");
        } else {
          (GodPower power, PowerButton button) = powerAndButton;
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        }
      }
      GUILayout.Label(Locales.Get(Locales.KeyGui.Cities.SelectCityToEditResourcesOfListHeader));
      foreach (City city in World.world.cities.list.Where(city => GUILayout.Button(city.name))) {
        KeyGuiCitiesResourceEditor.SelectedCity = city;
      }
      LoadSubMenuToggles();
    }
    internal override void AddSubMenus() {
      Load<KeyGuiCitiesResourceEditor>(Locales.KeyGui.Cities.ResourceEditorSubmenuName);
    }
  }
}
