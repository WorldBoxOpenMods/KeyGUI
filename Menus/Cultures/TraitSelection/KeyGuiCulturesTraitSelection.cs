using System.Linq;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KeyGUI.Menus.Cultures.TraitSelection {
  public class KeyGuiCulturesTraitSelection : KeyGuiMenu {
    protected override float MenuMaxHeight => 1500;
    protected override float MenuMinHeight => 900;
    private Vector2 _scrollPos;
    internal static Culture SelectedCulture;
    protected override void LoadGUI(int windowID) {
      if (SelectedCulture == null) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Cultures.TraitSelection.NoCultureSelectedError));
        return;
      }
      GUILayout.Label(string.Format(Locales.Get(Locales.KeyGui.Cultures.TraitSelection.SelectTraitsForCultureHeader), SelectedCulture.name));
      _scrollPos = GUILayout.BeginScrollView(_scrollPos);
      if (SelectedCulture.data.custom_data_string == null) {
        SelectedCulture.data.custom_data_string = new CustomDataContainer<string>();
      }
      bool clanTraitsSet = SelectedCulture.data.custom_data_string.TryGetValue("CultureTraits", out string clanTraitsJson);
      JArray clanTraitsArray = null;
      if (clanTraitsSet) {
        clanTraitsArray = JsonConvert.DeserializeObject<JArray>(clanTraitsJson);
      }
      foreach (ActorTrait trait in AssetManager.traits.list) {
        if (clanTraitsArray != null) {
          if (clanTraitsArray.Select(token => token.Value<string>()).Contains(trait.id)) {
            if (GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Cultures.TraitSelection.RemoveTraitFromCultureButton), trait.id, SelectedCulture.name))) {
              clanTraitsArray = new JArray(clanTraitsArray.Where(token => token.Value<string>() != trait.id).Select(token => (object)token).ToArray());
              if (clanTraitsArray.Count == 0) {
                SelectedCulture.data.custom_data_string.Remove("CultureTraits");
              } else {
                SelectedCulture.data.custom_data_string["CultureTraits"] = JsonConvert.SerializeObject(clanTraitsArray);
              }
            }
          } else {
            if (GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Cultures.TraitSelection.AddTraitToCultureButton), trait.id, SelectedCulture.name))) {
              clanTraitsArray.Add(trait.id);
              SelectedCulture.data.custom_data_string["CultureTraits"] = JsonConvert.SerializeObject(clanTraitsArray);
            }
          }
        } else {
          if (GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Cultures.TraitSelection.AddTraitToCultureButton), trait.id, SelectedCulture.name))) {
            SelectedCulture.data.custom_data_string["CultureTraits"] = JsonConvert.SerializeObject(new JArray(trait.id));
          }
        }
      }
      GUILayout.EndScrollView();
      GUILayout.Label("");
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Cultures.TraitSelection.CloseMenuButton))) {
        Enabled = false;
      }
    }
  }
}
