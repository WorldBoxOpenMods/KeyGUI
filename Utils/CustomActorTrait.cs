using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using strings;
using UnityEngine;

namespace KeyGUI.Utils {
  public class CustomActorTrait : ActorTrait {
    private const string NameValueGroupSeparator = "{+-Group-+}";
    private const string NameValuePairSeparator = "{+-Pair-+}";
    private const string NameValueSeparator = "{+-Value-+}";
    public string Author { get; private set; }
    public string Description { get; private set; }
    public string Sprite { get; private set; }
    public string[] OppositeTraits { get; private set; } = Array.Empty<string>();
    public string[] PartnerTraits { get; private set; } = Array.Empty<string>();

    public readonly Dictionary<ActorData, List<string>> PartnerTraitCache = new Dictionary<ActorData, List<string>>();

    public CustomActorTrait(string author, string description, string sprite) {
      Author = author;
      Description = description;
      Sprite = sprite;
    }

    public CustomActorTrait() { }

    // Old V1.1 format
    /*
    public override string ToString() {
      StringBuilder output = new StringBuilder();
      output.Append("TraitName:::");
      output.Append(id);
      output.Append("{+-+}");
      output.Append("TraitAuthor:::");
      output.Append(Author);
      output.Append("{+-+}");
      output.Append("TraitDescription:::");
      output.Append(Description);
      output.Append("{+-+}");
      output.Append("TraitVersion:::");
      output.Append(Version);
      output.Append("{+-+}");
      output.Append("TraitSprite:::");
      output.Append(Sprite);
      output.Append("{+-+}");
      output.Append("TraitGroup:::");
      output.Append(group_id);
      output.Append("{+-+}");
      output.Append("Diplomacy:::");
      output.Append(base_stats["diplomacy"]);
      output.Append("{+-+}");
      output.Append("PersonalityRationality:::");
      output.Append(base_stats["personality_rationality"]);
      output.Append("{+-+}");
      output.Append("Opinion:::");
      output.Append(base_stats["opinion"]);
      output.Append("{+-+}");
      output.Append("Fertility:::");
      output.Append(base_stats["fertility"]);
      output.Append("{+-+}");
      output.Append("MaxChildren:::");
      output.Append(base_stats["max_children"]);
      output.Append("{+-+}");
      output.Append("MaxAge:::");
      output.Append(base_stats["max_age"]);
      output.Append("{+-+}");
      output.Append("Damage:::");
      output.Append(base_stats["damage"]);
      output.Append("{+-+}");
      output.Append("Speed:::");
      output.Append(base_stats["speed"]);
      output.Append("{+-+}");
      output.Append("Health:::");
      output.Append(base_stats["health"]);
      output.Append("{+-+}");
      output.Append("Armor:::");
      output.Append(base_stats["armor"]);
      output.Append("{+-+}");
      output.Append("Intelligence:::");
      output.Append(base_stats["intelligence"]);
      output.Append("{+-+}");
      output.Append("AttackSpeed:::");
      output.Append(base_stats["attack_speed"]);
      output.Append("{+-+}");
      output.Append("CriticalChance:::");
      output.Append(base_stats["critical_chance"]);
      output.Append("{+-+}");
      output.Append("Warfare:::");
      output.Append(base_stats["warfare"]);
      output.Append("{+-+}");
      output.Append("Dodge:::");
      output.Append(base_stats["dodge"]);
      output.Append("{+-+}");
      output.Append("Stewardship:::");
      output.Append(base_stats["stewardship"]);
      output.Append("{+-+}");
      output.Append("LoyaltyTraits:::");
      output.Append(base_stats["loyalty_traits"]);
      output.Append("{+-+}");
      output.Append("ModArmor:::");
      output.Append(base_stats["mod_armor"]);
      output.Append("{+-+}");
      output.Append("ModDamage:::");
      output.Append(base_stats["mod_damage"]);
      output.Append("{+-+}");
      output.Append("ModHealth:::");
      output.Append(base_stats["mod_health"]);
      output.Append("{+-+}");
      output.Append("ModSpeed:::");
      output.Append(base_stats["mod_speed"]);
      output.Append("{+-+}");
      output.Append("ModDiplomacy:::");
      output.Append(base_stats["mod_diplomacy"]);
      output.Append("{+-+}");
      output.Append("ModCrit:::");
      output.Append(base_stats["mod_crit"]);
      output.Append("{+-+}");
      output.Append("ModAttackSpeed:::");
      output.Append(base_stats["mod_attack_speed"]);
      output.Append("{+-+}");
      output.Append("Cities:::");
      output.Append(base_stats["cities"]);
      output.Append("{+-+}");
      output.Append("ModSupplyTimer:::");
      output.Append(base_stats["mod_supply_timer"]);
      output.Append("{+-+}");
      output.Append("Accuracy:::");
      output.Append(base_stats["accuracy"]);
      output.Append("{+-+}");
      output.Append("Scale:::");
      output.Append(base_stats["scale"]);
      output.Append("{+-+}");
      output.Append("ZoneRange:::");
      output.Append(base_stats["zone_range"]);
      output.Append("{+-+}");
      output.Append("Range:::");
      output.Append(base_stats["range"]);
      output.Append("{+-+}");
      output.Append("BonusTowers:::");
      output.Append(base_stats["bonus_towers"]);
      output.Append("{+-+}");
      output.Append("ClanMembers:::");
      output.Append(base_stats["clan_members"]);
      output.Append("{+-+}");
      output.Append("Birth:::");
      output.Append(birth);
      output.Append("{+-+}");
      output.Append("Inherit:::");
      output.Append(inherit);
      return output.ToString();
    }
    */
    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.Append("TraitName");
      sb.Append(NameValueSeparator);
      sb.Append(id);
      sb.Append(NameValuePairSeparator);
      sb.Append("TraitDescription");
      sb.Append(NameValueSeparator);
      sb.Append(Description);
      sb.Append(NameValuePairSeparator);
      sb.Append("TraitAuthor");
      sb.Append(NameValueSeparator);
      sb.Append(Author);
      sb.Append(NameValuePairSeparator);
      sb.Append("TraitSprite");
      sb.Append(NameValueSeparator);
      sb.Append(Sprite);
      sb.Append(NameValuePairSeparator);
      sb.Append("TraitVersion");
      sb.Append(NameValueSeparator);
      sb.Append(2);
      sb.Append(NameValuePairSeparator);
      sb.Append("TraitGroup");
      sb.Append(NameValueSeparator);
      sb.Append(group_id);

