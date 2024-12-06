using System.Linq;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using KeyGUI.Menus.Stats.RaceModifier.StatSelection;
using UnityEngine;

namespace KeyGUI.Menus.Stats.RaceModifier {
  public class KeyGuiStatsRaceModifier : KeyGuiMenuManager {
    private bool _onlyShowCivRaces = true;
    internal override void AddSubMenus() {
      Load<KeyGuiStatsRaceModifierStatModifier>(Locales.KeyGui.Stats.RaceModifier.StatModifierSubmenuName);
    }

    protected override void LoadGUI() {
      if (GUILayout.Button(_onlyShowCivRaces ? Locales.Get(Locales.KeyGui.Stats.RaceModifier.ShowNonCivRacesButton) : Locales.Get(Locales.KeyGui.Stats.RaceModifier.HideNonCivRacesButton))) {
        _onlyShowCivRaces = !_onlyShowCivRaces;
        MenuRect.height = 0;
      }
      foreach (Race r in AssetManager.raceLibrary.list.Where(r => !_onlyShowCivRaces || r.civilization).Where(r => GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Stats.RaceModifier.ModifyRaceButton), r.id)))) {
        KeyGuiStatsRaceModifierStatModifier.SelectedRace = r;
      }
      LoadSubMenuToggles();
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Stats.RaceModifier.CloseMenuButton))) {
        Enabled = false;
      }
    }
  }
}
