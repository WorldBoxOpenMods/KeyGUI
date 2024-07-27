using System.Linq;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus.Localizations.Declarations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace KeyGUI.Menus.Clans.TraitSelection {
  public class KeyGuiClansTraitSelection : KeyGuiMenu {
    protected override float MenuMaxHeight => 1500;
    protected override float MenuMinHeight => 900;
    private Vector2 _scrollPos;
    internal static Clan SelectedClan;
    protected override void LoadGUI(int windowID) {
      if (SelectedClan == null) {
        GUILayout.Label(Locales.Get(Locales.KeyGui.Clans.TraitSelection.NoClanSelectedError));
        return;
      }
      GUILayout.Label(string.Format(Locales.Get(Locales.KeyGui.Clans.TraitSelection.SelectTraitsForClanHeader), SelectedClan.name));
      _scrollPos = GUILayout.BeginScrollView(_scrollPos);
      if (SelectedClan.data.custom_data_string == null) {
        SelectedClan.data.custom_data_string = new CustomDataContainer<string>();
      }
      bool clanTraitsSet = SelectedClan.data.custom_data_string.TryGetValue("ClanTraits", out string clanTraitsJson);
      JArray clanTraitsArray = null;
      if (clanTraitsSet) {
        clanTraitsArray = JsonConvert.DeserializeObject<JArray>(clanTraitsJson);
      }
      foreach (ActorTrait trait in AssetManager.traits.list) {
        if (clanTraitsArray != null) {
          if (clanTraitsArray.Select(token => token.Value<string>()).Contains(trait.id)) {
            if (GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Clans.TraitSelection.RemoveTraitFromClanButton), trait.id, SelectedClan.name))) {
              clanTraitsArray = new JArray(clanTraitsArray.Where(token => token.Value<string>() != trait.id).Select(token => (object) token).ToArray());
              if (clanTraitsArray.Count == 0) {
                SelectedClan.data.custom_data_string.Remove("ClanTraits");
              } else {
                SelectedClan.data.custom_data_string["ClanTraits"] = JsonConvert.SerializeObject(clanTraitsArray);
              }
            }
          } else {
            if (GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Clans.TraitSelection.AddTraitToClanButton), trait.id, SelectedClan.name))) {
              clanTraitsArray.Add(trait.id);
              SelectedClan.data.custom_data_string["ClanTraits"] = JsonConvert.SerializeObject(clanTraitsArray);
            }
          }
        } else {
          if (GUILayout.Button(string.Format(Locales.Get(Locales.KeyGui.Clans.TraitSelection.AddTraitToClanButton), trait.id, SelectedClan.name))) {
            SelectedClan.data.custom_data_string["ClanTraits"] = JsonConvert.SerializeObject(new JArray(trait.id));
          }
        }
      }
      GUILayout.EndScrollView();
      GUILayout.Label("");
      if (GUILayout.Button(Locales.Get(Locales.KeyGui.Clans.TraitSelection.CloseMenuButton))) {
        Enabled = false;
      }
    }
  }
}