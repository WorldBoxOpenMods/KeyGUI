using KeyGuiLocalizations = KeyGUI.Menus.Localizations.Declarations.Localizations.KeyGuiLocales;
using System.Collections.Generic;
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
using KeyGUI.Menus.Localizations.Declarations.Items;
using KeyGUI.Menus.Localizations.Declarations.ModConfig;
using KeyGUI.Menus.Localizations.Declarations.Plots;
using KeyGUI.Menus.Localizations.Declarations.Powers;
using KeyGUI.Menus.Localizations.Declarations.Stats;
using KeyGUI.Menus.Localizations.Declarations.Traits;
using KeyGUI.Menus.Localizations.Declarations.Zones;
using UnityEngine;

namespace KeyGUI.Menus.Localizations.Declarations {
  public class Locales : LocalesContainer {
    public static readonly Locales KeyGui = new Locales();
    public readonly KeyGuiDisasters Disasters = new KeyGuiDisasters();
    public readonly KeyGuiStats Stats = new KeyGuiStats();
    public readonly KeyGuiDestruction Destruction = new KeyGuiDestruction();
    public readonly KeyGuiCleansing Cleansing = new KeyGuiCleansing();
    public readonly KeyGuiCultures Cultures = new KeyGuiCultures();
    public readonly KeyGuiPlots Plots = new KeyGuiPlots();
    public readonly KeyGuiTraits Traits = new KeyGuiTraits();
    public readonly KeyGuiItems Items = new KeyGuiItems();
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
    public readonly LocaleDeclaration ModName = new LocaleDeclaration();
    public readonly LocaleDeclaration DisastersSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration StatsSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration DestructionSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration CleansingSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration CulturesSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration PlotsSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration TraitsSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration ItemsSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration CrabzillaSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration ClansSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration AlliancesSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration ActorsSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration GameSettingsSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration ZonesSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration CitiesSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration BuildingsSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration ModConfigSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration LocalesSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration DebugSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration TestingSubmenuText = new LocaleDeclaration();
    public readonly LocaleDeclaration MessagePopupTitle = new LocaleDeclaration();
    public readonly LocaleDeclaration KeyGuiOutdatedWarning = new LocaleDeclaration();
    public readonly LocaleDeclaration DownloadKeyGuiButton = new LocaleDeclaration();
    public readonly LocaleDeclaration DismissKeyGuiOutdatedWarningButton = new LocaleDeclaration();
    public readonly LocaleDeclaration ProblematicModWarning = new LocaleDeclaration();
    public readonly LocaleDeclaration DismissProblematicModWarningButton = new LocaleDeclaration();
    public readonly LocaleDeclaration CriticalModWarning = new LocaleDeclaration();
    public readonly LocaleDeclaration DismissCriticalModWarningButton = new LocaleDeclaration();
    public readonly LocaleDeclaration DisableCriticalModWarningButton = new LocaleDeclaration();
    private static readonly Dictionary<LocaleDeclaration, string> DefaultLocales = new Dictionary<LocaleDeclaration, string>() {
      {KeyGui.Alliances.WhisperOfAllianceButton, "Whisper Of Alliance"},
      {KeyGui.Buildings.BuildingPlacementBuildingSelector.CloseMenuButton, "Cancel"},
      {KeyGui.Buildings.BuildingPlacementBuildingSelectorSubmenuName, "Building Placement Building Selector"},
      {KeyGui.Buildings.BuildingPlacementPlaceSelectedBuildingButton, "Place Selected Building"},
      {KeyGui.Buildings.BuildingPlacementNoBuildingSelectedError, "No building selected to place!"},
      {KeyGui.Cities.ResourceEditor.NoCitySelectedError, "No city selected!"},
      {KeyGui.Cities.ResourceEditor.CloseMenuButton, "Cancel"},
      {KeyGui.Cities.ResourceEditorSubmenuName, "Resource Editor"},
      {KeyGui.Cities.ForceCityAsCapitalCityButton, "Force City As Capital City"},
      {KeyGui.Cities.ForceCityIntoOtherKingdomButton, "Force City Into Other Kingdom"},
      {KeyGui.Cities.SelectCityToEditResourcesOfListHeader, "Select City to edit resources of:"},
      {KeyGui.Clans.TraitSelection.RemoveTraitFromClanButton, "Remove {0} from {1}"},
      {KeyGui.Clans.TraitSelection.AddTraitToClanButton, "Add {0} to {1}"},
      {KeyGui.Clans.TraitSelection.NoClanSelectedError, "No clan selected!"},
      {KeyGui.Clans.TraitSelection.SelectTraitsForClanHeader, "Select Traits for {0}"},
      {KeyGui.Clans.TraitSelection.CloseMenuButton, "Close"},
      {KeyGui.Clans.TraitSelectionSubmenuName, "Clan Trait Selection"},
      {KeyGui.Clans.SelectClanListHeader, "Select Clan:"},
      {KeyGui.Cleansing.GetRidOfMushSporesButton, "Get Rid Of Mush Spores"},
      {KeyGui.Cleansing.CureThePlagueButton, "Cure The Plague"},
      {KeyGui.Cleansing.KillEveryTumorButton, "Kill Every Tumor"},
      {KeyGui.Cleansing.ExterminateZombiesButton, "Exterminate Zombies"},
      {KeyGui.Cleansing.DeleteAllRoadsButton, "Delete All Roads"},
      {KeyGui.Cleansing.LowerAllMountainsButton, "Lower All Mountains"},
      {KeyGui.Cleansing.LowerAllHillsButton, "Lower All Hills"},
      {KeyGui.Cleansing.DestroyAllBrokenBuildingsButton, "Destroy All Broken Buildings"},
      {KeyGui.Cleansing.BiomesCategoryHeader, "Biomes"},
      {KeyGui.Cleansing.BiomeDeletionListHeader, "Biome Deletion:"},
      {KeyGui.Cleansing.GetRidOfBiomeButton, "Get Rid Of {0} Biome"},
      {KeyGui.Cleansing.BiomeRandomizationListHeader, "Biome Randomization:"},
      {KeyGui.Cleansing.RandomizeBiomesButton, "Randomize Biomes"},
      {KeyGui.Cleansing.BiomeRandomizationBiomeEnablingDisablingListHeader, "Biome Enabling/Disabling:"},
      {KeyGui.Cleansing.BiomeRandomizationEnableBiomeButton, "Enable {0} Biome"},
      {KeyGui.Cleansing.BiomeRandomizationDisableBiomeButton, "Disable {0} Biome"},
      {KeyGui.Crabzilla.CrabzillaNotSpawnedError, "Some buttons are disabled because Crabzilla is not spawned."},
      {KeyGui.Crabzilla.LiveCrabzillaChangesSectionTitle, "Live Crabzilla Changes:"},
      {KeyGui.Crabzilla.InspectCrabzillaButton, "Inspect Crabzilla"},
      {KeyGui.Crabzilla.FullyHealCrabzillaButton, "Fully Heal Crabzilla"},
      {KeyGui.Crabzilla.CrabzillaStatChangesSectionTitle, "Crabzilla Stat Changes:"},
      {KeyGui.Crabzilla.AttackStatLabel, "Attack"},
      {KeyGui.Crabzilla.SetAttackButton, "Set Attack"},
      {KeyGui.Crabzilla.ExplosionStrengthStatLabel, "Explosion Strength"},
      {KeyGui.Crabzilla.SetExplosionStrengthButton, "Set Explosion Strength"},
      {KeyGui.Crabzilla.ExplosionRadiusStatLabel, "Explosion Radius"},
      {KeyGui.Crabzilla.SetExplosionRadiusButton, "Set Explosion Radius"},
      {KeyGui.Crabzilla.CrabzillaBoolFlagChangesSectionTitle, "Crabzilla Bool Flag Changes:"},
      {KeyGui.Crabzilla.SetTheFlagToBoolButton, "Set the {0} flag to {1}"},
      {KeyGui.Crabzilla.CrabzillaDoesntExistError, "Crabzilla somehow doesn't exist in your game Ig."},
      {KeyGui.Cultures.DeleteOneCultureButton, "Delete One Culture"},
      {KeyGui.Cultures.DeleteEveryCultureButton, "Delete Every Culture"},
      {KeyGui.Cultures.ResetOneCultureButton, "Reset One Culture"},
      {KeyGui.Cultures.ResetEveryCultureButton, "Reset Every Culture"},
      {KeyGui.Cultures.ResetTechOfOneCultureButton, "Reset Tech Of One Culture"},
      {KeyGui.Cultures.ResetTechOfEveryCultureButton, "Reset Tech Of Every Culture"},
      {KeyGui.Cultures.KnowledgeGainIncreaseDecreaseAmountLabel, "Knowledge Gain Increase/Decrease Amount:"},
      {KeyGui.Cultures.InvalidKnowledgeGainIncreaseDecreaseAmountEnteredError, "Invalid value entered!"},
      {KeyGui.Cultures.IncreaseKnowledgeGainOfOneCultureButton, "Increase Knowledge Gain Of One Culture"},
      {KeyGui.Cultures.IncreaseKnowledgeGainOfEveryCultureButton, "Increase Knowledge Gain Of Every Culture"},
      {KeyGui.Cultures.DecreaseKnowledgeGainOfOneCultureButton, "Decrease Knowledge Gain Of One Culture"},
      {KeyGui.Cultures.DecreaseKnowledgeGainOfEveryCultureButton, "Decrease Knowledge Gain Of Every Culture"},
      {KeyGui.Cultures.ForceExistingCultureUponSpecificCityButton, "Force Existing Culture Upon Specific City"},
      {KeyGui.Cultures.CreateNewCultureAtSpecificCityButton, "Create New Culture At Specific City"},
      {KeyGui.Destruction.MakeEverythingGoMadButton, "Make Everything Go Mad"},
      {KeyGui.Destruction.CauseAMassBombingButton, "Cause A Mass Bombing"},
      {KeyGui.Destruction.MassInfectEverythingWithMushSporesButton, "Mass Infect Everything With Mush Spores"},
      {KeyGui.Destruction.CauseEveryDisasterButton, "Cause Every Disaster"},
      {KeyGui.Destruction.CauseAWorldWarButton, "Cause A World War"},
      {KeyGui.Destruction.ChangeEveryBiomeToTheSwampBiomeButton, "Change Every Biome To The Swamp Biome"},
      {KeyGui.Destruction.ChangeEveryBiomeToTheCorruptedBiomeButton, "Change Every Biome To The Corrupted Biome"},
      {KeyGui.Disasters.BoilTogetherHeatwaveButton, "Boil Together Heatwave"},
      {KeyGui.Disasters.TriggerEarthquakeButton, "Trigger Earthquake"},
      {KeyGui.Disasters.CauseTornadoButton, "Cause Tornado"},
      {KeyGui.Disasters.ImplantModThoughtNoVillageFoundError, "No village found for Mad Thoughts!"},
      {KeyGui.Disasters.ImplantMadThoughtButton, "Implant Mad Thought"},
      {KeyGui.Disasters.GenerateMeteoriteButton, "Generate Meteorite"},
      {KeyGui.Disasters.BringForthEvilMageButton, "Bring Forth Evil Mage"},
      {KeyGui.Disasters.BuildSomeSnowmenButton, "Build Some Snowmen"},
      {KeyGui.Disasters.StealDemonsFromHellButton, "Steal Demons From Hell"},
      {KeyGui.Disasters.InvadeWorldWithBanditsButton, "Invade World With Bandits"},
      {KeyGui.Disasters.GrabFrozenOnesFromNorthPoleButton, "Grab Frozen Ones From North Pole"},
      {KeyGui.Disasters.UnleashNecromancerButton, "Unleash Necromancer"},
      {KeyGui.Disasters.AwakenDragonButton, "Awaken Dragon"},
      {KeyGui.Disasters.UncoverAliensButton, "Uncover Aliens"},
      {KeyGui.Disasters.DigUpBiomassNoVillageFoundError, "No village found for Biomass!"},
      {KeyGui.Disasters.DigUpBiomassButton, "Dig Up Biomass"},
      {KeyGui.Disasters.GiveBirthToTumorNoVillageFoundError, "No village found for Tumor!"},
      {KeyGui.Disasters.GiveBirthToTumorButton, "Give Birth To Tumor"},
      {KeyGui.Disasters.GrowGardenSurpriseNoVillageFoundError, "No village found for Garden Surprise!"},
      {KeyGui.Disasters.GrowGardenSurpriseNoWindmillFoundError, "No windmill found for Garden Surprise!"},
      {KeyGui.Disasters.GrowGardenSurpriseButton, "Grow Garden Surprise"},
      {KeyGui.Disasters.SummonGregNoVillageFoundError, "No village found for Greg!"},
      {KeyGui.Disasters.SummonGregNoMineFoundError, "No mine found for Greg!"},
      {KeyGui.Disasters.SummonGregButton, "Summon Greg"},
      {KeyGui.GameSettings.SavesLabel, "Saves"},
      {KeyGui.GameSettings.EnableAutosavesButton, "Enable autosaves"},
      {KeyGui.GameSettings.DisableAutosavesButton, "Disable autosaves"},
      {KeyGui.GameSettings.UnityFpsLabel, "Unity FPS"},
      {KeyGui.GameSettings.SetUnityFpsButton, "Set"},
      {KeyGui.GameSettings.UnitySpeedModifierLabel, "Unity Speed Modifier (in %)"},
      {KeyGui.GameSettings.SetUnitySpeedModifierButton, "Set"},
      {KeyGui.GameSettings.GameSpeedModifierLabel, "Game Speed Modifier (in %)"},
      {KeyGui.GameSettings.SetGameSpeedModifierButton, "Set"},
      {KeyGui.GameSettings.CameraMovementLabel, "Camera Movement"},
      {KeyGui.GameSettings.ToggleMouseDragButton, "Toggle Mouse Drag"},
      {KeyGui.GameSettings.MapGenerationLabel, "Map Generation"},
      {KeyGui.GameSettings.SetMapSeedButton, "Set seed"},
      {KeyGui.GameSettings.BoatsLabel, "Boats"},
      {KeyGui.GameSettings.EnableBoatsBreakingIceButton, "Enable Boats Breaking Ice"},
      {KeyGui.GameSettings.DisableBoatsBreakingIceButton, "Disable Boats Breaking Ice"},
      {KeyGui.GameSettings.WorldPropertiesLabel, "World Properties"},
      {KeyGui.GameSettings.SetWorldTimeButton, "Set world time"},
      {KeyGui.Plots.PeacePlotTargetSelection.CloseMenuButton, "Cancel"},
      {KeyGui.Plots.PeacePlotTargetSelectionSubmenuName, "Stop War"},
      {KeyGui.Plots.WarSelectorInitiatorKingdomLabel, "Select the Initiator Kingdom"},
      {KeyGui.Plots.WarSelectorTargetKingdomLabel, "Select the Target Kingdom"},
      {KeyGui.Plots.RebellionSelectorInitiatorCityLabel, "Select the Initiator City"},
      {KeyGui.Plots.AllianceCreationSelectorInitiatorKingdomLabel, "Select the Initiator Kingdom"},
      {KeyGui.Plots.AllianceCreationSelectorTargetKingdomLabel, "Select the Target Kingdom"},
      {KeyGui.Plots.AllianceJoinSelectorInitiatorKingdomLabel, "Select the Initiator Kingdom"},
      {KeyGui.Plots.AllianceJoinSelectorTargetAllianceLabel, "Select the Target Alliance"},
      {KeyGui.Plots.AllianceBreakSelectorInitiatorKingdomLabel, "Select the Initiator Kingdom"},
      {KeyGui.Plots.AllianceBreakSelectorTargetAllianceLabel, "Select the Target Alliance"},
      {KeyGui.Plots.TotalWarSelectorInitiatorKingdomLabel, "Select the Initiator Kingdom"},
      {KeyGui.Plots.CreatePlotCategoryHeader, "Create a plot"},
      {KeyGui.Plots.CreateWarPlotButton, "War"},
      {KeyGui.Plots.CreateRebellionPlotButton, "Rebellion"},
      {KeyGui.Plots.CreateAllianceCreationPlotButton, "Alliance Creation"},
      {KeyGui.Plots.CreateAllianceJoinPlotButton, "Alliance Join"},
      {KeyGui.Plots.CreateAllianceBreakPlotButton, "Alliance Break"},
      {KeyGui.Plots.CreateTotalWarPlotButton, "World War"},
      {KeyGui.Plots.CancelPlotCategoryHeader, "Cancel a plot"},
      {KeyGui.Plots.TogglePlotsCategoryHeader, "Toggle plots"},
      {KeyGui.Plots.AllowWarsButton, "Allow Wars"},
      {KeyGui.Plots.DisallowWarsButton, "Disallow Wars"},
      {KeyGui.Plots.AllowRebellionsButton, "Allow Rebellions"},
      {KeyGui.Plots.DisallowRebellionsButton, "Disallow Rebellions"},
      {KeyGui.Plots.AllowAllianceCreationButton, "Allow Alliance Creation"},
      {KeyGui.Plots.DisallowAllianceCreationButton, "Disallow Alliance Creation"},
      {KeyGui.Plots.AllowAllianceJoiningButton, "Allow Alliance Joining"},
      {KeyGui.Plots.DisallowAllianceJoiningButton, "Disallow Alliance Joining"},
      {KeyGui.Plots.AllowAllianceBreakingButton, "Allow Alliance Breaking"},
      {KeyGui.Plots.DisallowAllianceBreakingButton, "Disallow Alliance Breaking"},
      {KeyGui.Plots.AllowWarStoppingButton, "Allow War Stopping"},
      {KeyGui.Plots.DisallowWarStoppingButton, "Disallow War Stopping"},
      {KeyGui.Plots.AllowTotalWarsButton, "Allow Total Wars"},
      {KeyGui.Plots.DisallowTotalWarsButton, "Disallow Total Wars"},
      {KeyGui.Stats.RaceModifier.StatModifier.NoRaceSelectedError, "No race selected!"},
      {KeyGui.Stats.RaceModifier.StatModifier.ModifyingRaceLabel, "Modifying {0}"},
      {KeyGui.Stats.RaceModifier.StatModifier.ModifyStatButton, "Modify {0}"},
      {KeyGui.Stats.RaceModifier.StatModifier.InvalidStatNumberValueEnteredError, "Please enter a valid number."},
      {KeyGui.Stats.RaceModifier.StatModifier.CloseMenuButton, "Close"},
      {KeyGui.Stats.RaceModifier.StatModifierSubmenuName, "Race Stat Modifier"},
      {KeyGui.Stats.RaceModifier.ShowNonCivRacesButton, "Show non civ races too"},
      {KeyGui.Stats.RaceModifier.HideNonCivRacesButton, "Only show civ races"},
      {KeyGui.Stats.RaceModifier.ModifyRaceButton, "Modify {0}"},
      {KeyGui.Stats.RaceModifier.CloseMenuButton, "Close"},
      {KeyGui.Stats.StatBoundEditing.EditStat.NoStatSelectedError, "No stat selected!"},
      {KeyGui.Stats.StatBoundEditing.EditStat.ModifyingStatLabel, "Modifying {0}"},
      {KeyGui.Stats.StatBoundEditing.EditStat.DisableStatBoundsButton, "Disable stat bounds"},
      {KeyGui.Stats.StatBoundEditing.EditStat.EnableStatBoundsButton, "Enable stat bounds"},
      {KeyGui.Stats.StatBoundEditing.EditStat.HideStatButton, "Hide stat"},
      {KeyGui.Stats.StatBoundEditing.EditStat.ShowStatButton, "Show stat"},
      {KeyGui.Stats.StatBoundEditing.EditStat.CurrentMinAndMaxLabel, "Current Min: {0}, Current Max: {1}"},
      {KeyGui.Stats.StatBoundEditing.EditStat.MinLabel, "Min:"},
      {KeyGui.Stats.StatBoundEditing.EditStat.SetMinButton, "Set"},
      {KeyGui.Stats.StatBoundEditing.EditStat.InvalidMinNumberEnteredError, "Please enter a valid number."},
      {KeyGui.Stats.StatBoundEditing.EditStat.MaxLabel, "Max:"},
      {KeyGui.Stats.StatBoundEditing.EditStat.SetMaxButton, "Set"},
      {KeyGui.Stats.StatBoundEditing.EditStat.InvalidMaxNumberEnteredError, "Please enter a valid number."},
      {KeyGui.Stats.StatBoundEditing.EditStat.CloseMenuButton, "Close"},
      {KeyGui.Stats.StatBoundEditing.EditStatSubmenuName, "Edit Stat"},
      {KeyGui.Stats.StatBoundEditing.ModifyStatButton, "Modify {0}"},
      {KeyGui.Stats.StatBoundEditing.CloseMenuButton, "Close"},
      {KeyGui.Stats.StatBoundEditingSubmenuName, "Edit stat bounds"},
      {KeyGui.Stats.RaceModifierSubmenuName, "Mass change stats of a race"},
      {KeyGui.Traits.Instructions.NecessaryTraitValues, "Necessary Trait Values:\nTrait Name\nTrait Description\nTrait Group"},
      {KeyGui.Traits.Instructions.Tips, "Tips:\nIt's recommended to keep trait sprites 28x28 or smaller. Any size works, but larger ones can experience issues.\nKeyGUI includes a feature called \"Partner Traits\". Any traits that you select as partner traits for your trait will automatically be added to any unit that has your trait.\nThis is useful for combining special trait effects like immortality with your custom trait."},
      {KeyGui.Traits.Instructions.CloseMenuButton, "Close"},
      {KeyGui.Traits.InstructionsSubmenuName, "Trait Instructions"},
      {KeyGui.Zones.AddZoneToCityButton, "Add Zone To City"},
      {KeyGui.Zones.RemoveZoneFromCityButton, "Remove Zone From City"},
      {KeyGui.Zones.AddZoneToCultureButton, "Add Zone To Culture"},
      {KeyGui.Zones.RemoveZoneFromCultureButton, "Remove Zone From Culture"},
      {KeyGui.Localizations.DumpLocalesButton, "Dump Locales"},
      {KeyGui.Localizations.ChangeLocalesSectionHeader, "Change Locales"},
      {KeyGui.ModName, KeyGuiConfig.PluginName},
      {KeyGui.DisastersSubmenuText, "Disasters"},
      {KeyGui.StatsSubmenuText, "Stats"},
      {KeyGui.DestructionSubmenuText, "Destruction"},
      {KeyGui.CleansingSubmenuText, "Cleansing"},
      {KeyGui.CulturesSubmenuText, "Cultures"},
      {KeyGui.PlotsSubmenuText, "Plots"},
      {KeyGui.TraitsSubmenuText, "Traits"},
      {KeyGui.ItemsSubmenuText, "Items"},
      {KeyGui.CrabzillaSubmenuText, "Crabzilla"},
      {KeyGui.ClansSubmenuText, "Clans"},
      {KeyGui.AlliancesSubmenuText, "Alliances"},
      {KeyGui.ActorsSubmenuText, "Actors"},
      {KeyGui.GameSettingsSubmenuText, "Game Settings"},
      {KeyGui.ZonesSubmenuText, "Zones"},
      {KeyGui.CitiesSubmenuText, "Cities"},
      {KeyGui.BuildingsSubmenuText, "Buildings"},
      {KeyGui.ModConfigSubmenuText, "Mod Config"},
      {KeyGui.LocalesSubmenuText, "Locales"},
      {KeyGui.DebugSubmenuText, "Debug"},
      {KeyGui.TestingSubmenuText, "Testing"},
      {KeyGui.MessagePopupTitle, "Message"},
      {KeyGui.KeyGuiOutdatedWarning, "Your version of KeyGUI is outdated!"},
      {KeyGui.DownloadKeyGuiButton, "Download"},
      {KeyGui.DismissKeyGuiOutdatedWarningButton, "Dismiss"},
      {KeyGui.ProblematicModWarning, "WARNING: It's recommended to not use {0} because {1}!"},
      {KeyGui.DismissProblematicModWarningButton, "Dismiss"},
      {KeyGui.CriticalModWarning, "WARNING: The mod {0} has automatically been disabled by KeyGUI because {1}!"},
      {KeyGui.DismissCriticalModWarningButton, "Dismiss"},
      {KeyGui.DisableCriticalModWarningButton, "Don't disable it the next time"}
    };
    internal static void SyncDefaultLocale(LocaleDeclaration declaration) {
      if (DefaultLocales.TryGetValue(declaration, out string locale)) {
        declaration.DefaultValue = locale;
      } else {
        DefaultLocales[declaration] = declaration.DefaultValue;
      }
    }
    internal static Dictionary<LocaleDeclaration, string> GetDefaultLocales() {
      return new Dictionary<LocaleDeclaration, string>(DefaultLocales);
    }
    private static readonly List<LocaleDeclaration> DetectedMissingLocales = new List<LocaleDeclaration>();
    private Locales() { }
    public static string Get(LocaleDeclaration declaration) {
      string result = KeyGui.GetLocale(declaration);
      if (result == null) {
        result = DefaultLocales[declaration];
        if (KeyGuiLocales.SuccessfullyFinishedLoadingLocales && !DetectedMissingLocales.Contains(declaration)) {
          Debug.LogWarning($"Missing locale declaration from loaded JSON: {declaration.LocaleId}");
          if (result == null) {
            Debug.LogError($"Missing default locale declaration: {declaration.LocaleId}");
          }
          DetectedMissingLocales.Add(declaration);
        }
      }

      return result;
    }
  }
}
