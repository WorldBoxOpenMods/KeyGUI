using System.Collections.Generic;
using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using KeyGUI.Powers;
using KeyGUI.Menus.Cultures.TraitSelection;
using KeyGUI.Patches;
using UnityEngine;

namespace KeyGUI.Menus.Cultures {
  public class KeyGuiCultures : KeyGuiMenuManager {
    public readonly KeyGuiLocale TraitSelectionSubmenuName = "Culture Trait Selection";
    public readonly KeyGuiLocale DeleteOneCultureButton = "Delete One Culture";
    public readonly KeyGuiLocale DeleteEveryCultureButton = "Delete Every Culture";
    public readonly KeyGuiLocale ResetOneCultureButton = "Reset One Culture";
    public readonly KeyGuiLocale ResetEveryCultureButton = "Reset Every Culture";
    public readonly KeyGuiLocale ForceExistingCultureUponSpecificCityButton = "Force Existing Culture Upon Specific City";
    public readonly KeyGuiLocale CreateNewCultureAtSpecificCityButton = "Create New Culture At Specific City";
    public readonly KeyGuiLocale SelectCultureListHeader = "Select Culture:";
    
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
      if (GUILayout.Button(DeleteOneCultureButton)) {
        if (!KeyGui.Instance.TryGetPower<CultureDeletion>(out (GodPower, PowerButton) powerAndButton)) {
          Debug.LogError("Something went wrong with the Culture Deletion! Please report this to the mod author!");
        } else {
          (GodPower power, PowerButton button) = powerAndButton;
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        }
      }

      if (GUILayout.Button(DeleteEveryCultureButton)) {
        if (KeyGui.Instance.TryGetPower(out CultureDeletion power)) {
          List<Culture> cultures = World.world.cultures.list.ToList();
          foreach (Culture t in cultures) {
            power.DeleteCulture(t);
          }
        } else {
          // TODO: error handling
        }
      }
      if (GUILayout.Button(ResetOneCultureButton)) {
        if (!KeyGui.Instance.TryGetPower<CultureReset>(out (GodPower, PowerButton) powerAndButton)) {
          Debug.LogError("Something went wrong with the Culture Reset! Please report this to the mod author!");
        } else {
          (GodPower power, PowerButton button) = powerAndButton;
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        }
      }

      if (GUILayout.Button(ResetEveryCultureButton)) {
        if (KeyGui.Instance.TryGetPower(out CultureReset power)) {
          List<Culture> cultures = World.world.cultures.list.ToList();
          foreach (Culture t in cultures) {
            power.ResetCulture(t);
          }
        } else {
          // TODO: error handling
        }
      }

      if (GUILayout.Button(ForceExistingCultureUponSpecificCityButton)) {
        if (!KeyGui.Instance.TryGetPower<CultureForceSelectCulture>(out (GodPower, PowerButton) powerAndButton)) {
          Debug.LogError("Something went wrong with the Culture Conversion! Please report this to the mod author!");
        } else {
          (GodPower power, PowerButton button) = powerAndButton;
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        }
      }

      if (GUILayout.Button(CreateNewCultureAtSpecificCityButton)) {
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
        GUILayout.Label(SelectCultureListHeader);
        foreach (Culture culture in World.world.cultures.list.Where(culture => GUILayout.Button(culture.name))) {
          KeyGuiCulturesTraitSelection.SelectedCulture = culture;
        }
        GUILayout.EndScrollView();
        LoadSubMenuToggles();
      }
    }

    internal override void AddSubMenus() {
      Load<KeyGuiCulturesTraitSelection>(TraitSelectionSubmenuName);
    }
  }
}
