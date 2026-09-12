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

[assembly: MelonInfo(typeof(NovaLandsMod.Core), "", "", "Gott", null)]
[assembly: MelonGame("BEHEMUTT", "Nova Lands")]

namespace NovaLandsMod
{
    public class Core : MelonMod
    {
        public static Core Instance;
        public static HarmonyLib.Harmony harmony;
        public override void OnInitializeMelon()
        {
            Instance = this;
            harmony = this.HarmonyInstance;
            LoggerInstance.Msg("Initialized.");
            MelonEvents.OnGUI.Subscribe(DrawMenu, 100); // The higher the value, the lower the priority.
            Patches.Init();
        }


        private void DrawMenu()
        {
        }
        
        public static MethodInfo GetMethod(string MethodName, BindingFlags bindingAttributes = BindingFlags.NonPublic | BindingFlags.Static)
        {
            StackTrace stackTrace = new StackTrace();
            Type callingType = stackTrace.GetFrame(1).GetMethod().DeclaringType;
            return callingType.GetMethod(MethodName, bindingAttributes);
        }
    }
}