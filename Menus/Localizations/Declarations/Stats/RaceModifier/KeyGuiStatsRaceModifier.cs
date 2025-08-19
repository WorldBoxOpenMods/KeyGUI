using KeyGUI.Menus.Localizations.Declarations.Stats.RaceModifier.StatSelection;

namespace KeyGUI.Menus.Localizations.Declarations.Stats.RaceModifier {
  public class KeyGuiStatsRaceModifier : LocalesContainer {
    public readonly KeyGuiStatsRaceModifierStatModifier StatModifier = new KeyGuiStatsRaceModifierStatModifier();
    public readonly LocaleDeclaration StatModifierSubmenuName = new LocaleDeclaration("Race Stat Modifier");
    public readonly LocaleDeclaration ShowNonCivRacesButton = new LocaleDeclaration("Show non civ races too");
    public readonly LocaleDeclaration HideNonCivRacesButton = new LocaleDeclaration("Only show civ races");
    public readonly LocaleDeclaration ModifyRaceButton = new LocaleDeclaration("Modify {0}");
    public readonly LocaleDeclaration CloseMenuButton = new LocaleDeclaration("Close");
    internal KeyGuiStatsRaceModifier() { }
  }
}
