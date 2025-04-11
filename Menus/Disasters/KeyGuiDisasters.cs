using System.Collections.Generic;
using System.Linq;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Disasters {
  public class KeyGuiDisasters : KeyGuiMenu {
    protected override void LoadGUI(int windowID) {
      DisasterAsset disaster = null;

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.BoilTogetherHeatwaveButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.heatwave);
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.TriggerEarthquakeButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.small_earthquake);
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.CauseTornadoButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.tornado);
      }

      if (!(World.world.cities.list.Count > 0)) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Disasters.ImplantModThoughtNoVillageFoundError));
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.ImplantMadThoughtButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.mad_thoughts);
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.GenerateMeteoriteButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.small_meteorite);
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.BringForthEvilMageButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.wild_mage);
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.BuildSomeSnowmenButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.sudden_snowman);
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.StealDemonsFromHellButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.hellspawn);
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.InvadeWorldWithBanditsButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.ash_bandits);
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.GrabFrozenOnesFromNorthPoleButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.ice_ones_awoken);
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.UnleashNecromancerButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.underground_necromancer);
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.AwakenDragonButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.dragon_from_farlands);
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.UncoverAliensButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.alien_invasion);
      }

      if (!(World.world.cities.list.Count > 0)) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Disasters.DigUpBiomassNoVillageFoundError));
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.DigUpBiomassButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.biomass);
      }

      if (!(World.world.cities.list.Count > 0)) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Disasters.GiveBirthToTumorNoVillageFoundError));
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.GiveBirthToTumorButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.tumor);
      }

      if (!(World.world.cities.list.Count > 0)) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Disasters.GrowGardenSurpriseNoVillageFoundError));
      } else if (!World.world.cities.list.Any(c => c.buildings_dict_type.TryGetValue(S_BuildingType.type_windmill, out List<Building> _))) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Disasters.GrowGardenSurpriseNoWindmillFoundError));
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.GrowGardenSurpriseButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.garden_surprise);
      }

      if (!(World.world.cities.list.Count > 0)) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Disasters.SummonGregNoVillageFoundError));
      } else if (!World.world.cities.list.Any(c => c.buildings_dict_type.TryGetValue(S_BuildingType.type_mine, out List<Building> _))) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Disasters.SummonGregNoMineFoundError));
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.SummonGregButton))) {
        disaster = AssetManager.disasters.get(S_Disaster.greg_abominations);
      }

      if (World.world.world_laws.dict.TryGetValue(WorldLawLibrary.world_law_disasters_nature.id, out PlayerOptionData naturalDisasters)) {
        if (naturalDisasters.boolVal) naturalDisasters = null;
      }
      if (World.world.world_laws.dict.TryGetValue(WorldLawLibrary.world_law_disasters_other.id, out PlayerOptionData otherDisasters)) {
        if (otherDisasters.boolVal) otherDisasters = null;
      }
      if (naturalDisasters != null) naturalDisasters.boolVal = true;
      if (otherDisasters != null) otherDisasters.boolVal = true;

      disaster?.action(disaster);

      if (naturalDisasters != null) naturalDisasters.boolVal = false;
      if (otherDisasters != null) otherDisasters.boolVal = false;
    }
  }
}
