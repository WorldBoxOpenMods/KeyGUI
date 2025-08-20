using System.Linq;
using System.Reflection;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using KeyGUI.Patches;
using strings;
using UnityEngine;

namespace KeyGUI.Menus.Crabzilla {
  public class KeyGuiCrabzilla : KeyGuiMenu {
    public readonly KeyGuiLocale CrabzillaNotSpawnedError = "Some buttons are disabled because Crabzilla is not spawned.";
    public readonly KeyGuiLocale LiveCrabzillaChangesSectionTitle = "Live Crabzilla Changes:";
    public readonly KeyGuiLocale InspectCrabzillaButton = "Inspect Crabzilla";
    public readonly KeyGuiLocale FullyHealCrabzillaButton = "Fully Heal Crabzilla";
    public readonly KeyGuiLocale CrabzillaStatChangesSectionTitle = "Crabzilla Stat Changes:";
    public readonly KeyGuiLocale AttackStatLabel = "Attack";
    public readonly KeyGuiLocale SetAttackButton = "Set Attack";
    public readonly KeyGuiLocale ExplosionStrengthStatLabel = "Explosion Strength";
    public readonly KeyGuiLocale SetExplosionStrengthButton = "Set Explosion Strength";
    public readonly KeyGuiLocale ExplosionRadiusStatLabel = "Explosion Radius";
    public readonly KeyGuiLocale SetExplosionRadiusButton = "Set Explosion Radius";
    public readonly KeyGuiLocale CrabzillaBoolFlagChangesSectionTitle = "Crabzilla Bool Flag Changes:";
    public readonly KeyGuiLocale SetTheFlagToBoolButton = "Set the {0} flag to {1}";
    public readonly KeyGuiLocale CrabzillaDoesntExistError = "Crabzilla somehow doesn't exist in your game Ig.";
    
    protected override float MenuMaxWidth => 600;
    protected override float MenuMinWidth => 500;
    protected override float MenuMinHeight => 800;
    protected override float MenuMaxHeight => 1700;

    private ActorAsset _crabzillaAsset;
    private bool _crabzillaIsSpawned;
    private Vector2 _boolFlagSubmenuScrollPosition = Vector2.zero;
    private string _attack = "200";
    private string _explosionStrength = "1";
    private string _explosionRadius = "4";

    internal override void RegisterPatches() {
      KeyGui.Instance.RegisterPatch<CrabzillaManagement>();
    }

    protected override void InitializeMenu() {
      base.InitializeMenu();
      _crabzillaAsset = AssetManager.actor_library.get(SA.crabzilla);
    }

    protected override void UpdateMenu() {
      CrabzillaManagement.CrabzillaIsSpawned = false;
    }

    protected override void LoadGUI(int windowID) {
      if (_crabzillaAsset != null) {
        if (_crabzillaIsSpawned != CrabzillaManagement.CrabzillaIsSpawned) {
          _crabzillaIsSpawned = CrabzillaManagement.CrabzillaIsSpawned;
          MenuRect.height = 0;
        }
        if (CrabzillaManagement.CrabzillaIsSpawned == false) {
          GUILayout.Label(CrabzillaNotSpawnedError);
        }
        if (_crabzillaIsSpawned) {
          GUILayout.Label(LiveCrabzillaChangesSectionTitle);
          if (GUILayout.Button(InspectCrabzillaButton)) {
            foreach (Actor crabzilla in World.world.units.Where(crabzilla => crabzilla.asset.id == SA.crabzilla)) {
              SelectedUnit.select(crabzilla);
              bool currentCanCrabzillaBeInspected = crabzilla.asset.can_be_inspected;
              crabzilla.asset.can_be_inspected = true;
              CrabzillaManagement.IsCrabzillaKillable = false;
              ScrollWindow.showWindow("inspect_unit");
              crabzilla.asset.can_be_inspected = currentCanCrabzillaBeInspected;
              CrabzillaManagement.IsCrabzillaKillable = true;
            }
          }

          if (GUILayout.Button(FullyHealCrabzillaButton)) {
            foreach (Actor crabzilla in World.world.units.Where(crabzilla => crabzilla.asset.id == SA.crabzilla)) {
              crabzilla.restoreHealth(crabzilla.getMaxHealth());
            }
          }
        }
        GUILayout.Label(CrabzillaStatChangesSectionTitle);
        GUILayout.Label(AttackStatLabel);
        _attack = GUILayout.TextField(_attack);
        if (GUILayout.Button(SetAttackButton)) {
          AssetManager.terraform.get("crab_laser").damage = int.Parse(_attack);
        }
        GUILayout.Label(ExplosionStrengthStatLabel);
        _explosionStrength = GUILayout.TextField(_explosionStrength);
        if (GUILayout.Button(SetExplosionStrengthButton)) {
          AssetManager.terraform.get("crab_laser").explode_strength = int.Parse(_explosionStrength);
        }
        GUILayout.Label(ExplosionRadiusStatLabel);
        _explosionRadius = GUILayout.TextField(_explosionRadius);
        if (GUILayout.Button(SetExplosionRadiusButton)) {
          CrabzillaManagement.CrabzillaArmExplosionRadius = int.Parse(_explosionRadius);
        }

        GUILayout.Label(CrabzillaBoolFlagChangesSectionTitle);
        _boolFlagSubmenuScrollPosition = GUILayout.BeginScrollView(_boolFlagSubmenuScrollPosition);
        foreach (FieldInfo crabzillaField in _crabzillaAsset.GetType().GetFields()) {
          if (crabzillaField.FieldType == typeof(bool)) {
            bool currentCrabzillaBoolFlag = (bool)crabzillaField.GetValue(_crabzillaAsset);
            if (GUILayout.Button(string.Format(SetTheFlagToBoolButton, crabzillaField.Name, !currentCrabzillaBoolFlag))) {
              crabzillaField.SetValue(_crabzillaAsset, !currentCrabzillaBoolFlag);
            }
          }
        }
        GUILayout.EndScrollView();
      } else {
        GUILayout.Label(CrabzillaDoesntExistError);
      }
    }
  }
}
