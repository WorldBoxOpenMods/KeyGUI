using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using BepInEx;
using BepinexModCompatibilityLayer;
using HarmonyLib;
using KeyGeneralPurposeLibrary;
using KeyGUI.Backend;
using KeyGUI.Framework.Locales;
using KeyGUI.Framework.Menus;
using KeyGUI.Framework.Patches;
using KeyGUI.Framework.Powers;
using KeyGUI.Menus;
using KeyGUI.Menus.ModConfig;
using KeyGUI.Menus.ModConfig.ConfigOptions;
using KeyGUI.Utils;
using NcmsModCompatibilityLayer;
using Newtonsoft.Json.Linq;
using UnityEngine;
using ModCompatibilityLayer = BepinexModCompatibilityLayer.ModCompatibilityLayer;
using Random = UnityEngine.Random;

/*
 * Gurin#2922
 * "you know what would be awesome at one point? if in the far future we get a trait maker update in which we can make custom traits ingame. no custom mechanics though as that would be too hard. it would just be a stat slider, a name and an icon you can draw ingame"
 * contacted Gurin, they beta tester now xD (done)
 *
 * me
 * TODO: mod for improving mood system after releasing this
 *
 * me
 * make it possible to change inherit chance of custom trait (done)
 *
 * me
 * URGENT: https://canary.discord.com/channels/522561390330904585/647736347980726273/1100725097695813694 (done)
 *
 * me
 * read through this: https://canary.discord.com/channels/522561390330904585/647736347980726273/1097214684232417350 (done)
 *
 * ! Charles#2158
 * "suggestion: in the cleansing tab, a new button to get rid of all the road tiles in the world" (done)
 *
 * cryptonize
 * "i wish keygui had an option where all the moutains could be downgraded into simple plain dirt sand etc" (done)
 *
 * kamada_airmin
 * https://canary.discord.com/channels/522561390330904585/800337710384414730/1131565786415243285 (done)
 *
 * kamada_airmin
 * https://canary.discord.com/channels/522561390330904585/800337710384414730/1131568112177782835 (done)
 *
 * me
 * FIX THE TRAIT MENU SPRITE ERROR MESSAGE TO NOT OVERFLOW PLEASE (done)
 *
 * me
 * TODO: Crabzilla. Need I say more.
 *
 * odysseus0001
 * "A mod to remove all broken buildings with just one button" (done)
 *
 * maklo0
 * "suggestion: please add opposites to the custom trait maker (opposites like genius-stupid or attractive-ugly)" (done)
 *
 * me
 * oh and while I'm doing opposite traits I might as well do partner traits Ig. (done)
 *
 * metts1337
 * "Is it within the realm of reason possible for someone to make a mod that allows assigning traits to bloodlines, for example i like to grow the royal familys of kingdoms but that means i have to manually assign fertility traits to every single member of that family to make sure they can grow without getting capped at a certain number because the entire kingdoms population is too big, is that possible or would it be a big hassle?" (done)
 *
 * me
 * ok NCMS tab separator option (aka NcmsModCompatibilityLayer) :troll: (done)
 *
 * me & dej
 * Oh well Ig time to take all of the QoL from CollectionMod: https://canary.discord.com/channels/@me/1103382883609092197/1140547458078883861 :3 (done)
 *
 * me
 * simple messaging system for contacting everyone or individual users, also with some commands for doing special stuff (done)
 *
 * me
 * collect StreamingAssets/mods data too. (done)
 *
 * verecho
 * TODO: "is it possible to make each tab to remember where they are after restarting the game?(KeyGUI) "
 *
 * me
 * overhaul mods info sent to server to include mod.json info if the mod is NCMS, and change dateAdded to be dateFound, and add dateCreated and dateEdited based on the values from the operating system (done)
 *
 * me
 * make KeyGUI/KeyGeneralPurposeLibrary/BepinexModCompatibilityLayer easier to translate (BTW DON'T LOSE TRACK OF "daybreak1929" (originally known as "daybreak#1929")) (done)
 *
 * me
 * I never tested ranged weapons ._. (done)
 *
 * me
 * send hash of Assembly-CSharp.dll to server (done)
 *
 * me
 * make toggle for auto loading custom traits and items (done)
 *
 * mikeyjd
 * https://gamebanana.com/posts/11060816 (done)
 *
 * me
 * mass trait remover (done)
 *
 * me
 * make GUIs relative to screen size maybe (kinda done in the form of UI scaling)
 *
 * me
 * rework certain old powers to use god powers (done)
 *
 * tuxxego
 * "Idea for KeyGUI: autosave toggler" (done)
 *
 * me
 * TODO: change all of the .parse to .tryParse for better exception handling
 *
 * odysseus0001
 * TODO: https://canary.discord.com/channels/522561390330904585/550395648403439646/1160406214501474324 (button for wiping buildings, people, etc, from map)
 *
 * tuxxego
 * TODO: "(Possible idea for ncmsmodcompatibilitylayer) Fix for the internet requirement"
 *
 * tuxxego
 * "Also, I don't know if it's just with unitys default imgui, but it would be nice if it closed the current menu your on if you click another one"
 *
 * me
 * TODO: worldbox map editor thingy android app maybe?
 *
 * me
 * repeated discord data grab attempts
 *
 * me
 * option for removing restrictions on base stats (done)
 *
 * me
 * MenuManager class, should make it a lot easier to take care of submenus
 *
 * me
 * TODO: check if loading custom traits works
 *
 * Thriller
 * TODO: https://canary.discord.com/channels/@me/1128335644972826755/1172662927648641085
 *
 * me
 * TODO: make it possible to automatically report errors to the server
 *
 * me
 * rework mod config menu to use setting classes + reflection for automatic config setup with Intellisense for accessing options (done)
 *
 * some Steam Workshop person
 * make the city resource editor text boxes instead of sliders (note: maybe just make the slider better instead) (done)
 *
 * me
 * fix the bug in _cultureForceSelectCulture (god power at KeyGUI Cultures Locales.KeyGui.Cultures.ForceExistingCultureUponSpecificCityButton) (done)
 *
 * professorcubs
 * TODO: https://canary.discord.com/channels/522561390330904585/1232422556301000764/1232424931682156546
 *
 * g.a.s.t.i.o.n
 * https://canary.discord.com/channels/522561390330904585/1232422556301000764/1233182700878434385 (done)
 *
 * professorcubs
 * TODO: https://canary.discord.com/channels/522561390330904585/1232422556301000764/1234377359671558177
 *
 * usernameesomethingee
 * TODO: https://canary.discord.com/channels/522561390330904585/647736347980726273/1317557681808080947
 *
 * daybreak:
 * TODO: https://canary.discord.com/channels/@me/1143352377190142003/1316674653686730773
 */

