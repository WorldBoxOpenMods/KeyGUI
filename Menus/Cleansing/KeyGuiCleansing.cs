using System;
using System.Collections.Generic;
using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using strings;
using UnityEngine;

namespace KeyGUI.Menus.Cleansing {
  public class KeyGuiCleansing : KeyGuiMenu {
    public readonly KeyGuiLocale GetRidOfMushSporesButton = "Get Rid Of Mush Spores";
    public readonly KeyGuiLocale CureThePlagueButton = "Cure The Plague";
    public readonly KeyGuiLocale KillEveryTumorButton = "Kill Every Tumor";
    public readonly KeyGuiLocale ExterminateZombiesButton = "Exterminate Zombies";
    public readonly KeyGuiLocale DeleteAllRoadsButton = "Delete All Roads";
    public readonly KeyGuiLocale LowerAllMountainsButton = "Lower All Mountains";
    public readonly KeyGuiLocale LowerAllHillsButton = "Lower All Hills";
    public readonly KeyGuiLocale DestroyAllBrokenBuildingsButton = "Destroy All Broken Buildings";
    public readonly KeyGuiLocale BiomesCategoryHeader = "Biomes";
    public readonly KeyGuiLocale BiomeDeletionListHeader = "Biome Deletion:";
    public readonly KeyGuiLocale GetRidOfBiomeButton = "Get Rid Of {0} Biome";
    public readonly KeyGuiLocale BiomeRandomizationListHeader = "Biome Randomization:";
    public readonly KeyGuiLocale RandomizeBiomesButton = "Randomize Biomes";
    public readonly KeyGuiLocale BiomeRandomizationBiomeEnablingDisablingListHeader = "Biome Enabling/Disabling:";
    public readonly KeyGuiLocale BiomeRandomizationEnableBiomeButton = "Enable {0} Biome";
    public readonly KeyGuiLocale BiomeRandomizationDisableBiomeButton = "Disable {0} Biome";
    
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
      if (GUILayout.Button(GetRidOfMushSporesButton)) {
        foreach (Actor unit in World.world.units) {
          if (unit.hasTrait(S_Trait.mush_spores)) {
            unit.removeTrait(S_Trait.mush_spores);
          }
          if (unit.asset.id == SA.mush_unit || unit.asset.id == SA.mush_animal) {
            unit.die();
          }
        }
      }

      if (GUILayout.Button(CureThePlagueButton)) {
        foreach (Actor unit in World.world.units.Where(unit => unit.hasTrait(S_Trait.plague))) {
          unit.removeTrait(S_Trait.plague);
        }
      }

      if (GUILayout.Button(KillEveryTumorButton)) {
        foreach (Actor unit in World.world.units) {
          if (unit.hasTrait(S_Trait.tumor_infection)) {
            unit.removeTrait(S_Trait.tumor_infection);
          }
          if (unit.asset.id == SA.tumor_monster_unit || unit.asset.id == SA.tumor_monster_animal) {
            unit.die();
          }
        }
      }

      if (GUILayout.Button(ExterminateZombiesButton)) {
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

      if (GUILayout.Button(DeleteAllRoadsButton)) {
        foreach (WorldTile tile in World.world.tiles_list.Where(tile => tile.Type.road)) {
          tile.setTopTileType(TopTileLibrary.grass_high);
          MapAction.removeGreens(tile);
        }
        World.world.roads_calculator.clear();
      }

      if (GUILayout.Button(LowerAllMountainsButton)) {
        foreach (WorldTile tile in World.world.tiles_list.Where(tile => tile.Type.mountains)) {
          tile.setTileType(TileLibrary.hills);
          tile.unfreeze();
        }
      }

      if (GUILayout.Button(LowerAllHillsButton)) {
        foreach (WorldTile tile in World.world.tiles_list) {
          if (tile.top_type == TopTileLibrary.snow_hills) {
            tile.setTopTileType(TopTileLibrary.grass_high);
          }
          if (tile.Type == TileLibrary.hills) {
            tile.setTileType(TileLibrary.soil_high);
          }
        }
      }

      if (GUILayout.Button(DestroyAllBrokenBuildingsButton)) {
        foreach (Building building in World.world.buildings.Where(building => building.data.state == BuildingState.Ruins || building.data.state == BuildingState.CivAbandoned)) {
          building.startRemove();
        }
      }

      GUILayout.Label(BiomesCategoryHeader);

      GUILayout.Label(BiomeDeletionListHeader);
      if (GUILayout.Button(string.Format(GetRidOfBiomeButton, "Grass"))) {
        foreach (WorldTile tile in World.world.tiles_list.Where(tile => tile != null && tile.cur_tile_type.biome_asset.id == ST.biome_grass)) {
          MapAction.removeGreens(tile);
        }
      }

      for (int i = 0; i < BiomeTypes.Count; ++i) {
        if (BiomeTypes[i].enabled) {
          BiomeAsset biome = BiomeTypes[i].biome;
          if (GUILayout.Button(string.Format(GetRidOfBiomeButton, $"{char.ToUpper(biome.id[0])}{biome.id.Substring(1)}"))) {
            foreach (WorldTile tile in World.world.tiles_list.Where(tile => tile != null && tile.cur_tile_type.biome_asset == biome)) {
              MapAction.removeGreens(tile);
            }
          }
        }
      }

      GUILayout.Label(BiomeRandomizationListHeader);

      if (GUILayout.Button(RandomizeBiomesButton)) {
        for (int i = 0; i < 7; ++i) {
          List<Action<WorldTile, string>> actions = new List<Action<WorldTile, string>>();
          for (int j = 0; j < BiomeTypes.Count; ++j) {
            if (BiomeTypes[j].enabled) {
              string biomeId = BiomeTypes[j].biome.id;
              actions.Add((wt, _) => DropsLibrary.action_drop_seeds(wt, biomeId.Replace("biome_", "seeds_")));
            }
          }

          BiomeAsset[] enabledBiomes = BiomeTypes.Where((tuple, _) => tuple.enabled).Select((tuple, _) => tuple.biome).ToArray();
          foreach (WorldTile tile1 in World.world.tiles_list.Where(tile => tile != null && tile.cur_tile_type.biome_asset == enabledBiomes[UnityEngine.Random.Range(0, enabledBiomes.Length - 1)])) {
            actions[UnityEngine.Random.Range(0, actions.Count - 1)](tile1, null);
          }
        }
      }

      GUILayout.Label(BiomeRandomizationBiomeEnablingDisablingListHeader);

      for (int i = 0; i < BiomeTypes.Count; ++i) {
        switch (BiomeTypes[i].enabled) {
          case true when GUILayout.Button(string.Format(BiomeRandomizationDisableBiomeButton, $"{char.ToUpper(BiomeTypes[i].biome.id[0])}{BiomeTypes[i].biome.id.Substring(1)}")):
            BiomeTypes[i] = (BiomeTypes[i].biome, true);
            break;
          case false when GUILayout.Button(string.Format(BiomeRandomizationEnableBiomeButton, $"{char.ToUpper(BiomeTypes[i].biome.id[0])}{BiomeTypes[i].biome.id.Substring(1)}")):
            BiomeTypes[i] = (BiomeTypes[i].biome, false);
            break;
        }
      }
    }
  }
}
