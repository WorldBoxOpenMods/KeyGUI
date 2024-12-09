using KeyGUI.Menus.Localizations.Declarations.Cultures.TraitSelection;
namespace KeyGUI.Menus.Localizations.Declarations.Cultures {
  public class KeyGuiCultures : LocalesContainer {
    public readonly KeyGuiCulturesTraitSelection TraitSelection = new KeyGuiCulturesTraitSelection();
    public readonly LocaleDeclaration TraitSelectionSubmenuName = new LocaleDeclaration("Culture Trait Selection");
    public readonly LocaleDeclaration DeleteOneCultureButton = new LocaleDeclaration();
    public readonly LocaleDeclaration DeleteEveryCultureButton = new LocaleDeclaration();
    public readonly LocaleDeclaration ResetOneCultureButton = new LocaleDeclaration();
    public readonly LocaleDeclaration ResetEveryCultureButton = new LocaleDeclaration();
    public readonly LocaleDeclaration ResetTechOfOneCultureButton = new LocaleDeclaration();
    public readonly LocaleDeclaration ResetTechOfEveryCultureButton = new LocaleDeclaration();
    public readonly LocaleDeclaration KnowledgeGainIncreaseDecreaseAmountLabel = new LocaleDeclaration();
    public readonly LocaleDeclaration InvalidKnowledgeGainIncreaseDecreaseAmountEnteredError = new LocaleDeclaration();
    public readonly LocaleDeclaration IncreaseKnowledgeGainOfOneCultureButton = new LocaleDeclaration();
    public readonly LocaleDeclaration IncreaseKnowledgeGainOfEveryCultureButton = new LocaleDeclaration();
    public readonly LocaleDeclaration DecreaseKnowledgeGainOfOneCultureButton = new LocaleDeclaration();
    public readonly LocaleDeclaration DecreaseKnowledgeGainOfEveryCultureButton = new LocaleDeclaration();
    public readonly LocaleDeclaration ForceExistingCultureUponSpecificCityButton = new LocaleDeclaration();
    public readonly LocaleDeclaration CreateNewCultureAtSpecificCityButton = new LocaleDeclaration();
    public readonly LocaleDeclaration SelectCultureListHeader = new LocaleDeclaration("Select Culture:");
    internal KeyGuiCultures() { }
  }
}
