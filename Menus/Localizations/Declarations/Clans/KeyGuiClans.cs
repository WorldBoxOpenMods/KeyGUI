using KeyGUI.Menus.Localizations.Declarations.Clans.TraitSelection;

namespace KeyGUI.Menus.Localizations.Declarations.Clans {
  public class KeyGuiClans : LocalesContainer {
    public readonly KeyGuiClansTraitSelection TraitSelection = new KeyGuiClansTraitSelection();
    public readonly LocaleDeclaration TraitSelectionSubmenuName = new LocaleDeclaration();
    public readonly LocaleDeclaration SelectClanListHeader = new LocaleDeclaration();
    internal KeyGuiClans() { }
  }
}
