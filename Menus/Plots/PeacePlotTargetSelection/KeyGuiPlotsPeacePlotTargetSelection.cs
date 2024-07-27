using System.Linq;
using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.Assets;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Plots.PeacePlotTargetSelection {
  public class KeyGuiPlotsPeacePlotTargetSelection : KeyGuiMenu {

    protected override void LoadGUI(int windowID) {
      foreach (War war in World.world.wars.list.Where(t => GUILayout.Button(t.name))) {
        Actor initActor = war.main_defender?.king;
        if (war._asset.total_war) {
          initActor = war.main_attacker.king;
        }
        KeyLib.Get<KeyGenLibCustomPlotCreator>().PeacePlot(KeyLib.Get<KeyGenLibCustomPlotLibrary>()[KeyGenLibCustomPlotLibrary.PeacePlotIndex], initActor, war);
        Enabled = false;
      }
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Plots.PeacePlotTargetSelection.CloseMenuButton))) {
        Enabled = false;
      }
    }
  }
}