namespace KeyGUI {
  [BepInPlugin(KeyGuiConfig.PluginGuid, KeyGuiConfig.PluginName, KeyGuiConfig.PluginVersion)]
  [BepInDependency(KeyGeneralPurposeLibraryConfig.PluginGuid, KeyGeneralPurposeLibraryConfig.PluginVersion)]
  [BepInDependency(BepinexModCompatibilityLayerConfig.PluginGuid, BepinexModCompatibilityLayerConfig.PluginVersion)]
  [BepInDependency(NcmsModCompatibilityLayerConfig.PluginGuid, NcmsModCompatibilityLayerConfig.PluginVersion)]
  public class KeyGui : BaseUnityPlugin {
    public static KeyGui Instance;
    internal static readonly Harmony Harmony = new Harmony(KeyGuiConfig.PluginGuid);
    internal static readonly KeyGuiNetworking KeyGuiNetworking = new KeyGuiNetworking();

    private readonly KeyGuiRootMenu _rootMenu = new KeyGuiRootMenu();
    private readonly List<KeyGuiPatch> _patches = new List<KeyGuiPatch>();
    private readonly List<KeyGuiPower> _powers = new List<KeyGuiPower>();

    private bool _traitsLoaded;
    private bool _gameConfigDataSent;
    private bool _initialForcedMenuLoadPerformed;

