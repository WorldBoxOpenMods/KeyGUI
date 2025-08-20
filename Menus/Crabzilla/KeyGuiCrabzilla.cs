using System.Linq;
using System.Reflection;
using KeyGUI.Framework.Menus;
using KeyGUI.Menus.Localizations.Declarations;
using KeyGUI.Patches;
using strings;
using UnityEngine;

namespace KeyGUI.Menus.Crabzilla {
  public class KeyGuiCrabzilla : KeyGuiMenu {
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
          GUILayout.Label(Locales.Get(Locales.KeyGui.Crabzilla.CrabzillaNotSpawnedError));
        }
        if (_crabzillaIsSpawned) {
          GUILayout.Label(Locales.Get(Locales.KeyGui.Crabzilla.LiveCrabzillaChangesSectionTitle));
          if (GUILayout.Button(Locales.Get(Locales.KeyGui.Crabzilla.InspectCrabzillaButton))) {
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

          if (GUILayout.Button(Locales.Get(Locales.KeyGui.Crabzilla.FullyHealCrabzillaButton))) {
            foreach (Actor crabzilla in World.world.units.Where(crabzilla => crabzilla.asset.id == SA.crabzilla)) {
              crabzilla.restoreHealth(crabzilla.getMaxHealth());
            }
          }
        }
        GUILayout.Label(Locales.Get(Locales.KeyGui.Crabzilla.CrabzillaStatChangesSectionTitle));
        GUILayout.Label(Locales.Get(Locales.KeyGui.Crabzilla.AttackStatLabel));
        _attack = GUILayout.TextField(_attack);
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Crabzilla.SetAttackButton))) {
          AssetManager.terraform.get("crab_laser").damage = int.Parse(_attack);
        }
        GUILayout.Label(Locales.Get(Locales.KeyGui.Crabzilla.ExplosionStrengthStatLabel));
        _explosionStrength = GUILayout.TextField(_explosionStrength);
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Crabzilla.SetExplosionStrengthButton))) {
          AssetManager.terraform.get("crab_laser").explode_strength = int.Parse(_explosionStrength);
        }
        GUILayout.Label(Locales.Get(Locales.KeyGui.Crabzilla.ExplosionRadiusStatLabel));
        _explosionRadius = GUILayout.TextField(_explosionRadius);
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Crabzilla.SetExplosionRadiusButton))) {
          CrabzillaManagement.CrabzillaArmExplosionRadius = int.Parse(_explosionRadius);
        }

        GUILayout.Label(Locales.Get(Locales.KeyGui.Crabzilla.CrabzillaBoolFlagChangesSectionTitle));
        _boolFlagSubmenuScrollPosition = GUILayout.BeginScrollView(_boolFlagSubmenuScrollPosition);
        foreach (FieldInfo crabzillaField in _crabzillaAsset.GetType().GetFields()) {
          if (crabzillaField.FieldType == typeof(bool)) {
            bool currentCrabzillaBoolFlag = (bool)crabzillaField.GetValue(_crabzillaAsset);
            if (GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Crabzilla.SetTheFlagToBoolButton), crabzillaField.Name, !currentCrabzillaBoolFlag))) {
              crabzillaField.SetValue(_crabzillaAsset, !currentCrabzillaBoolFlag);
            }
          }
        }
        GUILayout.EndScrollView();
      } else {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Crabzilla.CrabzillaDoesntExistError));
      }
    }
  }
}