      sb.Append(NameValueGroupSeparator);
      foreach (BaseStatsContainer baseStat in base_stats._stats_list) {
        sb.Append(MapStatNameBetween22And50(baseStat.id));
        sb.Append(NameValueSeparator);
        sb.Append(baseStat.value);
        sb.Append(NameValuePairSeparator);
      }

      sb.Append("Birth");
      sb.Append(NameValueSeparator);
      sb.Append((float)rate_birth / 100);
      sb.Append(NameValuePairSeparator);
      sb.Append("Inherit");
      sb.Append(NameValueSeparator);
      sb.Append((float)rate_inherit / 100);
      sb.Append(NameValueGroupSeparator);
      sb.Append(OppositeTraits.Length > 0 ? OppositeTraits.Aggregate((traitA, traitB) => traitA + NameValuePairSeparator + traitB) : "");
      sb.Append(NameValueGroupSeparator);
      sb.Append(PartnerTraits.Length > 0 ? PartnerTraits.Aggregate((traitA, traitB) => traitA + NameValuePairSeparator + traitB) : "");
      return sb.ToString();
    }

    private static string ConvertToSnakeCase(string text) {
      if (text == null) {
        throw new ArgumentNullException(nameof(text));
      }
      if (text.Length < 2) {
        return text;
      }

      StringBuilder sb = new StringBuilder();
      sb.Append(char.ToLowerInvariant(text[0]));
      for (int i = 1; i < text.Length; ++i) {
        char c = text[i];
        if (char.IsUpper(c)) {
          sb.Append('_');
          sb.Append(char.ToLowerInvariant(c));
        } else {
          sb.Append(c);
        }
      }

      return sb.ToString();
    }

    public static string ConvertToPascalCase(string text) {
      string[] words = text.Split('_');
      for (int i = 0; i < words.Length; i++) {
        words[i] = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(words[i]);
      }

      return string.Join(string.Empty, words);
    }

    public static string MapStatNameBetween22And50(string stat) {
      List<(string, string)> conversions = new List<(string, string)> {
        ("fertility", S.birth_rate),
        ("max_age", S.lifespan)
      };
      foreach ((string, string) conversion in conversions) {
        if (stat == conversion.Item1) {
          return conversion.Item2;
        }
        if (stat == conversion.Item2) {
          return conversion.Item1;
        }
      }
      return stat;
    }

