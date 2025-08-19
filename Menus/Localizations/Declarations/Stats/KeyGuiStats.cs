using KeyGUI.Menus.Localizations.Declarations.Stats.RaceModifier;
using KeyGUI.Menus.Localizations.Declarations.Stats.StatBoundEditing;

namespace KeyGUI.Menus.Localizations.Declarations.Stats {
  public class KeyGuiStats : LocalesContainer {
    public readonly KeyGuiStatsRaceModifier RaceModifier = new KeyGuiStatsRaceModifier();
    public readonly KeyGuiStatsStatBoundEditing StatBoundEditing = new KeyGuiStatsStatBoundEditing();
    public readonly LocaleDeclaration StatBoundEditingSubmenuName = new LocaleDeclaration("Edit stat bounds");
    public readonly LocaleDeclaration RaceModifierSubmenuName = new LocaleDeclaration("Mass change stats of a race");
    internal KeyGuiStats() { }
  }
}
