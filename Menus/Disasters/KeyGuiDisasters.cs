using System.Collections.Generic;
using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using strings;
using UnityEngine;

namespace KeyGUI.Menus.Disasters {
  public class KeyGuiDisasters : KeyGuiMenu {
    public readonly KeyGuiLocale BoilTogetherHeatwaveButton = "Boil Together Heatwave";
    public readonly KeyGuiLocale TriggerEarthquakeButton = "Trigger Earthquake";
    public readonly KeyGuiLocale CauseTornadoButton = "Cause Tornado";
    public readonly KeyGuiLocale ImplantMadThoughtButton = "No village found for Mad Thoughts!";
    public readonly KeyGuiLocale ImplantModThoughtNoVillageFoundError = "Implant Mad Thought";
    public readonly KeyGuiLocale GenerateMeteoriteButton = "Generate Meteorite";
    public readonly KeyGuiLocale BringForthEvilMageButton = "Bring Forth Evil Mage";
    public readonly KeyGuiLocale BuildSomeSnowmenButton = "Build Some Snowmen";
    public readonly KeyGuiLocale StealDemonsFromHellButton = "Steal Demons From Hell";
    public readonly KeyGuiLocale InvadeWorldWithBanditsButton = "Invade World With Bandits";
    public readonly KeyGuiLocale GrabFrozenOnesFromNorthPoleButton = "Grab Frozen Ones From North Pole";
    public readonly KeyGuiLocale UnleashNecromancerButton = "Unleash Necromancer";
    public readonly KeyGuiLocale AwakenDragonButton = "Awaken Dragon";
    public readonly KeyGuiLocale UncoverAliensButton = "Uncover Aliens";
    public readonly KeyGuiLocale DigUpBiomassButton = "Dig Up Biomass";
    public readonly KeyGuiLocale DigUpBiomassNoVillageFoundError = "No village found for Biomass!";
    public readonly KeyGuiLocale GiveBirthToTumorButton = "Give Birth To Tumor";
    public readonly KeyGuiLocale GiveBirthToTumorNoVillageFoundError = "No village found for Tumor!";
    public readonly KeyGuiLocale GrowGardenSurpriseButton = "Grow Garden Surprise";
    public readonly KeyGuiLocale GrowGardenSurpriseNoVillageFoundError = "No village found for Garden Surprise!";
    public readonly KeyGuiLocale GrowGardenSurpriseNoWindmillFoundError = "No windmill found for Garden Surprise!";
    public readonly KeyGuiLocale SummonGregButton = "Summon Greg";
    public readonly KeyGuiLocale SummonGregNoVillageFoundError = "No village found for Greg!";
    public readonly KeyGuiLocale SummonGregNoMineFoundError = "No mine found for Greg!";
    
    protected override void LoadGUI(int windowID) {
      DisasterAsset disaster = null;

      if (GUILayout.Button(BoilTogetherHeatwaveButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.heatwave);
      }

      if (GUILayout.Button(TriggerEarthquakeButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.small_earthquake);
      }

      if (GUILayout.Button(CauseTornadoButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.tornado);
      }

      if (!(World.world.cities.list.Count > 0)) {
        GUILayout.Label(ImplantModThoughtNoVillageFoundError);
      } else if (GUILayout.Button(ImplantMadThoughtButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.mad_thoughts);
      }

      if (GUILayout.Button(GenerateMeteoriteButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.small_meteorite);
      }

      if (GUILayout.Button(BringForthEvilMageButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.wild_mage);
      }

      if (GUILayout.Button(BuildSomeSnowmenButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.sudden_snowman);
      }

      if (GUILayout.Button(StealDemonsFromHellButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.hellspawn);
      }

      if (GUILayout.Button(InvadeWorldWithBanditsButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.ash_bandits);
      }

      if (GUILayout.Button(GrabFrozenOnesFromNorthPoleButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.ice_ones_awoken);
      }

      if (GUILayout.Button(UnleashNecromancerButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.underground_necromancer);
      }

      if (GUILayout.Button(AwakenDragonButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.dragon_from_farlands);
      }

      if (GUILayout.Button(UncoverAliensButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.alien_invasion);
      }

      if (!(World.world.cities.list.Count > 0)) {
        GUILayout.Label(DigUpBiomassNoVillageFoundError);
      } else if (GUILayout.Button(DigUpBiomassButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.biomass);
      }

      if (!(World.world.cities.list.Count > 0)) {
        GUILayout.Label(GiveBirthToTumorNoVillageFoundError);
      } else if (GUILayout.Button(GiveBirthToTumorButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.tumor);
      }

      if (!(World.world.cities.list.Count > 0)) {
        GUILayout.Label(GrowGardenSurpriseNoVillageFoundError);
      } else if (!World.world.cities.list.Any(c => c.buildings_dict_type.TryGetValue(S_BuildingType.type_windmill, out List<Building> _))) {
        GUILayout.Label(GrowGardenSurpriseNoWindmillFoundError);
      } else if (GUILayout.Button(GrowGardenSurpriseButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.garden_surprise);
      }

      if (!(World.world.cities.list.Count > 0)) {
        GUILayout.Label(SummonGregNoVillageFoundError);
      } else if (!World.world.cities.list.Any(c => c.buildings_dict_type.TryGetValue(S_BuildingType.type_mine, out List<Building> _))) {
        GUILayout.Label(SummonGregNoMineFoundError);
      } else if (GUILayout.Button(SummonGregButton)) {
        disaster = AssetManager.disasters.get(S_Disaster.greg_abominations);
      }

      if (disaster != null) {
        if (World.world.world_laws.dict.TryGetValue(WorldLawLibrary.world_law_disasters_nature.id, out PlayerOptionData naturalDisasters)) {
          if (naturalDisasters.boolVal) naturalDisasters = null;
        }
        if (World.world.world_laws.dict.TryGetValue(WorldLawLibrary.world_law_disasters_other.id, out PlayerOptionData otherDisasters)) {
          if (otherDisasters.boolVal) otherDisasters = null;
        }
        if (naturalDisasters != null) naturalDisasters.boolVal = true;
        if (otherDisasters != null) otherDisasters.boolVal = true;

        disaster.action(disaster);

        if (naturalDisasters != null) naturalDisasters.boolVal = false;
        if (otherDisasters != null) otherDisasters.boolVal = false;
      }
    }
  }
}
