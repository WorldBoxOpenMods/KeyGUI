using System.Linq;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Clans.TraitSelection;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Clans {
  public class KeyGuiClans : KeyGuiMenuManager {
    protected override float MenuMaxHeight => 700;
    protected override float MenuMinHeight => 300;
    private Vector2 _scrollPos;

    protected override void PreMenuToggleUi() {
      _scrollPos = GUILayout.BeginScrollView(_scrollPos);
      GUILayout.Label(Locales.Get(Locales.KeyGui.Clans.SelectClanListHeader));
      foreach (Clan clan in World.world.clans.list.Where(clan => GUILayout.Button(clan.name))) {
        KeyGuiClansTraitSelection.SelectedClan = clan;
      }
      GUILayout.EndScrollView();
    }
    internal override void AddSubMenus() {
      Load<KeyGuiClansTraitSelection>(Locales.KeyGui.Clans.TraitSelectionSubmenuName);
    }
  }
}