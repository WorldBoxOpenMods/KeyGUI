using KeyGUI.Menus.Localizations.Declarations.Cultures.TraitSelection;

namespace KeyGUI.Menus.Localizations.Declarations.Cultures {
  public class KeyGuiCultures : LocalesContainer {
    public readonly KeyGuiCulturesTraitSelection TraitSelection = new KeyGuiCulturesTraitSelection();
    public readonly LocaleDeclaration TraitSelectionSubmenuName = "Culture Trait Selection";
    public readonly LocaleDeclaration DeleteOneCultureButton = "Delete One Culture";
    public readonly LocaleDeclaration DeleteEveryCultureButton = "Delete Every Culture";
    public readonly LocaleDeclaration ResetOneCultureButton = "Reset One Culture";
    public readonly LocaleDeclaration ResetEveryCultureButton = "Reset Every Culture";
    public readonly LocaleDeclaration ForceExistingCultureUponSpecificCityButton = "Force Existing Culture Upon Specific City";
    public readonly LocaleDeclaration CreateNewCultureAtSpecificCityButton = "Create New Culture At Specific City";
    public readonly LocaleDeclaration SelectCultureListHeader = "Select Culture:";
    internal KeyGuiCultures() { }
  }
}
