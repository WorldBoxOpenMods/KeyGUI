namespace KeyGUI.Menus.ModConfig.ConfigOptions {
  public class GameSettings<T> : ConfigOption<T> {
    internal GameSettings(string field, T defaultValue, string description) : base(field, defaultValue, description) { }
  }
  
  public abstract class GameSettings : GameSettings<object> {
    private GameSettings() : base(null, null, null) { }
    public static ConfigOption<bool> DisableAutosaves => new GameSettings<bool>("DisableAutosaves", false, "If true, KeyGUI will disable the games autosave system.");
    public static ConfigOption<bool> DisableBoatsBreakingIce => new GameSettings<bool>("DisableBoatsBreakingIce", false, "If true, KeyGUI will disable the ability of boats to break ice.");
  }
}