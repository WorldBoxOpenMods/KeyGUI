using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using strings;
using UnityEngine;

namespace KeyGUI.Menus.Destruction {
  public class KeyGuiDestruction : KeyGuiMenu {
    public readonly KeyGuiLocale MakeEverythingGoMadButton = "Make Everything Go Mad";
    public readonly KeyGuiLocale CauseAMassBombingButton = "Cause A Mass Bombing";
    public readonly KeyGuiLocale MassInfectEverythingWithMushSporesButton = "Mass Infect Everything With Mush Spores";
    public readonly KeyGuiLocale ChangeEveryBiomeToTheSwampBiomeButton = "Change Every Biome To The Swamp Biome";
    public readonly KeyGuiLocale ChangeEveryBiomeToTheCorruptedBiomeButton = "Change Every Biome To The Corrupted Biome";
    
    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(MakeEverythingGoMadButton)) {
        foreach (Actor unit in World.world.units) {
          unit.addTrait(S_Trait.madness, true);
        }
      }

      if (GUILayout.Button(CauseAMassBombingButton)) {
        foreach (Actor unit in World.world.units) {
          ActionLibrary.deathNuke(unit, unit.current_tile);
          ActionLibrary.deathMark(unit, unit.current_tile);
        }
      }

      if (GUILayout.Button(MassInfectEverythingWithMushSporesButton)) {
        foreach (Actor unit in World.world.units) {
          unit.addTrait(S_Trait.mush_spores, true);
        }
      }

      if (GUILayout.Button(ChangeEveryBiomeToTheSwampBiomeButton)) {
        foreach (WorldTile tile in World.world.tiles_list.Where(tile => tile != null)) {
          DropsLibrary.action_drop_seeds(tile, S_Drop.seeds_swamp);
        }
      }

      if (GUILayout.Button(ChangeEveryBiomeToTheCorruptedBiomeButton)) {
        foreach (WorldTile tile in World.world.tiles_list.Where(tile => tile != null)) {
          DropsLibrary.action_drop_seeds(tile, S_Drop.seeds_corrupted);
        }
      }
    }
  }
}
