using System.Collections.Generic;
using System.Linq;
using KeyGUI.Menus.Localizations;
using KeyGUI.Menus.Localizations.Declarations;
using KeyGUI.Menus.ModConfig;
using KeyGUI.Menus.ModConfig.ConfigOptions;
using UnityEngine;

namespace KeyGUI.MenuArchitecture {
  public abstract class KeyGuiMenuManager : KeyGuiMenu {
    private readonly List<KeyGuiMenu> _menus = new List<KeyGuiMenu>();
    
    internal void Load<T>(LocaleDeclaration title) where T : KeyGuiMenu, new() => Load<T>(title, (WindowID * 10) + _menus.Count + 1);
    internal void Load<T>(LocaleDeclaration title, int windowID) where T : KeyGuiMenu, new() => Load(title, windowID, new T());
    internal void Load<T>(LocaleDeclaration title, T instance) where T : KeyGuiMenu => Load(title, (WindowID * 10) + _menus.Count + 1, instance);
    internal void Load<T>(LocaleDeclaration title, int windowID, T instance) where T : KeyGuiMenu {
      Debug.Log("Loading " + Locales.Get(Title) + " " + Locales.Get(title) + "...");
      _menus.Add(instance);
      _menus.Last().InitMenuInfo(title, windowID, this, MenuRect.x + MenuRect.width);
      if (_menus.Last() is KeyGuiMenuManager manager) manager.AddSubMenus();
      Debug.Log("Loaded " + Locales.Get(Title) + " " + Locales.Get(title) + "...");
    }
    

    internal void AddMessage(KeyGuiMessage message) {
      _menus.Add(message);
    }
    
    private void DisableAllWindows(params KeyGuiMenu[] exceptions) {
      foreach (KeyGuiMenu menu in _menus.Where(menu => menu.OfferVisibilityToggle).Where(menu => !exceptions.Contains(menu))) {
        menu.Enabled = false;
      }
    }

    internal void LoadSubMenus() {
      foreach (KeyGuiMenu menu in _menus.Where(menu => menu.Enabled)) {
        menu.LoadMenu();
        if (menu is KeyGuiMenuManager manager) manager.LoadSubMenus();
      }
    }

    internal override void ForceLoadMenu() {
      base.ForceLoadMenu();
      foreach (KeyGuiMenu menu in _menus) {
        menu.ForceLoadMenu();
      }
    }

    protected override void LoadGUI(int windowID) {
      LoadGUI();
    }
    protected virtual void LoadGUI() { }

    protected void LoadSubMenuToggles() {
      foreach (KeyGuiMenu menu in _menus.Where(menu => menu.OfferVisibilityToggle).Where(menu => GUILayout.Button(Locales.Get(menu.Title)))) {
        if (KeyGuiModConfig.Get(General.OnlyAllowOneOpenSubmenu)) DisableAllWindows(menu);
        menu.Enabled = !menu.Enabled;
      }
    }
    
    protected override void InitializeMenu() {
      base.InitializeMenu();
      foreach (KeyGuiMenu menu in _menus.Where(menu => !menu.IsInitialized)) {
        Debug.Log($"Initializing {FullName}...");
        menu.Initialize();
        Debug.Log($"Initialized {FullName}!");
      }
    }
    
    protected override void UpdateMenu() {
      base.UpdateMenu();
      foreach (KeyGuiMenu menu in _menus) {
        menu.Update();
      }
    }

    internal abstract void AddSubMenus();
  }
}
