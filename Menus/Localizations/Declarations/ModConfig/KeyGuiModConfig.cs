namespace KeyGUI.Menus.Localizations.Declarations.ModConfig {
  public class KeyGuiModConfig : LocalesContainer {
    internal KeyGuiModConfig() { }
    /*
    private static ConfigFile _configFile;
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

    public static void SetUpConfig() {
      typeof(ConfigOption<>).Assembly.GetTypes().Where(t => typeof(ConfigOption).IsAssignableFrom(t)).Select(t => t.IsGenericType ? t.MakeGenericType(typeof(object)) : t).SelectMany(t => t.GetProperties()).Select(p => p.GetMethod).Where(m => typeof(ConfigOption).IsAssignableFrom(m.ReturnType)).Select(m => m.Invoke(null, null)).Where(o => o is ConfigOption).Cast<ConfigOption>().Do(configOption => configOption.Bind(_configFile));
      
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

    internal static T Get<T>(string section, string field) {
      if (typeof(T) == typeof(string)) return (T) Convert.ChangeType(_configFile[section, field].GetSerializedValue(), typeof(T));
      return JsonConvert.DeserializeObject<T>(_configFile[section, field].GetSerializedValue().Replace("\\", ""));
    }
    
    internal static void Set(string section, string field, object value) {
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
      foreach (ConfigDefinition cD in _configFile.Keys.Where(cD => cD.Section != "Internal")) {
        ShowBoolOptionToggle(cD.Section, cD.Key);
      }
    }
    
    private void ShowBoolOptionToggle(string section, string field) {
      if (GUILayout.Button(Get<bool>(section, field) ? BeautifyConfigOptionField(field) + ": ON" : BeautifyConfigOptionField(field) + ": OFF")) Set(section, field, !Get<bool>(section, field));
    }

    private static string BeautifyConfigOptionField(string field) {
      return field.Substring(1).Aggregate(field[0].ToString(), (current, c) => current + (char.IsUpper(c) ? " " + c.ToString().ToLower() : c.ToString()));
    }
    */
  }
}