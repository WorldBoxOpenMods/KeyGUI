using KeyGUI.Menus.Localizations.Declarations.Traits.Instructions;

namespace KeyGUI.Menus.Localizations.Declarations.Traits {
  public class KeyGuiTraits : LocalesContainer {
    public readonly KeyGuiTraitsInstructions Instructions = new KeyGuiTraitsInstructions();
    public readonly LocaleDeclaration InstructionsSubmenuName = new LocaleDeclaration("Trait Instructions");
    public readonly LocaleDeclaration CreateTraitSection = new LocaleDeclaration("Create Trait:");
    public readonly LocaleDeclaration CreateTraitName = new LocaleDeclaration("Trait Name");
    public readonly LocaleDeclaration CreateTraitDescription = new LocaleDeclaration("Trait Description");
    public readonly LocaleDeclaration CreateTraitBirthChance = new LocaleDeclaration("Trait Birth Chance");
    public readonly LocaleDeclaration CreateTraitInheritChance = new LocaleDeclaration("Trait Inherit Chance");
    public readonly LocaleDeclaration CreateTraitOppositeTraits = new LocaleDeclaration("Opposite Traits");
    public readonly LocaleDeclaration CreateTraitRemoveOppositeTrait = new LocaleDeclaration("Remove {0} From Opposite Traits");
    public readonly LocaleDeclaration CreateTraitAddOppositeTrait = new LocaleDeclaration("Add {0} To Opposite Traits");
    public readonly LocaleDeclaration CreateTraitPartnerTraits = new LocaleDeclaration("Partner Traits");
    public readonly LocaleDeclaration CreateTraitRemovePartnerTrait = new LocaleDeclaration("Remove {0} From Partner Traits");
    public readonly LocaleDeclaration CreateTraitAddPartnerTrait = new LocaleDeclaration("Add {0} To Partner Traits");
    public readonly LocaleDeclaration CreateTraitTraitGroup = new LocaleDeclaration("Trait Group");
    public readonly LocaleDeclaration CreateTraitCurrentTraitGroup = new LocaleDeclaration("Current Group: {0}");
    public readonly LocaleDeclaration CreateTraitSprite = new LocaleDeclaration("Sprite");
    public readonly LocaleDeclaration CreateTraitCurrentSprite = new LocaleDeclaration("Current Sprite: {0}");
    public readonly LocaleDeclaration CreateTraitCreateLabel = new LocaleDeclaration("Create!");
    public readonly LocaleDeclaration CreateTraitCancelEditingButton = new LocaleDeclaration("Cancel Editing");
    public readonly LocaleDeclaration CreateTraitButton = new LocaleDeclaration("Create Trait");
    public readonly LocaleDeclaration CreateTraitInvalidBirthChanceError = new LocaleDeclaration("The birth chance entered is invalid!");
    public readonly LocaleDeclaration CreateTraitInvalidInheritChanceError = new LocaleDeclaration("The inherit chance entered is invalid!");
    public readonly LocaleDeclaration CreateTraitInvalidStatError = new LocaleDeclaration("The stat value entered for {0} is invalid!");
    public readonly LocaleDeclaration CreateTraitSuccess = new LocaleDeclaration("Trait Created!");
    public readonly LocaleDeclaration CreateTraitNoGroupError = new LocaleDeclaration("Please select a trait group!");
    public readonly LocaleDeclaration CreateTraitNoNameError = new LocaleDeclaration("Please enter a trait name!");
    public readonly LocaleDeclaration EditTraitSection = new LocaleDeclaration("Edit Trait:");
    public readonly LocaleDeclaration DeleteTraitSection = new LocaleDeclaration("Delete Trait:");
    public readonly LocaleDeclaration DataControlSection = new LocaleDeclaration("Data Control:");
    public readonly LocaleDeclaration DataControlSaveButton = new LocaleDeclaration("Save");
    public readonly LocaleDeclaration DataControlLoadButton = new LocaleDeclaration("Load");
    public readonly LocaleDeclaration DataControlDisableAutoTraitLoad = new LocaleDeclaration("Turn Automatic Trait Loading Off");
    public readonly LocaleDeclaration DataControlEnableAutoTraitLoad = new LocaleDeclaration("Turn Automatic Trait Loading On");
  }
}
