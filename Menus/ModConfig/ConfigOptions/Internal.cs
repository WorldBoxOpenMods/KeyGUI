using System;

namespace KeyGUI.Menus.ModConfig.ConfigOptions {
  public class Internal<T> : ConfigOption<T> {
    internal Internal(string field, T defaultValue, string description) : base(field, defaultValue, description) { }
  }

  public abstract class Internal : Internal<object> {
    private Internal() : base(null, null, null) { }
    [Obsolete("Id is obsolete and holds no relevancy anymore, the new API uses InstallId.")]
    public static ConfigOption<string> Id => new Internal<string>("Id", "None", "The ID which was used for server communication in past KeyGUI versions. It holds no relevancy anymore.");
    public static ConfigOption<string> InstallId => new Internal<string>("InstallId", "None", "A made up ID for some simple data collection during server communication. Change this value to \"Optout\" to prevent any data collection from happening, refrain from touching it otherwise. To get data deleted that has already been stored of you, contact keygui-data-management@keymasterer.uk with your Install ID.");
    public static ConfigOption<string> Secret => new Internal<string>("Secret", "None", "A secret key used to authenticate your identity when sending data to the server. Avoid touching this value, look at the description of Installid for how to opt out of collection.");
    public static ConfigOption<string[]> WhitelistedMods => new Internal<string[]>("WhitelistedMods", Array.Empty<string>(), "Mods that are whitelisted by the user and won't be flagged as problematic.");
  }
}
