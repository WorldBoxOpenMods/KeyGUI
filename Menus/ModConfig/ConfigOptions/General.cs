namespace KeyGUI.Menus.ModConfig.ConfigOptions {
  public class General<T> : ConfigOption<T> {
    internal General(string field, T defaultValue, string description) : base(field, defaultValue, description) { }
}
  
  public abstract class General : General<object> {
    private General() : base(null, null, null) { }
    public static ConfigOption<bool> IgnoreCriticalMods => new General<bool>("IgnoreCriticalMods", false, "If true, KeyGUI won't do anything about mods that the server flags as critical.");
    public static ConfigOption<bool> OnlyAllowOneOpenSubmenu => new General<bool>("OnlyAllowOneOpenSubmenu", false, "If true, KeyGUI will only allow one submenu to be open at a time.");
    public static ConfigOption<bool> ForceOpenAllMenus => new General<bool>("ForceOpenAllMenus", false, "If true, KeyGUI will force all menus to be open.");
    public static ConfigOption<bool> InitializeAssetFoldersEveryStartup => new General<bool>("InitializeAssetFoldersEveryStartup", true, "If true, KeyGUI will create the most recent needed asset storage structure every startup. This is useful if you want to make sure that mod updates keep the folder up to date.");
    public static ConfigOption<string> ActiveLocale => new General<string>("ActiveLocale", "default", "The active locale to use for KeyGUI if available.");
  }
}
