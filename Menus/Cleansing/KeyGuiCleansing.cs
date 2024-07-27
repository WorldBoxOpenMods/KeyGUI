using System;
using System.Collections.Generic;
using System.Linq;
using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.BehaviourManipulation;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Cleansing {
  public class KeyGuiCleansing : KeyGuiMenu {
    private static readonly List<(BiomeAsset biome, bool enabled)> BiomeTypes = new List<(BiomeAsset biome, bool enabled)>();

    internal override void Initialize() {
      base.Initialize();
      if (Config.gameLoaded && BiomeTypes.Count == 0) {
        foreach (BiomeAsset biome in AssetManager.biome_library.list) {
          BiomeTypes.Add((biome, true));
        }
      }
    }

    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.GetRidOfMushSporesButton))) {
        KeyLib.Get<KeyGenLibWorldCleansingMethodCollection>().RemoveMushSpores();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.CureThePlagueButton))) {
        KeyLib.Get<KeyGenLibWorldCleansingMethodCollection>().RemoveThePlague();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.KillEveryTumorButton))) {
        KeyLib.Get<KeyGenLibWorldCleansingMethodCollection>().RemoveTumors();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.ExterminateZombiesButton))) {
        KeyLib.Get<KeyGenLibWorldCleansingMethodCollection>().RemoveZombies();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.DeleteAllRoadsButton))) {
        KeyLib.Get<KeyGenLibWorldCleansingMethodCollection>().RemoveRoads();
      }
      
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.LowerAllMountainsButton))) {
        KeyLib.Get<KeyGenLibWorldCleansingMethodCollection>().LowerMountains();
      }
      
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.LowerAllHillsButton))) {
        KeyLib.Get<KeyGenLibWorldCleansingMethodCollection>().LowerHills();
      }
      
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.DestroyAllBrokenBuildingsButton))) {
        KeyLib.Get<KeyGenLibWorldCleansingMethodCollection>().RemoveBrokenBuildings();
      }

      GUILayout.Label(Locales.Get(Locales.KeyGui.Cleansing.BiomesCategoryHeader));

      GUILayout.Label(Locales.Get(Locales.KeyGui.Cleansing.BiomeDeletionListHeader));
      if (GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Cleansing.GetRidOfBiomeButton), "Grass"))) {
        KeyLib.Get<KeyGenLibWorldTileManipulationMethodCollection>().ChangeSpecificWorldTiles(MapAction.removeGreens, tile => tile.cur_tile_type.biome_asset.id == ST.biome_grass);
      }

      for (int i = 0; i < BiomeTypes.Count; ++i) {
        if (BiomeTypes[i].enabled) {
          BiomeAsset biome = BiomeTypes[i].biome;
          if (GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Cleansing.GetRidOfBiomeButton), $"{char.ToUpper(biome.id[0])}{biome.id.Substring(1)}"))) {
            KeyLib.Get<KeyGenLibWorldTileManipulationMethodCollection>().ChangeSpecificWorldTiles(MapAction.removeGreens, tile => tile.cur_tile_type.biome_asset == biome);
          }
        }
      }

      GUILayout.Label(Locales.Get(Locales.KeyGui.Cleansing.BiomeRandomizationListHeader));

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.RandomizeBiomesButton))) {
        for (int i = 0; i < 7; ++i) {
          List<Action<WorldTile, string>> actions = new List<Action<WorldTile, string>>();
          for (int j = 0; j < BiomeTypes.Count; ++j) {
            if (BiomeTypes[j].enabled) {
              if (BiomeTypes[j].biome.id == ST.biome_grass) actions.Add(DropsLibrary.action_seeds_grass);
              else if (BiomeTypes[j].biome.id == ST.biome_savanna) actions.Add(DropsLibrary.action_seeds_savanna);
              else if (BiomeTypes[j].biome.id == ST.biome_mushroom) actions.Add(DropsLibrary.action_seeds_mushroom);
              else if (BiomeTypes[j].biome.id == ST.biome_enchanted) actions.Add(DropsLibrary.action_seeds_enchanted);
              else if (BiomeTypes[j].biome.id == ST.biome_corrupted) actions.Add(DropsLibrary.action_seeds_corrupted);
              else if (BiomeTypes[j].biome.id == ST.biome_swamp) actions.Add(DropsLibrary.action_seeds_swamp);
              else if (BiomeTypes[j].biome.id == ST.biome_infernal) actions.Add(DropsLibrary.action_seeds_infernal);
              else if (BiomeTypes[j].biome.id == ST.biome_jungle) actions.Add(DropsLibrary.action_seeds_jungle);
              else if (BiomeTypes[j].biome.id == ST.biome_candy) actions.Add(DropsLibrary.action_seeds_candy);
              else if (BiomeTypes[j].biome.id == ST.biome_desert) actions.Add(DropsLibrary.action_seeds_desert);
              else if (BiomeTypes[j].biome.id == ST.biome_crystal) actions.Add(DropsLibrary.action_seeds_crystal);
              else if (BiomeTypes[j].biome.id == ST.biome_lemon) actions.Add(DropsLibrary.action_seeds_lemon);
              else if (BiomeTypes[j].biome.id == ST.biome_permafrost) actions.Add(DropsLibrary.action_seeds_permafrost);
            }
          }

          BiomeAsset[] enabledBiomes = BiomeTypes.Where(((tuple, _) => tuple.enabled)).Select(((tuple, _) => tuple.biome)).ToArray();
          KeyLib.Get<KeyGenLibWorldTileManipulationMethodCollection>().ChangeSpecificWorldTiles(actions[UnityEngine.Random.Range(0, actions.Count - 1)], tile => tile.cur_tile_type.biome_asset == enabledBiomes[UnityEngine.Random.Range(0, enabledBiomes.Length - 1)]);
        }
      }

      GUILayout.Label(Locales.Get(Locales.KeyGui.Cleansing.BiomeRandomizationBiomeEnablingDisablingListHeader));

      for (int i = 0; i < BiomeTypes.Count; ++i) {
        switch (BiomeTypes[i].enabled) {
          case true when GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Cleansing.BiomeRandomizationDisableBiomeButton), $"{char.ToUpper(BiomeTypes[i].biome.id[0])}{BiomeTypes[i].biome.id.Substring(1)}")):
            BiomeTypes[i] = (BiomeTypes[i].biome, true);
            break;
          case false when GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Cleansing.BiomeRandomizationEnableBiomeButton), $"{char.ToUpper(BiomeTypes[i].biome.id[0])}{BiomeTypes[i].biome.id.Substring(1)}")):
            BiomeTypes[i] = (BiomeTypes[i].biome, false);
            break;
        }
      }
    }
  }
}
