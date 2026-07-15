using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using HarmonyLib;
using MelonLoader;
using UnityEngine;
using MelonLoader.Utils;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Diagnostics;

[assembly: MelonInfo(typeof(NovaLandsArchipelago.Core), "NovaLandsArchipelago", "0.1.0", "Gott", null)]
[assembly: MelonGame("BEHEMUTT", "Nova Lands")]

namespace NovaLandsArchipelago
{
    public class Core : MelonMod
    {
        public static Core Instance;
        private MelonPreferences_Category ConnectionData;
        private MelonPreferences_Entry<string> Server;
        private MelonPreferences_Entry<string> PlayerName;
        private MelonPreferences_Entry<string> Password;
        public static bool connected = true;
        public static HarmonyLib.Harmony harmony;
        public Dictionary<string, int> LocationIDs = new()
        {
            {"Research Mass Production I", 1},
            {"Research Explorer Needs I", 2},
            {"Research Automation I", 3},
            {"Research Deposits I", 4},
            {"Research Jetpack", 5},
            {"Research Farming I", 6},
            {"Research Power I", 7},
            {"Research Automation II", 8},
            {"Research Energy Rifle", 9},
            {"Research Ranching I", 10},
            {"Research Power II", 11},
            {"Research Mass Production II", 12},
            {"Research Deposits II", 13},
            {"Research Suit Armor", 14},
            {"Research Farming II", 15},
            {"Research Explorer Needs III", 16},
            {"Research Advanced Production I", 17},
            {"Research Explorer Needs II", 18},
            {"Research Ranching II", 19},
            {"Research Overclocking I", 20},
            {"Research Advanced Production II", 21},
            {"Research Modules I", 22},
            {"Research Farming III", 23},
            {"Research Advanced Production III", 24},
            {"Research Explorer Needs IV", 25},
            {"Research Ranching III", 26},
            {"Research Power III", 27},
            {"Research Complex Production I", 28},
            {"Research Liquids I", 29},
            {"Research Overclocking II", 30},
            {"Research Mass Transport", 31},
            {"Research Glass Works", 32},
            {"Research Complex Production II", 33},
            {"Research Superhard Minerals", 34},
            {"Research Supercomputer", 35},
            {"Research Nuclear Tech", 36},
            {"Research Hypercomputer", 37},
        };
        public override void OnInitializeMelon()
        {
            Instance = this;
            harmony = this.HarmonyInstance;
            LoggerInstance.Msg("Initialized.");
            MelonEvents.OnGUI.Subscribe(DrawMenu, 100); // The higher the value, the lower the priority.
            ConnectionData = MelonPreferences.CreateCategory("ConnectionData");
            Server = ConnectionData.CreateEntry("Server", "archipelago.gg:40000");
            PlayerName = ConnectionData.CreateEntry("PlayerName", "Player1");
            Password = ConnectionData.CreateEntry("Password", "");
            Patches.Init();
        }

        public ArchipelagoSession archipelagoSession;
        private enum FocusField { None, Server, Name, Password }
        private FocusField focused = FocusField.None;

