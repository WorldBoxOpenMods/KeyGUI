using KeyGUI.Framework.Menus;
using UnityEngine;

namespace KeyGUI.Menus.Saves {
  public class KeyGuiSaves : KeyGuiMenu {
    protected override void LoadGUI(int windowID) {
      // TODO
      if (GUILayout.Button("Save World Uncompressed")) {
      }
      if (GUILayout.Button("Load Uncompressed World")) {
      }
    }
  }
}
