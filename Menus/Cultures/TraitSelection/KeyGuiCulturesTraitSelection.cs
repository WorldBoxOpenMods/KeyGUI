using System.Linq;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KeyGUI.Menus.Cultures.TraitSelection {
  public class KeyGuiCulturesTraitSelection : KeyGuiMenu {
    public readonly KeyGuiLocale NoCultureSelectedError = "No culture selected!";
    public readonly KeyGuiLocale CloseMenuButton = "Close";
    public readonly KeyGuiLocale SelectTraitsForCultureHeader = "Select Traits for {0}";
    public readonly KeyGuiLocale RemoveTraitFromCultureButton = "Remove {0} from {1}";
    public readonly KeyGuiLocale AddTraitToCultureButton = "Add {0} to {1}";
    
    protected override float MenuMaxHeight => 1500;
    protected override float MenuMinHeight => 900;
    private Vector2 _scrollPos;
    internal static Culture SelectedCulture;
    protected override void LoadGUI(int windowID) {
      if (SelectedCulture == null) {
        GUILayout.Label(NoCultureSelectedError);
        return;
      }
      GUILayout.Label(string.Format(SelectTraitsForCultureHeader, SelectedCulture.name));
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
            if (GUILayout.Button(string.Format(RemoveTraitFromCultureButton, trait.id, SelectedCulture.name))) {
              clanTraitsArray = new JArray(clanTraitsArray.Where(token => token.Value<string>() != trait.id).Select(token => (object)token).ToArray());
              if (clanTraitsArray.Count == 0) {
                SelectedCulture.data.custom_data_string.Remove("CultureTraits");
              } else {
                SelectedCulture.data.custom_data_string["CultureTraits"] = JsonConvert.SerializeObject(clanTraitsArray);
              }
            }
          } else {
            if (GUILayout.Button(string.Format(AddTraitToCultureButton, trait.id, SelectedCulture.name))) {
              clanTraitsArray.Add(trait.id);
              SelectedCulture.data.custom_data_string["CultureTraits"] = JsonConvert.SerializeObject(clanTraitsArray);
            }
          }
        } else {
          if (GUILayout.Button(string.Format(AddTraitToCultureButton, trait.id, SelectedCulture.name))) {
            SelectedCulture.data.custom_data_string["CultureTraits"] = JsonConvert.SerializeObject(new JArray(trait.id));
          }
        }
      }
      GUILayout.EndScrollView();
      GUILayout.Label("");
      if (GUILayout.Button(CloseMenuButton)) {
        Enabled = false;
      }
    }
  }
}
