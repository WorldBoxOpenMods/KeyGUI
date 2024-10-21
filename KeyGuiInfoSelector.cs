using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.Assets;
using KeyGeneralPurposeLibrary.Powers;
using KeyGUI.Menus.Localizations;
using UnityEngine;

namespace KeyGUI {
  public class KeyGuiInfoSelector {
    private static int _godPowerIdIndex;
    private List<LocaleDeclaration> _userSelectionInfo;
    private List<object> _userSelections;
    private List<Type> _userSelectionTypes;
    private List<MapMode> _userSelectionMapModes;
    private List<List<MemberInfo>> _userSelectionObtainWantedValueInstructions;
    private readonly List<LocaleDeclaration> _initialUserSelectionInfo;
    private readonly List<object> _initialUserSelections;
    private readonly List<Type> _initialUserSelectionTypes;
    private readonly List<MapMode> _initialUserSelectionMapModes;
    private readonly List<List<MemberInfo>> _initialUserSelectionObtainWantedValueInstructions;

    private readonly object _methodInvocationInstance;
    private readonly MethodInfo _userSelectionMethod;
    
    private readonly GodPower _power;
    private readonly PowerButton _button;
    
    public KeyGuiInfoSelector(List<LocaleDeclaration> pUserSelectionInfo, List<object> pUserSelections, List<Type> pUserSelectionTypes, List<MapMode> pUserSelectionMapModes, List<List<MemberInfo>> pUserSelectionObtainWantedValueInstructions, MethodInfo pUserSelectionMethod, object pMethodInvocationInstance) {
      _initialUserSelectionInfo = pUserSelectionInfo;
      _initialUserSelections = pUserSelections;
      _initialUserSelectionTypes = pUserSelectionTypes;
      _initialUserSelectionMapModes = pUserSelectionMapModes;
      _initialUserSelectionObtainWantedValueInstructions = pUserSelectionObtainWantedValueInstructions;
      _userSelectionInfo = _initialUserSelectionInfo.ToList();
      _userSelections = _initialUserSelections.ToList();
      _userSelectionTypes = _initialUserSelectionTypes.ToList();
      _userSelectionMapModes = _initialUserSelectionMapModes.ToList();
      _userSelectionObtainWantedValueInstructions = _initialUserSelectionObtainWantedValueInstructions.ToList();
      
      _userSelectionMethod = pUserSelectionMethod;
      _methodInvocationInstance = pMethodInvocationInstance;
      _power = new GodPower {
        id = "select_info_keygui_" + _godPowerIdIndex++,
        name = "Select Info",
        force_map_text = MapMode.None,
        select_button_action = SelectInfoPowerButtonPress,
        click_action = ClickWithSelectInfo
      };
      AssetManager.powers.add(_power);
      PowerButton button = KeyLib.Get<KeyGenLibGodPowerButtonGenerator>().CreateHiddenPowerButton(
        KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"),
        _power, 
        KeyGenLibFileAssetManager.CreateSprite("KeyGeneralPurposeLibrary", "DefaultSprite"));
      _button = button;
    }

    private void Reset() {
      _userSelectionInfo = _initialUserSelectionInfo.ToList();
      _userSelections = _initialUserSelections.ToList();
      _userSelectionTypes = _initialUserSelectionTypes.ToList();
      _userSelectionMapModes = _initialUserSelectionMapModes.ToList();
      _userSelectionObtainWantedValueInstructions = _initialUserSelectionObtainWantedValueInstructions.ToList();
    }
    
    private bool SelectInfoPowerButtonPress(string _) {
      Reset();
      DisplayNextSelectionInfo();
      return false;
    }

    private void DisplayNextSelectionInfo() {
      WorldTip.showNow(_userSelectionInfo[0], false, "top");
      _power.force_map_text = _userSelectionMapModes[0];
    }

    public void Invoke() {
      _power.select_button_action(_power.id);
      PowerButtonSelector.instance.unselectAll();
      PowerButtonSelector.instance.highlightButton(_button);
      PowerButtonSelector.instance.setPower(_button);
    }

    private bool ClickWithSelectInfo(WorldTile pTile, string pPowerID) {
        object value = pTile;
        try {
          foreach (MemberInfo instruction in _userSelectionObtainWantedValueInstructions[0]) {
            if (value == null) {
              WorldTip.showNow("Invalid tile selected!", false, "top");
              return false;
            }
            if (instruction == null) {
              WorldTip.showNow("Invalid instruction by mod! The value when this happened was a " + value.GetType() + "! Please report this to the mod author!", false, "top");
              throw new Exception("Invalid instruction by mod! The value when this happened was a " + value.GetType() + "! Please report this to the mod author!");
            }
            switch (instruction) {
              case FieldInfo field:
                value = field.GetValue(value);
                break;
              case PropertyInfo property:
                value = property.GetValue(value);
                break;
              case MethodInfo method:
                value = method.Invoke(value, null);
                break;
              default:
                throw new Exception("Unknown MemberInfo type: " + instruction.GetType());
            }
          }
        } catch (Exception e) {
          Debug.LogError("Something went wrong with the Select Info! Please report this to the mod author!");
          Debug.LogError(e);
          return false;
        }

        if (value == null) return false;
        if (!_userSelectionTypes[0].IsInstanceOfType(value)) {
          WorldTip.showNow("Invalid instructions by mod! Got a " + value.GetType() + ", expected a " + _userSelectionTypes[0] + "! Please report this to the mod author!", false, "top");
          Debug.LogError("Invalid instructions by mod! Got a " + value.GetType() + ", expected a " + _userSelectionTypes[0] + "! Please report this to the mod author!");
          return false;
        }
        _userSelections.Add(value);
        _userSelectionInfo.RemoveAt(0);
        _userSelectionTypes.RemoveAt(0);
        _userSelectionMapModes.RemoveAt(0);
        _userSelectionObtainWantedValueInstructions.RemoveAt(0);
        if (_userSelectionInfo.Count == 0) {
          _userSelectionMethod.Invoke(_methodInvocationInstance, _userSelections.ToArray());
          _userSelections.Clear();
          _userSelectionInfo.Clear();
          _userSelectionTypes.Clear();
          _userSelectionObtainWantedValueInstructions.Clear();
          PowerButtonSelector.instance.unselectAll();
        } else {
          DisplayNextSelectionInfo();
        }
        return true;
    }
  }
}
