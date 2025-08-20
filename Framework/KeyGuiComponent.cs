using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KeyGUI.Framework.Locales;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KeyGUI.Framework {
  public class KeyGuiComponent {
    private readonly Dictionary<KeyGuiLocale, string> _locales = new Dictionary<KeyGuiLocale, string>();
    private readonly Dictionary<string, KeyGuiComponent> _containers = new Dictionary<string, KeyGuiComponent>();
    internal void InitLocales() {
      GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(f => f.FieldType == typeof(KeyGuiLocale)).ToList().ForEach(f => {
        KeyGuiLocale declaration = (KeyGuiLocale)f.GetValue(this);
        declaration.Initialize(this, f.Name);
        KeyGui.SyncDefaultLocale(declaration);
        _locales.Add(declaration, null);
      });
    }
    internal void AddLocalesContainer(KeyGuiLocale name, KeyGuiComponent container) {
      _containers[name.DefaultValue] = container;
      container.InitLocales();
    }
    internal void RefreshLocales() {
      _locales.Keys.ToList().ForEach(d => {
        if (d.BelongsToGameLocalesSystem) {
          LocalizedTextManager.instance._localized_text[d.LocaleId] = d;
        }
      });
      _containers.Values.ToList().ForEach(c => c.RefreshLocales());
    }
    internal void SetLocale(KeyGuiLocale declaration, string locale) {
      if (_locales.ContainsKey(declaration)) {
        _locales[declaration] = locale;
      } else {
        _containers.Values.ToList().ForEach(c => c.SetLocale(declaration, locale));
      }
    }
    internal void LoadLocales(JObject data) {
      foreach (KeyValuePair<string, JToken> pair in data) {
        if (pair.Value.Type == JTokenType.String) {
          KeyGuiLocale declaration = _locales.Keys.FirstOrDefault(d => d.LocaleId == pair.Key);
          if (declaration != null) {
            _locales[declaration] = pair.Value.Value<string>();
            if (declaration.BelongsToGameLocalesSystem && Config.game_loaded) {
              LocalizedTextManager.instance._localized_text[declaration.LocaleId] = pair.Value.Value<string>();
            }
          } else {
            Debug.LogWarning($"Unknown locale declaration: {pair.Key}");
          }
        } else {
          KeyGuiComponent container = _containers.FirstOrDefault(c => c.Key == pair.Key).Value;
          if (container != null) {
            container.LoadLocales((JObject)pair.Value);
          } else {
            Debug.LogWarning($"Unknown locale container: {pair.Key}");
          }
        }
      }
    }
    internal string GetLocale(KeyGuiLocale declaration) {
      if (_locales.TryGetValue(declaration, out string locale)) {
        return locale;
      }
      _containers.Values.ToList().ForEach(c => {
        string result = c.GetLocale(declaration);
        if (result != null) {
          locale = result;
        }
      });
      return locale;
    }
    internal JContainer SerializeAllLocales() {
      JObject result = new JObject();
      foreach (KeyValuePair<KeyGuiLocale, string> pair in _locales) {
        result.Add(pair.Key.LocaleId, pair.Value ?? pair.Key.DefaultValue);
      }
      foreach (KeyValuePair<string, KeyGuiComponent> pair in _containers) {
        result.Add(pair.Key, pair.Value.SerializeAllLocales());
      }
      return result;
    }
  }
}
