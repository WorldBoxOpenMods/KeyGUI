using KeyGUI.Menus.Localizations.Declarations.Stats.StatBoundEditing.EditStat;

namespace KeyGUI.Menus.Localizations.Declarations.Stats.StatBoundEditing {
  public class KeyGuiStatsStatBoundEditing : LocalesContainer {
    public readonly KeyGuiStatsStatBoundEditingEditStat EditStat = new KeyGuiStatsStatBoundEditingEditStat();
    public readonly LocaleDeclaration EditStatSubmenuName = new LocaleDeclaration();
    public readonly LocaleDeclaration ModifyStatButton = new LocaleDeclaration();
    public readonly LocaleDeclaration CloseMenuButton = new LocaleDeclaration();
    internal KeyGuiStatsStatBoundEditing() { }
  }
}