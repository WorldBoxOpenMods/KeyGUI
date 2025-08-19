using System.Collections.Generic;
using System.Linq;
using KeyGUI.Powers;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Cultures.TraitSelection;
using KeyGUI.Menus.Localizations.Declarations;
using KeyGUI.Patches;
using UnityEngine;

namespace KeyGUI.Menus.Cultures {
  public class KeyGuiCultures : KeyGuiMenuManager {
    private Vector2 _scrollPos;

    internal override void RegisterPatches() {
      KeyGui.Instance.RegisterPatch<CultureBirthTraitsManagement>();
    }
    internal override void RegisterPowers() {
      KeyGui.Instance.RegisterPower<CultureDeletion>();
      KeyGui.Instance.RegisterPower<CultureReset>();
      KeyGui.Instance.RegisterPower<CultureForceSelectCulture>();
      KeyGui.Instance.RegisterPower<CreateNewCulture>();
    }

    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.DeleteOneCultureButton))) {
        if (!KeyGui.Instance.TryGetPower<CultureDeletion>(out (GodPower, PowerButton) powerAndButton)) {
          Debug.LogError("Something went wrong with the Culture Deletion! Please report this to the mod author!");
        } else {
          (GodPower power, PowerButton button) = powerAndButton;
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.DeleteEveryCultureButton))) {
        if (KeyGui.Instance.TryGetPower(out CultureDeletion power)) {
          List<Culture> cultures = World.world.cultures.list.ToList();
          foreach (Culture t in cultures) {
            power.DeleteCulture(t);
          }
        } else {
          // TODO: error handling
        }
      }
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.ResetOneCultureButton))) {
        if (!KeyGui.Instance.TryGetPower<CultureReset>(out (GodPower, PowerButton) powerAndButton)) {
          Debug.LogError("Something went wrong with the Culture Reset! Please report this to the mod author!");
        } else {
          (GodPower power, PowerButton button) = powerAndButton;
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.ResetEveryCultureButton))) {
        if (KeyGui.Instance.TryGetPower(out CultureReset power)) {
          List<Culture> cultures = World.world.cultures.list.ToList();
          foreach (Culture t in cultures) {
            power.ResetCulture(t);
          }
        } else {
          // TODO: error handling
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.ForceExistingCultureUponSpecificCityButton))) {
        if (!KeyGui.Instance.TryGetPower<CultureForceSelectCulture>(out (GodPower, PowerButton) powerAndButton)) {
          Debug.LogError("Something went wrong with the Culture Conversion! Please report this to the mod author!");
        } else {
          (GodPower power, PowerButton button) = powerAndButton;
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.CreateNewCultureAtSpecificCityButton))) {
        if (!KeyGui.Instance.TryGetPower<CreateNewCulture>(out (GodPower, PowerButton) powerAndButton)) {
          Debug.LogError("Something went wrong with the Culture Creation! Please report this to the mod author!");
        } else {
          (GodPower power, PowerButton button) = powerAndButton;
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
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