    public void LoadTrait(string name, string author, string description, string sprite, string group, Dictionary<string, float> traitStats) {
      id = name;
      Author = author;
      Description = description;
      Sprite = sprite;
      group_id = group;
      can_be_given = true;
      can_be_removed = true;
      needs_to_be_explored = false;
      base_stats[S.diplomacy] = traitStats["diplomacy"];
      base_stats[S.personality_rationality] = traitStats["personalityRationality"];
      base_stats[S.opinion] = traitStats["opinion"];
      base_stats[S.birth_rate] = traitStats["fertility"];
      base_stats[S.lifespan] = traitStats["maxAge"];
      base_stats[S.damage] = traitStats["damage"];
      base_stats[S.speed] = traitStats["speed"];
      base_stats[S.health] = traitStats["health"];
      base_stats[S.armor] = traitStats["armor"];
      base_stats[S.intelligence] = traitStats["intelligence"];
      base_stats[S.attack_speed] = traitStats["attackSpeed"];
      base_stats[S.critical_chance] = traitStats["criticalChance"];
      base_stats[S.warfare] = traitStats["warfare"];
      base_stats[S.stewardship] = traitStats["stewardship"];
      base_stats[S.loyalty_traits] = traitStats["loyaltyTraits"];
      base_stats[S.cities] = traitStats["cities"];
      base_stats[S.accuracy] = traitStats["accuracy"];
      base_stats[S.scale] = traitStats["scale"];
      base_stats[S.range] = traitStats["range"];
      base_stats[S.bonus_towers] = traitStats["bonusTowers"];
      AddTraitToTraitLibraryWithoutSideEffects(this);
      CustomActorTraitsManager.AddTraitToLocalizedLibrary(id, Description);
    }

    public void LoadVersionOnePointOneTrait(string name, string author, string description, string sprite, string group, float birthChance, float inheritChance, Dictionary<string, float> traitStats) {
      id = name;
      Author = author;
      Description = description;
      Sprite = sprite;
      group_id = group;
      rate_birth = (int)Math.Round(birthChance * 100);
      rate_inherit = (int)Math.Round(inheritChance * 100);
      can_be_given = true;
      can_be_removed = true;
      needs_to_be_explored = false;
      base_stats[S.diplomacy] = traitStats["diplomacy"];
      base_stats[S.personality_rationality] = traitStats["personalityRationality"];
      base_stats[S.opinion] = traitStats["opinion"];
      base_stats[S.birth_rate] = traitStats["fertility"];
      base_stats[S.lifespan] = traitStats["maxAge"];
      base_stats[S.damage] = traitStats["damage"];
      base_stats[S.speed] = traitStats["speed"];
      base_stats[S.health] = traitStats["health"];
      base_stats[S.armor] = traitStats["armor"];
      base_stats[S.intelligence] = traitStats["intelligence"];
      base_stats[S.attack_speed] = traitStats["attackSpeed"];
      base_stats[S.critical_chance] = traitStats["criticalChance"];
      base_stats[S.warfare] = traitStats["warfare"];
      base_stats[S.stewardship] = traitStats["stewardship"];
      base_stats[S.loyalty_traits] = traitStats["loyaltyTraits"];
      base_stats[S.cities] = traitStats["cities"];
      base_stats[S.accuracy] = traitStats["accuracy"];
      base_stats[S.scale] = traitStats["scale"];
      base_stats[S.range] = traitStats["range"];
      base_stats[S.bonus_towers] = traitStats["bonusTowers"];
      AddTraitToTraitLibraryWithoutSideEffects(this);
      CustomActorTraitsManager.AddTraitToLocalizedLibrary(id, Description);
    }

