using System.Collections.Generic;
using System.Globalization;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Stats.StatBoundEditing.EditStat {
  public class KeyGuiStatsStatBoundEditingEditStat : KeyGuiMenu {
    internal static BaseStatAsset SelectedStat;
    private readonly Dictionary<string, (string min, string max)> _statModifierAmounts = new Dictionary<string, (string min, string max)>();

    protected override void LoadGUI(int windowID) {
      if (SelectedStat == null) {
        MenuRect.height = 0;
        GUILayout.Label(Locales.Get(Locales.KeyGui.Stats.StatBoundEditing.EditStat.NoStatSelectedError));
        return;
      }
      GUILayout.Label(string.Format(Locales.Get(Locales.KeyGui.Stats.StatBoundEditing.EditStat.ModifyingStatLabel), SelectedStat.id));
      if (_statModifierAmounts.ContainsKey(SelectedStat.id) == false) {
        _statModifierAmounts.Add(SelectedStat.id, (SelectedStat.normalize_min.ToString(NumberFormatInfo.InvariantInfo), SelectedStat.normalize_max.ToString(NumberFormatInfo.InvariantInfo)));
      }
      if (GUILayout.Button(SelectedStat.normalize ? Locales.Get(Locales.KeyGui.Stats.StatBoundEditing.EditStat.DisableStatBoundsButton) : Locales.Get(Locales.KeyGui.Stats.StatBoundEditing.EditStat.EnableStatBoundsButton))) {
        SelectedStat.normalize = !SelectedStat.normalize;
      }
      if (GUILayout.Button(SelectedStat.hidden ? Locales.Get(Locales.KeyGui.Stats.StatBoundEditing.EditStat.ShowStatButton) : Locales.Get(Locales.KeyGui.Stats.StatBoundEditing.EditStat.HideStatButton))) {
        SelectedStat.hidden = !SelectedStat.hidden;
      }
      if (SelectedStat.normalize) GUILayout.Label(string.Format(Locales.Get(Locales.KeyGui.Stats.StatBoundEditing.EditStat.CurrentMinAndMaxLabel), SelectedStat.normalize_min, SelectedStat.normalize_max));
      GUILayout.BeginHorizontal();
      (string min, string max) statValues = _statModifierAmounts[SelectedStat.id];
      GUILayout.Label(Locales.Get(Locales.KeyGui.Stats.StatBoundEditing.EditStat.MinLabel));
      statValues.min = GUILayout.TextField(statValues.min);
      if (float.TryParse(statValues.min, out float min)) {
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Stats.StatBoundEditing.EditStat.SetMinButton))) {
          SelectedStat.normalize_min = min;
        }
      } else {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Stats.StatBoundEditing.EditStat.InvalidMinNumberEnteredError));
      }
      GUILayout.Label(Locales.Get(Locales.KeyGui.Stats.StatBoundEditing.EditStat.MaxLabel));
      statValues.max = GUILayout.TextField(statValues.max);
      if (int.TryParse(statValues.max, out int max)) {
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Stats.StatBoundEditing.EditStat.SetMaxButton))) {
          SelectedStat.normalize_max = max;
        }
      } else {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Stats.StatBoundEditing.EditStat.InvalidMaxNumberEnteredError));
      }
      _statModifierAmounts[SelectedStat.id] = statValues;
      GUILayout.EndHorizontal();
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Stats.StatBoundEditing.EditStat.CloseMenuButton))) {
        Enabled = false;
      }
    }
  }
}
