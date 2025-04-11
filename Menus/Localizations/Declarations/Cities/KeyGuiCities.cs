using KeyGUI.Menus.Localizations.Declarations.Cities.ResourceEditor;

namespace KeyGUI.Menus.Localizations.Declarations.Cities {
  public class KeyGuiCities : LocalesContainer {
    public readonly KeyGuiCitiesResourceEditor ResourceEditor = new KeyGuiCitiesResourceEditor();
    public readonly LocaleDeclaration ResourceEditorSubmenuName = new LocaleDeclaration();
    public readonly LocaleDeclaration ForceCityAsCapitalCityButton = new LocaleDeclaration();
    public readonly LocaleDeclaration ForceCityIntoOtherKingdomButton = new LocaleDeclaration();
    public readonly LocaleDeclaration SelectCityToEditResourcesOfListHeader = new LocaleDeclaration();
    internal KeyGuiCities() { }
  }
}
