using GameSettingsC = KeyGUI.Menus.ModConfig.ConfigOptions.GameSettings;
using System;
using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.BehaviourManipulation;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using KeyGUI.Menus.ModConfig;
using UnityEngine;

namespace KeyGUI.Menus.GameSettings {
  public class KeyGuiGameSettings : KeyGuiMenu { 
    private string _fps = "60";
    private string _unitySpeed = "100";
    private string _gameSpeed = "100";
    private bool _fpsPatchApplied;
    private bool _unitySpeedPatchApplied;
    private string _mapSeed = "0";
    private string _worldTime = "0";

    internal override void Initialize() {
      base.Initialize();
      AutoSaveManager.lowMemory = KeyGuiModConfig.Get(GameSettingsC.DisableAutosaves);
      if (KeyGuiModConfig.Get(GameSettingsC.DisableBoatsBreakingIce)) {
        if (!KeyGenLibHarmonyPatchCollection.DisableBoatMovementOnIce) KeyLib.Get<KeyGenLibHarmonyPatchCollection>().ToggleBoatIceMovement();
      } else {
        if (KeyGenLibHarmonyPatchCollection.DisableBoatMovementOnIce) KeyLib.Get<KeyGenLibHarmonyPatchCollection>().ToggleBoatIceMovement();
      }
    }

