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
    public readonly LocaleDeclaration ModName = KeyGuiConfig.PluginName;
    public readonly LocaleDeclaration DisastersSubmenuText = "Disasters";
    public readonly LocaleDeclaration StatsSubmenuText = "Stats";
    public readonly LocaleDeclaration DestructionSubmenuText = "Destruction";
    public readonly LocaleDeclaration CleansingSubmenuText = "Cleansing";
    public readonly LocaleDeclaration CulturesSubmenuText = "Cultures";
    public readonly LocaleDeclaration PlotsSubmenuText = "Plots";
    public readonly LocaleDeclaration TraitsSubmenuText = "Traits";
    public readonly LocaleDeclaration CrabzillaSubmenuText = "Crabzilla";
    public readonly LocaleDeclaration ClansSubmenuText = "Clans";
    public readonly LocaleDeclaration AlliancesSubmenuText = "Alliances";
    public readonly LocaleDeclaration GameSettingsSubmenuText = "Game Settings";
    public readonly LocaleDeclaration ZonesSubmenuText = "Zones";
    public readonly LocaleDeclaration CitiesSubmenuText = "Cities";
    public readonly LocaleDeclaration BuildingsSubmenuText = "Buildings";
    public readonly LocaleDeclaration ModConfigSubmenuText = "Mod Config";
    public readonly LocaleDeclaration LocalesSubmenuText = "Locales";
    public readonly LocaleDeclaration DebugSubmenuText = "Debug";
    public readonly LocaleDeclaration TestingSubmenuText = "Testing";
    public readonly LocaleDeclaration MessagePopupTitle = "Message";
    public readonly LocaleDeclaration KeyGuiOutdatedWarning = "Your version of KeyGUI is outdated!";
    public readonly LocaleDeclaration DownloadKeyGuiButton = "Download";
    public readonly LocaleDeclaration DismissKeyGuiOutdatedWarningButton = "Dismiss";
    public readonly LocaleDeclaration ProblematicModWarning = "WARNING: It's recommended to not use {0} because {1}!";
    public readonly LocaleDeclaration DismissProblematicModWarningButton = "Dismiss";
    public readonly LocaleDeclaration CriticalModWarning = "WARNING: The mod {0} has automatically been disabled by KeyGUI because {1}!";
    public readonly LocaleDeclaration DismissCriticalModWarningButton = "Dismiss";
    public readonly LocaleDeclaration DisableCriticalModWarningButton = "Don't disable it the next time";
    internal KeyGui() { }
  }
}
