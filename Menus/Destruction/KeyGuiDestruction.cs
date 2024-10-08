﻿using System.Linq;
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
        for (int i = 0; i < World.world.mapStats.id_unit; ++i) {
          Actor unit = World.world.units.get("u_" + i);
          if (unit != null) {
            unit.applyMadness(unit);
          }
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Destruction.CauseAMassBombingButton))) {
        for (int i = 0; i < World.world.mapStats.id_unit; ++i) {
          Actor unit = World.world.units.get("u_" + i);
          if (unit != null) {
            ActionLibrary.deathNuke(unit, unit.currentTile);
            ActionLibrary.deathMark(unit, unit.currentTile);
          }
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Destruction.MassInfectEverythingWithMushSporesButton))) {
        for (int i = 0; i < World.world.mapStats.id_unit; ++i) {
          Actor unit = World.world.units.get("u_" + i);
          if (unit != null) {
            unit.addTrait("mushSpores", true);
          }
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

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Destruction.CauseAWorldWarButton))) {
        foreach (Actor actor in World.world.clans.list.Select(t => t.getChief())) {
          if (actor.getInfluence() < 101) {
            actor.data.set(S.influence, 101);
          }

          PlotAsset plotAsset = KeyLib.Get<KeyGenLibCustomPlotLibrary>()[KeyGenLibCustomPlotLibrary.WorldWarPlotIndex];
          KeyLib.Get<KeyGenLibCustomPlotCreator>().CreateTotalWarPlot(plotAsset, actor);
        }
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Destruction.ChangeEveryBiomeToTheSwampBiomeButton))) {
        KeyLib.Get<KeyGenLibWorldTileManipulationMethodCollection>().ChangeEveryWorldTile(DropsLibrary.action_seeds_swamp);
      }

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Destruction.ChangeEveryBiomeToTheCorruptedBiomeButton))) {
        KeyLib.Get<KeyGenLibWorldTileManipulationMethodCollection>().ChangeEveryWorldTile(DropsLibrary.action_seeds_corrupted);
      }
    }
  }
}