using TraitsC = KeyGUI.Menus.ModConfig.ConfigOptions.Traits;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.Assets;
using KeyGeneralPurposeLibrary.Classes;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using KeyGUI.Menus.ModConfig;
using KeyGUI.Menus.ModConfig.ConfigOptions;
using KeyGUI.Menus.Traits.Instructions;
using UnityEngine;

namespace KeyGUI.Menus.Traits {
  public class KeyGuiTraits : KeyGuiMenuManager {
    protected override float MenuMaxWidth => 1200;
    protected override float MenuMinWidth => 600;
    protected override float MenuMinHeight => 800;
    protected override float MenuMaxHeight => 1700;
    public KeyGuiTraits() {
      MenuRect.width = 600;
      if (KeyGuiModConfig.Get(General.InitializeAssetFoldersEveryStartup)) {
        KeyLib.Get<KeyGenLibFileAssetManager>().InitializeLocalFolders(KeyGuiConfig.PluginName);
      }
      SetDefaultStatValues();
      _autoTraitLoading = KeyGuiModConfig.Get(TraitsC.AutoloadTraits);
    }

    private void SetDefaultStatValues() {
      _traitStats.Add("diplomacy", "0");
      _traitStats.Add("personalityRationality", "0");
      _traitStats.Add("opinion", "0");
      _traitStats.Add("fertility", "0");
      _traitStats.Add("maxChildren", "0");
      _traitStats.Add("maxAge", "0");
      _traitStats.Add("damage", "0");
      _traitStats.Add("speed", "0");
      _traitStats.Add("health", "0");
      _traitStats.Add("armor", "0");
      _traitStats.Add("intelligence", "0");
      _traitStats.Add("attackSpeed", "0");
      _traitStats.Add("criticalChance", "0");
      _traitStats.Add("warfare", "0");
      _traitStats.Add("dodge", "0");
      _traitStats.Add("stewardship", "0");
      _traitStats.Add("loyaltyTraits", "0");
      _traitStats.Add("modArmor", "0%");
      _traitStats.Add("modDamage", "0%");
      _traitStats.Add("modHealth", "0%");
      _traitStats.Add("modSpeed", "0%");
      _traitStats.Add("modDiplomacy", "0%");
      _traitStats.Add("modCrit", "0%");
      _traitStats.Add("modAttackSpeed", "0%");
      _traitStats.Add("cities", "0");
      _traitStats.Add("modSupplyTimer", "0%");
      _traitStats.Add("accuracy", "0");
      _traitStats.Add("scale", "0");
      _traitStats.Add("zoneRange", "0");
      _traitStats.Add("range", "0");
      _traitStats.Add("bonusTowers", "0");
      _traitStats.Add("clanMembers", "0");
    }

    private Vector2 _scrollPos;
    private List<CustomTrait> _customTraits = new List<CustomTrait>();
    private bool _autoTraitLoading;
    private bool _editing;
    private string _createStatus = "";
    private string _traitID;
    private string _traitDescription;
    private string _sprite;
    private string _traitGroup;
    private string _traitBirth = "0";
    private string _traitInherit = "0";
    private readonly Dictionary<string, string> _traitStats = new Dictionary<string, string>();
    private readonly List<string> _oppositeTraits = new List<string>();
    private readonly List<string> _partnerTraits = new List<string>();

    internal override void Initialize() {
      base.Initialize();
      foreach (ActorTrait trait in AssetManager.traits.list) {
        if (trait is CustomTrait customTrait) {
          _customTraits.Add(customTrait);
        }
      }
    }
    internal override void AddSubMenus() {
      Load<KeyGuiTraitsInstructions>(Locales.KeyGui.Traits.InstructionsSubmenuName);
    }

