using KeyGUI.Menus.Localizations.Declarations.Clans.TraitSelection;

namespace KeyGUI.Menus.Localizations.Declarations.Clans {
  public class KeyGuiClans : LocalesContainer {
    public readonly KeyGuiClansTraitSelection TraitSelection = new KeyGuiClansTraitSelection();
    public readonly LocaleDeclaration TraitSelectionSubmenuName = "Clan Trait Selection";
    public readonly LocaleDeclaration SelectClanListHeader = "Select Clan:";
    internal KeyGuiClans() { }
  }
}
