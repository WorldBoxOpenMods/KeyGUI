using System.Linq;
using KeyGUI.Framework.Menus;
using strings;
using UnityEngine;

namespace KeyGUI.Menus.Debugging {
  internal class KeyGuiDebug : KeyGuiMenu {
    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button("Calculate Mobile Best Greg Odds Specific")) {
        int totalDisasterPoolAmount = AssetManager.disasters.list.Where(asset => asset.type != DisasterType.Nature).Where(asset => !asset.ages_allow.Any() || asset.ages_allow.Contains(S.age_despair)).Where(asset => asset.min_world_cities <= 3).Where(asset => asset.min_world_population <= 1000).Where(asset => !asset.premium_only).Select(asset => asset.rate).Sum();
        Debug.Log("Specific total mobile disaster pool amount: " + totalDisasterPoolAmount);
      }

      if (GUILayout.Button("Calculate PC Best Greg Odds Specific")) {
        int totalDisasterPoolAmount = AssetManager.disasters.list.Where(asset => asset.type != DisasterType.Nature).Where(asset => !asset.ages_allow.Any() || asset.ages_allow.Contains(S.age_despair)).Where(asset => asset.min_world_cities <= 3).Where(asset => asset.min_world_population <= 1000).Select(asset => asset.rate).Sum();
        Debug.Log("Specific total PC disaster pool amount: " + totalDisasterPoolAmount);
      }

      if (GUILayout.Button("Calculate Mobile Best Greg Odds Broad")) {
        int totalDisasterPoolAmount = AssetManager.disasters.list.Where(asset => asset.type != DisasterType.Nature).Where(asset => !asset.ages_allow.Any() || asset.ages_allow.Contains(S.age_despair)).Where(asset => !asset.premium_only).Select(asset => asset.rate).Sum();
        Debug.Log("Broad total mobile disaster pool amount: " + totalDisasterPoolAmount);
      }

      if (GUILayout.Button("Calculate PC Best Greg Odds Broad")) {
        int totalDisasterPoolAmount = AssetManager.disasters.list.Where(asset => asset.type != DisasterType.Nature).Where(asset => !asset.ages_allow.Any() || asset.ages_allow.Contains(S.age_despair)).Select(asset => asset.rate).Sum();
        Debug.Log("Broad total PC disaster pool amount: " + totalDisasterPoolAmount);
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
