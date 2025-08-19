namespace KeyGUI.Menus.Localizations.Declarations.Traits.Instructions {
  public class KeyGuiTraitsInstructions : LocalesContainer {
    public readonly LocaleDeclaration NecessaryTraitValues = new LocaleDeclaration("Necessary Trait Values:\nTrait Name\nTrait Description\nTrait Group");
    public readonly LocaleDeclaration Tips = new LocaleDeclaration("Tips:\nIt's recommended to keep trait sprites 28x28 or smaller. Any size works, but larger ones can experience issues.\nKeyGUI includes a feature called \"Partner Traits\". Any traits that you select as partner traits for your trait will automatically be added to any unit that has your trait.\nThis is useful for combining special trait effects like immortality with your custom trait.");
    public readonly LocaleDeclaration CloseMenuButton = new LocaleDeclaration("Close");
    internal KeyGuiTraitsInstructions() { }
  }
}
