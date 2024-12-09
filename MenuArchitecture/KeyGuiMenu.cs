using System;
using System.Collections.Generic;
using System.Reflection;
using BepinexModCompatibilityLayer;
using HarmonyLib;
using JetBrains.Annotations;
using KeyGUI.Menus.Localizations;
using UnityEngine;
// ReSharper disable InconsistentNaming

namespace KeyGUI.MenuArchitecture {
  public abstract class KeyGuiMenu {
    protected Rect MenuRect = new Rect(0, 0, 300, 50);
    protected virtual float MenuMaxWidth => 400;
    protected virtual float MenuMinWidth => 300;
    protected virtual float MenuMinHeight => 50;
    protected virtual float MenuMaxHeight => 500;
    
    internal LocaleDeclaration Title { get; private set; }
    protected string FullName => (ParentMenu?.FullName == null ? "" : ParentMenu.FullName + " ") + Title;
    private string PrettyWindowID => WindowID.ToString().Length >= 6 ? WindowID.ToString().Substring(0, 6) + (WindowID.ToString().Length > 6 ? "_" : "") + WindowID.ToString().Substring(6).Join(delimiter: "_") : WindowID + "0";

    internal bool Enabled;
    internal bool IsInitialized { get; private set; }
    private bool IsMenuInfoInitialized { get; set; }
    private byte RepeatedLogicErrorCount { get; set; }
    private byte RepeatedUiErrorCount { get; set; }
    private bool ShownUpdateError { get; set; }
    [CanBeNull] private KeyGuiMenu ParentMenu { get; set; }
    internal bool OfferVisibilityToggle = true;
    protected int WindowID { get; private set; }
    
    internal void Initialize() {
      if (IsInitialized) {
        return;
      }
      if (RepeatedLogicErrorCount > 2) {
        IsInitialized = true;
        Debug.LogError($"Repeated error count exceeded for {FullName} ({PrettyWindowID}), menu will not be initialized!");
        return;
      }
      try {
        InitializeMenu();
        RepeatedLogicErrorCount = 0;
      } catch (Exception e) {
        Debug.LogError($"Error initializing {FullName} ({PrettyWindowID})!");
        Debug.LogException(e);
        RepeatedLogicErrorCount++;
      }
    }

    protected virtual void InitializeMenu() {
      IsInitialized = true;
      if (!WindowFocusManager.Initialized) {
        WindowFocusManager.Initialize();
      }
    }
    internal void Update() {
      if (RepeatedLogicErrorCount > 2) {
        if (!ShownUpdateError) {
          Debug.LogError($"Repeated error count exceeded for {FullName} ({PrettyWindowID}), menu will not be updated!");
          ShownUpdateError = true;
        }
        return;
      }
      try {
        UpdateMenu();
        RepeatedLogicErrorCount = 0;
      } catch (Exception e) {
        Debug.LogError($"Error updating {FullName} ({PrettyWindowID})!");
        Debug.LogException(e);
        RepeatedLogicErrorCount++;
      }
    }
    protected virtual void UpdateMenu() {}
    public void LoadMenu() {
      if (!IsMenuInfoInitialized) {
        throw new InvalidMenuInvocationContextException("MenuInfo not initialized!");
      }
      MenuRect = GUILayout.Window(ModCompatibilityLayer.WindowIdSeparator.GetValidWindowId(WindowID, LoadGUI_Internal), MenuRect, LoadGUI_Internal, Title.Value, GUILayout.MaxWidth(MenuMaxWidth), GUILayout.MinWidth(MenuMinWidth), GUILayout.MinHeight(MenuMinHeight), GUILayout.MaxHeight(MenuMaxHeight));
    }
    internal virtual void ForceLoadMenu() {
      LoadMenu();
    }
    private void LoadGUI_Internal(int windowID) {
      Event current = Event.current;
      bool inUse = current.type == EventType.MouseDown || current.type == EventType.MouseUp || current.type == EventType.MouseDrag || current.type == EventType.MouseMove;
      if(inUse) {
        WindowFocusManager.WindowInUse = windowID;
      } else {
        WindowFocusManager.WindowInUse = -1;
      }
      if (RepeatedUiErrorCount > 2) {
          GUILayout.Label($"Repeated error count exceeded for {FullName} ({PrettyWindowID}), menu contents will not be loaded unless overriden!");
          if (GUILayout.Button("Override")) {
            RepeatedUiErrorCount = 0;
          }
      } else {
        try {
          LoadGUI(windowID);
          RepeatedUiErrorCount = 0;
        } catch (Exception e) {
          Debug.LogError($"Error loading GUI for {FullName} ({PrettyWindowID})!");
          Debug.LogException(e);
          RepeatedUiErrorCount++;
        }
      }
      GUI.DragWindow();
    }
    protected abstract void LoadGUI(int windowID);

