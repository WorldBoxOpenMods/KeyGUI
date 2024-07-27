using System.Linq;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Actors.ItemDropper.ItemCreator {
  public class KeyGuiActorsItemDropperItemCreator : KeyGuiMenu {
    internal static KeyGuiItem Item { get; set; }
    protected override float MenuMaxHeight => 1500;
    protected override float MenuMinHeight => 900;
    private Vector2 _itemAssetScrollPos;
    private Vector2 _itemModifierScrollPos;
    protected override void LoadGUI(int windowID) {
      if (Item == null) Item = new KeyGuiItem(this);
      GUILayout.Label(Locales.Get(Locales.KeyGui.Actors.ItemDropper.ItemCreator.ItemTypeSelectHeader));
      typeof(EquipmentType).GetEnumValues().Cast<EquipmentType>().ToList().ForEach(slot => {
        if (GUILayout.Button(slot.ToString())) {
          Item.SetSlot(slot, this);
        }
      });
      if (Item.Slot != null) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Actors.ItemDropper.ItemCreator.ItemAssetSelectHeader));
        _itemAssetScrollPos = GUILayout.BeginScrollView(_itemAssetScrollPos);
        foreach (ItemAsset item in AssetManager.items.list.Where(i => i.equipmentType == Item.Slot).Where(i => GUILayout.Button(i.id))) {
          Item.SetAsset(item, this);
        }
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Actors.ItemDropper.ItemCreator.NoItemAssetSelectOption))) {
          Item.SetAsset(null, this);
          Item.SetMaterial(null, this);
          Item.SetName(null, string.Format(Locales.Get(Locales.KeyGui.Actors.ItemDropper.ItemCreator.NoItemAssetItemName), Item.Slot));
          Enabled = false;
        }
        GUILayout.EndScrollView();
        if (Item.Asset != null) {
          GUILayout.Label(Locales.Get(Locales.KeyGui.Actors.ItemDropper.ItemCreator.ItemMaterialSelectHeader));
          foreach (string material in Item.Asset.materials.Where(material => GUILayout.Button(material))) {
            Item.SetMaterial(material, this);
          }
          if (Item.Material != null) {
            GUILayout.Label(Locales.Get(Locales.KeyGui.Actors.ItemDropper.ItemCreator.ItemModifiersSelectHeader));
            _itemModifierScrollPos = GUILayout.BeginScrollView(_itemModifierScrollPos);
            foreach (string modifier in AssetManager.items_modifiers.list.Select(m => m.id).Where(modifier => GUILayout.Button(string.Format((Item.Modifiers.Contains(modifier) ? Locales.Get(Locales.KeyGui.Actors.ItemDropper.ItemCreator.RemoveItemModifierButton) : Locales.Get(Locales.KeyGui.Actors.ItemDropper.ItemCreator.AddItemModifierButton)), modifier)))) {
              if (Item.Modifiers.Contains(modifier)) {
                Item.Modifiers.Remove(modifier);
              } else {
                Item.Modifiers.Add(modifier);
              }
            }
            GUILayout.EndScrollView();
            GUILayout.Label(Locales.Get(Locales.KeyGui.Actors.ItemDropper.ItemCreator.ItemNameSelectHeader));
            if (Item.Name == null) {
              Item.SetName(Item.Asset.id, this);
            }
            Item.SetName(GUILayout.TextField(Item.Name), this);
            if (GUILayout.Button(Locales.Get(Locales.KeyGui.Actors.ItemDropper.ItemCreator.ItemCreateButton))) {
              Enabled = false;
            }
          }
        }
      }
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Actors.ItemDropper.ItemCreator.ItemDiscardButton))) {
        Item = null;
        Enabled = false;
      }
    }
  }
}