    protected override void LoadGUI(int windowID) {
      _scrollPos = GUILayout.BeginScrollView(_scrollPos);
      GUILayout.Label("Create Trait:");

      GUILayout.Label("Trait Name");
      _traitID = GUILayout.TextField(_traitID);
      GUILayout.Label("Trait Description");
      _traitDescription = GUILayout.TextField(_traitDescription);
      GUILayout.Label("Trait Birth Chance");
      _traitBirth = GUILayout.TextField(_traitBirth);
      GUILayout.Label("Trait Inherit Chance");
      _traitInherit = GUILayout.TextField(_traitInherit);
      foreach (string stat in _traitStats.Keys.ToList()) {
        GUILayout.Label(LocalizedTextManager.stringExists(stat) ? LocalizedTextManager.getText(stat) : stat);
        _traitStats[stat] = GUILayout.TextField(_traitStats[stat]);
      }
      GUILayout.Label("Opposite Traits");
      foreach (ActorTrait trait in AssetManager.traits.list) {
        if (_oppositeTraits.Contains(trait.id)) {
          if (GUILayout.Button(string.Format("Remove {0} From Opposite Traits", trait.id))) {
            _oppositeTraits.Remove(trait.id);
          }
        } else {
          if (GUILayout.Button(string.Format("Add {0} To Opposite Traits", trait.id))) {
            _oppositeTraits.Add(trait.id);
          }
        }
      }
      GUILayout.Label("Partner Traits");
      foreach (ActorTrait trait in AssetManager.traits.list) {
        if (_partnerTraits.Contains(trait.id)) {
          if (GUILayout.Button(string.Format("Remove {0} From Partner Traits", trait.id))) {
            _partnerTraits.Remove(trait.id);
          }
        } else {
          if (GUILayout.Button(string.Format("Add {0} To Partner Traits", trait.id))) {
            _partnerTraits.Add(trait.id);
          }
        }
      }
      GUILayout.Label("Trait Group");
      GUILayout.Label("Current Group: " + _traitGroup);
      if (GUILayout.Button(TraitGroup.acquired)) {
        _traitGroup = TraitGroup.acquired;
      }

      if (GUILayout.Button(TraitGroup.body)) {
        _traitGroup = TraitGroup.body;
      }

      if (GUILayout.Button(TraitGroup.fun)) {
        _traitGroup = TraitGroup.fun;
      }

      if (GUILayout.Button(TraitGroup.mind)) {
        _traitGroup = TraitGroup.mind;
      }

      if (KeyGuiConfig.DebugIsLegal && GUILayout.Button(TraitGroup.personality)) {
        _traitGroup = TraitGroup.personality;
      }

      if (GUILayout.Button(TraitGroup.miscellaneous)) {
        _traitGroup = TraitGroup.miscellaneous;
      }

      if (GUILayout.Button(TraitGroup.special)) {
        _traitGroup = TraitGroup.special;
      }

      if (GUILayout.Button(TraitGroup.spirit)) {
        _traitGroup = TraitGroup.spirit;
      }

      GUILayout.Label("Sprite");
      GUILayout.Label("Current Sprite: " + _sprite);
      List<string> availableSprites;
      {
        string[] availableSpritesArray = Directory.GetFiles(Path.GetFullPath($"{Application.dataPath}/KeyLibraryModsData/{KeyGuiConfig.PluginName}/Sprites"));
        availableSprites = availableSpritesArray.ToList();
      }
      for (int i = 0; i < availableSprites.Count; ++i) {
        if (!availableSprites[i].Contains(".png")) {
          availableSprites.RemoveAt(i);
          --i;
        }
      }

      for (int i = 0; i < availableSprites.Count; ++i) {
        availableSprites[i] = Path.GetFullPath(availableSprites[i]);
        availableSprites[i] = availableSprites[i].Replace(Path.GetFullPath($"{Application.dataPath}/KeyLibraryModsData/{KeyGuiConfig.PluginName}/Sprites"), "");
        availableSprites[i] = availableSprites[i].Replace(".png", "");
        availableSprites[i] = availableSprites[i].Substring(1);
      }

      foreach (string t in availableSprites.Where(t => GUILayout.Button(t))) {
        _sprite = t;
      }

      if (availableSprites.Count == 0) {
        GUILayout.Button($"No PNGs Found In\nworldbox_Data\n->\nKeyLibraryModsData\n->\n{KeyGuiConfig.PluginName}\n->\nSprites");
      }

      GUILayout.Label("Create!");
      GUILayout.Label(_createStatus);
      if (_editing) {
        if (GUILayout.Button("Cancel Editing")) {
          _editing = false;
          _traitID = "";
        }
      }

      if (GUILayout.Button("Create Trait")) {
        if (!string.IsNullOrEmpty(_traitID)) {
          if (!string.IsNullOrEmpty(_traitGroup)) {
            bool statsAreValid = true;
            if (float.TryParse(_traitBirth, out _) == false) {
              statsAreValid = false;
              _createStatus = "The birth chance entered is invalid!";
            }
            if (float.TryParse(_traitInherit, out _) == false) {
              statsAreValid = false;
              _createStatus = "The inherit chance entered is invalid!";
            }
            foreach (string stat in _traitStats.Keys.Where(stat => !float.TryParse(_traitStats[stat].Replace("%", ""), out _))) {
              statsAreValid = false;
              _createStatus = "The stat value entered for " + stat + " is invalid!";
              break;
            }
            if (statsAreValid) {
              _createStatus = "Trait Created!";
              if (_traitID == "KeyGUI_Debug_Menu" && _traitDescription == "KeyGUI_Debug_Menu") {
                _createStatus = "Debug Enabled!";
                KeyGuiConfig.DebugIsLegal = true;
              } else {
                if (_editing) {
                  try {
                    DeleteTrait(AssetManager.traits.dict[_traitID] as CustomTrait, false);
                  } catch (Exception e) {
                    Debug.Log("Deleting the old version of the trait being edited failed:");
                    Debug.Log(e);
                  }

                  _editing = false;
                }

                CreateTrait();
                _traitID = "";
                _traitDescription = "";
                _traitGroup = "";
                _traitInherit = "0";
                _traitBirth = "0";
                _traitStats.Clear();
                SetDefaultStatValues();
                _partnerTraits.Clear();
                _oppositeTraits.Clear();
              }
            }
          } else {
            _createStatus = "Please select a trait group!";
          }
        } else {
          _createStatus = "Please enter a trait name!";
        }
      }

      GUILayout.Label("Edit Trait:");
      foreach (ActorTrait t in AssetManager.traits.list) {
        if (t is CustomTrait trait) {
          if (GUILayout.Button(trait.id)) {
            _editing = true;
            _traitID = trait.id;
            _traitDescription = trait.Description;
            for (int j = 0; j < _traitStats.Keys.Count; ++j) {
              string stat = _traitStats.Keys.ElementAt(j);
              _traitStats[stat] = trait.base_stats[ConvertToSnakeCase(stat)].ToString(CultureInfo.InvariantCulture);
            }

            _traitGroup = trait.group_id;
            _sprite = trait.Sprite;
            _traitBirth = trait.birth.ToString(CultureInfo.InvariantCulture);
            _traitInherit = trait.inherit.ToString(CultureInfo.InvariantCulture);
            _oppositeTraits.Clear();
            _oppositeTraits.AddRange(trait.OppositeTraits);
            _partnerTraits.Clear();
            _partnerTraits.AddRange(trait.PartnerTraits);
          }
        }
      }

      GUILayout.Label("Delete Trait:");
      for (int i = 0; i < AssetManager.traits.list.Count; i++) {
        if (AssetManager.traits.list[i] is CustomTrait trait) {
          if (trait.Author == "KeyGUI" && GUILayout.Button(trait.id)) {
            DeleteTrait(trait);
            --i;
          }
        }
      }

      GUILayout.Label("Data Control");
      if (GUILayout.Button("Save")) {
        KeyLib.Get<KeyGenLibCustomTraitManager>().SaveTraitsLocally(KeyGuiConfig.PluginName, _customTraits);
      }

      if (GUILayout.Button("Load")) {
        _customTraits = KeyLib.Get<KeyGenLibCustomTraitManager>().LoadTraits(KeyGuiConfig.PluginName);
      }

      if (GUILayout.Button(_autoTraitLoading ? "Turn Automatic Trait Loading Off" : "Turn Automatic Trait Loading On")) {
        _autoTraitLoading = !_autoTraitLoading;
        KeyGuiModConfig.Set(TraitsC.AutoloadTraits, _autoTraitLoading);
      }

      GUILayout.EndScrollView();
    }

