using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.ModConfig.ConfigOptions;
using Newtonsoft.Json;
using UnityEngine;

namespace KeyGUI.Menus.ModConfig {
  public class KeyGuiModConfig : KeyGuiMenu {
    private static ConfigFile _configFile;
    private static readonly List<ConfigOption> ConfigOptions = new List<ConfigOption>();
    private static volatile KeyGuiModConfig _instance;

    private KeyGuiModConfig(ConfigFile configFile) {
      _configFile = configFile;
    }

    internal static KeyGuiModConfig Init(ConfigFile configFile) {
      return _instance ?? (_instance = new KeyGuiModConfig(configFile));
    }

    internal static KeyGuiModConfig GetInstance() {
      return _instance;
    }
    
    private static void BindConfigOption(ConfigOption configOption) {
      configOption.Bind(_configFile);
      ConfigOptions.Add(configOption);
    }

    public static void SetUpConfig() {
      typeof(ConfigOption<>).Assembly.GetTypes().Where(t => typeof(ConfigOption).IsAssignableFrom(t)).Select(t => t.IsGenericType ? t.MakeGenericType(typeof(object)) : t).SelectMany(t => t.GetProperties()).Select(p => p.GetMethod).Where(m => typeof(ConfigOption).IsAssignableFrom(m.ReturnType)).Select(m => m.Invoke(null, null)).Where(o => o is ConfigOption).Cast<ConfigOption>().Do(configOption => BindConfigOption(configOption));
      
      // Migration from old KeyGUI config
      if (File.Exists(Path.GetFullPath(Paths.ConfigPath + "/KeyGUI.cfg"))) {
        ConfigFile oldConfig = new ConfigFile(Path.GetFullPath(Paths.ConfigPath + "/KeyGUI.cfg"), true);
        oldConfig.Bind("Internal", "Id", "UNABLE_TO_DETERMINE");
        oldConfig.Bind("General", "IgnoreCriticalMods", false);
        oldConfig.Bind("General", "WhitelistedMods", JsonConvert.SerializeObject(Array.Empty<string>()));
        _configFile["Internal", "Id"].SetSerializedValue(oldConfig["Internal", "Id"].GetSerializedValue());
        _configFile["General", "IgnoreCriticalMods"].SetSerializedValue(oldConfig["General", "IgnoreCriticalMods"].GetSerializedValue());
        _configFile["Internal", "WhitelistedMods"].SetSerializedValue(oldConfig["General", "WhitelistedMods"].GetSerializedValue());
        File.Delete(Path.GetFullPath(Paths.ConfigPath + "/KeyGUI.cfg"));
      }

      _configFile.Save();
    }

    private static T Get<T>(string section, string field) {
      if (typeof(T) == typeof(string)) return (T) Convert.ChangeType(_configFile[section, field].GetSerializedValue().Replace("\\", "").Replace("\"", ""), typeof(T));
      return JsonConvert.DeserializeObject<T>(_configFile[section, field].GetSerializedValue().Replace("\\", ""));
    }

    private static void Set(string section, string field, object value) {
      _configFile[section, field].SetSerializedValue(JsonConvert.SerializeObject(value));
      _configFile.Save();
    }

    internal static T Get<T>(ConfigOption<T> configOption) {
      return Get<T>(configOption.Section, configOption.Field);
    }
    
    internal static void Set<T>(ConfigOption<T> configOption, T value) {
      Set(configOption.Section, configOption.Field, value);
    }

    protected override void LoadGUI(int windowID) {
      foreach (ConfigOption configOption in ConfigOptions.Where(configOption => KeyGuiConfig.DebugIsLegal || !(configOption is Internal))) {
        ShowOptionToggle(configOption);
      }
    }

    private void ShowOptionToggle(ConfigOption option) {
      switch (option) {
        case ConfigOption<bool> boolOption:
          ShowBoolOptionToggle(boolOption);
          break;
        case ConfigOption<float> floatOption:
          ShowFloatOptionSlider(floatOption);
          break;
      }
    }
    
    private void ShowBoolOptionToggle(ConfigOption<bool> option) {
      if (GUILayout.Button(Get(option) ? BeautifyConfigOptionField(option.Field) + ": ON" : BeautifyConfigOptionField(option.Field) + ": OFF")) Set(option, !Get(option));
    }
    
    private void ShowFloatOptionSlider(ConfigOption<float> option) {
      GUILayout.BeginHorizontal();
      GUILayout.Label(BeautifyConfigOptionField(option.Field));
      Set(option,Mathf.Round(GUILayout.HorizontalSlider(Get(option), option.Minimum, option.Maximum) * 100f) / 100f);
      GUILayout.Label(Get(option).ToString(CultureInfo.InvariantCulture));
      GUILayout.EndHorizontal();
    }

    private static string BeautifyConfigOptionField(string field) {
      return field.Substring(1).Aggregate(field[0].ToString(), (current, c) => current + (char.IsUpper(c) ? " " + c.ToString().ToLower() : c.ToString()));
    }
  }
}
