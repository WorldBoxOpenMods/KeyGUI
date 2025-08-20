using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using KeyGUI.Menus.Stats.StatBoundEditing;

namespace KeyGUI.Menus.Stats {
  public class KeyGuiStats : KeyGuiMenuManager {
    public readonly KeyGuiLocale StatBoundEditingSubmenuName = "Edit stat bounds";

    internal override void AddSubMenus() {
      Load<KeyGuiStatsStatBoundEditing>(StatBoundEditingSubmenuName);
    }
  }
}
