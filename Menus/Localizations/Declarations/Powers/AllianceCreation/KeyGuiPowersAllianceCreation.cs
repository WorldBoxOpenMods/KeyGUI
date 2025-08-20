namespace KeyGUI.Menus.Localizations.Declarations.Powers.AllianceCreation {
  public class KeyGuiPowersAllianceCreation : LocalesContainer {
    public readonly LocaleDeclaration SelectFirstKingdom = new LocaleDeclaration("Select the first kingdom to create an alliance with.");
    public readonly LocaleDeclaration SelectSecondKingdom = new LocaleDeclaration("Select the second kingdom to create an alliance with.");
    public readonly LocaleDeclaration SameKingdomTwiceError = new LocaleDeclaration("Selected the same kingdom twice, try again.");
    public readonly LocaleDeclaration KingdomsAlreadyAlliedError = new LocaleDeclaration("The selected kingdoms are already allied, try again.");
    public readonly LocaleDeclaration CreationSuccess = new LocaleDeclaration("The kingdom {0} has successfully entered an alliance with kingdom {1}.");
    internal KeyGuiPowersAllianceCreation() { }
  }
}
