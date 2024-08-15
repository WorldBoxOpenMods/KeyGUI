namespace KeyGUI.Menus.Localizations {
  public class LocaleDeclaration {
    public LocalesContainer Parent { get; private set; }
    public string LocaleId { get; private set; }
    public string Value => Parent.GetLocale(this);
    internal LocaleDeclaration() { }
    internal void Initialize(LocalesContainer parent, string localeId) {
      Parent = parent;
      LocaleId = localeId;
    }

    public override string ToString() {
      return Value;
    }
  }
}