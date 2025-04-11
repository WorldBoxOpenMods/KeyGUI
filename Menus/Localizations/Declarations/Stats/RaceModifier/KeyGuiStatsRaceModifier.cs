using KeyGUI.Menus.Localizations.Declarations.Stats.RaceModifier.StatSelection;

namespace KeyGUI.Menus.Localizations.Declarations.Stats.RaceModifier {
  public class KeyGuiStatsRaceModifier : LocalesContainer {
    public readonly KeyGuiStatsRaceModifierStatModifier StatModifier = new KeyGuiStatsRaceModifierStatModifier();
    public readonly LocaleDeclaration StatModifierSubmenuName = new LocaleDeclaration();
    public readonly LocaleDeclaration ShowNonCivRacesButton = new LocaleDeclaration();
    public readonly LocaleDeclaration HideNonCivRacesButton = new LocaleDeclaration();
    public readonly LocaleDeclaration ModifyRaceButton = new LocaleDeclaration();
    public readonly LocaleDeclaration CloseMenuButton = new LocaleDeclaration();
    internal KeyGuiStatsRaceModifier() { }
  }
}
