using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
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
using KeyGUI.Menus.Localizations;
using KeyGUI.Menus.ModConfig;
using KeyGUI.Menus.ModConfig.ConfigOptions;
using KeyGUI.Menus.Plots;
using KeyGUI.Menus.Stats;
using KeyGUI.Menus.Traits;
using KeyGUI.Menus.Zones;
using UnityEngine;

namespace KeyGUI.Menus {
  public class KeyGuiRootMenu : KeyGuiMenuManager {
    public readonly KeyGuiLocale ModName = KeyGuiConfig.PluginName;
    public readonly KeyGuiLocale DisastersSubmenuText = "Disasters";
    public readonly KeyGuiLocale StatsSubmenuText = "Stats";
    public readonly KeyGuiLocale DestructionSubmenuText = "Destruction";
    public readonly KeyGuiLocale CleansingSubmenuText = "Cleansing";
    public readonly KeyGuiLocale CulturesSubmenuText = "Cultures";
    public readonly KeyGuiLocale PlotsSubmenuText = "Plots";
    public readonly KeyGuiLocale TraitsSubmenuText = "Traits";
    public readonly KeyGuiLocale CrabzillaSubmenuText = "Crabzilla";
    public readonly KeyGuiLocale ClansSubmenuText = "Clans";
    public readonly KeyGuiLocale AlliancesSubmenuText = "Alliances";
    public readonly KeyGuiLocale GameSettingsSubmenuText = "Game Settings";
    public readonly KeyGuiLocale ZonesSubmenuText = "Zones";
    public readonly KeyGuiLocale CitiesSubmenuText = "Cities";
    public readonly KeyGuiLocale BuildingsSubmenuText = "Buildings";
    public readonly KeyGuiLocale ModConfigSubmenuText = "Mod Config";
    public readonly KeyGuiLocale LocalesSubmenuText = "Locales";
    public readonly KeyGuiLocale DebugSubmenuText = "Debug";
    public readonly KeyGuiLocale TestingSubmenuText = "Testing";
    public readonly KeyGuiLocale MessagePopupTitle = "Message";
    public readonly KeyGuiLocale KeyGuiOutdatedWarning = "Your version of KeyGUI is outdated!";
    public readonly KeyGuiLocale DownloadKeyGuiButton = "Download";
    public readonly KeyGuiLocale DismissKeyGuiOutdatedWarningButton = "Dismiss";
    public readonly KeyGuiLocale ProblematicModWarning = "WARNING: It's recommended to not use {0} because {1}!";
    public readonly KeyGuiLocale DismissProblematicModWarningButton = "Dismiss";
    public readonly KeyGuiLocale CriticalModWarning = "WARNING: The mod {0} has automatically been disabled by KeyGUI because {1}!";
    public readonly KeyGuiLocale DismissCriticalModWarningButton = "Dismiss";
    public readonly KeyGuiLocale DisableCriticalModWarningButton = "Don't disable it the next time";
    
    private bool _modIsUpToDate = true;
    private (string, string)[] _problematicMods;
    private (string, string)[] _criticalMods;
    protected override void InitializeMenu() {
      base.InitializeMenu();
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
      Load<KeyGuiDisasters>(DisastersSubmenuText);
      Load<KeyGuiStats>(StatsSubmenuText);
      Load<KeyGuiDestruction>(DestructionSubmenuText);
      Load<KeyGuiCleansing>(CleansingSubmenuText);
      Load<KeyGuiCultures>(CulturesSubmenuText);
      Load<KeyGuiPlots>(PlotsSubmenuText);
      Load<KeyGuiTraits>(TraitsSubmenuText);
      Load<KeyGuiCrabzilla>(CrabzillaSubmenuText);
      Load<KeyGuiClans>(ClansSubmenuText);
      Load<KeyGuiAlliances>(AlliancesSubmenuText);
      Load<KeyGuiGameSettings>(GameSettingsSubmenuText);
      Load<KeyGuiZones>(ZonesSubmenuText);
      Load<KeyGuiCities>(CitiesSubmenuText);
      Load<KeyGuiBuildings>(BuildingsSubmenuText);
      Load<KeyGuiLocales>(LocalesSubmenuText);
      Load(ModConfigSubmenuText, KeyGuiModConfig.GetInstance());
      if (KeyGuiConfig.DebugIsLegal) {
        Load<KeyGuiDebug>(DebugSubmenuText, 10999_0);
        Load<KeyGuiTesting>(TestingSubmenuText, 10999_1);
      }
    }

    protected override void LoadGUI(int windowID) {
      if (!_modIsUpToDate) {
        GUILayout.Label(KeyGuiOutdatedWarning, new GUIStyle {
          alignment = TextAnchor.MiddleCenter,
          normal = new GUIStyleState {
            textColor = Color.white
          }
        });
        GUILayout.BeginHorizontal();
        if (GUILayout.Button(DownloadKeyGuiButton)) {
          Application.OpenURL("https://gamebanana.com/mods/438228");
        }

        if (GUILayout.Button(DismissKeyGuiOutdatedWarningButton)) {
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
            string.Format(ProblematicModWarning, modAndReason.mod, modAndReason.reason),
            new GUIStyle {
              alignment = TextAnchor.MiddleCenter,
              normal = new GUIStyleState {
                textColor = Color.red
              }
            }
          );
          if (GUILayout.Button(DismissProblematicModWarningButton)) {
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
            string.Format(CriticalModWarning, modAndReason.mod, modAndReason.reason),
            new GUIStyle {
              alignment = TextAnchor.MiddleCenter,
              normal = new GUIStyleState {
                textColor = Color.red
              }
            }
          );
          GUILayout.BeginHorizontal();
          if (GUILayout.Button(DismissCriticalModWarningButton)) {
            criticalModWarningsToDismiss.Add(modAndReason.mod);
            MenuRect.height = 0;
            MenuRect.width = 0;
          }

          if (GUILayout.Button(DisableCriticalModWarningButton)) {
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

        LoadSubMenuToggles();
      }
    }
  }
}
