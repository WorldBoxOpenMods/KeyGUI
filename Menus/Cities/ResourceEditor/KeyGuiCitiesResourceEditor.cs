using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using UnityEngine;

namespace KeyGUI.Menus.Cities.ResourceEditor {
  public class KeyGuiCitiesResourceEditor : KeyGuiMenu {
    public readonly KeyGuiLocale NoCitySelectedError = "No city selected!";
    public readonly KeyGuiLocale CloseMenuButton = "Cancel";
    
    protected override float MenuMaxHeight => 1500;
    protected override float MenuMinHeight => 900;
    private Vector2 _scrollPos;
    internal static City SelectedCity;
    protected override void LoadGUI(int windowID) {
      if (SelectedCity == null) {
        GUILayout.Label(NoCitySelectedError);
        return;
      }
      _scrollPos = GUILayout.BeginScrollView(_scrollPos);
      foreach (ResourceAsset rs in AssetManager.resources.list) {
        Debug.LogError("TODO: fix this after fixing the PowerBox equivalent");
        //GUILayout.Label(rs.id + ": " + SelectedCity.data.storage.get(rs.id));
        //SelectedCity.data.storage.set(rs.id, int.Parse(GUILayout.TextField(SelectedCity.data.storage.get(rs.id).ToString()).Where(char.IsDigit).Aggregate("", (current, c) => current + c)));
      }
      GUILayout.EndScrollView();
      if (GUILayout.Button(CloseMenuButton)) {
        Enabled = false;
      }
    }
  }
}
