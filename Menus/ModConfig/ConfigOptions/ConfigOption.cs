using System;
using System.Reflection;
using BepInEx.Configuration;
using Newtonsoft.Json;

namespace KeyGUI.Menus.ModConfig.ConfigOptions {
  public abstract class ConfigOption<T> : ConfigOption {
    public new T DefaultValue => (T)base.DefaultValue;
    public T Minimum = default;
    public T Maximum = default;
    internal ConfigOption(string field, T defaultValue, string description) : base(field, defaultValue, description) { }
  }

  public abstract class ConfigOption {
    public string Section { get; }
    public string Field { get; }
    protected object DefaultValue { get; }
    private string Description { get; }
    
    protected ConfigOption(string field, object defaultValue, string description) {
      Section = GetType().Name.Split('`')[0];
      Field = field;
      DefaultValue = defaultValue;
      Description = description;
    }
    
        
    public void Bind(ConfigFile configFile) {
      typeof(ConfigOption).GetMethod(nameof(Bind), BindingFlags.NonPublic | BindingFlags.Instance)?.MakeGenericMethod(DefaultValue.GetType()).Invoke(this, new[] { configFile, DefaultValue });
    }
    
    internal void Bind<T>(ConfigFile configFile, T value) {
      try {
        configFile.Bind(Section, Field, value, Description);
      } catch (ArgumentException) {
        configFile.Bind(Section, Field, JsonConvert.SerializeObject(value), Description);
      }
    }
  }
}
