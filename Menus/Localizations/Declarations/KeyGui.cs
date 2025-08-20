using KeyGuiLocalizations = KeyGUI.Menus.Localizations.Declarations.Localizations.KeyGuiLocales;
using KeyGUI.Menus.Localizations.Declarations.Alliances;
using KeyGUI.Menus.Localizations.Declarations.Buildings;
using KeyGUI.Menus.Localizations.Declarations.Cities;
using KeyGUI.Menus.Localizations.Declarations.Clans;
using KeyGUI.Menus.Localizations.Declarations.Cleansing;
using KeyGUI.Menus.Localizations.Declarations.Crabzilla;
using KeyGUI.Menus.Localizations.Declarations.Cultures;
using KeyGUI.Menus.Localizations.Declarations.Destruction;
using KeyGUI.Menus.Localizations.Declarations.Disasters;
using KeyGUI.Menus.Localizations.Declarations.GameSettings;
using KeyGUI.Menus.Localizations.Declarations.ModConfig;
using KeyGUI.Menus.Localizations.Declarations.Plots;
using KeyGUI.Menus.Localizations.Declarations.Powers;
using KeyGUI.Menus.Localizations.Declarations.Stats;
using KeyGUI.Menus.Localizations.Declarations.Traits;
using KeyGUI.Menus.Localizations.Declarations.Zones;

namespace KeyGUI.Menus.Localizations.Declarations {
  public class KeyGui : LocalesContainer {
    public readonly KeyGuiDisasters Disasters = new KeyGuiDisasters();
    public readonly KeyGuiStats Stats = new KeyGuiStats();
    public readonly KeyGuiDestruction Destruction = new KeyGuiDestruction();
    public readonly KeyGuiCleansing Cleansing = new KeyGuiCleansing();
    public readonly KeyGuiCultures Cultures = new KeyGuiCultures();
    public readonly KeyGuiPlots Plots = new KeyGuiPlots();
    public readonly KeyGuiTraits Traits = new KeyGuiTraits();
    public readonly KeyGuiCrabzilla Crabzilla = new KeyGuiCrabzilla();
    public readonly KeyGuiClans Clans = new KeyGuiClans();
    public readonly KeyGuiAlliances Alliances = new KeyGuiAlliances();
    public readonly KeyGuiGameSettings GameSettings = new KeyGuiGameSettings();
    public readonly KeyGuiZones Zones = new KeyGuiZones();
    public readonly KeyGuiCities Cities = new KeyGuiCities();
    public readonly KeyGuiBuildings Buildings = new KeyGuiBuildings();
    public readonly KeyGuiModConfig ModConfig = new KeyGuiModConfig();
    public readonly KeyGuiLocalizations Localizations = new KeyGuiLocalizations();
    public readonly KeyGuiPowers Powers = new KeyGuiPowers();
    public readonly LocaleDeclaration ModName = new LocaleDeclaration(KeyGuiConfig.PluginName);
    public readonly LocaleDeclaration DisastersSubmenuText = new LocaleDeclaration("Disasters");
    public readonly LocaleDeclaration StatsSubmenuText = new LocaleDeclaration("Stats");
    public readonly LocaleDeclaration DestructionSubmenuText = new LocaleDeclaration("Destruction");
    public readonly LocaleDeclaration CleansingSubmenuText = new LocaleDeclaration("Cleansing");
    public readonly LocaleDeclaration CulturesSubmenuText = new LocaleDeclaration("Cultures");
    public readonly LocaleDeclaration PlotsSubmenuText = new LocaleDeclaration("Plots");
    public readonly LocaleDeclaration TraitsSubmenuText = new LocaleDeclaration("Traits");
    public readonly LocaleDeclaration CrabzillaSubmenuText = new LocaleDeclaration("Crabzilla");
    public readonly LocaleDeclaration ClansSubmenuText = new LocaleDeclaration("Clans");
    public readonly LocaleDeclaration AlliancesSubmenuText = new LocaleDeclaration("Alliances");
    public readonly LocaleDeclaration GameSettingsSubmenuText = new LocaleDeclaration("Game Settings");
    public readonly LocaleDeclaration ZonesSubmenuText = new LocaleDeclaration("Zones");
    public readonly LocaleDeclaration CitiesSubmenuText = new LocaleDeclaration("Cities");
    public readonly LocaleDeclaration BuildingsSubmenuText = new LocaleDeclaration("Buildings");
    public readonly LocaleDeclaration ModConfigSubmenuText = new LocaleDeclaration("Mod Config");
    public readonly LocaleDeclaration LocalesSubmenuText = new LocaleDeclaration("Locales");
    public readonly LocaleDeclaration DebugSubmenuText = new LocaleDeclaration("Debug");
    public readonly LocaleDeclaration TestingSubmenuText = new LocaleDeclaration("Testing");
    public readonly LocaleDeclaration MessagePopupTitle = new LocaleDeclaration("Message");
    public readonly LocaleDeclaration KeyGuiOutdatedWarning = new LocaleDeclaration("Your version of KeyGUI is outdated!");
    public readonly LocaleDeclaration DownloadKeyGuiButton = new LocaleDeclaration("Download");
    public readonly LocaleDeclaration DismissKeyGuiOutdatedWarningButton = new LocaleDeclaration("Dismiss");
    public readonly LocaleDeclaration ProblematicModWarning = new LocaleDeclaration("WARNING: It's recommended to not use {0} because {1}!");
    public readonly LocaleDeclaration DismissProblematicModWarningButton = new LocaleDeclaration("Dismiss");
    public readonly LocaleDeclaration CriticalModWarning = new LocaleDeclaration("WARNING: The mod {0} has automatically been disabled by KeyGUI because {1}!");
    public readonly LocaleDeclaration DismissCriticalModWarningButton = new LocaleDeclaration("Dismiss");
    public readonly LocaleDeclaration DisableCriticalModWarningButton = new LocaleDeclaration("Don't disable it the next time");
    internal KeyGui() { }
  }
}
