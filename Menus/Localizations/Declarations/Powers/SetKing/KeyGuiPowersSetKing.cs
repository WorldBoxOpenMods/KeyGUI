namespace KeyGUI.Menus.Localizations.Declarations.Powers.SetKing {
  public class KeyGuiPowersSetKing : LocalesContainer {
    public readonly LocaleDeclaration SelectActor = new LocaleDeclaration("Select the actor to make king.");
    public readonly LocaleDeclaration Success = new LocaleDeclaration("Actor made king!");
    public readonly LocaleDeclaration NoKingdomOnSelectedActorError = new LocaleDeclaration("The actor you selected doesn't have a kingdom!");
    public readonly LocaleDeclaration NoActorSelectedError = new LocaleDeclaration("No actor to make king here, try again!");
    internal KeyGuiPowersSetKing() { }
  }
}