    protected override void LoadGUI(int windowID) {
      GUILayout.Label(Locales.Get(Locales.KeyGui.GameSettings.SavesLabel));
      if (GUILayout.Button(AutoSaveManager.lowMemory ? Locales.Get(Locales.KeyGui.GameSettings.EnableAutosavesButton) : Locales.Get(Locales.KeyGui.GameSettings.DisableAutosavesButton))) {
        KeyGuiModConfig.Set(GameSettingsC.DisableAutosaves, AutoSaveManager.lowMemory = !AutoSaveManager.lowMemory);
      }
      
      GUILayout.Label(Locales.Get(Locales.KeyGui.GameSettings.UnityFpsLabel));
      _fps = GUILayout.TextField(_fps);
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.GameSettings.SetUnityFpsButton))) {
        int fps;
        try {
          fps = int.Parse(_fps);
        } catch (OverflowException) {
          _fps = "60";
          fps = 60;
        }

        if (_fpsPatchApplied == false) {
          _fpsPatchApplied = true;
          KeyLib.Get<KeyGenLibHarmonyPatchCollection>().PatchTargetFramerateSetter();
        }

        KeyLib.Get<KeyGenLibHarmonyPatchCollection>().SetTargetFramerate(fps);
      }

      GUILayout.Label(Locales.Get(Locales.KeyGui.GameSettings.UnitySpeedModifierLabel));
      _unitySpeed = GUILayout.TextField(_unitySpeed);
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.GameSettings.SetUnitySpeedModifierButton))) {
        float gameSpeed;
        try {
          gameSpeed = float.Parse(_unitySpeed) / 100;
        } catch (OverflowException) {
          _unitySpeed = "100";
          gameSpeed = 1;
        }

        if (_unitySpeedPatchApplied == false) {
          _unitySpeedPatchApplied = true;
          KeyLib.Get<KeyGenLibHarmonyPatchCollection>().PatchDeltaTimeGetter();
        }

        KeyLib.Get<KeyGenLibHarmonyPatchCollection>().SetDeltaTime(gameSpeed);
      }

      GUILayout.Label(Locales.Get(Locales.KeyGui.GameSettings.GameSpeedModifierLabel));
      _gameSpeed = GUILayout.TextField(_gameSpeed);
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.GameSettings.SetGameSpeedModifierButton))) {
        float gameSpeed;
        try {
          gameSpeed = float.Parse(_gameSpeed) / 100;
        } catch (OverflowException) {
          _gameSpeed = "100";
          gameSpeed = 1;
        }

        Config.timeScale = gameSpeed;
      }

      GUILayout.Label(Locales.Get(Locales.KeyGui.GameSettings.CameraMovementLabel));
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.GameSettings.ToggleMouseDragButton))) {
        KeyLib.Get<KeyGenLibHarmonyPatchCollection>().ToggleMouseDrag();
      }

      GUILayout.Label(Locales.Get(Locales.KeyGui.GameSettings.MapGenerationLabel));
      _mapSeed = GUILayout.TextField(_mapSeed);
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.GameSettings.SetMapSeedButton))) {
        KeyLib.Get<KeyGenLibWorldGenerationManipulationMethodCollection>().SetWorldSeed(int.Parse(_mapSeed));
      }

      // if (GUILayout.Button(KeyGeneralPurposeLibraryHarmonyPatchCollection.UseFixedRandomSeed ? "Disable seeded world generation" : "Enable seeded world Generation")) { }

      GUILayout.Label(Locales.Get(Locales.KeyGui.GameSettings.BoatsLabel));
      if (GUILayout.Button(KeyGenLibHarmonyPatchCollection.DisableBoatMovementOnIce ? Locales.Get(Locales.KeyGui.GameSettings.EnableBoatsBreakingIceButton) : Locales.Get(Locales.KeyGui.GameSettings.DisableBoatsBreakingIceButton))) {
        KeyGuiModConfig.Set(GameSettingsC.DisableBoatsBreakingIce, !KeyGenLibHarmonyPatchCollection.DisableBoatMovementOnIce);
        if (KeyGuiModConfig.Get(GameSettingsC.DisableBoatsBreakingIce)) {
          if (!KeyGenLibHarmonyPatchCollection.DisableBoatMovementOnIce) KeyLib.Get<KeyGenLibHarmonyPatchCollection>().ToggleBoatIceMovement();
        } else {
          if (KeyGenLibHarmonyPatchCollection.DisableBoatMovementOnIce) KeyLib.Get<KeyGenLibHarmonyPatchCollection>().ToggleBoatIceMovement();
        }
      }
      
      GUILayout.Label(Locales.Get(Locales.KeyGui.GameSettings.WorldPropertiesLabel));
      _worldTime = GUILayout.TextField(_worldTime);
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.GameSettings.SetWorldTimeButton))) {
        try {
          SetWorldTime(double.Parse(_worldTime));
        } catch (OverflowException) { }
      }
      /* TODO: make this work in the future
      if (GUILayout.Button("Reset world")) {
        SetWorldTime(0);
        World.world.magnet.dropPickedUnits();
        World.world.cultures.clear();
        World.world.alliances.clear();
        World.world.kingdoms.clear();
        World.world.cities.clear();
        World.world.clans.clear();
        World.world.units.clear();
        World.world.unitGroupManager.clear();
        World.world.diplomacy.clear();
        World.world.fire_layer.clear();
        World.world.city_zone_helper.clear();
        World.world.lava_layer.clear();
        World.world.plots.clear();
        World.world.heat.clear();
        World.world.wars.clear();
        World.world.worldLog.clear();
        World.world.conwayLayer.clear();
        World.world.list_base_managers.ForEach(s => s.clear());
        World.world.buildings.clear();
        World.world.stackEffects.clear();
        World.world.zoneCalculator.clear();
        World.world.list_systems.ForEach(s => s.clearFull());
        AssetManager.world_behaviours.list.ForEach(b => b.manager.clear());
        WorldBehaviourActions._spread_fauna_checked.Clear();
        WorldBehaviourActions._spread_fauna_list.Clear();
        WorldBehaviourActions._spread_fauna_used.Clear();
        World.world.allTilesDirty();
        foreach (WorldTile tile in World.world.tilesDirty) {
          tile.clear();
        }
      }
      */
    }

    private static void SetWorldTime(double time) {
      World.world.mapStats.worldTime = time;
      int currentYear = World.world.mapStats.getCurrentYear();
      int currentMonth = World.world.mapStats.getCurrentMonth();
      if (World.world.mapStats._last_year != currentYear)
        World.world.mapStats._last_month = -1;
      World.world.mapStats._last_year = currentYear;
      World.world.mapStats._last_month = currentMonth;
    }
  }
}