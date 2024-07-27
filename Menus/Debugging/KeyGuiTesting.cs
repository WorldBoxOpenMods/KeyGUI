using KeyGUI.MenuArchitecture;
using UnityEngine;

namespace KeyGUI.Menus.Debugging {
  internal class KeyGuiTesting : KeyGuiMenu {
    // ReSharper disable Unity.PerformanceAnalysis
    protected override void LoadGUI(int windowID) { 
        /*
      if (GUILayout.Button("Reduce Orc Health")) {
        KeyLib.Get<KeyGenLibRaceManipulationMethodCollection>().ModifySpecificRaceStat(S.health, SK.orc, -5);
      }
      */

      if (GUILayout.Button("Ok so how big are the trait sprites?")) {
        Debug.Log(AssetManager.traits.list[4].getSprite().texture.width);
      }

      /*
      if (GUILayout.Button("Cure The Plague")) {
          for (int i = 0; i < World.world.mapStats.id_unit; ++i) {
              Actor unit = World.world.units.get("u_" + i);
              if (unit != null) {
                  unit.removeTrait("plague");
              }
          }
      }
      if (GUILayout.Button("Kill Every Tumor")) {
          for (int i = 0; i < World.world.mapStats.id_unit; ++i) {
              Actor unit = World.world.units.get("u_" + i);
              if (unit != null) {
                  unit.removeTrait("tumorInfection");
              }
          }
      }
      if (GUILayout.Button("Exterminate Zombies")) {
          for (int i = 0; i < World.world.mapStats.id_unit; ++i) {
              Actor unit = World.world.units.get("u_" + i);
              if (unit != null) {
                  unit.removeTrait("infected");
              }
          }
      }
      */
    }
  }
}