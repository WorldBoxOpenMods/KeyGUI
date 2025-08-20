using KeyGUI.Menus.Localizations.Declarations.Traits.Instructions;

namespace KeyGUI.Menus.Localizations.Declarations.Traits {
  public class KeyGuiTraits : LocalesContainer {
    public readonly KeyGuiTraitsInstructions Instructions = new KeyGuiTraitsInstructions();
    public readonly LocaleDeclaration InstructionsSubmenuName = "Trait Instructions";
    public readonly LocaleDeclaration CreateTraitSection = "Create Trait:";
    public readonly LocaleDeclaration CreateTraitName = "Trait Name";
    public readonly LocaleDeclaration CreateTraitDescription = "Trait Description";
    public readonly LocaleDeclaration CreateTraitBirthChance = "Trait Birth Chance";
    public readonly LocaleDeclaration CreateTraitInheritChance = "Trait Inherit Chance";
    public readonly LocaleDeclaration CreateTraitOppositeTraits = "Opposite Traits";
    public readonly LocaleDeclaration CreateTraitRemoveOppositeTrait = "Remove {0} From Opposite Traits";
    public readonly LocaleDeclaration CreateTraitAddOppositeTrait = "Add {0} To Opposite Traits";
    public readonly LocaleDeclaration CreateTraitPartnerTraits = "Partner Traits";
    public readonly LocaleDeclaration CreateTraitRemovePartnerTrait = "Remove {0} From Partner Traits";
    public readonly LocaleDeclaration CreateTraitAddPartnerTrait = "Add {0} To Partner Traits";
    public readonly LocaleDeclaration CreateTraitTraitGroup = "Trait Group";
    public readonly LocaleDeclaration CreateTraitCurrentTraitGroup = "Current Group: {0}";
    public readonly LocaleDeclaration CreateTraitSprite = "Sprite";
    public readonly LocaleDeclaration CreateTraitCurrentSprite = "Current Sprite: {0}";
    public readonly LocaleDeclaration CreateTraitCreateLabel = "Create!";
    public readonly LocaleDeclaration CreateTraitCancelEditingButton = "Cancel Editing";
    public readonly LocaleDeclaration CreateTraitButton = "Create Trait";
    public readonly LocaleDeclaration CreateTraitInvalidBirthChanceError = "The birth chance entered is invalid!";
    public readonly LocaleDeclaration CreateTraitInvalidInheritChanceError = "The inherit chance entered is invalid!";
    public readonly LocaleDeclaration CreateTraitInvalidStatError = "The stat value entered for {0} is invalid!";
    public readonly LocaleDeclaration CreateTraitSuccess = "Trait Created!";
    public readonly LocaleDeclaration CreateTraitNoGroupError = "Please select a trait group!";
    public readonly LocaleDeclaration CreateTraitNoNameError = "Please enter a trait name!";
    public readonly LocaleDeclaration EditTraitSection = "Edit Trait:";
    public readonly LocaleDeclaration DeleteTraitSection = "Delete Trait:";
    public readonly LocaleDeclaration DataControlSection = "Data Control:";
    public readonly LocaleDeclaration DataControlSaveButton = "Save";
    public readonly LocaleDeclaration DataControlLoadButton = "Load";
    public readonly LocaleDeclaration DataControlDisableAutoTraitLoad = "Turn Automatic Trait Loading Off";
    public readonly LocaleDeclaration DataControlEnableAutoTraitLoad = "Turn Automatic Trait Loading On";
  }
}
