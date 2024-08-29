using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KeyGUI.Menus.Localizations.Declarations;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KeyGUI.Menus.Localizations {
  public abstract class LocalesContainer {
    private readonly Dictionary<LocaleDeclaration, string> _locales = new Dictionary<LocaleDeclaration, string>();
    private readonly List<LocalesContainer> _containers = new List<LocalesContainer>();
    internal void Initialize() {
      GetType().GetFields().Where(f => f.FieldType == typeof(LocaleDeclaration)).ToList().ForEach(f => {
        LocaleDeclaration declaration = (LocaleDeclaration)f.GetValue(this);
        declaration.Initialize(this, f.Name);
        Locales.SyncDefaultLocale(declaration);
        _locales.Add(declaration, null);
      });
      GetType().GetFields(BindingFlags.Instance | BindingFlags.Public).Where(f => typeof(LocalesContainer).IsAssignableFrom(f.FieldType)).ToList().ForEach(f => {
        _containers.Add((LocalesContainer)f.GetValue(this));
      });
      _containers.ForEach(c => c.Initialize());
    }
    internal void Refresh() {
      _locales.Keys.ToList().ForEach(d => {
        if (d.BelongsToGameLocalesSystem) {
          LocalizedTextManager.instance.localizedText[d.LocaleId] = _locales[d];
        }
      });
      _containers.ForEach(c => c.Refresh());
    }
    internal void SetLocale(LocaleDeclaration declaration, string locale) {
      if (_locales.ContainsKey(declaration)) {
        _locales[declaration] = locale;
      } else {
        _containers.ForEach(c => c.SetLocale(declaration, locale));
      }
    }
    internal void LoadLocales(JObject data, string fullParentPath = "") {
      foreach (KeyValuePair<string, JToken> pair in data) {
        if (pair.Value.Type == JTokenType.String) {
          LocaleDeclaration declaration = _locales.Keys.FirstOrDefault(d => d.LocaleId == pair.Key);
          if (declaration != null) {
            _locales[declaration] = pair.Value.Value<string>();
            if (declaration.BelongsToGameLocalesSystem) {
              LocalizedTextManager.instance.localizedText[declaration.LocaleId] = pair.Value.Value<string>();
            }
          } else {
            Debug.LogWarning($"Unknown locale declaration: {pair.Key}");
          }
        } else {
          LocalesContainer container = _containers.FirstOrDefault(c => c.GetType().Name == fullParentPath + (pair.Key == "Localizations" ? "Locales" : pair.Key));
          if (container != null) {
            container.LoadLocales((JObject)pair.Value, fullParentPath + (pair.Key == "Localizations" ? "Locales" : pair.Key));
          } else {
            Debug.LogWarning($"Unknown locale container: {pair.Key}");
          }
        }
      }
    }
    internal string GetLocale(LocaleDeclaration declaration) {
      if (_locales.TryGetValue(declaration, out string locale)) {
        return locale;
      }
      _containers.ForEach(c => {
        string result = c.GetLocale(declaration);
        if (result != null) {
          locale = result;
        }
      });
      return locale;
    }
    internal JContainer SerializeAllLocales() {
      JObject result = new JObject();
      GetType().GetFields().Where(f => f.FieldType == typeof(LocaleDeclaration)).ToList().ForEach(f => {
        result.Add(f.Name, _locales[(LocaleDeclaration)f.GetValue(this)]);
      });
      GetType().GetFields(BindingFlags.Instance | BindingFlags.Public).Where(f => typeof(LocalesContainer).IsAssignableFrom(f.FieldType)).ToList().ForEach(f => {
        result.Add(f.Name, ((LocalesContainer)f.GetValue(this)).SerializeAllLocales());
      });
      return result;
    }
    protected List<LocaleDeclaration> GetDeclarations() {
      return _locales.Keys.ToList();
    }
  }
}
