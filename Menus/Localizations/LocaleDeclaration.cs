namespace KeyGUI.Menus.Localizations {
  public class LocaleDeclaration {
    public LocalesContainer Parent { get; private set; }
    public string Name { get; private set; }
    internal LocaleDeclaration() { }
    internal void Initialize(LocalesContainer parent, string name) {
      Parent = parent;
      Name = name;
    }

    public override string ToString() {
      return Parent.GetLocale(this); // this is a horrible bandaid soluation, please fix this
    }
  }
}