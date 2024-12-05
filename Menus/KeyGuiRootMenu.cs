using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Actors;
using KeyGUI.Menus.Alliances;
using KeyGUI.Menus.Buildings;
using KeyGUI.Menus.Cities;
using KeyGUI.Menus.Clans;
using KeyGUI.Menus.Cleansing;
using KeyGUI.Menus.Crabzilla;
using KeyGUI.Menus.Cultures;
using KeyGUI.Menus.Debugging;
using KeyGUI.Menus.Destruction;
using KeyGUI.Menus.Disasters;
using KeyGUI.Menus.GameSettings;
using KeyGUI.Menus.Items;
using KeyGUI.Menus.Localizations;
using KeyGUI.Menus.Localizations.Declarations;
using KeyGUI.Menus.ModConfig;
using KeyGUI.Menus.ModConfig.ConfigOptions;
using KeyGUI.Menus.Plots;
using KeyGUI.Menus.Stats;
using KeyGUI.Menus.Traits;
using KeyGUI.Menus.Zones;
using UnityEngine;

namespace KeyGUI.Menus {
  public class KeyGuiRootMenu : KeyGuiMenuManager {
    private bool _modIsUpToDate = true;
    private (string, string)[] _problematicMods;
    private (string, string)[] _criticalMods;
    internal override void Initialize() {
      base.Initialize();
      if (KeyGuiModConfig.Get(General.ShowDebugButton)) {
        DebugConfig.instance.debugButton.SetActive(true);
      }
      Config.MODDED = true;
    }
    internal void MarkModVersionAsOutdated() {
      _modIsUpToDate = false;
    }

    internal void SetProblematicMods((string, string)[] problematicMods) {
      _problematicMods = problematicMods;
    }
      
    internal void SetCriticalMods((string, string)[] criticalMods) {
      _criticalMods = criticalMods;
    }

    internal override void AddSubMenus() {
      Load<KeyGuiDisasters>(Locales.KeyGui.DisastersSubmenuText);
      Load<KeyGuiStats>(Locales.KeyGui.StatsSubmenuText);
      Load<KeyGuiDestruction>(Locales.KeyGui.DestructionSubmenuText);
      Load<KeyGuiCleansing>(Locales.KeyGui.CleansingSubmenuText);
      Load<KeyGuiCultures>(Locales.KeyGui.CulturesSubmenuText);
      Load<KeyGuiPlots>(Locales.KeyGui.PlotsSubmenuText);
      Load<KeyGuiTraits>(Locales.KeyGui.TraitsSubmenuText);
      Load<KeyGuiItems>(Locales.KeyGui.ItemsSubmenuText);
      Load<KeyGuiCrabzilla>(Locales.KeyGui.CrabzillaSubmenuText);
      Load<KeyGuiClans>(Locales.KeyGui.ClansSubmenuText);
      Load<KeyGuiAlliances>(Locales.KeyGui.AlliancesSubmenuText);
      Load<KeyGuiActors>(Locales.KeyGui.ActorsSubmenuText);
      Load<KeyGuiGameSettings>(Locales.KeyGui.GameSettingsSubmenuText);
      Load<KeyGuiZones>(Locales.KeyGui.ZonesSubmenuText);
      Load<KeyGuiCities>(Locales.KeyGui.CitiesSubmenuText);
      Load<KeyGuiBuildings>(Locales.KeyGui.BuildingsSubmenuText);
      Load<KeyGuiLocales>(Locales.KeyGui.LocalesSubmenuText);
      Load(Locales.KeyGui.ModConfigSubmenuText, KeyGuiModConfig.GetInstance());
      if (KeyGuiConfig.DebugIsLegal) {
        Load<KeyGuiDebug>(Locales.KeyGui.DebugSubmenuText, 10999_0);
        Load(Locales.KeyGui.TestingSubmenuText, 10999_1, new []{ new KeyGuiTesting() }.Select(t => {t.InitMenuInfo(Locales.KeyGui.TestingSubmenuText, 10999_1, null, MenuRect.x + (MenuRect.width * 2)); return t;}).First());
      }
    }

