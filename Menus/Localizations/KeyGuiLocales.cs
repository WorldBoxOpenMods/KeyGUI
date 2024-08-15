using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KeyGUI.Menus.Localizations {
  public class KeyGuiLocales : KeyGuiMenu {
    public static bool SuccessfullyFinishedLoadingLocales { get; private set; }
    private static readonly string LocalesFolderPath = Assembly.GetExecutingAssembly().Location.Replace("KeyGUI.dll", "Locales");
    private static string[] _possibleLocaleLanguages;
    internal static void EarlyInitialize() {
      Locales.KeyGui.Initialize();
      if (!Directory.Exists(LocalesFolderPath)) {
        foreach (KeyValuePair<LocaleDeclaration, string> locale in Locales.GetDefaultLocales()) {
          Locales.KeyGui.SetLocale(locale.Key, locale.Value);
        }
      } else {
        _possibleLocaleLanguages = GetPossibleLocaleLanguages();
        // TODO: load locales selected in config onto locales structure
      }
      SuccessfullyFinishedLoadingLocales = true;
    }

    protected override void LoadGUI(int windowID) {
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Localizations.DumpLocalesButton))) {
        StoreCurrentLocalesInFile();
      }
      if (_possibleLocaleLanguages == null) {
        return;
      }
      GUILayout.Label(Locales.Get(Locales.KeyGui.Localizations.ChangeLocalesSectionHeader));
      foreach (string language in _possibleLocaleLanguages.Where(language => GUILayout.Button(language))) {
        LoadLocales(language);
      }
    }
    
    public string GetLocale(LocaleDeclaration declaration) {
      return Locales.Get(declaration);
    }

    public void LoadLocales(string lang) {
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

    public void StoreCurrentLocalesInFile() {
      JObject data = new JObject();
      DumpLocalesInto(data);
      if (!Directory.Exists(LocalesFolderPath)) {
        Directory.CreateDirectory(LocalesFolderPath);
      }
      File.WriteAllText(Path.Combine(LocalesFolderPath, "current-locales-dump.json"), data.ToString());
    }
  }
}