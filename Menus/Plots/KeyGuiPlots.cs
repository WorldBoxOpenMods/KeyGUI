using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.Assets;
using KeyGeneralPurposeLibrary.Utils;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations;
using KeyGUI.Menus.Localizations.Declarations;
using KeyGUI.Menus.Plots.PeacePlotTargetSelection;
using UnityEngine;

namespace KeyGUI.Menus.Plots {
  public class KeyGuiPlots : KeyGuiMenuManager {
    private KeyGuiInfoSelector _warSelector;
    private KeyGuiInfoSelector _rebellionSelector;
    private KeyGuiInfoSelector _allianceCreationSelector;
    private KeyGuiInfoSelector _allianceJoinSelector;
    private KeyGuiInfoSelector _allianceBreakSelector;
    private KeyGuiInfoSelector _totalWarSelector;

    private readonly List<string> _bannedPlots = new List<string>();

    protected override void InitializeMenu() {
      base.InitializeMenu();
      _warSelector = new KeyGuiInfoSelector(
        new List<LocaleDeclaration> { Locales.KeyGui.Plots.WarSelectorInitiatorKingdomLabel, Locales.KeyGui.Plots.WarSelectorTargetKingdomLabel },
        new List<object> { KeyLib.Get<KeyGenLibCustomPlotLibrary>()[KeyGenLibCustomPlotLibrary.GenericWarPlotIndex] },
        new List<Type> { typeof(Actor), typeof(Kingdom) },
        new List<MapMode> { MapMode.Kingdoms, MapMode.Kingdoms },
        new List<List<MemberInfo>> { new List<MemberInfo> { typeof(WorldTile).GetInstance<FieldInfo>(nameof(WorldTile.zone)), typeof(TileZone).GetInstance<FieldInfo>(nameof(TileZone.city)), typeof(City).GetInstance<FieldInfo>(nameof(City.kingdom)), typeof(Kingdom).GetInstance<FieldInfo>(nameof(Kingdom.king)) }, new List<MemberInfo> { typeof(WorldTile).GetInstance<FieldInfo>(nameof(WorldTile.zone)), typeof(TileZone).GetInstance<FieldInfo>(nameof(TileZone.city)), typeof(City).GetInstance<FieldInfo>(nameof(City.kingdom)) } },
        KeyLib.Get<KeyGenLibCustomPlotCreator>().GetType().GetMethod(nameof(KeyGenLibCustomPlotCreator.CreateWarPlot)),
        KeyLib.Get<KeyGenLibCustomPlotCreator>()
      );
      _rebellionSelector = new KeyGuiInfoSelector(
        new List<LocaleDeclaration> { Locales.KeyGui.Plots.RebellionSelectorInitiatorCityLabel },
        new List<object> { KeyLib.Get<KeyGenLibCustomPlotLibrary>()[KeyGenLibCustomPlotLibrary.RebellionPlotIndex] },
        new List<Type> { typeof(Actor) },
        new List<MapMode> { MapMode.Cities },
        new List<List<MemberInfo>> { new List<MemberInfo> { typeof(WorldTile).GetInstance<FieldInfo>(nameof(WorldTile.zone)), typeof(TileZone).GetInstance<FieldInfo>(nameof(TileZone.city)), typeof(City).GetInstance<FieldInfo>(nameof(City.leader)) } },
        KeyLib.Get<KeyGenLibCustomPlotCreator>().GetType().GetMethod(nameof(KeyGenLibCustomPlotCreator.CreateRebellionPlot)),
        KeyLib.Get<KeyGenLibCustomPlotCreator>()
      );
      _allianceCreationSelector = new KeyGuiInfoSelector(
        new List<LocaleDeclaration> { Locales.KeyGui.Plots.AllianceCreationSelectorInitiatorKingdomLabel, Locales.KeyGui.Plots.AllianceCreationSelectorTargetKingdomLabel },
        new List<object> { KeyLib.Get<KeyGenLibCustomPlotLibrary>()[KeyGenLibCustomPlotLibrary.AllianceCreationPlotIndex] },
        new List<Type> { typeof(Actor), typeof(Kingdom) },
        new List<MapMode> { MapMode.Kingdoms, MapMode.Kingdoms },
        new List<List<MemberInfo>> { new List<MemberInfo> { typeof(WorldTile).GetInstance<FieldInfo>(nameof(WorldTile.zone)), typeof(TileZone).GetInstance<FieldInfo>(nameof(TileZone.city)), typeof(City).GetInstance<FieldInfo>(nameof(City.kingdom)), typeof(Kingdom).GetInstance<FieldInfo>(nameof(Kingdom.king)) }, new List<MemberInfo> { typeof(WorldTile).GetInstance<FieldInfo>(nameof(WorldTile.zone)), typeof(TileZone).GetInstance<FieldInfo>(nameof(TileZone.city)), typeof(City).GetInstance<FieldInfo>(nameof(City.kingdom)) } },
        KeyLib.Get<KeyGenLibCustomPlotCreator>().GetType().GetMethod(nameof(KeyGenLibCustomPlotCreator.AllianceCreationPlot)),
        KeyLib.Get<KeyGenLibCustomPlotCreator>()
      );
      _allianceJoinSelector = new KeyGuiInfoSelector(
        new List<LocaleDeclaration> { Locales.KeyGui.Plots.AllianceJoinSelectorInitiatorKingdomLabel, Locales.KeyGui.Plots.AllianceJoinSelectorTargetAllianceLabel },
        new List<object> { KeyLib.Get<KeyGenLibCustomPlotLibrary>()[KeyGenLibCustomPlotLibrary.AllianceJoinPlotIndex] },
        new List<Type> { typeof(Actor), typeof(Alliance) },
        new List<MapMode> { MapMode.Kingdoms, MapMode.Alliances },
        new List<List<MemberInfo>> { new List<MemberInfo> { typeof(WorldTile).GetInstance<FieldInfo>(nameof(WorldTile.zone)), typeof(TileZone).GetInstance<FieldInfo>(nameof(TileZone.city)), typeof(City).GetInstance<FieldInfo>(nameof(City.kingdom)), typeof(Kingdom).GetInstance<FieldInfo>(nameof(Kingdom.king)) }, new List<MemberInfo> { typeof(WorldTile).GetInstance<FieldInfo>(nameof(WorldTile.zone)), typeof(TileZone).GetInstance<FieldInfo>(nameof(TileZone.city)), typeof(City).GetInstance<FieldInfo>(nameof(City.kingdom)), typeof(Kingdom).GetInstance<MethodInfo>(nameof(Kingdom.getAlliance)) } },
        KeyLib.Get<KeyGenLibCustomPlotCreator>().GetType().GetMethod(nameof(KeyGenLibCustomPlotCreator.JoinAlliancePlot)),
        KeyLib.Get<KeyGenLibCustomPlotCreator>()
      );
      _allianceBreakSelector = new KeyGuiInfoSelector(
        new List<LocaleDeclaration> { Locales.KeyGui.Plots.AllianceBreakSelectorInitiatorKingdomLabel, Locales.KeyGui.Plots.AllianceBreakSelectorTargetAllianceLabel },
        new List<object> { KeyLib.Get<KeyGenLibCustomPlotLibrary>()[KeyGenLibCustomPlotLibrary.AllianceDisbandPlotIndex] },
        new List<Type> { typeof(Actor) },
        new List<MapMode> { MapMode.Alliances },
        new List<List<MemberInfo>> { new List<MemberInfo> { typeof(WorldTile).GetInstance<FieldInfo>(nameof(WorldTile.zone)), typeof(TileZone).GetInstance<FieldInfo>(nameof(TileZone.city)), typeof(City).GetInstance<FieldInfo>(nameof(City.kingdom)), typeof(Kingdom).GetInstance<FieldInfo>(nameof(Kingdom.king)) } },
        KeyLib.Get<KeyGenLibCustomPlotCreator>().GetType().GetMethod(nameof(KeyGenLibCustomPlotCreator.DisbandAlliancePlot)),
        KeyLib.Get<KeyGenLibCustomPlotCreator>()
      );
      _totalWarSelector = new KeyGuiInfoSelector(
        new List<LocaleDeclaration> { Locales.KeyGui.Plots.TotalWarSelectorInitiatorKingdomLabel },
        new List<object> { KeyLib.Get<KeyGenLibCustomPlotLibrary>()[KeyGenLibCustomPlotLibrary.WorldWarPlotIndex] },
        new List<Type> { typeof(Actor) },
        new List<MapMode> { MapMode.Kingdoms },
        new List<List<MemberInfo>> { new List<MemberInfo> { typeof(WorldTile).GetInstance<FieldInfo>(nameof(WorldTile.zone)), typeof(TileZone).GetInstance<FieldInfo>(nameof(TileZone.city)), typeof(City).GetInstance<FieldInfo>(nameof(City.kingdom)), typeof(Kingdom).GetInstance<FieldInfo>(nameof(Kingdom.king)) } },
        KeyLib.Get<KeyGenLibCustomPlotCreator>().GetType().GetMethod(nameof(KeyGenLibCustomPlotCreator.CreateTotalWarPlot)),
        KeyLib.Get<KeyGenLibCustomPlotCreator>()
      );
    }

