using ItemsC = KeyGUI.Menus.ModConfig.ConfigOptions.Items;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.Assets;
using KeyGeneralPurposeLibrary.Classes;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.ModConfig;
using KeyGUI.Menus.ModConfig.ConfigOptions;
using UnityEngine;

namespace KeyGUI.Menus.Items {
  public class KeyGuiItems : KeyGuiMenu {
    public KeyGuiItems() {
      if (KeyGuiModConfig.Get(General.InitializeAssetFoldersEveryStartup)) {
        KeyLib.Get<KeyGenLibFileAssetManager>().InitializeLocalFolders(KeyGuiConfig.PluginName);
      }
      _itemStats.Add("diplomacy", "0");
      _itemStats.Add("personalityRationality", "0");
      _itemStats.Add("opinion", "0");
      _itemStats.Add("fertility", "0");
      _itemStats.Add("maxChildren", "0");
      _itemStats.Add("maxAge", "0");
      _itemStats.Add("damage", "0");
      _itemStats.Add("speed", "0");
      _itemStats.Add("health", "0");
      _itemStats.Add("armor", "0");
      _itemStats.Add("intelligence", "0");
      _itemStats.Add("attackSpeed", "0");
      _itemStats.Add("criticalChance", "0");
      _itemStats.Add("warfare", "0");
      _itemStats.Add("dodge", "0");
      _itemStats.Add("stewardship", "0");
      _itemStats.Add("loyaltyTraits", "0");
      _itemStats.Add("modArmor", "0%");
      _itemStats.Add("modDamage", "0%");
      _itemStats.Add("modHealth", "0%");
      _itemStats.Add("modSpeed", "0%");
      _itemStats.Add("modDiplomacy", "0%");
      _itemStats.Add("modCrit", "0%");
      _itemStats.Add("modAttackSpeed", "0%");
      _itemStats.Add("cities", "0");
      _itemStats.Add("modSupplyTimer", "0%");
      _itemStats.Add("accuracy", "0");
      _itemStats.Add("scale", "0");
      _itemStats.Add("zoneRange", "0");
      _itemStats.Add("range", "0");
      _autoItemLoading = KeyGuiModConfig.Get(ItemsC.AutoloadItems);
    }

    private Vector2 _scrollPosition;
    private bool _autoItemLoading;
    private List<CustomItemAsset> _customItems = new List<CustomItemAsset>();
    private string _createStatus = "";
    private string _itemID;
    private string _sprite;
    private string _material;
    private bool _metallic;
    private EquipmentType _equipmentType;
    private WeaponType _weaponType;
    private readonly List<string> _itemModifiers = new List<string>();
    private readonly Dictionary<string, string> _itemStats = new Dictionary<string, string>();
    protected override float MenuMinHeight => 1000;
    protected override float MenuMaxHeight => 2100;

