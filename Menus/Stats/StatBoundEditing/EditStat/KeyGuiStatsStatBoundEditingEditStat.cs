using System.Collections.Generic;
using System.Globalization;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using UnityEngine;

namespace KeyGUI.Menus.Stats.StatBoundEditing.EditStat {
  public class KeyGuiStatsStatBoundEditingEditStat : KeyGuiMenu {
    public readonly KeyGuiLocale NoStatSelectedError = "No stat selected!";
    public readonly KeyGuiLocale ModifyingStatLabel = "Modifying {0}";
    public readonly KeyGuiLocale DisableStatBoundsButton = "Disable stat bounds";
    public readonly KeyGuiLocale EnableStatBoundsButton = "Enable stat bounds";
    public readonly KeyGuiLocale HideStatButton = "Hide stat";
    public readonly KeyGuiLocale ShowStatButton = "Show stat";
    public readonly KeyGuiLocale CurrentMinAndMaxLabel = "Current Min: {0}, Current Max: {1}";
    public readonly KeyGuiLocale MinLabel = "Min:";
    public readonly KeyGuiLocale SetMinButton = "Set";
    public readonly KeyGuiLocale InvalidMinNumberEnteredError = "Please enter a valid number.";
    public readonly KeyGuiLocale MaxLabel = "Max:";
    public readonly KeyGuiLocale SetMaxButton = "Set";
    public readonly KeyGuiLocale InvalidMaxNumberEnteredError = "Please enter a valid number.";
    public readonly KeyGuiLocale CloseMenuButton = "Close";
    
    internal static BaseStatAsset SelectedStat;
    private readonly Dictionary<string, (string min, string max)> _statModifierAmounts = new Dictionary<string, (string min, string max)>();

    protected override void LoadGUI(int windowID) {
      if (SelectedStat == null) {
        MenuRect.height = 0;
        GUILayout.Label(NoStatSelectedError);
        return;
      }
      GUILayout.Label(string.Format(ModifyingStatLabel, SelectedStat.id));
      if (_statModifierAmounts.ContainsKey(SelectedStat.id) == false) {
        _statModifierAmounts.Add(SelectedStat.id, (SelectedStat.normalize_min.ToString(NumberFormatInfo.InvariantInfo), SelectedStat.normalize_max.ToString(NumberFormatInfo.InvariantInfo)));
      }
      if (GUILayout.Button(SelectedStat.normalize ? DisableStatBoundsButton : EnableStatBoundsButton)) {
        SelectedStat.normalize = !SelectedStat.normalize;
      }
      if (GUILayout.Button(SelectedStat.hidden ? ShowStatButton : HideStatButton)) {
        SelectedStat.hidden = !SelectedStat.hidden;
      }
      if (SelectedStat.normalize) GUILayout.Label(string.Format(CurrentMinAndMaxLabel, SelectedStat.normalize_min, SelectedStat.normalize_max));
      GUILayout.BeginHorizontal();
      (string min, string max) statValues = _statModifierAmounts[SelectedStat.id];
      GUILayout.Label(MinLabel);
      statValues.min = GUILayout.TextField(statValues.min);
      if (float.TryParse(statValues.min, out float min)) {
        if (GUILayout.Button(SetMinButton)) {
          SelectedStat.normalize_min = min;
        }
      } else {
        GUILayout.Label(InvalidMinNumberEnteredError);
      }
      GUILayout.Label(MaxLabel);
      statValues.max = GUILayout.TextField(statValues.max);
      if (int.TryParse(statValues.max, out int max)) {
        if (GUILayout.Button(SetMaxButton)) {
          SelectedStat.normalize_max = max;
        }
      } else {
        GUILayout.Label(InvalidMaxNumberEnteredError);
      }
      _statModifierAmounts[SelectedStat.id] = statValues;
      GUILayout.EndHorizontal();
      if (GUILayout.Button(CloseMenuButton)) {
        Enabled = false;
      }
    }
  }
}
