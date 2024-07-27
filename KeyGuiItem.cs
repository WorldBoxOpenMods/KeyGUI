using System.Collections.Generic;

namespace KeyGUI {
  public class KeyGuiItem {
    public KeyGuiItem(object owner) {
      _owner = owner;
      Modifiers = new List<string>();
    }
    private readonly object _owner;
    public string Name { get; private set; }
    public ItemAsset Asset { get; private set; }
    public string Material { get; private set; }
    public List<string> Modifiers { get; private set; }
    public EquipmentType? Slot { get; private set; }
    public bool NullItem => (Name == null || Asset == null || Material == null) && Slot != null;
    
    public void SetName(string name, object caller) {
      if (caller == _owner) Name = name;
    }
    public void SetAsset(ItemAsset asset, object caller) {
      if (caller == _owner) Asset = asset;
    }
    public void SetMaterial(string material, object caller) {
      if (caller == _owner) Material = material;
    }
    public void SetSlot(EquipmentType slot, object caller) {
      if (caller == _owner) Slot = slot;
    }
  }
}