    private void CreateTrait() {
      Dictionary<string, float> traitStats = KeyLib.Get<KeyGenLibCustomTraitManager>().ConvertTraitStats(_traitStats);
      CustomTrait newTrait = new CustomTrait();
      newTrait.LoadVersionOnePointTwoTrait(_traitID, "KeyGUI", _traitDescription, _sprite, _traitGroup, float.Parse(_traitBirth, CultureInfo.InvariantCulture), float.Parse(_traitInherit, CultureInfo.InvariantCulture), traitStats, _oppositeTraits, _partnerTraits);
      _customTraits.Add(newTrait);
    }

    private void DeleteTrait(CustomTrait trait, bool clean = true) {
      if (clean) {
        for (int j = 0; j < World.world.units.ToList().Count; j++) {
          if (World.world.units.ToList()[j].hasTrait(trait.id)) {
            World.world.units.ToList()[j].removeTrait(trait.id);
          }

          if (World.world.units.ToList()[j].asset.traits.Contains(trait.id)) {
            World.world.units.ToList()[j].asset.traits.Remove(trait.id);
          }
        }
      }

      AssetManager.traits.dict.Remove(trait.id);
      AssetManager.traits.list.Remove(trait);
      LocalizedTextManager.instance.localizedText.Remove("trait_" + trait.id);
      LocalizedTextManager.instance.localizedText.Remove("trait_" + trait.id + "_info");
      _customTraits.Remove(trait);
    }

    private static string ConvertToSnakeCase(string text) {
      if (text == null) {
        throw new ArgumentNullException(nameof(text));
      }

      if (text.Length < 2) {
        return text;
      }

      StringBuilder sb = new StringBuilder();
      sb.Append(char.ToLowerInvariant(text[0]));
      for (int i = 1; i < text.Length; ++i) {
        char c = text[i];
        if (char.IsUpper(c)) {
          sb.Append('_');
          sb.Append(char.ToLowerInvariant(c));
        } else {
          sb.Append(c);
        }
      }

      return sb.ToString();
    }
  }
}
