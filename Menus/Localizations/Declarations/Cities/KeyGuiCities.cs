using KeyGUI.Menus.Localizations.Declarations.Cities.ResourceEditor;

namespace KeyGUI.Menus.Localizations.Declarations.Cities {
  public class KeyGuiCities : LocalesContainer {
    public readonly KeyGuiCitiesResourceEditor ResourceEditor = new KeyGuiCitiesResourceEditor();
    public readonly LocaleDeclaration ResourceEditorSubmenuName = "Resource Editor";
    public readonly LocaleDeclaration ForceCityAsCapitalCityButton = "Force City As Capital City";
    public readonly LocaleDeclaration ForceCityIntoOtherKingdomButton = "Force City Into Other Kingdom";
    public readonly LocaleDeclaration SelectCityToEditResourcesOfListHeader = "Select City to edit resources of:";
    internal KeyGuiCities() { }
  }
}
