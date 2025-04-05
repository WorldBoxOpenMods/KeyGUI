using System.Collections.Generic;
using System.Linq;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Plots {
  public class KeyGuiPlots : KeyGuiMenu {

    private readonly List<string> _bannedPlots = new List<string>();

    protected override void UpdateMenu() {
      base.UpdateMenu();
      foreach (Plot plot in from plotType in _bannedPlots from plot in World.world.plots.list where plot.data.plot_type_id == plotType select plot) {
        plot._state = PlotState.Cancelled;
        plot.setAlive(false);
      }
    }

    protected override void LoadGUI(int windowID) {
      GUILayout.Label(Locales.Get(Locales.KeyGui.Plots.CancelPlotCategoryHeader));
      foreach (Plot t in World.world.plots.list.Where(t => GUILayout.Button(t.name))) {
        Debug.Log(t.data.plot_type_id);
        t._state = PlotState.Cancelled;
        t.setAlive(false);
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
