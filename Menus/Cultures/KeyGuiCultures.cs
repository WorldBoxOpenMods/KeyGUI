using System.Collections.Generic;
using System.Linq;
using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.BehaviourManipulation;
using KeyGeneralPurposeLibrary.Powers;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Cultures.TraitSelection;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Cultures {
  public class KeyGuiCultures : KeyGuiMenuManager {
    private Vector2 _scrollPos;


    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.DeleteOneCultureButton))) {
        GodPower power = KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.CultureDeletionIndex];
        PowerButton button = KeyLib.Get<KeyGenLibGodPowerButtonLibrary>()[KeyGenLibGodPowerButtonLibrary.CultureDeletionButtonIndex];
        if (button != null) {
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        } else {
          Debug.LogError("Something went wrong with the Culture Deletion! Please report this to the mod author!");
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.DeleteEveryCultureButton))) {
        List<Culture> cultures = World.world.cultures.list.ToList();
        foreach (Culture t in cultures) {
          KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().DeleteCulture(t);
        }
      }
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.ResetOneCultureButton))) {
        GodPower power = KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.CultureResetIndex];
        PowerButton button = KeyLib.Get<KeyGenLibGodPowerButtonLibrary>()[KeyGenLibGodPowerButtonLibrary.CultureResetButtonIndex];
        if (button != null) {
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        } else {
          Debug.LogError("Something went wrong with the Culture Reset! Please report this to the mod author!");
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.ResetEveryCultureButton))) {
        List<Culture> cultures = World.world.cultures.list.ToList();
        foreach (Culture t in cultures) {
          KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ResetCulture(t);
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.ResetTechOfEveryCultureButton))) {
        foreach (Culture t in World.world.cultures.list) {
          KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ResetCultureTech(t);
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.ForceExistingCultureUponSpecificCityButton))) {
        GodPower power = KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.CultureForceSelectCultureIndex];
        PowerButton button = KeyLib.Get<KeyGenLibGodPowerButtonLibrary>()[KeyGenLibGodPowerButtonLibrary.CultureForceSelectCultureButtonIndex];
        if (button != null) {
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        } else {
          Debug.LogError("Something went wrong with the Culture Conversion! Please report this to the mod author!");
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.CreateNewCultureAtSpecificCityButton))) {
        GodPower power = KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.CreateNewCultureIndex];
        PowerButton button = KeyLib.Get<KeyGenLibGodPowerButtonLibrary>()[KeyGenLibGodPowerButtonLibrary.CreateNewCultureButtonIndex];
        if (button != null) {
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        } else {
          Debug.LogError("Something went wrong with the Culture Creation! Please report this to the mod author!");
        }
      }

      if (World.world.cultures.list.Count > 0) {
        _scrollPos = GUILayout.BeginScrollView(_scrollPos);
        GUILayout.Label(Locales.Get(Locales.KeyGui.Cultures.SelectCultureListHeader));
        foreach (Culture culture in World.world.cultures.list.Where(culture => GUILayout.Button(culture.name))) {
          KeyGuiCulturesTraitSelection.SelectedCulture = culture;
        }
        GUILayout.EndScrollView();
        LoadSubMenuToggles();
      }
    }
    
    internal override void AddSubMenus() {
      Load<KeyGuiCulturesTraitSelection>(Locales.KeyGui.Cultures.TraitSelectionSubmenuName);
    }
  }
}
