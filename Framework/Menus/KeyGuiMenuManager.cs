using System.Collections.Generic;
using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Menus.ModConfig;
using KeyGUI.Menus.ModConfig.ConfigOptions;
using UnityEngine;

namespace KeyGUI.Framework.Menus {
  public abstract class KeyGuiMenuManager : KeyGuiMenu {
    private readonly List<KeyGuiMenu> _menus = new List<KeyGuiMenu>();

    internal T Load<T>(KeyGuiLocale title) where T : KeyGuiMenu, new() {
      return Load<T>(title, WindowID * 10 + _menus.Count + 1);
    }
    internal T Load<T>(KeyGuiLocale title, int windowID) where T : KeyGuiMenu, new() {
      return Load(title, windowID, new T());
    }
    internal T Load<T>(KeyGuiLocale title, T instance) where T : KeyGuiMenu {
      return Load(title, WindowID * 10 + _menus.Count + 1, instance);
    }
    internal T Load<T>(KeyGuiLocale title, int windowID, T instance) where T : KeyGuiMenu {
      Debug.Log($"Loading {Title} {title}...");
      AddLocalesContainer(title, instance);
      _menus.Add(instance);
      _menus.Last().RegisterPatches();
      _menus.Last().InitMenuInfo(title, windowID, this, MenuRect.x + MenuRect.width);
      if (_menus.Last() is KeyGuiMenuManager manager) manager.AddSubMenus();
      Debug.Log($"Loaded {Title} {title}...");
      return instance;
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
      foreach (KeyGuiMenu menu in _menus.Where(menu => menu.OfferVisibilityToggle).Where(menu => GUILayout.Button(menu.Title))) {
        if (KeyGuiModConfig.Get(General.OnlyAllowOneOpenSubmenu)) DisableAllWindows(menu);
        menu.Enabled = !menu.Enabled;
      }
    }

    protected override void InitializeMenu() {
      base.InitializeMenu();
      foreach (KeyGuiMenu menu in _menus.Where(menu => !menu.IsInitialized)) {
        Debug.Log($"Initializing {FullName}...");
        menu.RegisterPowers();
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
