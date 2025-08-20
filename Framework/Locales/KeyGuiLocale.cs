using KeyGUI.Menus.Localizations;

namespace KeyGUI.Framework.Locales {
  public class KeyGuiLocale {
    public KeyGuiComponent Parent { get; private set; }
    public string LocaleId { get; private set; }
    internal string DefaultValue { get; set; }
    internal bool BelongsToGameLocalesSystem { get; }
    public string Value => Parent.GetLocale(this);
    internal KeyGuiLocale(bool belongsToGameLocalesSystem, string defaultValue) : this(defaultValue, belongsToGameLocalesSystem) { }
    internal KeyGuiLocale(string defaultValue, bool belongsToGameLocalesSystem = false) {
      DefaultValue = defaultValue;
      BelongsToGameLocalesSystem = belongsToGameLocalesSystem;
    }
    internal void Initialize(KeyGuiComponent parent, string localeId) {
      Parent = parent;
      LocaleId = localeId;
    }

    public override string ToString() {
      return KeyGui.GetLocale(this);
    }

    public static implicit operator string(KeyGuiLocale localeDeclaration) {
      return localeDeclaration.ToString();
    }

    public static implicit operator KeyGuiLocale(string defaultValue) {
      return new KeyGuiLocale(defaultValue);
    }
  }
}
