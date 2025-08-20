using System.Collections.Generic;
using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using UnityEngine;

namespace KeyGUI.Menus.Plots {
  public class KeyGuiPlots : KeyGuiMenu {
    public readonly KeyGuiLocale CancelPlotCategoryHeader = "Cancel a plot";
    public readonly KeyGuiLocale TogglePlotsCategoryHeader = "Toggle plots";
    public readonly KeyGuiLocale AllowWarsButton = "Allow Wars";
    public readonly KeyGuiLocale DisallowWarsButton = "Disallow Wars";
    public readonly KeyGuiLocale AllowRebellionsButton = "Allow Rebellions";
    public readonly KeyGuiLocale DisallowRebellionsButton = "Disallow Rebellions";
    public readonly KeyGuiLocale AllowAllianceCreationButton = "Allow Alliance Creation";
    public readonly KeyGuiLocale DisallowAllianceCreationButton = "Disallow Alliance Creation";
    public readonly KeyGuiLocale AllowAllianceJoiningButton = "Allow Alliance Joining";
    public readonly KeyGuiLocale DisallowAllianceJoiningButton = "Disallow Alliance Joining";
    public readonly KeyGuiLocale AllowAllianceBreakingButton = "Allow Alliance Breaking";
    public readonly KeyGuiLocale DisallowAllianceBreakingButton = "Disallow Alliance Breaking";
    public readonly KeyGuiLocale AllowWarStoppingButton = "Allow War Stopping";
    public readonly KeyGuiLocale DisallowWarStoppingButton = "Disallow War Stopping";
    public readonly KeyGuiLocale AllowTotalWarsButton = "Allow Total Wars";
    public readonly KeyGuiLocale DisallowTotalWarsButton = "Disallow Total Wars";

    private readonly List<string> _bannedPlots = new List<string>();

    protected override void UpdateMenu() {
      base.UpdateMenu();
      foreach (Plot plot in from plotType in _bannedPlots from plot in World.world.plots.list where plot.data.plot_type_id == plotType select plot) {
        plot._state = PlotState.Cancelled;
        plot.setAlive(false);
      }
    }

    protected override void LoadGUI(int windowID) {
      GUILayout.Label(CancelPlotCategoryHeader);
      foreach (Plot t in World.world.plots.list.Where(t => GUILayout.Button(t.name))) {
        Debug.Log(t.data.plot_type_id);
        t._state = PlotState.Cancelled;
        t.setAlive(false);
      }

      GUILayout.Label(TogglePlotsCategoryHeader);
      if (_bannedPlots.Contains("new_war")) {
        if (GUILayout.Button(AllowWarsButton)) {
          _bannedPlots.Remove("new_war");
        }
      } else if (GUILayout.Button(DisallowWarsButton)) {
        _bannedPlots.Add("new_war");
      }

      if (_bannedPlots.Contains("rebellion")) {
        if (GUILayout.Button(AllowRebellionsButton)) {
          _bannedPlots.Remove("rebellion");
        }
      } else if (GUILayout.Button(DisallowRebellionsButton)) {
        _bannedPlots.Add("rebellion");
      }

      if (_bannedPlots.Contains("alliance_create")) {
        if (GUILayout.Button(AllowAllianceCreationButton)) {
          _bannedPlots.Remove("alliance_create");
        }
      } else if (GUILayout.Button(DisallowAllianceCreationButton)) {
        _bannedPlots.Add("alliance_create");
      }

      if (_bannedPlots.Contains("alliance_join")) {
        if (GUILayout.Button(AllowAllianceJoiningButton)) {
          _bannedPlots.Remove("alliance_join");
        }
      } else if (GUILayout.Button(DisallowAllianceJoiningButton)) {
        _bannedPlots.Add("alliance_join");
      }

      if (_bannedPlots.Contains("alliance_break")) {
        if (GUILayout.Button(AllowAllianceBreakingButton)) {
          _bannedPlots.Remove("alliance_break");
        }
      } else if (GUILayout.Button(DisallowAllianceBreakingButton)) {
        _bannedPlots.Add("alliance_break");
      }

      if (_bannedPlots.Contains("stop_war")) {
        if (GUILayout.Button(AllowWarStoppingButton)) {
          _bannedPlots.Remove("stop_war");
        }
      } else if (GUILayout.Button(DisallowWarStoppingButton)) {
        _bannedPlots.Add("stop_war");
      }

      if (_bannedPlots.Contains("total_war")) {
        if (GUILayout.Button(AllowTotalWarsButton)) {
          _bannedPlots.Remove("total_war");
        }
      } else if (GUILayout.Button(DisallowTotalWarsButton)) {
        _bannedPlots.Add("total_war");
      }
    }
  }
}
