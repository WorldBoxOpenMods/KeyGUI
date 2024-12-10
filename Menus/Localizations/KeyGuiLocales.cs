using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using KeyGUI.Menus.ModConfig;
using KeyGUI.Menus.ModConfig.ConfigOptions;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KeyGUI.Menus.Localizations {
  public class KeyGuiLocales : KeyGuiMenu {
    public static bool SuccessfullyFinishedLoadingLocales { get; private set; }
    private static readonly string LocalesFolderPath = Assembly.GetExecutingAssembly().Location.Replace("KeyGUI.dll", "Locales");
    private static string[] _possibleLocaleLanguages;
    private static string _activeLocale;
    private static bool _defaultLocaleSelected;
    internal static void EarlyInitialize() {
      Locales.KeyGui.Initialize();
      if (Directory.Exists(LocalesFolderPath)) {
        _possibleLocaleLanguages = GetPossibleLocaleLanguages();
        _activeLocale = KeyGuiModConfig.Get(General.ActiveLocale);
        if (_activeLocale == "default") {
          _activeLocale = "en";
          _defaultLocaleSelected = true;
        }
        if (!_possibleLocaleLanguages.Contains(_activeLocale)) {
          Debug.LogWarning($"No locale file found for current set locale \"{_activeLocale}\". Defaulting to en.");
          _activeLocale = "en";
        }
        if (_possibleLocaleLanguages.Contains(_activeLocale)) {
          SuccessfullyFinishedLoadingLocales = true;
          LoadLocales(_activeLocale);
          return;
        }
        Debug.LogError("No English locale file found, falling back to default locales.");
      }
      _activeLocale = "default";
      _defaultLocaleSelected = true;
      foreach (KeyValuePair<LocaleDeclaration, string> locale in Locales.GetDefaultLocales()) {
        Locales.KeyGui.SetLocale(locale.Key, locale.Value);
      }
      SuccessfullyFinishedLoadingLocales = true;
    }

    protected override void InitializeMenu() {
      base.InitializeMenu();
      if (!SuccessfullyFinishedLoadingLocales) {
        return;
      }
      if (_defaultLocaleSelected) {
        string previousActiveLocale = _activeLocale;
        _activeLocale = LocalizedTextManager.instance.language;
        if (_possibleLocaleLanguages.Contains(_activeLocale)) {
          KeyGuiModConfig.Set(General.ActiveLocale, _activeLocale);
          LoadLocales(_activeLocale);
        } else {
          _activeLocale = previousActiveLocale;
        }
      }
      Locales.KeyGui.Refresh();
    }

    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Localizations.DumpLocalesButton))) {
        StoreCurrentLocalesInFile();
      }
      if (_possibleLocaleLanguages == null) {
        return;
      }
      if (KeyGuiConfig.DebugIsLegal) {
        if (GUILayout.Button(Locales.Get(Locales.KeyGui.Localizations.LoadAndStoreAllLocalesButton))) {
          foreach (string language in _possibleLocaleLanguages) {
            LoadLocales(language);
            StoreCurrentLocalesInFile(language);
          }
        }
      }
      GUILayout.Label(Locales.Get(Locales.KeyGui.Localizations.CurrentLocaleText) + _activeLocale);
      GUILayout.Label(Locales.Get(Locales.KeyGui.Localizations.ChangeLocalesSectionHeader));
      foreach (string language in _possibleLocaleLanguages.Where(language => GUILayout.Button(language))) {
        KeyGuiModConfig.Set(General.ActiveLocale, language);
        _activeLocale = language;
        LoadLocales(language);
      }
    }
    
    public string GetLocale(LocaleDeclaration declaration) {
      return Locales.Get(declaration);
    }

    public static void LoadLocales(string lang) {
      if (!Directory.Exists(LocalesFolderPath)) {
        return;
      }
      foreach (JObject data in Directory.GetFiles(LocalesFolderPath, "*.json").Where(f => Path.GetFileNameWithoutExtension(f) == lang).Select(filePath => JObject.Parse(File.ReadAllText(filePath)))) {
        Locales.KeyGui.LoadLocales(data, "KeyGui");
      }
    }
    
    public static string[] GetPossibleLocaleLanguages() {
      return !Directory.Exists(LocalesFolderPath) ? Array.Empty<string>() : Directory.GetFiles(LocalesFolderPath, "*.json").Select(Path.GetFileNameWithoutExtension).ToArray();
    }
    
    public void DumpLocalesInto(JObject data) {
      JContainer locales = Locales.KeyGui.SerializeAllLocales();
      if (locales is JObject localesObject) {
        foreach (KeyValuePair<string, JToken> locale in localesObject) {
          data.Add(locale.Key, locale.Value);
        }
      }
    }

    public void StoreCurrentLocalesInFile(string name = "current-locales-dump") {
      JObject data = new JObject();
      DumpLocalesInto(data);
      if (!Directory.Exists(LocalesFolderPath)) {
        Directory.CreateDirectory(LocalesFolderPath);
      }
      File.WriteAllText(Path.Combine(LocalesFolderPath, $"{name}.json"), data.ToString());
    }
  }
}
