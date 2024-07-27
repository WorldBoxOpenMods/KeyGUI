using KeyGUI.Menus.Localizations.Declarations.Actors.ItemDropper;
using KeyGUI.Menus.Localizations.Declarations.Actors.TraitRemovalSelection;

namespace KeyGUI.Menus.Localizations.Declarations.Actors {
  public class KeyGuiActors : LocalesContainer {
    public readonly KeyGuiActorsItemDropper ItemDropper = new KeyGuiActorsItemDropper();
    public readonly KeyGuiActorsTraitRemovalSelection TraitRemovalSelection = new KeyGuiActorsTraitRemovalSelection();
    public readonly LocaleDeclaration TraitRemovalSelectionSubmenuName = new LocaleDeclaration();
    public readonly LocaleDeclaration ItemDropperSubmenuName = new LocaleDeclaration();
    public readonly LocaleDeclaration TraitRemovalRainDropButton = new LocaleDeclaration();
    public readonly LocaleDeclaration MakeActorKingButton = new LocaleDeclaration();
    internal KeyGuiActors() { }
  }
}