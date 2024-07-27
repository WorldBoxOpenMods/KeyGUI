using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.BehaviourManipulation;
using KeyGeneralPurposeLibrary.Powers;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Cultures {
  public class KeyGuiCultures : KeyGuiMenu {
    private string _knowledgeModifierString = "1";

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

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.ResetTechOfOneCultureButton))) {
        GodPower power = KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.CultureTechResetIndex];
        PowerButton button = KeyLib.Get<KeyGenLibGodPowerButtonLibrary>()[KeyGenLibGodPowerButtonLibrary.CultureTechResetButtonIndex];
        if (button != null) {
          power.select_button_action(power.id);
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        } else {
          Debug.LogError("Something went wrong with the Culture Tech Reset! Please report this to the mod author!");
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.ResetTechOfEveryCultureButton))) {
        foreach (Culture t in World.world.cultures.list) {
          KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ResetCultureTech(t);
        }
      }

      GUILayout.Label(Locales.Get(Locales.KeyGui.Cultures.KnowledgeGainIncreaseDecreaseAmountLabel));
      _knowledgeModifierString = GUILayout.TextField(_knowledgeModifierString);
      if (string.IsNullOrEmpty(_knowledgeModifierString) || !int.TryParse(_knowledgeModifierString, NumberStyles.Integer, CultureInfo.InvariantCulture, out int modifier) || modifier == 0) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Cultures.InvalidKnowledgeGainIncreaseDecreaseAmountEnteredError));
      } else {
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.IncreaseKnowledgeGainOfOneCultureButton))) {
          GodPower power = KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.CultureKnowledgeGainModificationIndex];
          power.dropID = modifier.ToString();
          PowerButton button = KeyLib.Get<KeyGenLibGodPowerButtonLibrary>()[KeyGenLibGodPowerButtonLibrary.CultureKnowledgeGainIncreaseButtonIndex];
          if (button != null) {
            power.select_button_action(power.id);
            PowerButtonSelector.instance.unselectAll();
            PowerButtonSelector.instance.setPower(button);
          } else {
            Debug.LogError("Something went wrong with the Culture Knowledge Gain Increase! Please report this to the mod author!");
          }
        }

        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.IncreaseKnowledgeGainOfEveryCultureButton))) {
          foreach (Culture t in World.world.cultures.list) {
            KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ModifyKnowledgeGain(t, modifier);
          }
        }

        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.DecreaseKnowledgeGainOfOneCultureButton))) {
          GodPower power = KeyLib.Get<KeyGenLibGodPowerLibrary>()[KeyGenLibGodPowerLibrary.CultureKnowledgeGainModificationIndex];
          power.dropID = (-modifier).ToString();
          PowerButton button = KeyLib.Get<KeyGenLibGodPowerButtonLibrary>()[KeyGenLibGodPowerButtonLibrary.CultureKnowledgeGainIncreaseButtonIndex];
          if (button != null) {
            power.select_button_action(power.id);
            PowerButtonSelector.instance.unselectAll();
            PowerButtonSelector.instance.setPower(button);
          } else {
            Debug.LogError("Something went wrong with the Culture Knowledge Gain Decrease! Please report this to the mod author!");
          }
        }

        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.DecreaseKnowledgeGainOfEveryCultureButton))) {
          foreach (Culture t in World.world.cultures.list) {
            KeyLib.Get<KeyGenLibCultureManipulationMethodCollection>().ModifyKnowledgeGain(t, -modifier);
          }
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
    }
  }
}