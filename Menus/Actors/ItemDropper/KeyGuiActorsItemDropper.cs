using System;
using System.Collections.Generic;
using System.Linq;
using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.Powers;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Actors.ItemDropper.ItemCreator;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Actors.ItemDropper {
  public class KeyGuiActorsItemDropper : KeyGuiMenuManager {
    private List<KeyGuiItem> _items = new List<KeyGuiItem>();
    private DropAsset _addItemDrop;
    internal override void Initialize() {
      base.Initialize();
      _addItemDrop = new DropAsset() {
        id = "item_mass_addition_rain_keygui",
        path_texture = "drops/drop_gamma_rain",
        random_frame = true,
        default_scale = 0.1f,
        action_landed = AddItemsToActorsAt,
        material = "mat_world_object_lit",
        sound_drop = "event:/SFX/DROPS/DropRainGamma"
      };
      AssetManager.drops.add(_addItemDrop);
    }
    protected override void PreMenuToggleUi() {
      GUILayout.Label(Locales.Get(Locales.KeyGui.Actors.ItemDropper.CreatedItemsListHeader));
      _items = _items.Where(i => !GUILayout.Button(i.Name)).ToList();
      if (GUILayout.Button(KeyGuiActorsItemDropperItemCreator.Item != null ? Locales.Get(Locales.KeyGui.Actors.ItemDropper.LoadItemFromItemCreatorButton) : Locales.Get(Locales.KeyGui.Actors.ItemDropper.LoadItemFromItemCreatorButtonNoItemError))) {
        if (KeyGuiActorsItemDropperItemCreator.Item != null) {
          _items.Add(KeyGuiActorsItemDropperItemCreator.Item);
          KeyGuiActorsItemDropperItemCreator.Item = null;
        }
      }
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Actors.ItemDropper.AddItemRainButton))) {
        PowerButton button = KeyLib.Get<KeyGenLibGodPowerButtonLibrary>()[KeyGenLibGodPowerButtonLibrary.MassItemAdditionRainButtonIndex];
        if (button != null) {
          PowerButtonSelector.instance.unselectAll();
          PowerButtonSelector.instance.setPower(button);
        } else {
          Debug.LogError("Something went wrong with the Mass Trait Removal Rain! Please report this to the mod author!");
        }
      }
    }
    internal override void AddSubMenus() {
      Load<KeyGuiActorsItemDropperItemCreator>(Locales.KeyGui.Actors.ItemDropperSubmenuName);
    }

    private void AddItemsToActorsAt(WorldTile pTile = null, string pDropId = null) {
      if (pTile != null) {
        if (pTile.hasUnits()) {
          foreach (Actor unit in pTile._units) {
            foreach (KeyGuiItem item in _items) {
              if (item.NullItem) {
                unit.equipment?.getSlot(item.Slot ?? throw new Exception("Something went very wrong with the KeyGuiActorItemDropper! Please report this error to the mod author!")).setItem(null);
                continue;
              }
              ItemData itemData = ItemGenerator.generateItem(item.Asset, item.Material);
              itemData.name = item.Name;
              itemData.modifiers = item.Modifiers.ToList();
              unit.equipment?.getSlot(item.Slot ?? throw new Exception("Something went very wrong with the KeyGuiActorItemDropper! Please report this error to the mod author!")).setItem(itemData);
            }
          }
        }
      }
    }
  }
}