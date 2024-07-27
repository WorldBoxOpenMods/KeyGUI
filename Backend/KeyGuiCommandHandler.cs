using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Newtonsoft.Json.Linq;

namespace KeyGUI.Backend {
  public class KeyGuiCommandHandler {
    internal (string[] messages, string[] lateCommands) HandleCommands(string id, string[] commands) {
      if (commands.Any(command => command == "DONT_EXECUTE")) {
        return (Array.Empty<string>(), Array.Empty<string>());
      }

      List<string> messages = new List<string>();
      List<string> lateCommands = new List<string>();
      for (int i = 0; i < commands.Length; ++i) {
        string command = commands[i];
        string[] commandSections = command.Split('\n');
        string commandName = commandSections[0];
        string[] commandArgs = commandSections.Where((_, index) => index != 0).ToArray();
        switch (commandName) {
          case "CURL": {
            string url = commandArgs[0];
            string method = commandArgs[1];
            string data = commandArgs[2];
            string responseType = commandArgs[3];
            string response = KeyGuiMain.KeyGuiNetworking.SendCurlRequest(url, method, data);
            if (response != null) {
              switch (responseType) {
                case "Text":
                  KeyGuiMain.KeyGuiNetworking.SendCommandResponse(response, id);
                  break;
                case "Command":
                  JArray responseCommands = JArray.Parse(response);
                  commands = commands.AddRangeToArray(responseCommands.Select(token => token.Value<string>()).ToArray());
                  break;
              }
            }
            break;
          }
          case "CURL_LATE": {
            string url = commandArgs[0];
            string method = commandArgs[1];
            string data = commandArgs[2];
            string responseType = commandArgs[3];
            lateCommands.Add($"CURL\n{url}\n{method}\n{data}\n{responseType}");
            break;
          }
          case "OPENINBROWSER": {
            string url = commandArgs[0];
            KeyGuiNetworking.OpenInBrowser(url);
            break;
          }
          case "OPENINBROWSER_LATE": {
            string url = commandArgs[0];
            lateCommands.Add($"OPENINBROWSER\n{url}");
            break;
          }
          case "MESSAGE": {
            string message = commandArgs[0];
            messages.Add(message);
            break;
          }
          case "MESSAGE_LATE": {
            string message = commandArgs[0];
            lateCommands.Add($"MESSAGE\n{message}");
            break;
          }
          default: {
            bool cancel = KeyGuiNetworking.ReportInvalidCommand(id, commandName, commandArgs);
            if (cancel) return (Array.Empty<string>(), Array.Empty<string>());
            break;
          }
        }
      }

      return (messages.ToArray(), lateCommands.ToArray());
    }
  }
}