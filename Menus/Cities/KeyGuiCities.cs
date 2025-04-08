using System.Linq;
using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.PowersLib;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Cities.ResourceEditor;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Cities {
  public class KeyGuiCities : KeyGuiMenuManager {
    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cities.ForceCityAsCapitalCityButton))) {
        GodPower power = KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.ForceCityAsCapitalCityIndex];
        PowerButton button = KeyLib.Get<KeyGenLibGodPowerButtonLibrary>()[KeyGenLibGodPowerButtonLibrary.ForceCityAsCapitalCityButtonIndex];
        if (button != null) {
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        } else {
          Debug.LogError("Something went wrong with the Force City As Capital City! Please report this to the mod author!");
        }
      }
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cities.ForceCityIntoOtherKingdomButton))) {
        GodPower power = KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.ForceCityIntoOtherKingdomIndex];
        PowerButton button = KeyLib.Get<KeyGenLibGodPowerButtonLibrary>()[KeyGenLibGodPowerButtonLibrary.ForceCityIntoOtherKingdomButtonIndex];
        if (button != null) {
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        } else {
          Debug.LogError("Something went wrong with the Force City Into Other Kingdom! Please report this to the mod author!");
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
