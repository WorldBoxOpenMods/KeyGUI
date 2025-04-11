using KeyGUI.Menus.Localizations.Declarations.Actors.ItemDropper.ItemCreator;

namespace KeyGUI.Menus.Localizations.Declarations.Actors.ItemDropper {
  public class KeyGuiActorsItemDropper : LocalesContainer {
    public readonly KeyGuiActorsItemDropperItemCreator ItemCreator = new KeyGuiActorsItemDropperItemCreator();
    public readonly LocaleDeclaration CreatedItemsListHeader = new LocaleDeclaration();
    public readonly LocaleDeclaration LoadItemFromItemCreatorButton = new LocaleDeclaration();
    public readonly LocaleDeclaration LoadItemFromItemCreatorButtonNoItemError = new LocaleDeclaration();
    public readonly LocaleDeclaration AddItemRainButton = new LocaleDeclaration();
    public readonly LocaleDeclaration ItemCreatorSubmenuName = new LocaleDeclaration();
    internal KeyGuiActorsItemDropper() { }
  }
}
