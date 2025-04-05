using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.BehaviourManipulation;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Destruction {
  public class KeyGuiDestruction : KeyGuiMenu {
    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Destruction.MakeEverythingGoMadButton))) {
        foreach (Actor unit in World.world.units) {
          unit.addTrait(S_Trait.madness, true);
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Destruction.CauseAMassBombingButton))) {
        foreach (Actor unit in World.world.units) {
          ActionLibrary.deathNuke(unit, unit.current_tile);
          ActionLibrary.deathMark(unit, unit.current_tile);
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Destruction.MassInfectEverythingWithMushSporesButton))) {
        foreach (Actor unit in World.world.units) {
          unit.addTrait(S_Trait.mush_spores, true);
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Destruction.ChangeEveryBiomeToTheSwampBiomeButton))) {
        KeyLib.Get<KeyGenLibWorldTileManipulationMethodCollection>().ChangeEveryWorldTile((wt, _) => DropsLibrary.action_drop_seeds(wt, S_Drop.seeds_swamp));
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Destruction.ChangeEveryBiomeToTheCorruptedBiomeButton))) {
        KeyLib.Get<KeyGenLibWorldTileManipulationMethodCollection>().ChangeEveryWorldTile((wt, _) => DropsLibrary.action_drop_seeds(wt, S_Drop.seeds_corrupted));
      }
    }
  }
}
