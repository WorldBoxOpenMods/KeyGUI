using GameSettingsC = KeyGUI.Menus.ModConfig.ConfigOptions.GameSettings;
using System;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using KeyGUI.Menus.ModConfig;
using KeyGUI.Patches;
using UnityEngine;

namespace KeyGUI.Menus.GameSettings {
  public class KeyGuiGameSettings : KeyGuiMenu {
    private string _fps = "60";
    private string _unitySpeed = "100";
    private string _gameSpeed = "100";
    private string _worldTime = "0";

    internal override void RegisterPatches() {
      KeyGui.Instance.RegisterPatch<TargetFramerateEditor>();
      KeyGui.Instance.RegisterPatch<UnitySpeedEditor>();
      KeyGui.Instance.RegisterPatch<MouseDragToggler>();
      KeyGui.Instance.RegisterPatch<IceBoatMovementToggler>();
    }

    protected override void InitializeMenu() {
      base.InitializeMenu();
      AutoSaveManager.lowMemory = KeyGuiModConfig.Get(GameSettingsC.DisableAutosaves);
      IceBoatMovementToggler.DisableBoatMovementOnIce = KeyGuiModConfig.Get(GameSettingsC.DisableBoatsBreakingIce);
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
        TargetFramerateEditor.TargetFramerate = fps;
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
        UnitySpeedEditor.DeltaTime = gameSpeed;
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

        Config.time_scale_asset = new WorldTimeScaleAsset {
          sonic = true,
          multiplier = gameSpeed
        };
      }

      GUILayout.Label(Locales.Get(Locales.KeyGui.GameSettings.CameraMovementLabel));
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.GameSettings.ToggleMouseDragButton))) {
        MouseDragToggler.AllowMouseDrag = !MouseDragToggler.AllowMouseDrag;
      }

      GUILayout.Label(Locales.Get(Locales.KeyGui.GameSettings.BoatsLabel));
      if (GUILayout.Button(IceBoatMovementToggler.DisableBoatMovementOnIce ? Locales.Get(Locales.KeyGui.GameSettings.EnableBoatsBreakingIceButton) : Locales.Get(Locales.KeyGui.GameSettings.DisableBoatsBreakingIceButton))) {
        KeyGuiModConfig.Set(GameSettingsC.DisableBoatsBreakingIce, !IceBoatMovementToggler.DisableBoatMovementOnIce);
        IceBoatMovementToggler.DisableBoatMovementOnIce = KeyGuiModConfig.Get(GameSettingsC.DisableBoatsBreakingIce);
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
      World.world.map_stats.world_time = time;
    }
  }
}
