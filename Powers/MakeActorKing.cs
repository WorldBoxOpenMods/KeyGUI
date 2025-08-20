using System.Linq;
using KeyGUI.Framework.Powers;
using KeyGUI.Menus.Localizations.Declarations;

namespace KeyGUI.Powers {
  public class MakeActorKing : KeyGuiPower {
    public MakeActorKing() : base(new GodPower() {
      id = "make_actor_king_keygui",
      name = "Make Actor King",
      force_map_mode = MetaType.None
    }) {
    }

    protected override bool PowerButtonPress(string pPower) {
      WorldTip.showNow(Locales.KeyGui.Powers.SetKing.SelectActor, false, "top");
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
          WorldTip.showNow(Locales.KeyGui.Powers.SetKing.Success, false, "top");
          return true;
        }
        WorldTip.showNow(Locales.KeyGui.Powers.SetKing.NoKingdomOnSelectedActorError, false, "top");
      }
      WorldTip.showNow(Locales.KeyGui.Powers.SetKing.NoActorSelectedError, false, "top");
      return false;
    }

  }
}