    protected override void UpdateMenu() {
      base.UpdateMenu();
      foreach (Plot plot in from plotType in _bannedPlots from plot in World.world.plots.list where plot.data.plot_type_id == plotType select plot) plot.setState(PlotState.Cancelled);
    }
    internal override void AddSubMenus() {
      Load<KeyGuiPlotsPeacePlotTargetSelection>(Locales.KeyGui.Plots.PeacePlotTargetSelectionSubmenuName);
    }

    protected override void LoadGUI() {
      GUILayout.Label(Locales.Get(Locales.KeyGui.Plots.CreatePlotCategoryHeader));
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.CreateWarPlotButton))) {
        _warSelector.Invoke();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.CreateRebellionPlotButton))) {
        _rebellionSelector.Invoke();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.CreateAllianceCreationPlotButton))) {
        _allianceCreationSelector.Invoke();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.CreateAllianceJoinPlotButton))) {
        _allianceJoinSelector.Invoke();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.CreateAllianceBreakPlotButton))) {
        _allianceBreakSelector.Invoke();
      }
      
      LoadSubMenuToggles();
      
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.CreateTotalWarPlotButton))) {
        _totalWarSelector.Invoke();
      }

      GUILayout.Label(Locales.Get(Locales.KeyGui.Plots.CancelPlotCategoryHeader));
      foreach (Plot t in World.world.plots.list.Where(t => GUILayout.Button(t.name))) {
        Debug.Log(t.data.plot_type_id);
        t.setState(PlotState.Cancelled);
      }

      GUILayout.Label(Locales.Get(Locales.KeyGui.Plots.TogglePlotsCategoryHeader));
      if (_bannedPlots.Contains("new_war")) {
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.AllowWarsButton))) {
          _bannedPlots.Remove("new_war");
        }
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.DisallowWarsButton))) {
        _bannedPlots.Add("new_war");
      }

      if (_bannedPlots.Contains("rebellion")) {
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.AllowRebellionsButton))) {
          _bannedPlots.Remove("rebellion");
        }
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.DisallowRebellionsButton))) {
        _bannedPlots.Add("rebellion");
      }

      if (_bannedPlots.Contains("alliance_create")) {
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.AllowAllianceCreationButton))) {
          _bannedPlots.Remove("alliance_create");
        }
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.DisallowAllianceCreationButton))) {
        _bannedPlots.Add("alliance_create");
      }

      if (_bannedPlots.Contains("alliance_join")) {
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.AllowAllianceJoiningButton))) {
          _bannedPlots.Remove("alliance_join");
        }
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.DisallowAllianceJoiningButton))) {
        _bannedPlots.Add("alliance_join");
      }

      if (_bannedPlots.Contains("alliance_break")) {
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.AllowAllianceBreakingButton))) {
          _bannedPlots.Remove("alliance_break");
        }
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.DisallowAllianceBreakingButton))) {
        _bannedPlots.Add("alliance_break");
      }

      if (_bannedPlots.Contains("stop_war")) {
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.AllowWarStoppingButton))) {
          _bannedPlots.Remove("stop_war");
        }
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.DisallowWarStoppingButton))) {
        _bannedPlots.Add("stop_war");
      }

      if (_bannedPlots.Contains("total_war")) {
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.AllowTotalWarsButton))) {
          _bannedPlots.Remove("total_war");
        }
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.DisallowTotalWarsButton))) {
        _bannedPlots.Add("total_war");
      }
    }
  }
}
