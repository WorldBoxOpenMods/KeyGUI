using System.Collections.Generic;
using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Powers;

namespace KeyGUI.Powers {
  public class WhisperOfAlliance : KeyGuiPower {
    public readonly KeyGuiLocale SelectFirstKingdom = "Select the first kingdom to create an alliance with.";
    public readonly KeyGuiLocale SelectSecondKingdom = "Select the second kingdom to create an alliance with.";
    public readonly KeyGuiLocale SameKingdomTwiceError = "Selected the same kingdom twice, try again.";
    public readonly KeyGuiLocale KingdomsAlreadyAlliedError = "The selected kingdoms are already allied, try again.";
    public readonly KeyGuiLocale CreationSuccess = "The kingdom {0} has successfully entered an alliance with kingdom {1}.";
    
    public WhisperOfAlliance() : base(new GodPower() {
      id = "create_alliance_keygui",
      name = "Whisper Of Alliance",
      force_map_mode = MetaType.Alliance
    }) {
    }

    protected override bool PowerButtonPress(string pPower) {
      WorldTip.showNow(SelectFirstKingdom, false, "top");
      Config.whisper_A = null;
      Config.whisper_B = null;
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      City city = pTile.zone.city;
      if (city == null) {
        return false;
      }

      Kingdom kingdom = city.kingdom;
      if (Config.whisper_A == null) {
        Config.whisper_A = kingdom;
        WorldTip.showNow(SelectSecondKingdom, false, "top");
        return false;
      }

      if (Config.whisper_B == null && Config.whisper_A == kingdom) {
        WorldTip.showNow(SameKingdomTwiceError, false, "top");
        return false;
      }

      if (Config.whisper_B == null) {
        Config.whisper_B = kingdom;
      }

      if (Config.whisper_B != Config.whisper_A) {
        if (Alliance.isSame(Config.whisper_A.getAlliance(), Config.whisper_B.getAlliance())) {
          WorldTip.showNow(KingdomsAlreadyAlliedError, false, "top");
          Config.whisper_B = null;
          return false;
        }

        foreach (War war in World.world.wars.getWars(Config.whisper_A).Where(war => war.isInWarWith(Config.whisper_A, Config.whisper_B))) {
          war.removeFromWar(Config.whisper_A, true);
          war.removeFromWar(Config.whisper_B, true);
        }

        Alliance allianceA = Config.whisper_A.getAlliance();
        Alliance allianceB = Config.whisper_B.getAlliance();
        if (allianceA != null) {
          if (allianceB != null) {
            IEnumerable<Kingdom> kingdoms = allianceB.kingdoms_list;
            World.world.alliances.dissolveAlliance(allianceB);
            foreach (Kingdom ally in kingdoms) {
              ForceIntoAlliance(allianceA, ally);
            }
          } else {
            ForceIntoAlliance(allianceA, Config.whisper_B);
          }
        } else {
          if (allianceB == null) {
            ForceNewAlliance(Config.whisper_A, Config.whisper_B);
          } else {
            ForceIntoAlliance(allianceB, Config.whisper_A);
          }
        }
        WorldTip.showNow(string.Format(CreationSuccess, Config.whisper_A.name, Config.whisper_B.name), false, "top");
        Config.whisper_A = null;
        Config.whisper_B = null;
      }

      return true;
    }
    private static void ForceIntoAlliance(Alliance alliance, Kingdom kingdom) {
      alliance.kingdoms_hashset.Add(kingdom);
      kingdom.allianceJoin(alliance);
      alliance.recalculate();
      alliance.data.timestamp_member_joined = World.world.getCurWorldTime();
    }

    private static void ForceNewAlliance(Kingdom kingdomA, Kingdom kingdomB) {
      Alliance alliance = World.world.alliances.newObject();
      alliance.createNewAlliance();
      alliance.data.founder_kingdom_id = kingdomA.data.id;
      alliance.data.founder_kingdom_name = kingdomA.data.name;
      if (kingdomA.king != null) {
        alliance.data.founder_actor_id = kingdomA.king.data.id;
        alliance.data.founder_actor_name = kingdomA.king.getName();
      }
      ForceIntoAlliance(alliance, kingdomA);
      ForceIntoAlliance(alliance, kingdomB);
      WorldLog.logAllianceCreated(alliance);
    }
  }
}