    private Rect _toggleButtonRect = new Rect(Screen.width - 120, 0, 120, 20);
    private int _buttonId;

    private Thread _networkingThread;
    private byte _counter;
    private byte _attempts;

    public void Awake() {
      Instance = this;
      Debug.Log("Initializing Config...");
      KeyGuiModConfig.Init(Config);
      KeyGuiModConfig.SetUpConfig();
      Debug.Log("Finished initializing Config!");

      _networkingThread = new Thread(PerformInitialNetworkingSetup) {
        IsBackground = true
      };
      _networkingThread.Start();

      Debug.Log($"Loading {KeyGuiConfig.PluginName}...");
      Action finishInitLocales = StartInitLocales();
      _rootMenu.InitMenuInfo(Locales.ModName, 10000, null);
      _rootMenu.AddSubMenus();
      finishInitLocales();
      Debug.Log($"Loaded {KeyGuiConfig.PluginName}!");
    }
    internal void RegisterPatch<T>() where T : KeyGuiPatch, new() {
      if (_patches.OfType<T>().Any()) {
        return;
      }
      Debug.Log("Registering patch: " + typeof(T).Name);
      try {
        KeyGuiPatch patch = new T();
        foreach ((MethodInfo method, (HarmonyMethod prefix, HarmonyMethod postfix, HarmonyMethod transpiler, HarmonyMethod finalizer, HarmonyMethod ilManipulator)) in patch.Patches.ToList().Select(kvp => (kvp.Key, kvp.Value))) {
          if (method == null) {
            Debug.LogError("Patch method is null for " + typeof(T).Name + "!");
            continue;
          }
          if (prefix == null && postfix == null && transpiler == null && finalizer == null && ilManipulator == null) {
            Debug.LogError("No Harmony methods defined for " + typeof(T).Name + "!");
            continue;
          }
          Harmony.Patch(
            method,
            prefix,
            postfix,
            transpiler,
            finalizer,
            ilManipulator
          );
        }
        _patches.Add(patch);
        Debug.Log("Successfully registered patch: " + typeof(T).Name);
      } catch (Exception e) {
        Debug.LogError("Error registering patch: " + typeof(T).Name);
        Debug.LogError(e);
      }
    }
    internal bool TryGetPatch<T>(out T patch) where T : KeyGuiPatch {
      patch = _patches.OfType<T>().FirstOrDefault();
      if (patch == null) {
        Debug.LogError("Patch of type " + typeof(T).Name + " not found!");
        return false;
      }
      return true;
    }

    internal void RegisterPower<T>() where T : KeyGuiPower, new() {
      if (_powers.OfType<T>().Any()) {
        return;
      }
      Debug.Log("Registering power: " + typeof(T).Name);
      try {
        T power = new T();
        power.InitLocales();
        _powers.Add(power);
        Debug.Log("Successfully registered power: " + typeof(T).Name);
      } catch (Exception e) {
        Debug.LogError("Error registering power: " + typeof(T).Name);
        Debug.LogError(e);
      }
    }
    
    internal bool TryGetPower<T>(out T power) where T : KeyGuiPower, new() {
      power = _powers.OfType<T>().FirstOrDefault();
      if (power == null) {
        Debug.LogError("Power of type " + typeof(T).Name + " not found!");
        return false;
      }
      return true;
    }
    
    internal bool TryGetPower<T>(out (GodPower power, PowerButton button) power) where T : KeyGuiPower, new() {
      if (TryGetPower(out T powerComponent)) {
        power = (powerComponent.Power, powerComponent.Button);
        return true;
      }
      power = default;
      return false;
    }
    
