using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using HarmonyLib;
using MelonLoader;
using UnityEngine;
using MelonLoader.Utils;
using Il2Cpp;
using AwesomeNamespace;
using System;
using System.Collections.Generic;
using System.Reflection;

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
        public static bool connected = false;
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
        public HarmonyPatches HarmonyPatches = new();
        public override void OnInitializeMelon()
        {
            Instance = this;
            LoggerInstance.Msg("Initialized.");
            MelonEvents.OnGUI.Subscribe(DrawMenu, 100); // The higher the value, the lower the priority.
            ConnectionData = MelonPreferences.CreateCategory("ConnectionData");
            Server = ConnectionData.CreateEntry("Server", "archipelago.gg:40000");
            PlayerName = ConnectionData.CreateEntry("PlayerName", "Player1");
            Password = ConnectionData.CreateEntry("Password", "");
            HarmonyPatches.PatchAll();
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

            if (!connected || 1 == 1)
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
                    result = session.TryConnectAndLogin("Nova Lands", PlayerName.Value, ItemsHandlingFlags.AllItems, version, [], Password.Value);
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
                y += spacing;
                if (GUI.Button(new Rect(x, y, 120, height), new GUIContent("Research"), buttonStyle))
                {
                    // Obtain a live Il2Cpp ResearchesList instance (cannot be null or a managed object)
                    var researchesListType = AccessTools.TypeByName("ResearchesList");
                    if (researchesListType == null)
                    {
                        LoggerInstance.Msg("ResearchesList type not found in game assemblies.");
                        Instance.CheckLocation("MASS_PRODUCTION_I_NAME");
                        return;
                    }

                    var researchesListInstance = UnityEngine.Object.FindObjectOfType(researchesListType, false);
                    if (researchesListInstance == null)
                    {
                        LoggerInstance.Msg("No ResearchesList instance found in scene.");
                        Instance.CheckLocation("MASS_PRODUCTION_I_NAME");
                        return;
                    }

                    // Create the game's descriptor type (Il2Cpp-backed) instead of the local managed ResearchDescriptor.
                    var descriptorType = AccessTools.TypeByName("ResearchDescriptor") ?? AccessTools.TypeByName("ResearchesList+ResearchDescriptor");
                    object descriptor = null;
                    if (descriptorType != null)
                    {
                        try
                        {
                            descriptor = Activator.CreateInstance(descriptorType);
                            var field = descriptorType.GetField("researchName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                            if (field != null)
                                field.SetValue(descriptor, "MASS_PRODUCTION_I_NAME");
                            else
                            {
                                var prop = descriptorType.GetProperty("researchName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                                if (prop != null)
                                    prop.SetValue(descriptor, "MASS_PRODUCTION_I_NAME");
                            }
                        }
                        catch (Exception ex)
                        {
                            LoggerInstance.Msg($"Failed to create Il2Cpp descriptor instance: {ex}");
                            descriptor = null;
                        }
                    }

                    if (descriptor == null)
                    {
                        // Fallback to safe path to avoid Il2CppInterop exceptions.
                        LoggerInstance.Msg("Descriptor unavailable — calling CheckLocation directly.");
                        Instance.CheckLocation("MASS_PRODUCTION_I_NAME");
                        return;
                    }

                    // Call ReversePatch with a valid Il2Cpp __instance and Il2Cpp descriptor
                    ResearchesList_OnResearched.ReversePatch(researchesListInstance, descriptor);
                }
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
                case "JETPACK_I_NAME":
                    check = "Research Jetpack";
                    break;
                case "3D_PRINTING_I_NAME":
                    check = "Research Explorer Needs I";
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
    }

    public class ResearchDescriptor
    {
        public string researchName;
    }
}