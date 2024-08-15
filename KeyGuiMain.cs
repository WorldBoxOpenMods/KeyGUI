using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BepInEx;
using BepinexModCompatibilityLayer;
using HarmonyLib;
using KeyGeneralPurposeLibrary;
using KeyGeneralPurposeLibrary.Assets;
using KeyGeneralPurposeLibrary.BehaviourManipulation;
using KeyGUI.Backend;
using KeyGUI.MenuArchitecture;
using KeyGUI.Menus;
using KeyGUI.Menus.Localizations;
using KeyGUI.Menus.Localizations.Declarations;
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
 * TODO: Oh well Ig time to take all of the QoL from CollectionMod: https://canary.discord.com/channels/@me/1103382883609092197/1140547458078883861 :3
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
 * TODO: make KeyGUI/KeyGeneralPurposeLibrary/BepinexModCompatibilityLayer easier to translate (BTW DON'T LOSE TRACK OF "daybreak1929" (originally known as "daybreak#1929"))
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
 * TODO: make NCMS mod compiling multi threaded
 *
 * me
 * TODO: make GUIs relative to screen size maybe
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
 * TODO: rework mod config menu to use setting classes + reflection for automatic config setup with Intellisense for accessing options
 *
 * some Steam Workshop person
 * TODO: make the city resource editor text boxes instead of sliders (note: maybe just make the slider better instead)
 *
 * me
 * TODO: fix the bug in _cultureForceSelectCulture (god power at KeyGUI Cultures Locales.KeyGui.Cultures.ForceExistingCultureUponSpecificCityButton)
 *
 * professorcubs
 * TODO: https://canary.discord.com/channels/522561390330904585/1232422556301000764/1232424931682156546
 *
 * g.a.s.t.i.o.n
 * TODO: https://canary.discord.com/channels/522561390330904585/1232422556301000764/1233182700878434385
 *
 * professorcubs
 * TODO: https://canary.discord.com/channels/522561390330904585/1232422556301000764/1234377359671558177
 */

namespace KeyGUI {
  [BepInPlugin(KeyGuiConfig.PluginGuid, KeyGuiConfig.PluginName, KeyGuiConfig.PluginVersion)]
  [BepInDependency(KeyGeneralPurposeLibraryConfig.PluginGuid, KeyGeneralPurposeLibraryConfig.PluginVersion)]
  [BepInDependency(BepinexModCompatibilityLayerConfig.PluginGuid, BepinexModCompatibilityLayerConfig.PluginVersion)]
  [BepInDependency(NcmsModCompatibilityLayerConfig.PluginGuid, NcmsModCompatibilityLayerConfig.PluginVersion)]
  public class KeyGuiMain : BaseUnityPlugin {
    public static KeyGuiMain Instance;
    internal static readonly KeyGuiNetworking KeyGuiNetworking = new KeyGuiNetworking();
    private static readonly KeyGuiCommandHandler KeyGuiCommandHandler = new KeyGuiCommandHandler();
    private static string[] _lateCommands = Array.Empty<string>();

    private readonly KeyGuiRootMenu _rootMenu = new KeyGuiRootMenu();

    private bool _traitsLoaded;
    private bool _itemsLoaded;
    private bool _gameConfigDataSent;
    private bool _lateCommandsExecuted;
    private bool _initialForcedMenuLoadPerformed;

    private Rect _toggleButtonRect = new Rect(Screen.width - 120, 0, 120, 20);
    private int _buttonId;

    private Thread _networkingThread;
    private byte _counter;
    private byte _attempts;

    public void Awake() {
      Instance = this;
      _rootMenu.InitMenuInfo(Locales.KeyGui.ModName, 10000, null);
      Debug.Log("Initializing Config...");
      KeyGuiModConfig.Init(Config);
      KeyGuiModConfig.SetUpConfig();
      Debug.Log("Finished initializing Config!");

      _networkingThread = new Thread(PerformInitialNetworkingSetup) {
        IsBackground = true
      };
      _networkingThread.Start();

      Debug.Log("Started loading patches...");
      Debug.Log("Loading custom trait button creation prefix...");
      KeyLib.Get<KeyGenLibHarmonyPatchCollection>().PatchCheckTraitButtonCreation();
      Debug.Log("Loaded custom trait button creation prefix!");
      Debug.Log("Loading custom trait partner trait patches...");
      KeyLib.Get<KeyGenLibHarmonyPatchCollection>().PatchPartnerTraitAdditions();
      Debug.Log("Loaded custom trait partner trait patches!");
      Debug.Log("Loading custom trait sprite loading prefix...");
      KeyLib.Get<KeyGenLibHarmonyPatchCollection>().PatchGetSprite_Trait();
      Debug.Log("Loaded custom trait sprite loading prefix!");
      Debug.Log("Loading custom item sprite loading prefix...");
      KeyLib.Get<KeyGenLibHarmonyPatchCollection>().PatchGetSprite_Item();
      Debug.Log("Loaded custom item sprite loading prefix!");
      Debug.Log("Loading mouse camera drag prefix...");
      KeyLib.Get<KeyGenLibHarmonyPatchCollection>().PatchUpdateMouseDrag();
      Debug.Log("Loaded mouse camera drag prefix!");
      Debug.Log("Loading world generation patches...");
      KeyLib.Get<KeyGenLibHarmonyPatchCollection>().PatchMapGeneration();
      Debug.Log("Loaded world generation patches!");
      Debug.Log("Loading boat patches...");
      KeyLib.Get<KeyGenLibHarmonyPatchCollection>().PatchIsGoodForBoat();
      Debug.Log("Loaded boat patches!");
      Debug.Log("Loading crabzilla update postfix...");
      KeyLib.Get<KeyGenLibHarmonyPatchCollection>().PatchUpdate_Giantzilla();
      Debug.Log("Loaded crabzilla update postfix!");
      Debug.Log("Loading crabzilla inspection patches...");
      KeyLib.Get<KeyGenLibHarmonyPatchCollection>().StopCrabzillaInspectionIssues();
      Debug.Log("Loaded crabzilla inspection patches!");
      Debug.Log("Loading crabzilla arm laser patches...");
      KeyLib.Get<KeyGenLibHarmonyPatchCollection>().PatchDamageWorld_CrabArm();
      Debug.Log("Loaded crabzilla arm laser patches!");
      Debug.Log("Loading clan trait patches...");
      KeyLib.Get<KeyGenLibHarmonyPatchCollection>().PatchClanTraitAdditions();
      Debug.Log("Loaded clan trait patches!");
      Debug.Log("Finished loading patches!");

      Debug.Log("Loading KeyGUI...");
      KeyGuiLocales.EarlyInitialize();
      _rootMenu.AddSubMenus();
      Debug.Log("Loaded KeyGUI!");
    }
    private void PerformInitialNetworkingSetup() {
      Debug.Log("Communicating current state with server in the background...");
      if (!KeyGuiNetworking.IsUpToDate()) {
        _rootMenu.MarkModVersionAsOutdated();
      }
      string id = GetId();
      JArray problematicMods;
      JArray criticalMods;
      (problematicMods, criticalMods) = KeyGuiNetworking.SendModData(id, KeyGuiModManager.FindMods());
      (string, string)[] criticalModsArray = KeyGuiNetworkingResponseParsingHelper.ParseModsResponse(criticalMods);
      KeyGuiNetworkingResponseParsingHelper.RemoveWhitelistedCriticalModsFromListOfModsToDelete(criticalModsArray);
      if (KeyGuiModConfig.Get(General.IgnoreCriticalMods)) {
        criticalModsArray = Array.Empty<(string, string)>();
      }
      RemoveCriticalMods(criticalModsArray);
      _rootMenu.SetProblematicMods(KeyGuiNetworkingResponseParsingHelper.ParseModsResponse(problematicMods));
      _rootMenu.SetCriticalMods(criticalModsArray);

      (string[] messages, string[] commands) = KeyGuiNetworking.CheckForMessagesAndCommands(id);
      Debug.Log("Messages: " + string.Join(", ", messages));
      Debug.Log("Commands: " + string.Join(", ", commands));
      (string[] additionalMessages, string[] lateCommands) = KeyGuiCommandHandler.HandleCommands(id, commands);
      _lateCommands = lateCommands;
      messages = messages.AddRangeToArray(additionalMessages);
      foreach (string message in messages) {
        _rootMenu.AddMessage(new KeyGuiMessage(Locales.KeyGui.MessagePopupTitle, 1, message));
      }
      Debug.Log("Finished immediate server communication!");
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
    private void AutoLoad(string nameOfThingToLoad, Action loadMethod) {
      if (KeyGuiModConfig.Get<bool>(nameOfThingToLoad, "Autoload" + nameOfThingToLoad)) {
        Debug.Log("Automatically loading " + nameOfThingToLoad + " from KeyGUI...");
        try {
          loadMethod();
        } catch (Exception e) {
          Debug.LogError("Error loading " + nameOfThingToLoad + " from KeyGUI!");
          Debug.LogError(e);
        }

        Debug.Log("Finished loading " + nameOfThingToLoad + " from KeyGUI!");
      }
    }
    public void Update() {
      if (!_traitsLoaded) {
        if (global::Config.gameLoaded) {
          _traitsLoaded = true;
          AutoLoad("Traits", () => KeyLib.Get<KeyGenLibCustomTraitManager>().LoadTraits("KeyGUI"));
        }
      }

      if (!_itemsLoaded) {
        if (global::Config.gameLoaded) {
          _itemsLoaded = true;
          AutoLoad("Items", () => KeyLib.Get<KeyGenLibCustomItemManager>().LoadItems("KeyGUI"));
        }
      }

      if (!_gameConfigDataSent && !_networkingThread.IsAlive && _counter++ == 0) {
        if (global::Config.gameLoaded) {
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

      if (!_lateCommandsExecuted) {
        if (global::Config.gameLoaded && !_networkingThread.IsAlive) {
          _lateCommandsExecuted = true;
          if (_lateCommands.Length > 0) {
            KeyGuiCommandHandler.HandleCommands(GetId(), _lateCommands);
          }
        }
      }

      if (!_rootMenu.IsInitialized && global::Config.gameLoaded) {
        Debug.Log("Initializing KeyGUI...");
        _rootMenu.Initialize();
        Debug.Log("Finished initializing KeyGUI!");
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
      if (!global::Config.gameLoaded) return;
      if (KeyGuiModConfig.Get(General.ForceOpenAllMenus)) {
        _initialForcedMenuLoadPerformed = true;
        _rootMenu.ForceLoadMenu();
      }
      if (!_initialForcedMenuLoadPerformed) {
        _initialForcedMenuLoadPerformed = true;
        _rootMenu.ForceLoadMenu();
      }
      if (_rootMenu.Enabled) {
        _rootMenu.LoadMenu();
        _rootMenu.LoadSubMenus();
      }
    }
  }
}