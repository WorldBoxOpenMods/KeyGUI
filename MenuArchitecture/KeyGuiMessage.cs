using KeyGUI.Menus.Localizations;
using UnityEngine;

namespace KeyGUI.MenuArchitecture {
  public class KeyGuiMessage : KeyGuiMenu {
    private string Message { get; }
    private bool _firstWindowDisplay = true;
    public KeyGuiMessage(LocaleDeclaration title, int windowID, string message) {
      InitMenuInfo(title, windowID, this);
      Message = message;
      OfferVisibilityToggle = false;
      Enabled = true;
    }
    protected override void LoadGUI(int windowID) {
      GUILayout.Label("Message From Creator:");
      GUILayout.Label(Message);
      GUILayout.Label("");
      if (GUILayout.Button("Dismiss")) {
        Enabled = false;
      }

      if (_firstWindowDisplay) {
        _firstWindowDisplay = false;
        MenuRect.x = Screen.width / 2f - MenuRect.width / 2f;
        MenuRect.y = Screen.height / 2f - MenuRect.height / 2f;
      }
    }
  }
}