    public void LoadVersionOnePointTwoTrait(string name, string author, string description, string sprite, string group, int birthChance, int inheritChance, Dictionary<string, float> traitStats, List<string> oppositeTraits, List<string> partnerTraits) {
      id = name;
      Author = author;
      Description = description;
      Sprite = sprite;
      group_id = group;
      rate_birth = birthChance;
      rate_inherit = inheritChance;
      can_be_given = true;
      can_be_removed = true;
      needs_to_be_explored = false;
      base_stats[S.diplomacy] = traitStats["diplomacy"];
      base_stats[S.personality_rationality] = traitStats["personalityRationality"];
      base_stats[S.opinion] = traitStats["opinion"];
      base_stats[S.birth_rate] = traitStats["fertility"];
      base_stats[S.lifespan] = traitStats["maxAge"];
      base_stats[S.damage] = traitStats["damage"];
      base_stats[S.speed] = traitStats["speed"];
      base_stats[S.health] = traitStats["health"];
      base_stats[S.armor] = traitStats["armor"];
      base_stats[S.intelligence] = traitStats["intelligence"];
      base_stats[S.attack_speed] = traitStats["attackSpeed"];
      base_stats[S.critical_chance] = traitStats["criticalChance"];
      base_stats[S.warfare] = traitStats["warfare"];
      base_stats[S.stewardship] = traitStats["stewardship"];
      base_stats[S.loyalty_traits] = traitStats["loyaltyTraits"];
      base_stats[S.cities] = traitStats["cities"];
      base_stats[S.accuracy] = traitStats["accuracy"];
      base_stats[S.scale] = traitStats["scale"];
      base_stats[S.range] = traitStats["range"];
      base_stats[S.bonus_towers] = traitStats["bonusTowers"];
      OppositeTraits = oppositeTraits.ToArray();
      opposite_list = OppositeTraits.ToList();
      PartnerTraits = partnerTraits.ToArray();
      AddTraitToTraitLibraryWithoutSideEffects(this);
      CustomActorTraitsManager.AddTraitToLocalizedLibrary(id, Description);
    }


    public void LoadTrait(string trait) {
      if (!trait.Contains("TraitVersion" + NameValueSeparator)) {
        LoadVersionOnePointOneTrait(trait);
        return;
      }

      string[] groups = trait.Split(new[] {NameValueGroupSeparator}, StringSplitOptions.None);
      string[] generalInfo = groups[0].Split(new[] {NameValuePairSeparator}, StringSplitOptions.None);
      KeyValuePair<string, string>[] generalInfoPairs = new KeyValuePair<string, string>[generalInfo.Length];
      for (int i = 0; i < generalInfo.Length; i++) {
        string[] pair = generalInfo[i].Split(new[] {NameValueSeparator}, StringSplitOptions.None);
        generalInfoPairs[i] = new KeyValuePair<string, string>(ConvertToSnakeCase(pair[0]), pair[1]);
      }

      string[] baseStats = groups[1].Split(new[] {NameValuePairSeparator}, StringSplitOptions.None);
      KeyValuePair<string, string>[] baseStatsPairs = new KeyValuePair<string, string>[baseStats.Length];
      for (int i = 0; i < baseStats.Length; i++) {
        string[] pair = baseStats[i].Split(new[] {NameValueSeparator}, StringSplitOptions.None);
        baseStatsPairs[i] = new KeyValuePair<string, string>(ConvertToSnakeCase(pair[0]), pair[1]);
      }

      OppositeTraits = groups[2].Split(new[] {NameValuePairSeparator}, StringSplitOptions.None);

      PartnerTraits = groups[3].Split(new[] {NameValuePairSeparator}, StringSplitOptions.None);


      Dictionary<string, string> generalInfoDictionary = generalInfoPairs.ToDictionary(x => x.Key, x => x.Value);
      id = generalInfoDictionary["trait_name"];
      Description = generalInfoDictionary["trait_description"];
      Author = generalInfoDictionary["trait_author"];
      Sprite = generalInfoDictionary["trait_sprite"];
      group_id = generalInfoDictionary["trait_group"];

      Debug.Log("Trying to set the base stats.");
      baseStatsPairs = baseStatsPairs.GroupBy(x => x.Key).Select(x => x.First()).ToArray();
      Dictionary<string, float> baseStatsDictionary = CustomActorTraitsManager.ConvertTraitStats(baseStatsPairs.ToDictionary(x => x.Key, x => x.Value));
      rate_birth = (int)(baseStatsDictionary["birth"] * 100);
      rate_inherit = (int)(baseStatsDictionary["inherit"] * 100);
      baseStatsDictionary.Remove("birth");
      baseStatsDictionary.Remove("inherit");
      if (base_stats == null) {
        base_stats = new BaseStats();
      }
      for (int i = 0; i < baseStatsDictionary.Count; ++i) {
        base_stats[MapStatNameBetween22And50(baseStatsDictionary.Keys.ElementAt(i))] = baseStatsDictionary[baseStatsDictionary.Keys.ElementAt(i)];
      }

      opposite_list = OppositeTraits.ToList();

      AddTraitToTraitLibraryWithoutSideEffects(this);
      cached_sprite = FileAssetManager.CreateSprite("KeyGUI", Sprite);
      CustomActorTraitsManager.AddTraitToLocalizedLibrary(id, Description);
    }

