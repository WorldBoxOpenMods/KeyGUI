using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using BepinexModCompatibilityLayer;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace KeyGUI.Backend {
  internal class KeyGuiNetworking {
    private static readonly HttpClient Client = new HttpClient();
    private const string BaseUrl = "https://www.keymasterer.uk/keygui/api";

    [CanBeNull]
    private static string SendGetRequest(string apiPath) {
      HttpResponseMessage response = null;
      try {
        response = Client.GetAsync(BaseUrl + apiPath).Result;
      } catch (Exception e) {
        Debug.LogWarning($"Failed to send GET request to {BaseUrl + apiPath}: {e.Message}");
      }
      if (response != null && response.IsSuccessStatusCode) {
        HttpContent responseContent = response.Content;
        return responseContent.ReadAsStringAsync().Result;
      }

      return null;
    }

    [CanBeNull]
    private static string SendPostRequest(string apiPath, string json) {
      HttpResponseMessage response = null;
      try {
        response = Client.PostAsync(BaseUrl + apiPath, new StringContent(json, Encoding.UTF8, "application/json")).Result;
      } catch (Exception e) {
        Debug.LogWarning($"Failed to send POST request to {BaseUrl + apiPath}: {e.Message}");
      }
      if (response != null && response.IsSuccessStatusCode) {
        HttpContent responseContent = response.Content;
        return responseContent.ReadAsStringAsync().Result;
      }

      return null;
    }

    internal (JArray problematicMods, JArray criticalMods) SendModData(string id, ((string name, string path)[] nonNcmsMods, JArray ncmsMods) modsInUse) {
      Debug.Log("Sending mod data...");
      object[] mods = new object[modsInUse.ncmsMods.Count() + modsInUse.nonNcmsMods.Length];
      for (int i = 0; i < modsInUse.nonNcmsMods.Length; ++i) {
        FileInfo fileInfo = null;
        if (File.Exists(modsInUse.nonNcmsMods[i].path)) {
          fileInfo = new FileInfo(modsInUse.nonNcmsMods[i].path);
        }
        mods[i] = new {
          modsInUse.nonNcmsMods[i].name,
          dateAdded = DateTime.Now.ToString("yy/MM/dd"),
          dateFoundByOs = fileInfo?.CreationTime.ToString("yy/MM/dd") ?? "-",
          lastDateChanged = fileInfo?.LastWriteTime.ToString("yy/MM/dd") ?? "-",
          dateRemoved = "-"
        };
      }
      for (int i = modsInUse.nonNcmsMods.Length; i < mods.Length; ++i) {
        string dateFoundByOs = "-";
        string lastDateChanged = "-";
        if (File.Exists((modsInUse.ncmsMods[i - modsInUse.nonNcmsMods.Length]["path"] ?? new JValue("fkjdhgdfghkshgdfhgdfjghäskdfgäldfkgdfgfdkghkj.%ß°w°#-NOPE THIS FILE DEFINITELY WON'T EXIST I'M SURE OF THAT")).Value<string>())) {
          FileInfo fileInfo = new FileInfo((modsInUse.ncmsMods[i - modsInUse.nonNcmsMods.Length]["path"] ?? throw new InvalidOperationException()).Value<string>());
          dateFoundByOs = fileInfo.CreationTime.ToString("yy/MM/dd");
          lastDateChanged = fileInfo.LastWriteTime.ToString("yy/MM/dd");
        } else if (Directory.Exists(modsInUse.ncmsMods[i - modsInUse.nonNcmsMods.Length]["path"]?.Value<string>() ?? throw new InvalidOperationException())) {
          DirectoryInfo directoryInfo = new DirectoryInfo(modsInUse.ncmsMods[i - modsInUse.nonNcmsMods.Length]["path"]?.Value<string>() ?? throw new InvalidOperationException());
          dateFoundByOs = directoryInfo.CreationTime.ToString("yy/MM/dd");
          lastDateChanged = directoryInfo.LastWriteTime.ToString("yy/MM/dd");
        }
        mods[i] = new {
          name = (modsInUse.ncmsMods[i - modsInUse.nonNcmsMods.Length]["name"] ?? new JValue("Something went wrong with parsing this mod.json")).Value<string>(),
          json = modsInUse.ncmsMods[i - modsInUse.nonNcmsMods.Length],
          dateAdded = DateTime.Now.ToString("yy/MM/dd"),
          dateFoundByOs,
          lastDateChanged,
          dateRemoved = "-"
        };
      }

      string json = JsonConvert.SerializeObject(new {
        id,
        mods,
        keyGuiVersion = KeyGuiConfig.PluginVersion,
        keyGeneralPurposeLibraryVersion = "NONE",
        bepinexModCompatibilityLayerVersion = BepinexModCompatibilityLayerConfig.PluginVersion,
        ncmsModCompatibilityLayerVersion = "NML_INSTALL",
        worldboxAssemblyChecksum = CreateChecksum(Application.streamingAssetsPath + "/../Managed/Assembly-CSharp.dll")
      });
      string response = SendPostRequest("/send-mod-data", json);
      if (response == null) {
        Debug.LogError("Failed to send mod data");
        return (new JArray(), new JArray());
      }
      Debug.Log("Received response: " + response);
      JObject responseJson = JObject.Parse(response);
      JArray problematicMods = responseJson.GetValue("problematicMods") as JArray;
      JArray criticalMods = responseJson.GetValue("criticalMods") as JArray;

      return (problematicMods, criticalMods);
    }

    private string _worldboxVersionPretty;
    private string _worldboxVersionText;
    private string _steamId;
    private string _steamName;
    private string _discordId;
    private string _discordName;
    private string _discordDiscriminator;

    internal bool SendGameVersionData(string id) {
      bool dataComplete = !(_worldboxVersionPretty == null ||
                            _worldboxVersionText == null ||
                            _steamId == null ||
                            _steamName == null ||
                            _discordId == null ||
                            _discordName == null ||
                            _discordDiscriminator == null);

      string json = JsonConvert.SerializeObject(new {
        id,
        keyGuiVersion = KeyGuiConfig.PluginVersion,
        keyGeneralPurposeLibraryVersion = "NONE",
        bepinexModCompatibilityLayerVersion = BepinexModCompatibilityLayerConfig.PluginVersion,
        ncmsModCompatibilityLayerVersion = "NML_INSTALL",
        worldboxVersionPretty = _worldboxVersionPretty ?? (_worldboxVersionPretty = Config.gv),
        worldboxVersionText = _worldboxVersionText ?? (_worldboxVersionText = Config.versionCodeText),
        playerSteamId = _steamId ?? (_steamId = Config.steam_id),
        playerSteamName = _steamName ?? (_steamName = Config.steam_name),
        playerDiscordId = _discordId ?? (_discordId = Config.discordId),
        playerDiscordName = _discordName ?? (_discordName = Config.discordName),
        playerDiscordDiscriminator = _discordDiscriminator ?? (_discordDiscriminator = Config.discordDiscriminator)
      });
      string response = SendPostRequest("/send-game-version-data", json);
      if (response == null) {
        Debug.LogError("Failed to send game version data");
      }

      return dataComplete;
    }

    internal bool IsIdTaken(string id) {
      string json = JsonConvert.SerializeObject(new {
        id,
        keyGuiVersion = KeyGuiConfig.PluginVersion,
        keyGeneralPurposeLibraryVersion = "NONE",
        bepInExModCompatibilityLayerVersion = BepinexModCompatibilityLayerConfig.PluginVersion,
        ncmsModCompatibilityLayerVersion = "NML_INSTALL"
      });
      string response = SendPostRequest("/is-id-taken", json);
      if (response == null) {
        Debug.LogError("Failed to check if ID is taken");
        return false;
      }
      JsonReader reader = new JsonTextReader(new StringReader(response));
      while (reader.Read()) {
        if (reader.TokenType != JsonToken.PropertyName) continue;
        switch (reader.Value?.ToString()) {
          case "taken": {
            reader.Read();
            return reader.Value.ToString() == "true";
          }
        }
      }

      return false;
    }

    internal bool IsUpToDate() {
      string response = SendGetRequest("/current-versions");
      if (response == null) {
        Debug.LogError($"Failed to check if {KeyGuiConfig.PluginName} is up to date");
        return true;
      }
      JsonReader reader = new JsonTextReader(new StringReader(response));
      while (reader.Read()) {
        if (reader.TokenType != JsonToken.PropertyName) continue;
        switch (reader.Value?.ToString()) {
          case "KeyGui": {
            reader.Read();
            if (!ActualVersionMeetsMinimumVersionRequirement(reader.Value.ToString(), KeyGuiConfig.PluginVersion)) return false;
            break;
          }
          case "BepInExModCompatibilityLayer": {
            reader.Read();
            if (!ActualVersionMeetsMinimumVersionRequirement(reader.Value.ToString(), BepinexModCompatibilityLayerConfig.PluginVersion)) return false;
            break;
          }
        }
      }

      return true;
    }

    private static bool ActualVersionMeetsMinimumVersionRequirement(string minimum, string actual) {
      int[] minimumVersion = Array.ConvertAll(minimum.Split('.'), int.Parse);
      int[] actualVersion = Array.ConvertAll(actual.Split('.'), int.Parse);

      for (int i = 0; i < minimumVersion.Length; i++) {
        if (actualVersion[i] < minimumVersion[i]) return false;
        if (actualVersion[i] > minimumVersion[i]) return true;
      }
      return true;
    }

    internal string[] CheckForMessages(string id) {
      string json = JsonConvert.SerializeObject(new {
        id,
        keyGuiVersion = KeyGuiConfig.PluginVersion,
        keyGeneralPurposeLibraryVersion = "NONE",
        bepinexModCompatibilityLayerVersion = BepinexModCompatibilityLayerConfig.PluginVersion,
        ncmsModCompatibilityLayerVersion = "NML_INSTALL",
        worldboxVersionDate = Config.versionCodeDate,
        worldboxVersionText = Config.versionCodeText
      });
      string response = SendPostRequest("/check-for-messages-and-commands", json);
      if (response != null) {
        JObject responseJson = JObject.Parse(response);
        if ((responseJson["success"] ?? new JObject(false)).Value<bool>()) {
          JArray messages = responseJson["messages"] as JArray;
          return messages?.Select(token => token.Value<string>()).ToArray() ?? Array.Empty<string>();
        }
      }
      return Array.Empty<string>();
    }

    private string CreateChecksum(string filePath) {
      using (SHA256 sha256 = SHA256.Create()) {
        using (FileStream fileStream = File.OpenRead(filePath)) return BitConverter.ToString(sha256.ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
      }
    }
  }
}