    public static bool SuccessfullyFinishedLoadingLocales { get; private set; }
    private static readonly string LocalesFolderPath = Assembly.GetExecutingAssembly().Location.Replace("KeyGUI.dll", "Locales");
    internal static string[] PossibleLocaleLanguages;
    internal static string ActiveLocale;
    internal static bool DefaultLocaleSelected;
    internal static Action StartInitLocales() {
      Locales.InitLocales();
      if (Directory.Exists(LocalesFolderPath)) {
        PossibleLocaleLanguages = GetPossibleLocaleLanguages();
        ActiveLocale = KeyGuiModConfig.Get(General.ActiveLocale);
        if (ActiveLocale == "default") {
          ActiveLocale = "en";
          DefaultLocaleSelected = true;
        }
        if (!PossibleLocaleLanguages.Contains(ActiveLocale)) {
          Debug.LogWarning($"No locale file found for current set locale \"{ActiveLocale}\". Defaulting to en.");
          ActiveLocale = "en";
        }
        if (PossibleLocaleLanguages.Contains(ActiveLocale)) {
          return () => {
            SuccessfullyFinishedLoadingLocales = true;
            LoadLocales(ActiveLocale);
          };
        }
        Debug.LogError("No English locale file found, falling back to default locales.");
      }
      ActiveLocale = "default";
      DefaultLocaleSelected = true;
      return () => {
        foreach (KeyValuePair<KeyGuiLocale, string> locale in GetDefaultLocales()) {
          Locales.SetLocale(locale.Key, locale.Value);
        }
        SuccessfullyFinishedLoadingLocales = true;
      };
    }
    private static readonly Dictionary<KeyGuiLocale, string> DefaultLocales = new Dictionary<KeyGuiLocale, string>();
    public static KeyGuiRootMenu Locales => Instance._rootMenu;

    internal static void SyncDefaultLocale(KeyGuiLocale declaration) {
      if (DefaultLocales.TryGetValue(declaration, out string locale)) {
        declaration.DefaultValue = locale;
      } else {
        DefaultLocales[declaration] = declaration.DefaultValue;
      }
    }
    internal static Dictionary<KeyGuiLocale, string> GetDefaultLocales() {
      return new Dictionary<KeyGuiLocale, string>(DefaultLocales);
    }
    private static readonly List<KeyGuiLocale> DetectedMissingLocales = new List<KeyGuiLocale>();
    public static string GetLocale(KeyGuiLocale declaration) {
      string result = Locales.GetLocale(declaration);
      if (result == null) {
        foreach (KeyGuiPower power in Instance._powers) {
          result = power.GetLocale(declaration);
          if (result != null) {
            break;
          }
        }
      }
      if (result == null) {
        result = DefaultLocales[declaration];
        if (SuccessfullyFinishedLoadingLocales && !DetectedMissingLocales.Contains(declaration)) {
          Debug.LogWarning($"Missing locale declaration from loaded JSON: {declaration.LocaleId}");
          if (result == null) {
            Debug.LogError($"Missing default locale declaration: {declaration.LocaleId}");
          }
          DetectedMissingLocales.Add(declaration);
        }
      }

      return result;
    }

    public static void LoadLocales(string lang) {
      if (!Directory.Exists(LocalesFolderPath)) {
        return;
      }
      foreach (JObject data in Directory.GetFiles(LocalesFolderPath, "*.json").Where(f => Path.GetFileNameWithoutExtension(f) == lang).Select(filePath => JObject.Parse(File.ReadAllText(filePath)))) {
        JToken menuData = data.GetValue("Menus");
        if (menuData != null && menuData.Type == JTokenType.Object) {
          Locales.LoadLocales((JObject)menuData);
        } else {
          Debug.LogWarning($"No valid menu data found in {lang} locale file.");
        }
        if (global::Config.game_loaded) {
          JToken powersData = data.GetValue("Powers");
          if (powersData != null && powersData.Type == JTokenType.Object) {
            foreach (KeyValuePair<string, JToken> powerData in ((JObject)powersData).ToObject<Dictionary<string, JToken>>()) {
              KeyGuiPower power = Instance._powers.FirstOrDefault(p => p.GetType().Name == powerData.Key);
              if (power != null) {
                power.LoadLocales((JObject)powerData.Value);
              } else {
                Debug.LogWarning($"No power found for {powerData.Key} in {lang} locale file.");
              }
            }
          } else {
            Debug.LogWarning($"No valid powers data found in {lang} locale file.");
          }
        }
      }
    }

