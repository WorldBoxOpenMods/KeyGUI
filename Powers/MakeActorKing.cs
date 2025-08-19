using System.Linq;
using KeyGUI.MenuArchitecture;

namespace KeyGUI.Powers {
  public class MakeActorKing : KeyGuiPower {
    public MakeActorKing() : base(new GodPower() {
      id = "make_actor_king_keygui",
      name = "Make Actor King",
      force_map_mode = MetaType.None
    }) {
    }

    protected override bool PowerButtonPress(string pPower) {
      WorldTip.showNow("KGPLL_SetKing_SelectActor", true, "top");
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
          WorldTip.showNow("KGPLL_SetKing_Success", true, "top");
          return true;
        }
        WorldTip.showNow("KGPLL_SetKing_NoKingdomOnSelectedActorError", true, "top");
      }
      WorldTip.showNow("KGPLL_SetKing_NoActorSelectedError", true, "top");
      return false;
    }

  }
}
