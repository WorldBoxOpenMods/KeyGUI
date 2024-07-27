namespace KeyGUI.Menus.ModConfig.ConfigOptions {
  public class Items<T> : ConfigOption<T> {
    internal Items(string field, T defaultValue, string description) : base(field, defaultValue, description) { }
  }
  
  public abstract class Items : Items<object> {
    private Items() : base(null, null, null) { }
    public static ConfigOption<bool> AutoloadItems => new Items<bool>("AutoloadItems", true, "If true, KeyGUI will automatically load custom items stored in worldbox_Data/KeyLibraryModsData/KeyGUI/Items.");
  }
}