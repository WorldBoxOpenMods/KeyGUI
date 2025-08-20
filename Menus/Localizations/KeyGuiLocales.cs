using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using KeyGUI.Menus.ModConfig;
using KeyGUI.Menus.ModConfig.ConfigOptions;
using UnityEngine;

namespace KeyGUI.Menus.Localizations {
  public class KeyGuiLocales : KeyGuiMenu {
    public readonly KeyGuiLocale DumpLocalesButton = "Dump Locales";
    public readonly KeyGuiLocale CurrentLocaleText = "Current locale: ";
    public readonly KeyGuiLocale ChangeLocalesSectionHeader = "Change Locales";
    public readonly KeyGuiLocale LoadAndStoreAllLocalesButton = "Load and store all locales";
    
    protected override void InitializeMenu() {
      base.InitializeMenu();
      if (!KeyGui.SuccessfullyFinishedLoadingLocales) {
        return;
      }
      if (KeyGui.DefaultLocaleSelected) {
        string previousActiveLocale = KeyGui.ActiveLocale;
        KeyGui.ActiveLocale = LocalizedTextManager.instance.language;
        if (KeyGui.PossibleLocaleLanguages.Contains(KeyGui.ActiveLocale)) {
          KeyGuiModConfig.Set(General.ActiveLocale, KeyGui.ActiveLocale);
          KeyGui.LoadLocales(KeyGui.ActiveLocale);
        } else {
          KeyGui.ActiveLocale = previousActiveLocale;
        }
      }
      KeyGui.Locales.RefreshLocales();
    }

    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(DumpLocalesButton)) {
        KeyGui.Instance.StoreCurrentLocalesInFile();
      }
      if (KeyGui.PossibleLocaleLanguages == null) {
        return;
      }
      if (KeyGuiConfig.DebugIsLegal) {
        if (GUILayout.Button(LoadAndStoreAllLocalesButton)) {
          foreach (string language in KeyGui.PossibleLocaleLanguages) {
            KeyGui.LoadLocales(language);
            KeyGui.Instance.StoreCurrentLocalesInFile(language);
          }
        }
      }
      GUILayout.Label(CurrentLocaleText + KeyGui.ActiveLocale);
      GUILayout.Label(ChangeLocalesSectionHeader);
      foreach (string language in KeyGui.PossibleLocaleLanguages.Where(language => GUILayout.Button(language))) {
        KeyGuiModConfig.Set(General.ActiveLocale, language);
        KeyGui.ActiveLocale = language;
        KeyGui.LoadLocales(language);
      }
    }
  }
}
