using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.Assets;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Disasters {
  public class KeyGuiDisasters : KeyGuiMenu {
    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.BoilTogetherHeatwaveButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnHeatwave();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.TriggerEarthquakeButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnEarthquake();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.CauseTornadoButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnTornado();
      }

      if (!KeyLib.Get<KeyGenLibDisasterGenerator>().CheckForVillage()) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Disasters.ImplantModThoughtNoVillageFoundError));
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.ImplantMadThoughtButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnMadThought();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.GenerateMeteoriteButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnMeteorite();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.BringForthEvilMageButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnEvilMage();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.BuildSomeSnowmenButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnSnowmen();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.StealDemonsFromHellButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnDemons();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.InvadeWorldWithBanditsButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnBandits();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.GrabFrozenOnesFromNorthPoleButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnFrozenOnes();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.UnleashNecromancerButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnNecromancer();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.AwakenDragonButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnDragon();
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.UncoverAliensButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnAliens();
      }

      if (!KeyLib.Get<KeyGenLibDisasterGenerator>().CheckForVillage()) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Disasters.DigUpBiomassNoVillageFoundError));
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.DigUpBiomassButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnBiomass();
      }

      if (!KeyLib.Get<KeyGenLibDisasterGenerator>().CheckForVillage()) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Disasters.GiveBirthToTumorNoVillageFoundError));
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.GiveBirthToTumorButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnTumor();
      }

      if (!KeyLib.Get<KeyGenLibDisasterGenerator>().CheckForVillage()) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Disasters.GrowGardenSurpriseNoVillageFoundError));
      } else if (!KeyLib.Get<KeyGenLibDisasterGenerator>().CheckForWindmill()) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Disasters.GrowGardenSurpriseNoWindmillFoundError));
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.GrowGardenSurpriseButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnGardenSurprise();
      }

      if (!KeyLib.Get<KeyGenLibDisasterGenerator>().CheckForVillage()) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Disasters.SummonGregNoVillageFoundError));
      } else if (!KeyLib.Get<KeyGenLibDisasterGenerator>().CheckForMine()) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Disasters.SummonGregNoMineFoundError));
      } else if (GUILayout.Button(Locales.Get(Locales.KeyGui.Disasters.SummonGregButton))) {
        KeyLib.Get<KeyGenLibDisasterGenerator>().SpawnGreg();
      }
    }
  }
}