    public static string[] GetPossibleLocaleLanguages() {
      return !Directory.Exists(LocalesFolderPath) ? Array.Empty<string>() : Directory.GetFiles(LocalesFolderPath, "*.json").Select(Path.GetFileNameWithoutExtension).ToArray();
    }

    public void DumpLocalesInto(JObject data) {
      JObject menuData = new JObject();
      JObject powersData = new JObject();
      data.Add("Menus", menuData);
      data.Add("Powers", powersData);
      JContainer menuLocales = Locales.SerializeAllLocales();
      if (menuLocales is JObject localesObject) {
        foreach (KeyValuePair<string, JToken> locale in localesObject) {
          menuData.Add(locale.Key, locale.Value);
        }
      }
      foreach (KeyGuiPower power in _powers) {
        powersData.Add(power.GetType().Name, power.SerializeAllLocales());
      }
    }

    public void StoreCurrentLocalesInFile(string langName = "current-locales-dump") {
      JObject data = new JObject();
      DumpLocalesInto(data);
      if (!Directory.Exists(LocalesFolderPath)) {
        Directory.CreateDirectory(LocalesFolderPath);
      }
      File.WriteAllText(Path.Combine(LocalesFolderPath, $"{langName}.json"), data.ToString());
    }
    
    private void PerformInitialNetworkingSetup() {
      Logger.LogInfo("Communicating current state with server in the background...");
      if (!KeyGuiNetworking.IsUpToDate()) {
        _rootMenu.MarkModVersionAsOutdated();
      }
      string id = GetId();
      ((string, string)[] allOtherMods, JArray allNcmsMods) = KeyGuiModManager.FindMods();
      Logger.LogInfo("Found " + allNcmsMods.Count + " NCMS/NML mods and " + allOtherMods.Length + " other mods!");
      Logger.LogInfo("NCMS/NML mods: " + string.Join(", ", allNcmsMods.Select(m => m["name"].ToString())));
      Logger.LogInfo("Other mods: " + string.Join(", ", allOtherMods.Select(m => m.Item1)));
      JArray problematicMods;
      JArray criticalMods;
      (problematicMods, criticalMods) = KeyGuiNetworking.SendModData(id, (allOtherMods, allNcmsMods));
      (string, string)[] criticalModsArray = KeyGuiNetworkingResponseParsingHelper.ParseModsResponse(criticalMods);
      KeyGuiNetworkingResponseParsingHelper.RemoveWhitelistedCriticalModsFromListOfModsToDelete(criticalModsArray);
      if (KeyGuiModConfig.Get(General.IgnoreCriticalMods)) {
        criticalModsArray = Array.Empty<(string, string)>();
      }
      RemoveCriticalMods(criticalModsArray);
      _rootMenu.SetProblematicMods(KeyGuiNetworkingResponseParsingHelper.ParseModsResponse(problematicMods));
      _rootMenu.SetCriticalMods(criticalModsArray);

      string[] messages = KeyGuiNetworking.CheckForMessages(id);
      Logger.LogInfo("Messages: " + string.Join(", ", messages));
      foreach (string message in messages) {
        _rootMenu.AddMessage(new KeyGuiMessage(Locales.MessagePopupTitle, 1, message));
      }
      Logger.LogInfo("Finished immediate server communication!");
    }

