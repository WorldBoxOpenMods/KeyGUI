using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using KeyGUI.Menus.Stats.StatBoundEditing;

namespace KeyGUI.Menus.Stats {
  public class KeyGuiStats : KeyGuiMenuManager {
    internal override void AddSubMenus() {
      Load<KeyGuiStatsStatBoundEditing>(Locales.KeyGui.Stats.StatBoundEditingSubmenuName);
    }
  }
}
