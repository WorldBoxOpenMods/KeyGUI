namespace KeyGUI {
  public static class KeyGuiConfig {
    public const string PluginGuid = "key.worldbox.keygui";
    public const string PluginName = "KeyGUI";
    public const string PluginVersion = "1.1.0";

    internal static bool DebugIsLegal =
#if DEBUG
      true;
#else
      false;
#endif
  }
}
