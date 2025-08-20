using System.Collections.Generic;
using UnityEngine;

namespace KeyGUI.Menus.Localizations.Declarations {
  public static class Locales {
    private static readonly Dictionary<LocaleDeclaration, string> DefaultLocales = new Dictionary<LocaleDeclaration, string>();
    public static readonly KeyGui KeyGui = new KeyGui();

    internal static void SyncDefaultLocale(LocaleDeclaration declaration) {
      if (DefaultLocales.TryGetValue(declaration, out string locale)) {
        declaration.DefaultValue = locale;
      } else {
        DefaultLocales[declaration] = declaration.DefaultValue;
      }
    }
    internal static Dictionary<LocaleDeclaration, string> GetDefaultLocales() {
      return new Dictionary<LocaleDeclaration, string>(DefaultLocales);
    }
    private static readonly List<LocaleDeclaration> DetectedMissingLocales = new List<LocaleDeclaration>();
    public static string Get(LocaleDeclaration declaration) {
      string result = KeyGui.GetLocale(declaration);
      if (result == null) {
        result = DefaultLocales[declaration];
        if (KeyGuiLocales.SuccessfullyFinishedLoadingLocales && !DetectedMissingLocales.Contains(declaration)) {
          Debug.LogWarning($"Missing locale declaration from loaded JSON: {declaration.LocaleId}");
          if (result == null) {
            Debug.LogError($"Missing default locale declaration: {declaration.LocaleId}");
          }
          DetectedMissingLocales.Add(declaration);
        }
      }

      return result;
    }
  }
}
