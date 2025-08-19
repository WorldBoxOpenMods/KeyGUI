using KeyGUI.Menus.Localizations.Declarations.Stats.StatBoundEditing.EditStat;

namespace KeyGUI.Menus.Localizations.Declarations.Stats.StatBoundEditing {
  public class KeyGuiStatsStatBoundEditing : LocalesContainer {
    public readonly KeyGuiStatsStatBoundEditingEditStat EditStat = new KeyGuiStatsStatBoundEditingEditStat();
    public readonly LocaleDeclaration EditStatSubmenuName = new LocaleDeclaration("Edit Stat");
    public readonly LocaleDeclaration ModifyStatButton = new LocaleDeclaration("Modify {0}");
    public readonly LocaleDeclaration CloseMenuButton = new LocaleDeclaration("Close");
    internal KeyGuiStatsStatBoundEditing() { }
  }
}
