using KeyGUI.Menus.Localizations.Declarations.Cities.ResourceEditor;

namespace KeyGUI.Menus.Localizations.Declarations.Cities {
  public class KeyGuiCities : LocalesContainer {
    public readonly KeyGuiCitiesResourceEditor ResourceEditor = new KeyGuiCitiesResourceEditor();
    public readonly LocaleDeclaration ResourceEditorSubmenuName = new LocaleDeclaration("Resource Editor");
    public readonly LocaleDeclaration ForceCityAsCapitalCityButton = new LocaleDeclaration("Force City As Capital City");
    public readonly LocaleDeclaration ForceCityIntoOtherKingdomButton = new LocaleDeclaration("Force City Into Other Kingdom");
    public readonly LocaleDeclaration SelectCityToEditResourcesOfListHeader = new LocaleDeclaration("Select City to edit resources of:");
    internal KeyGuiCities() { }
  }
}
