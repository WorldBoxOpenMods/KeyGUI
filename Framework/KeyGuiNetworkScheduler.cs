using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BepinexModCompatibilityLayer;
using KeyGUI.Menus.ModConfig;
using KeyGUI.Menus.ModConfig.ConfigOptions;
using KeyGUI.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace KeyGUI.Framework {
  public class KeyGuiNetworkScheduler {
    // ReSharper disable ClassNeverInstantiated.Global, UnassignedField.Global, CollectionNeverUpdated.Global, MemberCanBePrivate.Global
    public class ModVersionResponse {
      public string KeyGuiVersion;
      public string BepinexModCompatibilityLayerVersion;
    }
    public class RegistrationResponse {
      public string Id;
      public string Secret;
    }
    public class ModIssueInfo {
      public string ModName;
      public string IssueDescription;
    }
    public class GameDataResponse {
      public List<ModIssueInfo> ProblematicMods;
      public List<ModIssueInfo> CriticalMods;
    }
    // ReSharper restore ClassNeverInstantiated.Global, UnassignedField.Global, CollectionNeverUpdated.Global, MemberCanBePrivate.Global
    private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings {
      ContractResolver = new DefaultContractResolver {
        NamingStrategy = new SnakeCaseNamingStrategy()
      }
    };
    private static readonly HttpClient Client = new HttpClient();
    private const string BaseUrl = "https://keygui.keymasterer.uk/api";

    private static async Task<TResponse> SendGetRequest<TResponse>(string apiPath) where TResponse : class {
      HttpResponseMessage response = null;
      try {
        response = await Client.GetAsync(BaseUrl + apiPath);
      } catch (Exception e) {
        Debug.LogWarning($"Failed to send GET request to {BaseUrl + apiPath}: {e.Message}");
      }
      if (response != null && response.IsSuccessStatusCode) {
        HttpContent responseContent = response.Content;
        try {
          return JsonConvert.DeserializeObject<TResponse>(await responseContent.ReadAsStringAsync(), JsonSerializerSettings);
        } catch (Exception e) {
          Debug.LogWarning($"Failed to deserialize response from {BaseUrl + apiPath}: {e.Message}");
        }
      }

      return null;
    }

    private static async Task<TResponse> SendPostRequest<TResponse>(string apiPath, object json) where TResponse : class {
      string jsonString = json as string ?? JsonConvert.SerializeObject(json, JsonSerializerSettings);
      HttpResponseMessage response = null;
      try {
        response = await Client.PostAsync(BaseUrl + apiPath, new StringContent(jsonString, Encoding.UTF8, "application/json"));
      } catch (Exception e) {
        Debug.LogWarning($"Failed to send POST request to {BaseUrl + apiPath}: {e.Message}");
      }
      if (response != null && response.IsSuccessStatusCode) {
        HttpContent responseContent = response.Content;
        try {
          return JsonConvert.DeserializeObject<TResponse>(await responseContent.ReadAsStringAsync(), JsonSerializerSettings);
        } catch (Exception e) {
          Debug.LogWarning($"Failed to deserialize response from {BaseUrl + apiPath}: {e.Message}");
        }
      }

      return null;
    }
    public async Task RunAsync() {
      Debug.Log("Communicating current state with server in the background...");
      (string id, string secret) = await GetCredentials();
      Debug.Log($"Using id: {id}, secret: {secret?.Substring(0, 8)}...");
      if (id == null || secret == null) {
        Debug.Log("Skipping further communication due to optout or failure to get credentials");
        return;
      }
      await PerformInitialNetworkingSetup(id, secret);
      bool gameInfoSent = false;
      while (true) {
        await Task.Delay(TimeSpan.FromSeconds(5));
        if (!gameInfoSent && Config.game_loaded) {
          gameInfoSent = true;
          Debug.Log("Game loaded, sending accounts data in the background...");
          for (int i = 0; i < 15; ++i) {
            await Task.Delay(TimeSpan.FromSeconds(1));
            try {
              await SendPostRequest<object>($"/users/{id}/accounts", new {
                Secret = secret,
                SteamId = Config.steam_id,
                SteamName = Config.steam_name,
                DiscordId = Config.discordId,
                DiscordName = Config.discordName
              });
            } catch (Exception) {
              // ignored
            }
          }
        }
      }
    }

    private async Task PerformInitialNetworkingSetup(string id, string secret) {
      if (!await IsUpToDate()) {
        KeyGui.Instance.MarkModVersionAsOutdated();
      }
      (KeyGuiModManager.NativeModInfo[] nativeMods, KeyGuiModManager.BepinexModInfo[] bepinexMods, KeyGuiModManager.NmlModInfo[] nmlMods) = KeyGuiModManager.FindMods();
      Debug.Log($"Found {nmlMods.Length} NML mods, {bepinexMods.Length} BepInEx mods, and {nativeMods.Length} native mods!");
      Debug.Log($"NML mods: {string.Join(", ", nmlMods.Select(m => m.Name))}");
      Debug.Log($"BepInEx mods: {string.Join(", ", bepinexMods.Select(m => m.Name))}");
      Debug.Log($"Native mods: {string.Join(", ", nativeMods.Select(m => m.Name))}");
      GameDataResponse gameDataResponse = await SendGameData(id, secret, nativeMods, bepinexMods, nmlMods);
      (string Name, string)[] criticalMods = gameDataResponse.CriticalMods.Select(m => (m.ModName, m.IssueDescription)).ToArray();
      if (KeyGuiModConfig.Get(General.IgnoreCriticalMods)) {
        criticalMods = Array.Empty<(string, string)>();
      }
      foreach (string modName in criticalMods.Select(iI => iI.Name)) {
        KeyGuiModManager.RemoveMod(modName);
      }
      KeyGui.Instance.SetProblematicMods(gameDataResponse.ProblematicMods.Select(m => (m.ModName, m.IssueDescription)).ToArray());
      KeyGui.Instance.SetCriticalMods(criticalMods);

      string[] messages = await SendGetRequest<string[]>($"/users/{id}/messages");
      Debug.Log($"Messages: {string.Join(", ", messages)}");
      foreach (string message in messages) {
        KeyGui.Instance.AddMessage(message);
      }
    }

    private async Task<bool> IsUpToDate() {
      ModVersionResponse modVersionResponse = await SendGetRequest<ModVersionResponse>("/mods/current-versions");
      if (modVersionResponse == null) {
        Debug.LogError($"Failed to check if {KeyGuiConfig.PluginName} is up to date");
        return true;
      }
      return ActualVersionMeetsMinimumVersionRequirement(modVersionResponse.KeyGuiVersion, KeyGuiConfig.PluginVersion) && ActualVersionMeetsMinimumVersionRequirement(modVersionResponse.BepinexModCompatibilityLayerVersion, BepinexModCompatibilityLayerConfig.PluginVersion);
    }

    private static bool ActualVersionMeetsMinimumVersionRequirement(string minimum, string actual) {
      int[] minimumVersion = Array.ConvertAll(minimum.Split('.'), int.Parse);
      int[] actualVersion = Array.ConvertAll(actual.Split('.'), int.Parse);

      for (int i = 0; i < minimumVersion.Length; i++) {
        int actualPart = i < actualVersion.Length ? actualVersion[i] : 0;
        int minimumPart = i < minimumVersion.Length ? minimumVersion[i] : 0;
        if (actualPart < minimumPart) return false;
        if (actualPart > minimumPart) return true;
      }
      return true;
    }

    private async Task<(string id, string secret)> GetCredentials() {
      switch (KeyGuiModConfig.Get(Internal.InstallId)) {
        case "Optout":
          return (null, null);
        case "None": {
          RegistrationResponse registrationResponse = await SendPostRequest<RegistrationResponse>("/users/register", new { });
          if (registrationResponse == null) {
            Debug.LogError("Failed to register user");
            return (null, null);
          }
          KeyGuiModConfig.Set(Internal.InstallId, registrationResponse.Id);
          KeyGuiModConfig.Set(Internal.Secret, registrationResponse.Secret);
          break;
        }
      }
      return (KeyGuiModConfig.Get(Internal.InstallId), KeyGuiModConfig.Get(Internal.Secret));
    }

    private async Task<GameDataResponse> SendGameData(string id, string secret, KeyGuiModManager.NativeModInfo[] nativeMods, KeyGuiModManager.BepinexModInfo[] bepinexMods, KeyGuiModManager.NmlModInfo[] nmlMods) {
      GameDataResponse response = await SendPostRequest<GameDataResponse>($"/users/{id}/mods", new {
        Secret = secret,
        KeyGuiVersion = KeyGuiConfig.PluginVersion,
        BepinexModCompatibilityLayerVersion = BepinexModCompatibilityLayerConfig.PluginVersion,
        UnityRuntime = Application.unityVersion,
        WorldboxAssemblyChecksum = CreateChecksum($"{Application.streamingAssetsPath}/../Managed/Assembly-CSharp.dll"),
        WorldboxVersion = Config.gv,
        WorldboxVersionBuildNumber = Config.versionCodeText,
        InstalledMods = new List<IEnumerable<object>> {
          nmlMods,
          bepinexMods,
          nativeMods
        }.SelectMany(m => m).ToList()
      });
      if (response == null) {
        Debug.LogWarning("Failed to send game data");
        return new GameDataResponse {
          ProblematicMods = new List<ModIssueInfo>(),
          CriticalMods = new List<ModIssueInfo>()
        };
      }
      return response;
    }

    private static string CreateChecksum(string filePath) {
      using (SHA256 sha256 = SHA256.Create()) {
        using (FileStream fileStream = File.OpenRead(filePath)) return BitConverter.ToString(sha256.ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
      }
    }
  }
}
