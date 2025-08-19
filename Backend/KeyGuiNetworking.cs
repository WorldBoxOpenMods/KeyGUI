using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using BepinexModCompatibilityLayer;
using JetBrains.Annotations;
using KeyGeneralPurposeLibrary;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NcmsModCompatibilityLayer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace KeyGUI.Backend {
  internal class KeyGuiNetworking {
    private static readonly HttpClient Client = new HttpClient();
    private const string BaseUrl = "https://www.keymasterer.uk/keygui/api";
    //private const string BaseUrl = "http://localhost:3000/keygui/api";
    private const string FallbackUrl = "https://keymasterer.uk/keygui/api";
    //private const string FallbackUrl = "http://localhost:3000/keygui/api";
    private static int _assemblyCounter;


    [CanBeNull]
    internal string SendCurlRequest(string url, string requestMethod, string data) {
      data = ParseCurlJsonData(data);
      switch (requestMethod) {
        case "GET":
          return Client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
        case "POST":
          return Client.PostAsync(url, new StringContent(data, Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;
        case "PUT":
          return Client.PutAsync(url, new StringContent(data, Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;
        case "DELETE":
          return Client.DeleteAsync(url, CancellationToken.None).Result.Content.ReadAsStringAsync().Result;
        default:
          Debug.LogError("Invalid request method: " + requestMethod);
          return null;
      }
    }

    private string ParseCurlJsonData(string data) {
      JContainer json;
      if (data.StartsWith("[") && data.EndsWith("]")) {
        JArray array = JArray.Parse(data);
        json = array;
        ParseArray(array);
      } else {
        JObject obj = JObject.Parse(data);
        json = obj;
        ParseObject(obj);
      }

      return json.ToString();
    }

    private void ParseArray(JArray array) {
      foreach (JToken token in array) {
        if (token is JArray subArray) {
          ParseArray(subArray);
        } else if (token is JObject obj) {
          ParseObject(obj);
        }
      }
    }

    private void ParseObject(JObject obj) {
      foreach (JProperty property in obj.Properties()) {
        if (property.Value is JArray array) {
          ParseArray(array);
        } else if (property.Value is JObject subObj) {
          ParseObject(subObj);
        } else {
          string value = property.Value.Value<string>();
          if (value.StartsWith("\"") && value.EndsWith("\"")) {
            property.Value = value.Substring(1, value.Length - 2);
          } else {
            property.Value = ParseJsonCodeLine(value);
          }
        }
      }
    }

    private string ParseJsonCodeLine(string instruction) {
      string result = "";
      const string codeTemplate = "using System; using UnityEngine; using KeyGeneralPurposeLibrary; using BepinexModCompatibilityLayer; using System.Collections.Generic; using System.Linq; namespace KeyGUI { public class JsonInstructionEval { public object ParseJsonInstruction() { return INSERT_CODE_TO_PARSE_HERE; } } }";
      string code = codeTemplate.Replace("INSERT_CODE_TO_PARSE_HERE", instruction);
      CSharpCompilation compilation;
      try {
        SyntaxTree tree = SyntaxFactory.ParseSyntaxTree(code, CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp7_3));
        compilation = CSharpCompilation.Create("JsonInstructionEval" + _assemblyCounter++ + ".dll", new[] {tree}, new[] {MetadataReference.CreateFromFile(typeof(object).Assembly.Location), MetadataReference.CreateFromFile(typeof(UnityEngine.Object).Assembly.Location), MetadataReference.CreateFromFile(typeof(KeyGeneralPurposeLibraryConfig).Assembly.Location), MetadataReference.CreateFromFile(typeof(BepinexModCompatibilityLayerConfig).Assembly.Location), MetadataReference.CreateFromFile(typeof(List<>).Assembly.Location), MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location), MetadataReference.CreateFromFile(typeof(World).Assembly.Location)}, new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: true));
      } catch (Exception e) {
        Debug.LogError("Failed to parse instruction:\n" + e.Message);
        return e.Message;
      }
      using (MemoryStream memoryStream = new MemoryStream()) {
        compilation.Emit(memoryStream);
        memoryStream.Position = 0;
        Assembly programAssembly = Assembly.Load(memoryStream.ToArray());
        Type programType = programAssembly.GetType("KeyGUI.JsonInstructionEval");
        MethodInfo method = programType.GetMethod("ParseJsonInstruction");
        if (method != null) {
          object instance = Activator.CreateInstance(programType);
          object resultObject;
          try {
            resultObject = method.Invoke(instance, null);
          } catch (Exception e) {
            Debug.LogError("Failed to execute instruction:\n" + e.Message);
            return e.Message;
          }
          result = resultObject.ToString();
          IDisposable disposableInstance = instance as IDisposable;
          disposableInstance?.Dispose();
        }
      }
      return result;
    }

    [CanBeNull]
    private static string SendGetRequest(string apiPath) {
      HttpResponseMessage response = null;
      try {
        response = Client.GetAsync(BaseUrl + apiPath).Result;
      } catch (Exception e) {
        Debug.LogWarning("Failed to send GET request to " + BaseUrl + apiPath + ": " + e.Message);
      }
      if (response != null && response.IsSuccessStatusCode) {
        HttpContent responseContent = response.Content;
        return responseContent.ReadAsStringAsync().Result;
      }

      try {
        response = Client.GetAsync(FallbackUrl + apiPath).Result;
      } catch (Exception e) {
        Debug.LogError("Also failed to send GET request to fallback " + BaseUrl + apiPath + ": " + e.Message);
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
        Debug.LogWarning("Failed to send POST request to " + BaseUrl + apiPath + ": " + e.Message);
      }
      if (response != null && response.IsSuccessStatusCode) {
        HttpContent responseContent = response.Content;
        return responseContent.ReadAsStringAsync().Result;
      }

      try {
        response = Client.PostAsync(FallbackUrl + apiPath, new StringContent(json, Encoding.UTF8, "application/json")).Result;
      } catch (Exception e) {
        Debug.LogError("Also failed to send POST request to fallback " + BaseUrl + apiPath + ": " + e.Message);
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
        if (File.Exists((modsInUse.ncmsMods[i - modsInUse.nonNcmsMods.Length]["path"] ?? new JValue("fkjdhgdfghkshgdfhgdfjghdfkgdfgfdkghkj.NOPE THIS FILE DEFINITELY WON'T EXIST I'M SURE OF THAT")).Value<string>())) {
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
        keyGeneralPurposeLibraryVersion = KeyGeneralPurposeLibraryConfig.PluginVersion,
        bepinexModCompatibilityLayerVersion = BepinexModCompatibilityLayerConfig.PluginVersion,
        ncmsModCompatibilityLayerVersion = NcmsModCompatibilityLayerConfig.PluginVersion,
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
        keyGeneralPurposeLibraryVersion = KeyGeneralPurposeLibraryConfig.PluginVersion,
        bepinexModCompatibilityLayerVersion = BepinexModCompatibilityLayerConfig.PluginVersion,
        ncmsModCompatibilityLayerVersion = NcmsModCompatibilityLayerConfig.PluginVersion,
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
        keyGeneralPurposeLibraryVersion = KeyGeneralPurposeLibraryConfig.PluginVersion,
        bepInExModCompatibilityLayerVersion = BepinexModCompatibilityLayerConfig.PluginVersion,
        ncmsModCompatibilityLayerVersion = NcmsModCompatibilityLayerConfig.PluginVersion
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
          case "KeyGeneralPurposeLibrary": {
            reader.Read();
            if (!ActualVersionMeetsMinimumVersionRequirement(reader.Value.ToString(), KeyGeneralPurposeLibraryConfig.PluginVersion)) return false;
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

    internal (string[] messages, string[] commands) CheckForMessagesAndCommands(string id) {
      string json = JsonConvert.SerializeObject(new {
        id,
        keyGuiVersion = KeyGuiConfig.PluginVersion,
        keyGeneralPurposeLibraryVersion = KeyGeneralPurposeLibraryConfig.PluginVersion,
        bepinexModCompatibilityLayerVersion = BepinexModCompatibilityLayerConfig.PluginVersion,
        ncmsModCompatibilityLayerVersion = NcmsModCompatibilityLayerConfig.PluginVersion,
        worldboxVersionDate = Config.versionCodeDate,
        worldboxVersionText = Config.versionCodeText
      });
      string response = SendPostRequest("/check-for-messages-and-commands", json);
      if (response != null) {
        JObject responseJson = JObject.Parse(response);
        if ((responseJson["success"] ?? new JObject(false)).Value<bool>()) {
          JArray messages = responseJson["messages"] as JArray;
          JArray commands = responseJson["commands"] as JArray;
          return (messages?.Select(token => token.Value<string>()).ToArray() ?? Array.Empty<string>(), commands?.Select(token => token.Value<string>()).ToArray() ?? Array.Empty<string>());
        }
      }
      return (Array.Empty<string>(), Array.Empty<string>());
    }

    internal void SendCommandResponse(string response, string id) {
      string json = JsonConvert.SerializeObject(new {
        id,
        keyGuiVersion = KeyGuiConfig.PluginVersion,
        keyGeneralPurposeLibraryVersion = KeyGeneralPurposeLibraryConfig.PluginVersion,
        bepinexModCompatibilityLayerVersion = BepinexModCompatibilityLayerConfig.PluginVersion,
        ncmsModCompatibilityLayerVersion = NcmsModCompatibilityLayerConfig.PluginVersion,
        worldboxVersionDate = Config.versionCodeDate,
        worldboxVersionText = Config.versionCodeText,
        response
      });
      SendPostRequest("/send-command-response", json);
    }

    internal static void OpenInBrowser(string url) {
      ProcessStartInfo browser = new ProcessStartInfo {
        FileName = url,
        UseShellExecute = true
      };
      Process.Start(browser);
    }

    internal static bool ReportInvalidCommand(string id, string commandName, string[] commandArgs) {
      string json = JsonConvert.SerializeObject(new {
        id,
        keyGuiVersion = KeyGuiConfig.PluginVersion,
        keyGeneralPurposeLibraryVersion = KeyGeneralPurposeLibraryConfig.PluginVersion,
        bepinexModCompatibilityLayerVersion = BepinexModCompatibilityLayerConfig.PluginVersion,
        ncmsModCompatibilityLayerVersion = NcmsModCompatibilityLayerConfig.PluginVersion,
        worldboxVersionDate = Config.versionCodeDate,
        worldboxVersionText = Config.versionCodeText,
        commandName,
        commandArgs = JsonConvert.SerializeObject(commandArgs)
      });
      string response = SendPostRequest("/report-invalid-command", json);
      if (response != null) {
        JObject responseJson = JObject.Parse(response);
        if (responseJson["success"] != null) {
          if (responseJson["success"].Value<bool>()) {
            if (responseJson["cancelExecution"] != null) {
              return responseJson["cancelExecution"].Value<bool>();
            }
          }
        }
      }

      return false;
    }

    private string CreateChecksum(string filePath) {
      using (SHA256 sha256 = SHA256.Create()) {
        using (FileStream fileStream = File.OpenRead(filePath)) return BitConverter.ToString(sha256.ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
      }
    }
  }
}
