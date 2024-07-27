using System.Linq;
using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.BehaviourManipulation;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Stats.RaceModifier.StatSelection {
  public class KeyGuiStatsRaceModifierStatModifier : KeyGuiMenu {
    protected override float MenuMinHeight => 800;
    protected override float MenuMaxHeight => 1700;
    internal static Race SelectedRace;
    private Vector2 _scrollPos;
    private string _statModifierAmount = "1";
    protected override void LoadGUI(int windowID) {
      if (SelectedRace == null) {
        MenuRect.height = 0;
        GUILayout.Label(Locales.Get(Locales.KeyGui.Stats.RaceModifier.StatModifier.NoRaceSelectedError));
        return;
      }
      GUILayout.Label(string.Format(Locales.Get(Locales.KeyGui.Stats.RaceModifier.StatModifier.ModifyingRaceLabel), SelectedRace.id));
      _statModifierAmount = GUILayout.TextField(_statModifierAmount);
      if (int.TryParse(_statModifierAmount, out _)) {
        _scrollPos = GUILayout.BeginScrollView(_scrollPos);
        foreach (BaseStatAsset s in AssetManager.base_stats_library.list.Where(s => GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Stats.RaceModifier.StatModifier.ModifyStatButton), s.id)))) {
          KeyLib.Get<KeyGenLibRaceManipulationMethodCollection>().ModifySpecificRaceStat(s.id, SelectedRace, int.Parse(_statModifierAmount));
        }
        GUILayout.EndScrollView();
      } else {
        MenuRect.height = 0;
        GUILayout.Label(Locales.Get(Locales.KeyGui.Stats.RaceModifier.StatModifier.InvalidStatNumberValueEnteredError));
      }
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Stats.RaceModifier.StatModifier.CloseMenuButton))) {
        Enabled = false;
      }
    }
  }
}