    protected override void PreMenuToggleUi() {
      base.PreMenuToggleUi();
      if (!_modIsUpToDate) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.KeyGuiOutdatedWarning), new GUIStyle {
          alignment = TextAnchor.MiddleCenter,
          normal = new GUIStyleState {
            textColor = Color.white
          }
        });
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.DownloadKeyGuiButton))) {
          Application.OpenURL("https://gamebanana.com/mods/438228");
        }

        if (GUILayout.Button(Locales.Get(Locales.KeyGui.DismissKeyGuiOutdatedWarningButton))) {
          _modIsUpToDate = true;
          MenuRect.height = 0;
          MenuRect.width = 0;
        }

        GUILayout.EndHorizontal();
        GUILayout.Label("----------------------------------------------------------------------",
          new GUIStyle {
            alignment = TextAnchor.MiddleCenter,
            normal = new GUIStyleState {
              textColor = Color.white
            }
          }
        );
      }

      if (_problematicMods != null) {
        List<string> modsThatUserIsFineWithUsing = new List<string>();
        foreach ((string mod, string reason) modAndReason in _problematicMods) {
          GUILayout.Label(
            string.Format(Locales.Get(Locales.KeyGui.ProblematicModWarning), modAndReason.mod, modAndReason.reason),
            new GUIStyle {
              alignment = TextAnchor.MiddleCenter,
              normal = new GUIStyleState {
                textColor = Color.red
              }
            }
          );
          if (GUILayout.Button(Locales.Get(Locales.KeyGui.DismissProblematicModWarningButton))) {
            modsThatUserIsFineWithUsing.Add(modAndReason.mod);
            MenuRect.height = 0;
            MenuRect.width = 0;
          }
        }

        foreach (string mod in modsThatUserIsFineWithUsing) {
          _problematicMods = _problematicMods.Where(val => val.Item1 != mod).ToArray();
        }

        if (_problematicMods.Length > 0) {
          GUILayout.Label("----------------------------------------------------------------------",
            new GUIStyle {
              alignment = TextAnchor.MiddleCenter,
              normal = new GUIStyleState {
                textColor = Color.white
              }
            }
          );
        }
      }

      if (_criticalMods != null) {
        List<string> modsThatUserIsFineWithUsing = new List<string>();
        List<string> criticalModWarningsToDismiss = new List<string>();
        foreach ((string mod, string reason) modAndReason in _criticalMods) {
          GUILayout.Label(
            string.Format(Locales.Get(Locales.KeyGui.CriticalModWarning), modAndReason.mod, modAndReason.reason),
            new GUIStyle {
              alignment = TextAnchor.MiddleCenter,
              normal = new GUIStyleState {
                textColor = Color.red
              }
            }
          );
          GUILayout.BeginHorizontal();
          if (GUILayout.Button(Locales.Get(Locales.KeyGui.DismissCriticalModWarningButton))) {
            criticalModWarningsToDismiss.Add(modAndReason.mod);
            MenuRect.height = 0;
            MenuRect.width = 0;
          }

          if (GUILayout.Button(Locales.Get(Locales.KeyGui.DisableCriticalModWarningButton))) {
            modsThatUserIsFineWithUsing.Add(modAndReason.mod);
            MenuRect.height = 0;
            MenuRect.width = 0;
          }

          GUILayout.EndHorizontal();
        }

        foreach (string mod in modsThatUserIsFineWithUsing) {
          for (int i = 0; i < _criticalMods.Length; i++) {
            if (_criticalMods[i].Item1 == mod) {
              _criticalMods = _criticalMods.Where((val, idx) => idx != i).ToArray();
            }
          }

          KeyGuiModConfig.Set(Internal.WhitelistedMods, KeyGuiModConfig.Get(Internal.WhitelistedMods).AddItem(mod).ToArray());
        }

        foreach (string mod in criticalModWarningsToDismiss) {
          for (int i = 0; i < _criticalMods.Length; i++) {
            if (_criticalMods[i].Item1 == mod) {
              _criticalMods = _criticalMods.Where((val, idx) => idx != i).ToArray();
            }
          }
        }

        if (_criticalMods.Length > 0) {
          GUILayout.Label("----------------------------------------------------------------------",
            new GUIStyle {
              alignment = TextAnchor.MiddleCenter,
              normal = new GUIStyleState {
                textColor = Color.white
              }
            }
          );
        }
      }
    }
  }
}
