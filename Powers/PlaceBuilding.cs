using KeyGUI.MenuArchitecture;

namespace KeyGUI.Powers {
  public class PlaceBuilding : KeyGuiPower {
    public PlaceBuilding() : base(new GodPower() {
      id = "place_building_keygui",
      name = "Place Building",
      force_map_mode = MetaType.None
    }) {
    }

    protected override bool PowerButtonPress(string pPower) {
      WorldTip.showNow("KGPLL_PlaceBuilding_SelectCity", true, "top");
      return false;
    }

    protected override bool ClickWithPower(WorldTile pTile, string pPowerID) {
      BuildingAsset buildingToPlace = AssetManager.buildings.get(AssetManager.powers.get(pPowerID).drop_id);
      if (buildingToPlace != null) {
        Building newBuilding = World.world.buildings.addBuilding(buildingToPlace.id, pTile);
        if (newBuilding == null) {
          EffectsLibrary.spawnAtTile("fx_bad_place", pTile, 0.25f);
          WorldTip.showNow("KGPLL_PlaceBuilding_InvalidTileError", true, "top");
          return false;
        }
        WorldTip.showNow("KGPLL_PlaceBuilding_Success", true, "top");
        return true;
      }
      WorldTip.showNow("KGPLL_PlaceBuilding_NoBuildingSelected", true, "top");
      return false;
    }
  }
}