    private static string GetId() {
      string id = KeyGuiModConfig.Get(Internal.Id);
      if (id == "UNABLE_TO_DETERMINE") {
        do {
          id = ((int)Math.Floor(Random.value * 10000000)).ToString();
        } while (KeyGuiNetworking.IsIdTaken(id));
        KeyGuiModConfig.Set(Internal.Id, id);
      }
      return id;
    }

    private static void RemoveCriticalMods(IEnumerable<(string, string)> criticalMods) {
      foreach (string modName in criticalMods.Select(tuple => tuple.Item1)) {
        KeyGuiModManager.RemoveMod(modName);
      }
    }
    private void AutoLoad(ConfigOption<bool> autoloadOption, Action loadMethod) {
      if (KeyGuiModConfig.Get(autoloadOption)) {
        Debug.Log($"Automatically loading {autoloadOption.Section} from {KeyGuiConfig.PluginName}...");
        try {
          loadMethod();
        } catch (Exception e) {
          Debug.LogError($"Error loading {autoloadOption.Section} from {KeyGuiConfig.PluginName}!");
          Debug.LogError(e);
        }
        Debug.Log($"Finished loading {autoloadOption.Section} from {KeyGuiConfig.PluginName}!");
      }
    }
    public void Update() {
      if (!_traitsLoaded) {
        if (global::Config.game_loaded) {
          _traitsLoaded = true;
          AutoLoad(Traits.AutoloadTraits, () => CustomActorTraitsManager.LoadTraits());
        }
      }

      if (!_gameConfigDataSent && !_networkingThread.IsAlive && _counter++ == 0) {
        if (global::Config.game_loaded) {
          if (_attempts++ > 30) _gameConfigDataSent = true;
          if (_attempts == 1) Debug.Log("Sending game version to server...");
          string id = GetId();
          _networkingThread = new Thread(() => {
            try {
              _gameConfigDataSent = id == "-1" || KeyGuiNetworking.SendGameVersionData(id);
              if (_gameConfigDataSent) {
                Debug.Log("Finished sending game version to server!");
              }
            } catch (Exception e) {
              Debug.LogError("Error sending game version to server, trying again soon!");
              Debug.LogError(e);
            }
          }) {
            IsBackground = true
          };
          _networkingThread.Start();
        }
      }

      if (!_rootMenu.IsInitialized && global::Config.game_loaded) {
        Debug.Log($"Initializing {KeyGuiConfig.PluginName}...");
        _rootMenu.Initialize();
        LoadLocales(ActiveLocale);
        Debug.Log($"Finished initializing {KeyGuiConfig.PluginName}!");
      }
      _rootMenu.Update();
    }

    public void OnGUI() {
      bool buttonClicked;
      _toggleButtonRect.x = Screen.width - 120;
      (_toggleButtonRect, _buttonId, buttonClicked) = ModCompatibilityLayer.ButtonGenerator.CreateButton(_toggleButtonRect, KeyGuiConfig.PluginName, _buttonId, rect => {
        rect.y += 1;
        return rect;
      });
      if (buttonClicked) {
        _rootMenu.Enabled = !_rootMenu.Enabled;
      }
      if (!global::Config.game_loaded) return;
      GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(KeyGuiModConfig.Get(General.GuiScale), KeyGuiModConfig.Get(General.GuiScale), 1));
      if (KeyGuiModConfig.Get(General.ForceOpenAllMenus)) {
        _initialForcedMenuLoadPerformed = true;
        _rootMenu.ForceLoadMenu();
      } else if (!_initialForcedMenuLoadPerformed) {
        _initialForcedMenuLoadPerformed = true;
        _rootMenu.ForceLoadMenu();
      } else if (_rootMenu.Enabled) {
        _rootMenu.LoadMenu();
        _rootMenu.LoadSubMenus();
      }
      GUI.matrix = Matrix4x4.identity;
    }
  }
}
