// ReSharper disable InconsistentNaming
namespace KeyGUI.Menus.Localizations.Declarations.KeyGeneralPurposeLibrary {
  public class KeyGeneralPurposeLibraryLocales : LocalesContainer {
    public readonly LocaleDeclaration KGPLL_AllianceCreation_SelectFirstKingdom = new LocaleDeclaration(true, "Select the first kingdom to create an alliance with.");
    public readonly LocaleDeclaration KGPLL_AllianceCreation_SelectSecondKingdom = new LocaleDeclaration(true, "Select the second kingdom to create an alliance with.");
    public readonly LocaleDeclaration KGPLL_AllianceCreation_SameKingdomTwiceError = new LocaleDeclaration(true, "Selected the same kingdom twice, try again.");
    public readonly LocaleDeclaration KGPLL_AllianceCreation_KingdomsAlreadyAlliedError = new LocaleDeclaration(true, "The selected kingdoms are already allied, try again.");
    public readonly LocaleDeclaration KGPLL_AllianceCreation_CreationSuccess = new LocaleDeclaration(true, "The kingdom {0} has successfully entered an alliance with kingdom {1}.");
    public readonly LocaleDeclaration KGPLL_CultureDeletion_SelectCulture = new LocaleDeclaration(true, "Select the culture to delete.");
    public readonly LocaleDeclaration KGPLL_CultureDeletion_Success = new LocaleDeclaration(true, "Culture deleted!");
    public readonly LocaleDeclaration KGPLL_CultureDeletion_NoCultureSelectedError = new LocaleDeclaration(true, "No culture to delete here, try again!");
    public readonly LocaleDeclaration KGPLL_CultureFullReset_SelectCulture = new LocaleDeclaration(true, "Select the culture to reset.");
    public readonly LocaleDeclaration KGPLL_CultureFullReset_Success = new LocaleDeclaration(true, "Culture reset!");
    public readonly LocaleDeclaration KGPLL_CultureFullReset_NoCultureSelectedError = new LocaleDeclaration(true, "No culture to reset here, try again!");
    public readonly LocaleDeclaration KGPLL_CultureTechReset_SelectCulture = new LocaleDeclaration(true, "Select the culture to reset the tech of.");
    public readonly LocaleDeclaration KGPLL_CultureTechReset_Success = new LocaleDeclaration(true, "Culture tech reset!");
    public readonly LocaleDeclaration KGPLL_CultureTechReset_NoCultureSelectedError = new LocaleDeclaration(true, "No culture to reset the tech of here, try again!");
    public readonly LocaleDeclaration KGPLL_CultureKnowledgeGainModification_SelectIncreaseCulture = new LocaleDeclaration(true, "Select the culture to increase the knowledge gain of.");
    public readonly LocaleDeclaration KGPLL_CultureKnowledgeGainModification_SelectDecreaseCulture = new LocaleDeclaration(true, "Select the culture to decrease the knowledge gain of.");
    public readonly LocaleDeclaration KGPLL_CultureKnowledgeGainModification_IncreaseSuccess = new LocaleDeclaration(true, "Culture knowledge gain increased!");
    public readonly LocaleDeclaration KGPLL_CultureKnowledgeGainModification_DecreaseSuccess = new LocaleDeclaration(true, "Culture knowledge gain decreased!");
    public readonly LocaleDeclaration KGPLL_CultureKnowledgeGainModification_InvalidGainValueError = new LocaleDeclaration(true, "Invalid value for knowledge gain provided, try again!");
    public readonly LocaleDeclaration KGPLL_CultureKnowledgeGainModification_NoCultureSelectedError = new LocaleDeclaration(true, "No culture to change the knowledge gain of here, try again!");
    public readonly LocaleDeclaration KGPLL_CultureForceConversion_SelectCulture = new LocaleDeclaration(true, "Select the culture to force upon the city.");
    public readonly LocaleDeclaration KGPLL_CultureForceConversion_NoCultureSelectedError = new LocaleDeclaration(true, "No culture to force upon city here, try again!");
    public readonly LocaleDeclaration KGPLL_CultureForceConversion_SelectCity = new LocaleDeclaration(true, "Select the city to force the culture upon.");
    public readonly LocaleDeclaration KGPLL_CultureForceConversion_Success = new LocaleDeclaration(true, "Culture forced upon city!");
    public readonly LocaleDeclaration KGPLL_CultureForceConversion_NoCitySelectedError = new LocaleDeclaration(true, "No city to force culture upon here, try again!");
    public readonly LocaleDeclaration KGPLL_CultureCreation_SelectCity = new LocaleDeclaration(true, "Select the city to create a new culture for.");
    public readonly LocaleDeclaration KGPLL_CultureCreation_Success = new LocaleDeclaration(true, "New culture created!");
    public readonly LocaleDeclaration KGPLL_CultureCreation_NoCitySelected = new LocaleDeclaration(true, "No city to create new culture for here, try again!");
    public readonly LocaleDeclaration KGPLL_CityZoneAddition_SelectCity = new LocaleDeclaration(true, "Select the city to add zones to.");
    public readonly LocaleDeclaration KGPLL_CityZoneAddition_SelectZones = new LocaleDeclaration(true, "Select zones to add to the city.");
    public readonly LocaleDeclaration KGPLL_CityZoneAddition_NoCitySelectedError = new LocaleDeclaration(true, "No city to add zone to here, try again!");
    public readonly LocaleDeclaration KGPLL_CityZoneAddition_ZoneOwnershipConflict = new LocaleDeclaration(true, "The zone you're trying to add already belongs to another city, please remove it from that city first!");
    public readonly LocaleDeclaration KGPLL_CityZoneRemoval_SelectCity = new LocaleDeclaration(true, "Select the city to remove zones from.");
    public readonly LocaleDeclaration KGPLL_CityZoneRemoval_SelectZones = new LocaleDeclaration(true, "Select zones to remove.");
    public readonly LocaleDeclaration KGPLL_CityZoneRemoval_NoCitySelectedError = new LocaleDeclaration(true, "No city to remove zone from here, try again!");
    public readonly LocaleDeclaration KGPLL_CityZoneRemoval_ZoneOwnershipConflict = new LocaleDeclaration(true, "The zone you're trying to remove doesn't belong to the city you selected, please select the correct city!");
    public readonly LocaleDeclaration KGPLL_CultureZoneAddition_SelectCulture = new LocaleDeclaration(true, "Select the culture to add zones to.");
    public readonly LocaleDeclaration KGPLL_CultureZoneAddition_SelectZones = new LocaleDeclaration(true, "Select zones to add to the culture.");
    public readonly LocaleDeclaration KGPLL_CultureZoneAddition_NoCultureSelected = new LocaleDeclaration(true, "No culture to add zone to here, try again!");
    public readonly LocaleDeclaration KGPLL_CultureZoneAddition_ZoneOwnershipConflict = new LocaleDeclaration(true, "The zone you're trying to add already belongs to another culture, please remove it from that culture first!");
    public readonly LocaleDeclaration KGPLL_CultureZoneRemoval_SelectCulture = new LocaleDeclaration(true, "Select the culture to remove zones from.");
    public readonly LocaleDeclaration KGPLL_CultureZoneRemoval_SelectZones = new LocaleDeclaration(true, "Select zones to remove.");
    public readonly LocaleDeclaration KGPLL_CultureZoneRemoval_NoCultureSelected = new LocaleDeclaration(true, "No culture to remove zone from here, try again!");
    public readonly LocaleDeclaration KGPLL_CultureZoneRemoval_ZoneOwnershipConflict = new LocaleDeclaration(true, "The zone you're trying to remove doesn't belong to the culture you selected, please select the correct culture!");
    public readonly LocaleDeclaration KGPLL_ForceCapital_SelectCity = new LocaleDeclaration(true, "Select the city to force as the capital city.");
    public readonly LocaleDeclaration KGPLL_ForceCapital_Success = new LocaleDeclaration(true, "City forced as capital city!");
    public readonly LocaleDeclaration KGPLL_ForceCapital_NoCitySelectedError = new LocaleDeclaration(true, "No city to force as capital city here, try again!");
    public readonly LocaleDeclaration KGPLL_ChangeCityKingdom_SelectCity = new LocaleDeclaration(true, "Select the city to force into another kingdom.");
    public readonly LocaleDeclaration KGPLL_ChangeCityKingdom_SelectKingdom = new LocaleDeclaration(true, "Select the kingdom to force the city into.");
    public readonly LocaleDeclaration KGPLL_ChangeCityKingdom_NoCitySelectedError = new LocaleDeclaration(true, "No city to force into other kingdom here, try again!");
    public readonly LocaleDeclaration KGPLL_ChangeCityKingdom_Success = new LocaleDeclaration(true, "City forced into other kingdom!");
    public readonly LocaleDeclaration KGPLL_ChangeCityKingdom_NoKingdomSelectedError = new LocaleDeclaration(true, "The tile you selected isn't owned by any kingdom!");
    public readonly LocaleDeclaration KGPLL_SetKing_SelectActor = new LocaleDeclaration(true, "Select the actor to make king.");
    public readonly LocaleDeclaration KGPLL_SetKing_Success = new LocaleDeclaration(true, "Actor made king!");
    public readonly LocaleDeclaration KGPLL_SetKing_NoKingdomOnSelectedActorError = new LocaleDeclaration(true, "The actor you selected doesn't have a kingdom!");
    public readonly LocaleDeclaration KGPLL_SetKing_NoActorSelectedError = new LocaleDeclaration(true, "No actor to make king here, try again!");
    public readonly LocaleDeclaration KGPLL_PlaceBuilding_SelectCity = new LocaleDeclaration(true, "Select the city to place the building in.");
    public readonly LocaleDeclaration KGPLL_PlaceBuilding_InvalidTileError = new LocaleDeclaration(true, "Can't place building here, try again!");
    public readonly LocaleDeclaration KGPLL_PlaceBuilding_Success = new LocaleDeclaration(true, "Building placed!");
    public readonly LocaleDeclaration KGPLL_PlaceBuilding_NoBuildingSelected = new LocaleDeclaration(true, "No building to place selected, try again!");
  }
}/*
    public readonly LocaleDeclaration KGPLL_ = new LocaleDeclaration(true, );
*/
