using System.Linq;
using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.Assets;
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

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Destruction.CauseEveryDisasterButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnHeatwave();
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnEarthquake();
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnTornado();
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnMadThought();
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnMeteorite();
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnEvilMage();
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnSnowmen();
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnDemons();
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnBandits();
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnFrozenOnes();
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnNecromancer();
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnDragon();
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnAliens();
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnBiomass();
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnTumor();
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnGardenSurprise();
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnGreg();
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
