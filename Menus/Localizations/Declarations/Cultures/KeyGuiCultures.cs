using KeyGUI.Menus.Localizations.Declarations.Cultures.TraitSelection;

namespace KeyGUI.Menus.Localizations.Declarations.Cultures {
  public class KeyGuiCultures : LocalesContainer {
    public readonly KeyGuiCulturesTraitSelection TraitSelection = new KeyGuiCulturesTraitSelection();
    public readonly LocaleDeclaration TraitSelectionSubmenuName = new LocaleDeclaration("Culture Trait Selection");
    public readonly LocaleDeclaration DeleteOneCultureButton = new LocaleDeclaration("Delete One Culture");
    public readonly LocaleDeclaration DeleteEveryCultureButton = new LocaleDeclaration("Delete Every Culture");
    public readonly LocaleDeclaration ResetOneCultureButton = new LocaleDeclaration("Reset One Culture");
    public readonly LocaleDeclaration ResetEveryCultureButton = new LocaleDeclaration("Reset Every Culture");
    public readonly LocaleDeclaration ForceExistingCultureUponSpecificCityButton = new LocaleDeclaration("Force Existing Culture Upon Specific City");
    public readonly LocaleDeclaration CreateNewCultureAtSpecificCityButton = new LocaleDeclaration("Create New Culture At Specific City");
    public readonly LocaleDeclaration SelectCultureListHeader = new LocaleDeclaration("Select Culture:");
    internal KeyGuiCultures() { }
  }
}
