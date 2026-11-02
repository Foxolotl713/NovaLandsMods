using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Harmony;
using HarmonyLib;
using MelonLoader;
using MelonLoader.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using static Il2CppSystem.Xml.XmlWellFormedWriter.AttributeValueCache;

[assembly: MelonInfo(typeof(NovaLandsArchipelago.Core), "NovaLandsArchipelago", "0.2.0", "Gott", null)]
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
        public static HarmonyLib.Harmony harmony;
        public Dictionary<string, int> LocationIDs = new()
        {
            {"Research Mass Production I", 101},
            {"Research Explorer Needs I", 102},
            {"Research Automation I", 103},
            {"Research Deposits I", 104},
            {"Research Jetpack", 105},
            {"Research Farming I", 106},
            {"Research Power I", 107},
            {"Research Automation II", 108},
            {"Research Energy Rifle", 109},
            {"Research Ranching I", 110},
            {"Research Power II", 111},
            {"Research Mass Production II", 112},
            {"Research Deposits II", 113},
            {"Research Suit Armor", 114},
            {"Research Farming II", 115},
            {"Research Explorer Needs III", 116},
            {"Research Advanced Production I", 117},
            {"Research Explorer Needs II", 118},
            {"Research Ranching II", 119},
            {"Research Overclocking I", 120},
            {"Research Advanced Production II", 121},
            {"Research Modules I", 122},
            {"Research Farming III", 123},
            {"Research Advanced Production III", 124},
            {"Research Explorer Needs IV", 125},
            {"Research Ranching III", 126},
            {"Research Power III", 127},
            {"Research Complex Production I", 128},
            {"Research Liquids I", 129},
            {"Research Overclocking II", 130},
            {"Research Mass Transport", 131},
            {"Research Glass Works", 132},
            {"Research Complex Production II", 133},
            {"Research Superhard Minerals", 134},
            {"Research Supercomputer", 135},
            {"Research Nuclear Tech", 136},
            {"Research Hypercomputer", 137},
            {"Moschillar", 201},
            {"Drameleon", 202},
            {"Tunasa", 203},
            {"Museum: 1 Grass Dioramas", 311},
            {"Museum: 2 Grass Dioramas", 312},
            {"Museum: 3 Grass Dioramas", 313},
            {"Museum: 4 Grass Dioramas", 314},
            {"Museum: 5 Grass Dioramas", 315},
            {"Museum: 1 Rock Dioramas", 321},
            {"Museum: 2 Rock Dioramas", 322},
            {"Museum: 3 Rock Dioramas", 323},
            {"Museum: 4 Rock Dioramas", 324},
            {"Museum: 5 Rock Dioramas", 325},
            {"Museum: 1 Desert Dioramas", 331},
            {"Museum: 2 Desert Dioramas", 332},
            {"Museum: 3 Desert Dioramas", 333},
            {"Museum: 4 Desert Dioramas", 334},
            {"Museum: 5 Desert Dioramas", 335},
            {"Museum: 1 Sea Dioramas", 341},
            {"Museum: 2 Sea Dioramas", 342},
            {"Museum: 3 Sea Dioramas", 343},
            {"Museum: 4 Sea Dioramas", 344},
            {"Museum: 5 Sea Dioramas", 345},
            {"Museum: 1 Forest Dioramas", 351},
            {"Museum: 2 Forest Dioramas", 352},
            {"Museum: 3 Forest Dioramas", 353},
            {"Museum: 4 Forest Dioramas", 354},
            {"Museum: 5 Forest Dioramas", 355},
            {"Museum: 1 Snow Dioramas", 361},
            {"Museum: 2 Snow Dioramas", 362},
            {"Museum: 3 Snow Dioramas", 363},
            {"Museum: 4 Snow Dioramas", 364},
            {"Museum: 5 Snow Dioramas", 365},
            {"Museum: 1 Metallic Dioramas", 371},
            {"Museum: 2 Metallic Dioramas", 372},
            {"Museum: 3 Metallic Dioramas", 373},
            {"Museum: 4 Metallic Dioramas", 374},
            {"Museum: 5 Metallic Dioramas", 375},
            {"Museum: 1 Behemittium Dioramas", 381},
            {"Museum: 2 Behemittium Dioramas", 382},
            {"Museum: 3 Behemittium Dioramas", 383},
            {"Museum: 4 Behemittium Dioramas", 384},
            {"Museum: 5 Behemittium Dioramas", 385},
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
        public int goal;
        public float timeleft;
        public string currentTrap;
        public int museumGoal;
        private enum FocusField { None, Server, Name, Password }
        private FocusField focused = FocusField.None;
        private void DrawMenu()
        {
            timeleft -= Time.deltaTime;
            if (timeleft <= 0)
            {
                timeleft = 1000f; // Reset the timer to 10 seconds
                currentTrap="None";
                Time.timeScale = 1f; // Reset the time scale to normal
                foreach (var cam in UnityEngine.Camera.allCameras)
                {
                    if (cam != UnityEngine.Camera.main)
                    {
                        cam.orthographic = true;
                        cam.orthographicSize = 4.125f;
                    }
                }
            }
            // Allow drawing UI even when connected so we can provide a test button for research purchase.
            // Layout values
            int x = 10;
            int y = 140;
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
                    // Must go BEFORE a successful connection attempt
                    session.Items.ItemReceived += (receivedItemsHelper) => {
                        var itemReceivedName = receivedItemsHelper.PeekItem().ItemName;
                        if (itemReceivedName.ToString().EndsWith("Trap"))
                        {
                            timeleft = 40f;
                            switch (itemReceivedName.ToString())
                            {
                                case "Slow Trap":
                                    timeleft = 8f;
                                    currentTrap = "Slow Trap";
                                    Time.timeScale = 0.2f; // Slow down time
                                    break;
                                case "Fast Trap":
                                    timeleft = 400f;
                                    currentTrap = "Fast Trap";
                                    Time.timeScale = 10f; // Speed up time
                                    break;
                                case "Zoom In Trap":
                                    currentTrap = "Zoom In Trap";
                                    foreach (var cam in UnityEngine.Camera.allCameras)
                                    {
                                        if (cam != UnityEngine.Camera.main)
                                        {
                                            cam.orthographic = true;
                                            cam.orthographicSize = 2f; // Zoom in
                                        }
                                    }
                                    break;
                                case "Zoom Out Trap":
                                    currentTrap = "Zoom Out Trap";
                                    foreach (var cam in UnityEngine.Camera.allCameras)
                                    {
                                        if (cam != UnityEngine.Camera.main)
                                        {
                                            cam.orthographic = true;
                                            cam.orthographicSize = 10f; // Zoom out
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Patches.researches.Add(itemReceivedName.ToString());
                        }
                        receivedItemsHelper.DequeueItem();
                    };

                    // Call TryConnectAndLogin with an empty string[] for the fifth parameter and the password as the sixth
                    result = session.TryConnectAndLogin("Nova Lands", PlayerName.Value, ItemsHandlingFlags.AllItems, version, password: Password.Value);
                    if (result.Successful)
                    {
                        LoggerInstance.Msg("Successfully connected to Archipelago server!");
                        connected = true;
                        // keep a reference to the session (stored as object so we can reflectively call send methods)
                        archipelagoSession = session;
                        LoginSuccessful Result = (LoginSuccessful)result;
                        goal = int.Parse(Result.SlotData["goal"].ToString());
                        museumGoal = int.Parse(Result.SlotData["museum_diorama_count"].ToString());
                    }
                    else
                    {
                        LoggerInstance.Error($"Failed to connect: {result}");
                    }
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
                case "Moschillar":
                    check = "Moschillar";
                    break;
                case "Drameleon":
                    check = "Drameleon";
                    break;
                case "Tunasa":
                    check = "Tunasa";
                    break;
                case "POWER_I_NAME":
                    check = "Research Power I";
                    break;
                case "POWER_II_NAME":
                    check = "Research Power II";
                    break;
                case "OVERCLOCKIN_I_NAME":
                    check = "Research Overclocking I";
                    break;
                case "OVERCLOCKIN_II_NAME":
                    check = "Research Overclocking II";
                    break;
                case "MG1":
                    check = "Museum: 1 Grass Dioramas";
                    break;
                case "MG2":
                    check = "Museum: 2 Grass Dioramas";
                    break;
                case "MG3":
                    check = "Museum: 3 Grass Dioramas";
                    break;
                case "MG4":
                    check = "Museum: 4 Grass Dioramas";
                    break;
                case "MG5":
                    check = "Museum: 5 Grass Dioramas";
                    break;
                case "MR1":
                    check = "Museum: 1 Rock Dioramas";
                    break;
                case "MR2":
                    check = "Museum: 2 Rock Dioramas";
                    break;
                case "MR3":
                    check = "Museum: 3 Rock Dioramas";
                    break;
                case "MR4":
                    check = "Museum: 4 Rock Dioramas";
                    break;
                case "MR5":
                    check = "Museum: 5 Rock Dioramas";
                    break;
                case "MD1":
                    check = "Museum: 1 Desert Dioramas";
                    break;
                case "MD2":
                    check = "Museum: 2 Desert Dioramas";
                    break;
                case "MD3":
                    check = "Museum: 3 Desert Dioramas";
                    break;
                case "MD4":
                    check = "Museum: 4 Desert Dioramas";
                    break;
                case "MD5":
                    check = "Museum: 5 Desert Dioramas";
                    break;
                case "MS1":
                    check = "Museum: 1 Sea Dioramas";
                    break;
                case "MS2":
                    check = "Museum: 2 Sea Dioramas";
                    break;
                case "MS3":
                    check = "Museum: 3 Sea Dioramas";
                    break;
                case "MS4":
                    check = "Museum: 4 Sea Dioramas";
                    break;
                case "MS5":
                    check = "Museum: 5 Sea Dioramas";
                    break;
                case "MF1":
                    check = "Museum: 1 Forest Dioramas";
                    break;
                case "MF2":
                    check = "Museum: 2 Forest Dioramas";
                    break;
                case "MF3":
                    check = "Museum: 3 Forest Dioramas";
                    break;
                case "MF4":
                    check = "Museum: 4 Forest Dioramas";
                    break;
                case "MF5":
                    check = "Museum: 5 Forest Dioramas";
                    break;
                case "MSn1":
                    check = "Museum: 1 Snow Dioramas";
                    break;
                case "MSn2":
                    check = "Museum: 2 Snow Dioramas";
                    break;
                case "MSn3":
                    check = "Museum: 3 Snow Dioramas";
                    break;
                case "MSn4":
                    check = "Museum: 4 Snow Dioramas";
                    break;
                case "MSn5":
                    check = "Museum: 5 Snow Dioramas";
                    break;
                case "MM1":
                    check = "Museum: 1 Metallic Dioramas";
                    break;
                case "MM2":
                    check = "Museum: 2 Metallic Dioramas";
                    break;
                case "MM3":
                    check = "Museum: 3 Metallic Dioramas";
                    break;
                case "MM4":
                    check = "Museum: 4 Metallic Dioramas";
                    break;
                case "MM5":
                    check = "Museum: 5 Metallic Dioramas";
                    break;
                case "MB1":
                    check = "Museum: 1 Behemittium Dioramas";
                    break;
                case "MB2":
                    check = "Museum: 2 Behemittium Dioramas";
                    break;
                case "MB3":
                    check = "Museum: 3 Behemittium Dioramas";
                    break;
                case "MB4":
                    check = "Museum: 4 Behemittium Dioramas";
                    break;
                case "MB5":
                    check = "Museum: 5 Behemittium Dioramas";
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
        public void Goal(int reason)
        {
            if (!connected)
            {
                LoggerInstance.Msg($"Not connected, skipping goal update for: {goal}");
                return;
            }
            if (goal == reason)
            {
                archipelagoSession.SetGoalAchieved();
                LoggerInstance.Msg($"Goal achieved!");
            }
        }
        public static MethodInfo GetMethod(string MethodName, BindingFlags bindingAttributes = BindingFlags.NonPublic | BindingFlags.Static)
        {
            StackTrace stackTrace = new StackTrace();
            Type callingType = stackTrace.GetFrame(1).GetMethod().DeclaringType;
            return callingType.GetMethod(MethodName, bindingAttributes);
        }
    }
}