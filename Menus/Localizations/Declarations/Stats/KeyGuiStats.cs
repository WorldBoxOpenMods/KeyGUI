using KeyGUI.Menus.Localizations.Declarations.Stats.StatBoundEditing;

namespace KeyGUI.Menus.Localizations.Declarations.Stats {
  public class KeyGuiStats : LocalesContainer {
    public readonly KeyGuiStatsStatBoundEditing StatBoundEditing = new KeyGuiStatsStatBoundEditing();
    public readonly LocaleDeclaration StatBoundEditingSubmenuName = new LocaleDeclaration("Edit stat bounds");
    internal KeyGuiStats() { }
  }
}
