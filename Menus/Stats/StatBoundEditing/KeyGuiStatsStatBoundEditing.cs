using System.Linq;
using KeyGUI.Framework.Menus;
using KeyGUI.Menus.Localizations.Declarations;
using KeyGUI.Menus.Stats.StatBoundEditing.EditStat;
using UnityEngine;

namespace KeyGUI.Menus.Stats.StatBoundEditing {
  public class KeyGuiStatsStatBoundEditing : KeyGuiMenuManager {
    protected override float MenuMinHeight => 800;
    protected override float MenuMaxHeight => 1700;
    private Vector2 _scrollPos;
    protected override void LoadGUI() {
      _scrollPos = GUILayout.BeginScrollView(_scrollPos);
      foreach (BaseStatAsset s in AssetManager.base_stats_library.list.Where(s => GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Stats.StatBoundEditing.ModifyStatButton), s.id)))) {
        KeyGuiStatsStatBoundEditingEditStat.SelectedStat = s;
      }
      GUILayout.EndScrollView();

      LoadSubMenuToggles();

      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Stats.StatBoundEditing.CloseMenuButton))) {
        Enabled = false;
      }
    }
    internal override void AddSubMenus() {
      Load<KeyGuiStatsStatBoundEditingEditStat>(Locales.KeyGui.Stats.StatBoundEditing.EditStatSubmenuName);
    }
  }
}
