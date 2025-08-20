using KeyGUI.Menus.Localizations.Declarations;

namespace KeyGUI.Menus.Localizations {
  public class LocaleDeclaration {
    public LocalesContainer Parent { get; private set; }
    public string LocaleId { get; private set; }
    internal string DefaultValue { get; set; }
    internal bool BelongsToGameLocalesSystem { get; }
    public string Value => Parent.GetLocale(this);
    internal LocaleDeclaration(bool belongsToGameLocalesSystem, string defaultValue) : this(defaultValue, belongsToGameLocalesSystem) { }
    internal LocaleDeclaration(string defaultValue, bool belongsToGameLocalesSystem = false) {
      DefaultValue = defaultValue;
      BelongsToGameLocalesSystem = belongsToGameLocalesSystem;
    }
    internal void Initialize(LocalesContainer parent, string localeId) {
      Parent = parent;
      LocaleId = localeId;
    }

    public override string ToString() {
      return Locales.Get(this);
    }

    public static implicit operator string(LocaleDeclaration localeDeclaration) {
      return localeDeclaration.ToString();
    }

    public static implicit operator LocaleDeclaration(string defaultValue) {
      return new LocaleDeclaration(defaultValue);
    }
  }
}