    private static void AddTraitToTraitLibraryWithoutSideEffects(ActorTrait trait) {
      string id = trait.id;
      if (AssetManager.traits.dict.ContainsKey(id)) {
        for (int index = 0; index < AssetManager.traits.list.Count; ++index) {
          if (AssetManager.traits.list[index].id == id) {
            AssetManager.traits.list.RemoveAt(index);
            break;
          }
        }

        AssetManager.traits.dict.Remove(id);
        Debug.LogWarning("[" + AssetManager.traits.id + "] duplicate asset - overwriting... " + id);
      }

      AssetManager.traits.t = trait;
      AssetManager.traits.t.create();
      AssetManager.traits.t.setHash(BaseAssetLibrary._latest_hash++);
      AssetManager.traits.list.Add(trait);
      AssetManager.traits.dict.Add(id, trait);
    }

    public void LoadVersionOnePointOneTrait(string trait) {
      if (!trait.Contains("TraitVersion:::")) {
        trait = ConvertVersionZeroTraitToVersionOne(trait);
      }
      string[] traitData = trait.Split(new[] {"{+-+}"}, StringSplitOptions.None);
      KeyValuePair<string, string>[] traitDataPairs = traitData.Select(t => t.Split(new[] {":::"}, StringSplitOptions.None)).Select(traitDataPair => traitDataPair.Length > 1 ? new KeyValuePair<string, string>(ConvertToSnakeCase(traitDataPair[0]), traitDataPair[1]) : new KeyValuePair<string, string>(null, null)).Where(x => x.Key != null).ToArray();
      Dictionary<string, string> traitDataDictionary = traitDataPairs.ToDictionary(x => x.Key, x => x.Value);
      id = traitDataDictionary["trait_name"];
      Author = traitDataDictionary["trait_author"];
      Description = traitDataDictionary["trait_description"];
      Sprite = traitDataDictionary["trait_sprite"];
      group_id = traitDataDictionary["trait_group"];
      can_be_given = true;
      can_be_removed = true;
      needs_to_be_explored = false;
      traitDataDictionary.Remove("trait_name");
      traitDataDictionary.Remove("trait_author");
      traitDataDictionary.Remove("trait_description");
      traitDataDictionary.Remove("trait_sprite");
      traitDataDictionary.Remove("trait_version");
      traitDataDictionary.Remove("trait_group");
      Dictionary<string, float> traitStatsDictionary = CustomActorTraitsManager.ConvertTraitStats(traitDataDictionary);
      if (traitStatsDictionary.ContainsKey("birth")) {
        rate_birth = (int)(traitStatsDictionary["birth"] * 100);
        traitStatsDictionary.Remove("birth");
      }

      if (traitStatsDictionary.ContainsKey("inherit")) {
        rate_inherit = (int)(traitStatsDictionary["inherit"] * 100);
        traitStatsDictionary.Remove("inherit");
      }
      if (base_stats == null) {
        base_stats = new BaseStats();
      }
      for (int i = 0; i < traitStatsDictionary.Count; ++i) {
        if (AssetManager.base_stats_library.dict.ContainsKey(MapStatNameBetween22And50(traitStatsDictionary.Keys.ElementAt(i)))) {
          base_stats[MapStatNameBetween22And50(traitStatsDictionary.Keys.ElementAt(i))] = traitStatsDictionary[traitStatsDictionary.Keys.ElementAt(i)];
        } else {
          Debug.LogWarning("Failed to load in stat " + traitStatsDictionary.Keys.ElementAt(i) + " for trait " + id);
        }
      }
      AddTraitToTraitLibraryWithoutSideEffects(this);
      CustomActorTraitsManager.AddTraitToLocalizedLibrary(id, Description);
    }

