using System.Linq;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Buildings.BuildingPlacementBuildingSelector {
  public class KeyGuiBuildingsBuildingPlacementBuildingSelector : KeyGuiMenu {
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
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Buildings.BuildingPlacementBuildingSelector.CloseMenuButton))) {
        Enabled = false;
      }
    }
  }
}