    internal void InitMenuInfo(LocaleDeclaration title, int windowID, KeyGuiMenu parentMenu) {
      IsMenuInfoInitialized = true;
      Title = title;
      bool windowIdAssigned = false;
      do {
        try {
          WindowID = WindowIdSeparator.RequestId(windowID, this);
          windowIdAssigned = true;
        } catch (InvalidMenuInvocationContextException) {
          Debug.LogError("Internal overlap of window ID " + windowID + " for menu " + FullName + " with " + WindowIdSeparator.GetMenuFromWindowId(windowID).FullName + "!");
        }
      } while (!windowIdAssigned);
      ParentMenu = parentMenu;
    }
    internal void InitMenuInfo(LocaleDeclaration title, int windowID, KeyGuiMenu parentMenu, float menuHorizontalOffset) {
      InitMenuInfo(title, windowID, parentMenu);
      MenuRect.x = menuHorizontalOffset;
    }

    private static class WindowFocusManager {
      public static int WindowInUse = -1;
      public static bool Initialized { get; private set; }

      public static void Initialize() {
        if (Initialized) {
          return;
        }
        Initialized = true;
        Harmony harmony = new Harmony(KeyGuiConfig.PluginGuid);
        MethodInfo original = AccessTools.Method(typeof(MapBox), nameof(MapBox.updateControls));
        MethodInfo patch = AccessTools.Method(typeof(WindowFocusManager), nameof(updateControls_Prefix));
        harmony.Patch(original, new HarmonyMethod(patch));

        original = AccessTools.Method(typeof(MapBox), nameof(MapBox.isActionHappening));
        patch = AccessTools.Method(typeof(WindowFocusManager), nameof(isActionHappening_Postfix));
        harmony.Patch(original, null, new HarmonyMethod(patch));
        
        original = AccessTools.Method(typeof(MapBox), nameof(MapBox.checkEmptyClick));
        patch = AccessTools.Method(typeof(WindowFocusManager), nameof(checkEmptyClick_Prefix));
        harmony.Patch(original, new HarmonyMethod(patch));
        
        original = AccessTools.Method(typeof(MoveCamera), nameof(MoveCamera.updateMouseCameraDrag));
        patch = AccessTools.Method(typeof(WindowFocusManager), nameof(updateMouseCameraDrag_Prefix));
        harmony.Patch(original, new HarmonyMethod(patch));
      }

      // click-through fix
      // code mostly copied from SimpleGUI, thanks for that cody <3
      public static void isActionHappening_Postfix(ref bool __result)
      {
        if (WindowInUse != -1) {
          __result = true; // "menu in use" is the action happening
        }
      }

      public static bool updateControls_Prefix()
      {
        if (WindowInUse != -1) {
          return false; // cancel all control input if a KeyGuiMenu is in use
        }
        return true;
      }

      public static bool checkEmptyClick_Prefix()
      {
        if (WindowInUse != -1) {
          return false; // cancel empty click usage when a KeyGuiMenu is in use
        }
        return true;
      }
      
      public static bool updateMouseCameraDrag_Prefix()
      {
        if(WindowInUse != -1) {
          return false; // cancel camera drag when a KeyGuiMenu is in use
        }

        return true;
      }
    }

    private static class WindowIdSeparator {
      private static readonly Dictionary<int, KeyGuiMenu> RegisteredWindows = new Dictionary<int, KeyGuiMenu>();
      public static int RequestId(int windowId, KeyGuiMenu menu) {
        if (RegisteredWindows.ContainsKey(windowId)) {
          throw new InvalidMenuInvocationContextException("Window ID already in use!");
        }
        RegisteredWindows.Add(windowId, menu);
        return windowId;
      }
      
      public static KeyGuiMenu GetMenuFromWindowId(int windowId) {
        return RegisteredWindows.TryGetValue(windowId, out KeyGuiMenu window) ? window : null;
      }
    }
  }
}
