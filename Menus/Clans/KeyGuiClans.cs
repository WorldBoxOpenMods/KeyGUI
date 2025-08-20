using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using KeyGUI.Menus.Clans.TraitSelection;
using KeyGUI.Patches;
using UnityEngine;

namespace KeyGUI.Menus.Clans {
  public class KeyGuiClans : KeyGuiMenuManager {
    public readonly KeyGuiLocale TraitSelectionSubmenuName = "Clan Trait Selection";
    public readonly KeyGuiLocale SelectClanListHeader = "Select Clan:";
    
    protected override float MenuMaxHeight => 700;
    protected override float MenuMinHeight => 300;
    private Vector2 _scrollPos;

    internal override void RegisterPatches() {
      KeyGui.Instance.RegisterPatch<ClanBirthTraitsManagement>();
    }

    protected override void LoadGUI(int windowID) {
      _scrollPos = GUILayout.BeginScrollView(_scrollPos);
      GUILayout.Label(SelectClanListHeader);
      foreach (Clan clan in World.world.clans.list.Where(clan => GUILayout.Button(clan.name))) {
        KeyGuiClansTraitSelection.SelectedClan = clan;
      }
      GUILayout.EndScrollView();
      LoadSubMenuToggles();
    }
    internal override void AddSubMenus() {
      Load<KeyGuiClansTraitSelection>(TraitSelectionSubmenuName);
    }
  }
}
