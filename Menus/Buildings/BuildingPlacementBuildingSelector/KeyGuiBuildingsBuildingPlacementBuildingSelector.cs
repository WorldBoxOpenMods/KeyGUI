using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using UnityEngine;

namespace KeyGUI.Menus.Buildings.BuildingPlacementBuildingSelector {
  public class KeyGuiBuildingsBuildingPlacementBuildingSelector : KeyGuiMenu {
    public readonly KeyGuiLocale CloseMenuButton = "Cancel";

    internal static BuildingAsset SelectedBuilding { get; private set; }
    protected override float MenuMaxHeight => 1500;
    protected override float MenuMinHeight => 900;
    private Vector2 _scrollPos;
    protected override void LoadGUI(int windowID) {
      _scrollPos = GUILayout.BeginScrollView(_scrollPos);
      foreach (BuildingAsset buildingAsset in AssetManager.buildings.list.Where(buildingAsset => GUILayout.Button(buildingAsset.id))) {
        SelectedBuilding = buildingAsset;
      }
      GUILayout.EndScrollView();
      if (GUILayout.Button(CloseMenuButton)) {
        Enabled = false;
      }
    }
  }
}
