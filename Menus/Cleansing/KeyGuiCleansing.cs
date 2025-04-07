using System;
using System.Collections.Generic;
using System.Linq;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Cleansing {
  public class KeyGuiCleansing : KeyGuiMenu {
    private static readonly List<(BiomeAsset biome, bool enabled)> BiomeTypes = new List<(BiomeAsset biome, bool enabled)>();

    protected override void InitializeMenu() {
      base.InitializeMenu();
      if (Config.game_loaded && BiomeTypes.Count == 0) {
        foreach (BiomeAsset biome in AssetManager.biome_library.list) {
          BiomeTypes.Add((biome, true));
        }
      }
    }

    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.GetRidOfMushSporesButton))) {
        foreach (Actor unit in World.world.units) {
          if (unit.hasTrait(S_Trait.mush_spores)) {
            unit.removeTrait(S_Trait.mush_spores);
          }
          if (unit.asset.id == SA.mush_unit || unit.asset.id == SA.mush_animal) {
            unit.die();
          }
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.CureThePlagueButton))) {
        foreach (Actor unit in World.world.units.Where(unit => unit.hasTrait(S_Trait.plague))) {
          unit.removeTrait(S_Trait.plague);
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.KillEveryTumorButton))) {
        foreach (Actor unit in World.world.units) {
          if (unit.hasTrait(S_Trait.tumor_infection)) {
            unit.removeTrait(S_Trait.tumor_infection);
          }
          if (unit.asset.id == SA.tumor_monster_unit || unit.asset.id == SA.tumor_monster_animal) {
            unit.die();
          }
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.ExterminateZombiesButton))) {
        foreach (Actor unit in World.world.units) {
          if (unit.hasTrait(S_Trait.infected)) {
            unit.removeTrait(S_Trait.infected);
          }
          if (unit.hasTrait(S_Trait.zombie)) {
            unit.die();
          } else if (unit.asset.id == SA.zombie || unit.asset.id == SA.zombie_animal || unit.asset.id == SA.zombie_dragon) {
            unit.die();
          }
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.DeleteAllRoadsButton))) {
        foreach (WorldTile tile in World.world.tiles_list.Where(tile => tile.Type.road)) {
          tile.setTopTileType(TopTileLibrary.grass_high);
          MapAction.removeGreens(tile);
        }
        World.world.roads_calculator.clear();
      }
      
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.LowerAllMountainsButton))) {
        foreach (WorldTile tile in World.world.tiles_list.Where(tile => tile.Type.mountains)) {
          tile.setTileType(TileLibrary.hills);
          tile.unfreeze();
        }
      }
      
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.LowerAllHillsButton))) {
        foreach (WorldTile tile in World.world.tiles_list) {
          if (tile.top_type == TopTileLibrary.snow_hills) {
            tile.setTopTileType(TopTileLibrary.grass_high);
          }
          if (tile.Type == TileLibrary.hills) {
            tile.setTileType(TileLibrary.soil_high);
          }
        }
      }
      
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.DestroyAllBrokenBuildingsButton))) {
        foreach (Building building in World.world.buildings.Where(building => building.data.state == BuildingState.Ruins || building.data.state == BuildingState.CivAbandoned)) {
          building.startRemove();
        }
      }

      GUILayout.Label(Locales.Get(Locales.KeyGui.Cleansing.BiomesCategoryHeader));

      GUILayout.Label(Locales.Get(Locales.KeyGui.Cleansing.BiomeDeletionListHeader));
      if (GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Cleansing.GetRidOfBiomeButton), "Grass"))) {
        foreach (WorldTile tile in World.world.tiles_list.Where(tile => tile != null && tile.cur_tile_type.biome_asset.id == ST.biome_grass)) {
          MapAction.removeGreens(tile);
        }
      }

      for (int i = 0; i < BiomeTypes.Count; ++i) {
        if (BiomeTypes[i].enabled) {
          BiomeAsset biome = BiomeTypes[i].biome;
          if (GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Cleansing.GetRidOfBiomeButton), $"{char.ToUpper(biome.id[0])}{biome.id.Substring(1)}"))) {
            foreach (WorldTile tile in World.world.tiles_list.Where(tile => tile != null && tile.cur_tile_type.biome_asset == biome)) {
              MapAction.removeGreens(tile);
            }
          }
        }
      }

      GUILayout.Label(Locales.Get(Locales.KeyGui.Cleansing.BiomeRandomizationListHeader));

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cleansing.RandomizeBiomesButton))) {
        for (int i = 0; i < 7; ++i) {
          List<Action<WorldTile, string>> actions = new List<Action<WorldTile, string>>();
          for (int j = 0; j < BiomeTypes.Count; ++j) {
            if (BiomeTypes[j].enabled) {
              string biomeId = BiomeTypes[j].biome.id;
              actions.Add((wt, _) => DropsLibrary.action_drop_seeds(wt, biomeId.Replace("biome_", "seeds_")));
            }
          }

          BiomeAsset[] enabledBiomes = BiomeTypes.Where(((tuple, _) => tuple.enabled)).Select(((tuple, _) => tuple.biome)).ToArray();
          foreach (WorldTile tile1 in World.world.tiles_list.Where(tile => tile != null && tile.cur_tile_type.biome_asset == enabledBiomes[UnityEngine.Random.Range(0, enabledBiomes.Length - 1)])) {
            actions[UnityEngine.Random.Range(0, actions.Count - 1)](tile1, null);
          }
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
