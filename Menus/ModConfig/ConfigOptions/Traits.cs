namespace KeyGUI.Menus.ModConfig.ConfigOptions {
  public class Traits<T> : ConfigOption<T> {
    internal Traits(string field, T defaultValue, string description) : base(field, defaultValue, description) { }
  }
  
  public abstract class Traits : Traits<object> {
    private Traits() : base(null, null, null) { }
    public static ConfigOption<bool> ShowTraitsTutorial => new Traits<bool>("ShowTraitsTutorial", true, "If true, KeyGUI will show basic instructions on how to use the custom trait maker.");
    public static ConfigOption<bool> AutoloadTraits => new Traits<bool>("AutoloadTraits", true, "If true, KeyGUI will automatically load custom traits stored in worldbox_Data/KeyLibraryModsData/KeyGUI/Traits.");
  }
}