    private static string ConvertVersionZeroTraitToVersionOne(string trait) {
      string[] traitData = trait.Split(new[] {";;;"}, StringSplitOptions.None);
      StringBuilder newTrait = new StringBuilder();
      newTrait.Append("TraitName:::");
      newTrait.Append(traitData[0]);
      newTrait.Append("{+-+}");
      newTrait.Append("TraitAuthor:::");
      newTrait.Append(traitData[1]);
      newTrait.Append("{+-+}");
      newTrait.Append("TraitDescription:::");
      newTrait.Append(traitData[2]);
      newTrait.Append("{+-+}");
      newTrait.Append("TraitVersion:::1");
      newTrait.Append("{+-+}");
      newTrait.Append("TraitSprite:::");
      newTrait.Append(traitData[3]);
      newTrait.Append("{+-+}");
      newTrait.Append("TraitGroup:::");
      newTrait.Append(traitData[4]);
      newTrait.Append("{+-+}");
      newTrait.Append("Diplomacy:::");
      newTrait.Append(traitData[5]);
      newTrait.Append("{+-+}");
      newTrait.Append("PersonalityRationality:::");
      newTrait.Append(traitData[6]);
      newTrait.Append("{+-+}");
      newTrait.Append("Opinion:::");
      newTrait.Append(traitData[7]);
      newTrait.Append("{+-+}");
      newTrait.Append("Fertility:::");
      newTrait.Append(traitData[8]);
      newTrait.Append("{+-+}");
      newTrait.Append("MaxChildren:::");
      newTrait.Append(traitData[9]);
      newTrait.Append("{+-+}");
      newTrait.Append("MaxAge:::");
      newTrait.Append(traitData[10]);
      newTrait.Append("{+-+}");
      newTrait.Append("Damage:::");
      newTrait.Append(traitData[11]);
      newTrait.Append("{+-+}");
      newTrait.Append("Speed:::");
      newTrait.Append(traitData[12]);
      newTrait.Append("{+-+}");
      newTrait.Append("Health:::");
      newTrait.Append(traitData[13]);
      newTrait.Append("{+-+}");
      newTrait.Append("Armor:::");
      newTrait.Append(traitData[14]);
      newTrait.Append("{+-+}");
      newTrait.Append("Intelligence:::");
      newTrait.Append(traitData[15]);
      newTrait.Append("{+-+}");
      newTrait.Append("AttackSpeed:::");
      newTrait.Append(traitData[16]);
      newTrait.Append("{+-+}");
      newTrait.Append("CriticalChance:::");
      newTrait.Append(traitData[17]);
      newTrait.Append("{+-+}");
      newTrait.Append("Warfare:::");
      newTrait.Append(traitData[18]);
      newTrait.Append("{+-+}");
      newTrait.Append("Dodge:::");
      newTrait.Append(traitData[19]);
      newTrait.Append("{+-+}");
      newTrait.Append("Stewardship:::");
      newTrait.Append(traitData[20]);
      newTrait.Append("{+-+}");
      newTrait.Append("LoyaltyTraits:::");
      newTrait.Append(traitData[21]);
      newTrait.Append("{+-+}");
      newTrait.Append("ModArmor:::");
      newTrait.Append(traitData[22]);
      newTrait.Append("{+-+}");
      newTrait.Append("ModDamage:::");
      newTrait.Append(traitData[23]);
      newTrait.Append("{+-+}");
      newTrait.Append("ModHealth:::");
      newTrait.Append(traitData[24]);
      newTrait.Append("{+-+}");
      newTrait.Append("ModSpeed:::");
      newTrait.Append(traitData[25]);
      newTrait.Append("{+-+}");
      newTrait.Append("ModDiplomacy:::");
      newTrait.Append(traitData[26]);
      newTrait.Append("{+-+}");
      newTrait.Append("ModCrit:::");
      newTrait.Append(traitData[27]);
      newTrait.Append("{+-+}");
      newTrait.Append("ModAttackSpeed:::");
      newTrait.Append(traitData[28]);
      newTrait.Append("{+-+}");
      newTrait.Append("Cities:::");
      newTrait.Append(traitData[29]);
      newTrait.Append("{+-+}");
      newTrait.Append("ModSupplyTimer:::");
      newTrait.Append(traitData[30]);
      newTrait.Append("{+-+}");
      newTrait.Append("Accuracy:::");
      newTrait.Append(traitData[31]);
      newTrait.Append("{+-+}");
      newTrait.Append("Scale:::");
      newTrait.Append(traitData[32]);
      newTrait.Append("{+-+}");
      newTrait.Append("ZoneRange:::");
      newTrait.Append(traitData[33]);
      newTrait.Append("{+-+}");
      newTrait.Append("Range:::");
      newTrait.Append(traitData[34]);
      return newTrait.ToString();
    }
  }
}
