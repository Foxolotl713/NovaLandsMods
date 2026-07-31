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

[assembly: MelonInfo(typeof(NovaLandsArchipelago.Core), "NovaLandsZoom", "1.0.1", "Gott", null)]
[assembly: MelonGame("BEHEMUTT", "Nova Lands")]

namespace NovaLandsArchipelago
{
    public class Core : MelonMod
    {
        public static Core Instance;
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
            Patches.Init();
        }
        private float zoomValue = 1f;
        private const float ZoomMin = -1f;
        private const float ZoomMax = 4f;
        private float lastZoomValue = 4.125f;

        private void DrawMenu()
        {
            int x = 10;
            int y = 10;
            int height = 22;
            int spacing = 28;
            GUIStyle buttonStyle = new(GUI.skin.button);
            y += spacing;

            // Label for slider
            GUI.Label(new Rect(x + 130, y, 120, height), $"Zoom: {zoomValue:0.00}");
            y += height;

            // Slider (adjusts orthographic size)
            float newZoom = GUI.HorizontalSlider(new Rect(x + 130, y, 120, height), zoomValue, ZoomMin, ZoomMax);

            // Apply change when slider moved
            if (Math.Abs(newZoom - zoomValue) > 0.001f)
            {
                zoomValue = newZoom;
                foreach (var cam in UnityEngine.Camera.allCameras)
                {
                    if(cam!=UnityEngine.Camera.main)
                    {
                        cam.orthographic = true;
                        cam.orthographicSize = (float)Math.Pow(4.125f, zoomValue);
                    }
                }
            }
            y += spacing;

            // Keep reset button
            if (GUI.Button(new Rect(x + 130, y, 120, height), new GUIContent("Reset zoom"), buttonStyle))
            {
                zoomValue = 1f;
                foreach (var cam in UnityEngine.Camera.allCameras)
                {
                    if(cam!=UnityEngine.Camera.main)
                    {
                        cam.orthographic = true;
                        cam.orthographicSize = 4.125f;
                    }
                }
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