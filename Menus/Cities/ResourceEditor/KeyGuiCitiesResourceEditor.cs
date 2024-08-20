using System;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using UnityEngine;

namespace KeyGUI.Menus.Cities.ResourceEditor {
  public class KeyGuiCitiesResourceEditor : KeyGuiMenu {
    protected override float MenuMaxHeight => 1500;
    protected override float MenuMinHeight => 900;
    private Vector2 _scrollPos;
    internal static City SelectedCity;
    protected override void LoadGUI(int windowID) {
      if (SelectedCity == null) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Cities.ResourceEditor.NoCitySelectedError));
        return;
      }
      _scrollPos = GUILayout.BeginScrollView(_scrollPos);
      foreach (ResourceAsset rs in AssetManager.resources.list) {
        GUILayout.Label(rs.id + ": " + SelectedCity.data.storage.get(rs.id));
        SelectedCity.data.storage.set(rs.id, (int) Math.Round(GUILayout.HorizontalSlider(SelectedCity.data.storage.get(rs.id), 0f, SelectedCity.data.storage.get(rs.id) + 1)));
      }
      GUILayout.EndScrollView();
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cities.ResourceEditor.CloseMenuButton))) {
        Enabled = false;
      }
    }
  }
}