    protected override void LoadGUI(int windowID) {
      _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
      GUILayout.Label("Create Item:");

      GUILayout.Label("General Info:");
      GUILayout.Label("Item Name");
      _itemID = GUILayout.TextField(_itemID);
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

      GUILayout.Label("Material:");
      GUILayout.Label("Current Material: " + _material);
      if (GUILayout.Button("Adamantine")) {
        _material = "adamantine";
      }

      if (GUILayout.Button("Mythril")) {
        _material = "mythril";
      }

      if (GUILayout.Button("Steel")) {
        _material = "steel";
      }

      if (GUILayout.Button("Iron")) {
        _material = "iron";
      }

      if (GUILayout.Button("Silver")) {
        _material = "silver";
      }

      if (GUILayout.Button("Bronze")) {
        _material = "bronze";
      }

      if (GUILayout.Button("Copper")) {
        _material = "copper";
      }

      if (GUILayout.Button("Leather")) {
        _material = "leather";
      }

      if (GUILayout.Button(!_metallic ? "Make metallic" : "Make non-metallic")) {
        _metallic = !_metallic;
      }

      // TODO: Make more item types than just weapons possible
      GUILayout.Label("Equipment Type:");
      GUILayout.Label("Current Equipment Type: " + _equipmentType);
      if (GUILayout.Button("Weapon")) {
        _equipmentType = EquipmentType.Weapon;
      }

      if (KeyGuiConfig.DebugIsLegal) {
        if (GUILayout.Button("Amulet")) {
          _equipmentType = EquipmentType.Amulet;
        }

        if (GUILayout.Button("Helmet")) {
          _equipmentType = EquipmentType.Helmet;
        }

        if (GUILayout.Button("Armor")) {
          _equipmentType = EquipmentType.Armor;
        }

        if (GUILayout.Button("Boots")) {
          _equipmentType = EquipmentType.Boots;
        }

        if (GUILayout.Button("Ring")) {
          _equipmentType = EquipmentType.Ring;
        }
      }

      if (_equipmentType == EquipmentType.Weapon) {
        GUILayout.Label("Weapon Type");
        GUILayout.Label("Current Weapon Type: " + _weaponType);
        if (GUILayout.Button("Melee")) {
          _weaponType = WeaponType.Melee;
        }
        /*
        if (GUILayout.Button("Ranged")) {
          _weaponType = WeaponType.Range;
        }
        */
      }

      GUILayout.Label("Stats:");
      foreach (string stat in _itemStats.Keys.ToList()) {
        GUILayout.Label(LocalizedTextManager.stringExists(stat) ? LocalizedTextManager.getText(stat) : stat);
        _itemStats[stat] = GUILayout.TextField(_itemStats[stat]);
      }
      
      /*
      GUILayout.Label("Modifiers:");
      foreach (ItemAsset t in AssetManager.items_modifiers.list) {
          if (_itemModifiers.Contains(t.id)) {
              if (GUILayout.Button("Remove " + t.id)) {
                  _itemModifiers.Remove(t.id);
              }
          } else {
              if (GUILayout.Button("Add " + t.id)) {
                  _itemModifiers.Add(t.id);
              }
          }
      }
      */

      GUILayout.Label("Create!");
      GUILayout.Label(_createStatus);
      if (GUILayout.Button("Create Item")) {
        if (!string.IsNullOrEmpty(_itemID)) {
          bool statsAreValid = true;
          foreach (string stat in _itemStats.Keys.Where(stat => !float.TryParse(_itemStats[stat].Replace("%", ""), out _))) {
            statsAreValid = false;
            _createStatus = "The stat value entered for " + stat + " is invalid!";
            break;
          }
          if (statsAreValid) {
            _createStatus = "Item Created!";
            CreateItem();
          }
        } else {
          _createStatus = "Please enter an item name!";
        }
      }

      GUILayout.Label("Delete items");
      for (int i = 0; i < AssetManager.items.list.Count; ++i) {
        if (AssetManager.items.list[i] is CustomItemAsset customItem) {
          if (customItem.Author == "KeyGUI" && GUILayout.Button(customItem.id)) {
            for (int j = 0; j < World.world.units.ToList().Count; j++) {
              if (World.world.units.ToList()[j].equipment.weapon.data.id == customItem.id) {
                World.world.units.ToList()[j].equipment.weapon.emptySlot();
              }
            }

            for (int j = 0; j < World.world.units.ToList().Count; j++) {
              if (World.world.units.ToList()[j].equipment.armor.data.id == customItem.id) {
                World.world.units.ToList()[j].equipment.armor.emptySlot();
              }
            }

            for (int j = 0; j < World.world.units.ToList().Count; j++) {
              if (World.world.units.ToList()[j].equipment.helmet.data.id == customItem.id) {
                World.world.units.ToList()[j].equipment.helmet.emptySlot();
              }
            }

            for (int j = 0; j < World.world.units.ToList().Count; j++) {
              if (World.world.units.ToList()[j].equipment.boots.data.id == customItem.id) {
                World.world.units.ToList()[j].equipment.boots.emptySlot();
              }
            }

            for (int j = 0; j < World.world.units.ToList().Count; j++) {
              if (World.world.units.ToList()[j].equipment.amulet.data.id == customItem.id) {
                World.world.units.ToList()[j].equipment.amulet.emptySlot();
              }
            }

            AssetManager.items.list.Remove(customItem);
            AssetManager.items.dict.Remove(customItem.id);
            LocalizedTextManager.instance.localizedText.Remove("item_" + customItem.id);
            _customItems.Remove(customItem);
            --i;
          }
        }
      }

      GUILayout.Label("Data Control");
      if (GUILayout.Button("Save")) {
        KeyLib.Get<KeyGenLibCustomItemManager>().SaveItemsLocally(KeyGuiConfig.PluginName, _customItems);
      }

      if (GUILayout.Button("Load")) {
        _customItems = KeyLib.Get<KeyGenLibCustomItemManager>().LoadItems(KeyGuiConfig.PluginName);
      }
      
      if (GUILayout.Button(_autoItemLoading ? "Turn Automatic Item Loading Off" : "Turn Automatic Item Loading On")) {
        _autoItemLoading = !_autoItemLoading;
        KeyGuiModConfig.Set(ItemsC.AutoloadItems, _autoItemLoading);
      }

      GUILayout.EndScrollView();
    }

    private void CreateItem() {
      CustomItemAsset customItemAsset = new CustomItemAsset("KeyGUI", _sprite);
      Dictionary<string, float> itemStats = KeyLib.Get<KeyGenLibCustomItemManager>().ConvertItemStats(_itemStats);
      _customItems.Add(customItemAsset);
      customItemAsset.LoadItem(_itemID, 2, _material, _metallic, _equipmentType, _weaponType, itemStats, _itemModifiers);
    }
  }
}
