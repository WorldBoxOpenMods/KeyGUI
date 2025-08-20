using GameSettingsC = KeyGUI.Menus.ModConfig.ConfigOptions.GameSettings;
using System;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using KeyGUI.Menus.ModConfig;
using KeyGUI.Patches;
using UnityEngine;

namespace KeyGUI.Menus.GameSettings {
  public class KeyGuiGameSettings : KeyGuiMenu {
    public readonly KeyGuiLocale SavesLabel = "Saves";
    public readonly KeyGuiLocale EnableAutosavesButton = "Enable autosaves";
    public readonly KeyGuiLocale DisableAutosavesButton = "Disable autosaves";
    public readonly KeyGuiLocale UnityFpsLabel = "Unity FPS";
    public readonly KeyGuiLocale SetUnityFpsButton = "Set";
    public readonly KeyGuiLocale UnitySpeedModifierLabel = "Unity Speed Modifier (in %)";
    public readonly KeyGuiLocale SetUnitySpeedModifierButton = "Set";
    public readonly KeyGuiLocale GameSpeedModifierLabel = "Game Speed Modifier (in %)";
    public readonly KeyGuiLocale SetGameSpeedModifierButton = "Set";
    public readonly KeyGuiLocale CameraMovementLabel = "Camera Movement";
    public readonly KeyGuiLocale ToggleMouseDragButton = "Toggle Mouse Drag";
    public readonly KeyGuiLocale BoatsLabel = "Boats";
    public readonly KeyGuiLocale EnableBoatsBreakingIceButton = "Enable Boats Breaking Ice";
    public readonly KeyGuiLocale DisableBoatsBreakingIceButton = "Disable Boats Breaking Ice";
    public readonly KeyGuiLocale WorldPropertiesLabel = "World Properties";
    public readonly KeyGuiLocale SetWorldTimeButton = "Set world time";
    
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
      GUILayout.Label(SavesLabel);
      if (GUILayout.Button(AutoSaveManager.lowMemory ? EnableAutosavesButton : DisableAutosavesButton)) {
        KeyGuiModConfig.Set(GameSettingsC.DisableAutosaves, AutoSaveManager.lowMemory = !AutoSaveManager.lowMemory);
      }

      GUILayout.Label(UnityFpsLabel);
      _fps = GUILayout.TextField(_fps);
      if (GUILayout.Button(SetUnityFpsButton)) {
        int fps;
        try {
          fps = int.Parse(_fps);
        } catch (OverflowException) {
          _fps = "60";
          fps = 60;
        }
        TargetFramerateEditor.TargetFramerate = fps;
      }

      GUILayout.Label(UnitySpeedModifierLabel);
      _unitySpeed = GUILayout.TextField(_unitySpeed);
      if (GUILayout.Button(SetUnitySpeedModifierButton)) {
        float gameSpeed;
        try {
          gameSpeed = float.Parse(_unitySpeed) / 100;
        } catch (OverflowException) {
          _unitySpeed = "100";
          gameSpeed = 1;
        }
        UnitySpeedEditor.DeltaTime = gameSpeed;
      }

      GUILayout.Label(GameSpeedModifierLabel);
      _gameSpeed = GUILayout.TextField(_gameSpeed);
      if (GUILayout.Button(SetGameSpeedModifierButton)) {
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

      GUILayout.Label(CameraMovementLabel);
      if (GUILayout.Button(ToggleMouseDragButton)) {
        MouseDragToggler.AllowMouseDrag = !MouseDragToggler.AllowMouseDrag;
      }

      GUILayout.Label(BoatsLabel);
      if (GUILayout.Button(IceBoatMovementToggler.DisableBoatMovementOnIce ? EnableBoatsBreakingIceButton : DisableBoatsBreakingIceButton)) {
        KeyGuiModConfig.Set(GameSettingsC.DisableBoatsBreakingIce, !IceBoatMovementToggler.DisableBoatMovementOnIce);
        IceBoatMovementToggler.DisableBoatMovementOnIce = KeyGuiModConfig.Get(GameSettingsC.DisableBoatsBreakingIce);
      }

      GUILayout.Label(WorldPropertiesLabel);
      _worldTime = GUILayout.TextField(_worldTime);
      if (GUILayout.Button(SetWorldTimeButton)) {
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