        private void DrawMenu()
        {
            // Allow drawing UI even when connected so we can provide a test button for research purchase.
            // Layout values
            int x = 10;
            int y = 10;
            int width = 300;
            int height = 22;
            int labelWidth = 60;
            int spacing = 28;
            GUIStyle buttonStyle = new(GUI.skin.button);

            // Handle keyboard input when a field is focused
            Event e = Event.current;
            if (!connected && e.type == EventType.KeyDown && focused != FocusField.None)
            {
                if (e.keyCode == KeyCode.Backspace)
                {
                    switch (focused)
                    {
                        case FocusField.Server:
                            if (Server.Value.Length > 0) Server.Value = Server.Value[..^1];
                            break;
                        case FocusField.Name:
                            if (PlayerName.Value.Length > 0) PlayerName.Value = PlayerName.Value[..^1];
                            break;
                        case FocusField.Password:
                            if (Password.Value.Length > 0) Password.Value = Password.Value[..^1];
                            break;
                    }
                }
                else if (e.keyCode == KeyCode.Return || e.keyCode == KeyCode.KeypadEnter)
                {
                    // finish editing on enter
                    focused = FocusField.None;
                }
                else
                {
                    char c = e.character;
                    if (!char.IsControl(c))
                    {
                        switch (focused)
                        {
                            case FocusField.Server:
                                Server.Value += c;
                                break;
                            case FocusField.Name:
                                PlayerName.Value += c;
                                break;
                            case FocusField.Password:
                                Password.Value += c;
                                break;
                        }
                    }
                }
            }

            if (!connected)
            {
                // Background box covering all fields
                GUI.Box(new Rect(x - 6, y - 8, width + 16, height * 3 + spacing * 2 + 16), "");

                // Server
                GUI.Label(new Rect(x, y, labelWidth, height), "Server:");
                string serverDisplay = Server.Value;
                if (focused == FocusField.Server) serverDisplay += "|";
                if (GUI.Button(new Rect(x + labelWidth, y, width - labelWidth, height), new GUIContent(serverDisplay), buttonStyle))
                    focused = FocusField.Server;
                y += spacing;

                // Name
                GUI.Label(new Rect(x, y, labelWidth, height), "Name:");
                string nameDisplay = PlayerName.Value;
                if (focused == FocusField.Name) nameDisplay += "|";
                if (GUI.Button(new Rect(x + labelWidth, y, width - labelWidth, height), new GUIContent(nameDisplay), buttonStyle))
                    focused = FocusField.Name;
                y += spacing;

                // Password (masked)
                GUI.Label(new Rect(x, y, labelWidth, height), "Password:");
                string masked = new('*', Password.Value.Length);
                if (focused == FocusField.Password) masked += "|";
                if (GUI.Button(new Rect(x + labelWidth, y, width - labelWidth, height), new GUIContent(masked), buttonStyle))
                    focused = FocusField.Password;
                y += spacing;

                // Connect button
                if (GUI.Button(new Rect(x, y, 120, height), new GUIContent("Connect"), buttonStyle))
                {
                    var session = ArchipelagoSessionFactory.CreateSession(Server.Value);


                    var version = Version.Parse("0.6.7");
                    LoginResult result;
                    // Call TryConnectAndLogin with an empty string[] for the fifth parameter and the password as the sixth
                    result = session.TryConnectAndLogin("Nova Lands", PlayerName.Value, ItemsHandlingFlags.AllItems, version, System.Array.Empty<string>(), Password.Value);
                    if (result.Successful)
                    {
                        LoggerInstance.Msg("Successfully connected to Archipelago server!");
                        connected = true;
                        // keep a reference to the session (stored as object so we can reflectively call send methods)
                        archipelagoSession = session;
                    }
                    else
                    {
                        LoggerInstance.Error($"Failed to connect: {result}");
                    }
                }
            }
            y += spacing;
            if (GUI.Button(new Rect(x + 130, y, 120, height), new GUIContent("Zoom in"), buttonStyle))
            {
                UnityEngine.Camera.main.orthographicSize = 2f; // Adjust the zoom level as needed
            }
            y += spacing;
            if (GUI.Button(new Rect(x + 130, y, 120, height), new GUIContent("Zoom back"), buttonStyle))
            {
                UnityEngine.Camera.main.orthographicSize = 4.125f; // Adjust the zoom level as needed
            }
        }
        public void CheckLocation(string location)
        {
            if (!connected)
            {
                LoggerInstance.Msg($"Not connected, skipping location check for: {location}");
                return;
            }
            LoggerInstance.Msg($"Checking location for research: {location}");
            var check = "";
            switch (location)
            {
                case "MASS_PRODUCTION_I_NAME":
                    check = "Research Mass Production I";
                    break;
                case "MASS_PRODUCTION_II_NAME":
                    check = "Research Mass Production II";
                    break;
                case "JETPACK_I_NAME":
                    check = "Research Jetpack";
                    break;
                case "3D_PRINTING_I_NAME":
                    check = "Research Explorer Needs I";
                    break;
                case "AUTOMATION_I_NAME":
                    check = "Research Automation I";
                    break;
                case "AUTOMATION_II_NAME":
                    check = "Research Automation II";
                    break;
                case "CONTAINERS_I_NAME":
                    check = "Research Deposits I";
                    break;
                case "CONTAINERS_II_NAME":
                    check = "Research Deposits II";
                    break;
                case "ENERGY_RIFLE_I_NAME":
                    check = "Research Energy Rifle";
                    break;
                case "EXPLORER_NEEDS_II_NAME":
                    check = "Research Explorer Needs II";
                    break;
                case "ADVANCED_PRODUCTION_I_NAME":
                    check = "Research Advanced Production I";
                    break;
                case "FARMING_I_NAME":
                    check = "Research Farming I";
                    break;
                case "FARMING_II_NAME":
                    check = "Research Farming II";
                    break;
                case "ADVANCED_PRODUCTION_II_NAME":
                    check = "Research Advanced Production II";
                    break;
                case "RANCHING_I_NAME":
                    check = "Research Ranching I";
                    break;
                case "MODULES_I_NAME":
                    check = "Research Modules I";
                    break;
                case "RANCHING_II_NAME":
                    check = "Research Ranching II";
                    break;
                case "RANCHING_III_NAME":
                    check = "Research Ranching III";
                    break;
                case "EXPLORER_NEEDS_III_NAME":
                    check = "Research Explorer Needs III";
                    break;
                case "POWER_III_NAME":
                    check = "Research Power III";
                    break;
                case "ADVANCED_PRODUCTION_III_NAME":
                    check = "Research Advanced Production III";
                    break;
                case "LIQUIDS_I_NAME":
                    check = "Research Liquids I";
                    break;
                case "COMPLEX_PRODUCTION_I_NAME":
                    check = "Research Complex Production I";
                    break;
                case "FARMING_III_NAME":
                    check = "Research Farming III";
                    break;
                case "SHIELD_ARMOR_NAME":
                    check = "Research Suit Armor";
                    break;
                case "EXPLORER_NEEDS_IV_NAME":
                    check = "Research Explorer Needs IV";
                    break;
                case "SUPERHARD_MINERALS_NAME":
                    check = "Research Superhard Minerals";
                    break;
                case "COMPLEX_PRODUCTION_II_NAME":
                    check = "Research Complex Production II";
                    break;
                case "GLASS_WORKS_NAME":
                    check = "Research Glass Works";
                    break;
                case "SUPERCOMPUTER_RESEARCH_NAME":
                    check = "Research Supercomputer";
                    break;
                case "DRONES_NAME":
                    check = "Research Mass Transport";
                    break;
                case "NUCLEAR_TECH_NAME":
                    check = "Research Nuclear Tech";
                    break;
                case "HYPERCOMPUTER_NAME":
                    check = "Research Hypercomputer";
                    break;
                default:
                    LoggerInstance.Msg($"Unknown location checked: {location}");
                    break;
            }
            if (check != "")
            {
                archipelagoSession.Locations.CompleteLocationChecks(LocationIDs[check]);
                LoggerInstance.Msg($"Checked location: {check} (ID: {LocationIDs[check]})");
            }
        }
        public static MethodInfo GetMethod(string MethodName, BindingFlags bindingAttributes = BindingFlags.NonPublic | BindingFlags.Static)
        {
            StackTrace stackTrace = new StackTrace();
            Type callingType = stackTrace.GetFrame(1).GetMethod().DeclaringType;
            return callingType.GetMethod(MethodName, bindingAttributes);
        }
    }

    public class ResearchDescriptor
    {
        public string researchName;
    }
}