using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Powers;

namespace KeyGUI.Powers {
  public class MakeActorKing : KeyGuiPower {
    public readonly KeyGuiLocale SelectActor = "Select the actor to make king.";
    public readonly KeyGuiLocale Success = "Actor made king!";
    public readonly KeyGuiLocale NoKingdomOnSelectedActorError = "The actor you selected doesn't have a kingdom!";
    public readonly KeyGuiLocale NoActorSelectedError = "No actor to make king here, try again!";
    
    public MakeActorKing() : base(new GodPower() {
      id = "make_actor_king_keygui",
      name = "Make Actor King",
      force_map_mode = MetaType.None
    }) {
    }

    protected override bool PowerButtonPress(string pPower) {
      WorldTip.showNow(SelectActor, false, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      Actor actorToMakeKing = pTile._units.FirstOrDefault(a => a.kingdom?.isCiv() ?? false);
      if (actorToMakeKing != null) {
        if (actorToMakeKing.kingdom != null) {
          if (actorToMakeKing.kingdom.king != null) {
            actorToMakeKing.kingdom.king.setProfession(UnitProfession.Unit);
          }
          actorToMakeKing.kingdom.setKing(actorToMakeKing);
          WorldTip.showNow(Success, false, "top");
          return true;
        }
        WorldTip.showNow(NoKingdomOnSelectedActorError, false, "top");
      }
      WorldTip.showNow(NoActorSelectedError, false, "top");
      return false;
    }

  }
}
