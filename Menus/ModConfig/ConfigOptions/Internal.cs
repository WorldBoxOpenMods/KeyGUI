using System;

namespace KeyGUI.Menus.ModConfig.ConfigOptions {
  public class Internal<T> : ConfigOption<T> {
    internal Internal(string field, T defaultValue, string description) : base(field, defaultValue, description) { }
  }
  
  public abstract class Internal : Internal<object> {
    private Internal() : base(null, null, null) { }
    public static ConfigOption<string> Id => new Internal<string>("Id", "UNABLE_TO_DETERMINE", "A made up ID for some simple data collection during server communication. Change this value to -1 to prevent any data collection from happening, refrain from touching it otherwise. To get data deleted that has already been stored of you, contact keygui-data-management@keymasterer.uk with your ID.");
    public static ConfigOption<string[]> WhitelistedMods => new Internal<string[]>("WhitelistedMods", Array.Empty<string>(), "Mods that are whitelisted by the user and won't be flagged as problematic.");
  }
}