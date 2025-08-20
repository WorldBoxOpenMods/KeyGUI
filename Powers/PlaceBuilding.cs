using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Powers;

namespace KeyGUI.Powers {
  public class PlaceBuilding : KeyGuiPower {
    public readonly KeyGuiLocale SelectCity = "Select the city to place the building in.";
    public readonly KeyGuiLocale InvalidTileError = "Can't place building here, try again!";
    public readonly KeyGuiLocale Success = "Building placed!";
    public readonly KeyGuiLocale NoBuildingSelected = "No building to place selected, try again!";
    
    public PlaceBuilding() : base(new GodPower() {
      id = "place_building_keygui",
      name = "Place Building",
      force_map_mode = MetaType.None
    }) {
    }

    protected override bool PowerButtonPress(string pPower) {
      WorldTip.showNow(SelectCity, false, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      BuildingAsset buildingToPlace = AssetManager.buildings.get(AssetManager.powers.get(pPowerID).drop_id);
      if (buildingToPlace != null) {
        Building newBuilding = World.world.buildings.addBuilding(buildingToPlace.id, pTile);
        if (newBuilding == null) {
          EffectsLibrary.spawnAtTile("fx_bad_place", pTile, 0.25f);
          WorldTip.showNow(InvalidTileError, false, "top");
          return false;
        }
        WorldTip.showNow(Success, false, "top");
        return true;
      }
      WorldTip.showNow(NoBuildingSelected, false, "top");
      return false;
    }
  }
}
