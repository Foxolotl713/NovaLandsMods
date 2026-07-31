using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace NovaLandsArchipelago
{
    internal static class Patches
    {
        public static Type TMP_Text = GetTMP_TextDynamic();
        public static Dictionary<string, string> replacements = new()
        {
            /*{"OPTIONS", "Wanna change something?"},
            {"EXIT", "DON'T LEAVE ME!!" },
            {"PLAY", "Get your head in the game!" },
            {"EXTRAS", "Cool Skins and stuff" },
            {"CREDITS", "Meet the team!" }*/
        };

        private static Type GetTMP_TextDynamic()
        {
            Assembly textMeshProAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == "Unity.TextMeshPro");

            if (textMeshProAssembly == null)
            {
                throw new DllNotFoundException($"Could not locate assembly: \"Unity.TextMeshPro, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\"");
            }

            return textMeshProAssembly.GetType("Il2CppTMPro.TMP_Text") ?? textMeshProAssembly.GetType("TMPro.TMP_Text");
        }

        public static unsafe void Init()
        {

            Core.harmony.Patch(typeof(Text).GetMethod("set_text"), new HarmonyMethod(Core.GetMethod(nameof(TextPatch))));
            Core.harmony.Patch(typeof(TextMesh).GetMethod("set_text"), new HarmonyMethod(Core.GetMethod(nameof(TextPatch))));
            Core.harmony.Patch(TMP_Text.GetMethod("set_text"), new HarmonyMethod(Core.GetMethod(nameof(TextPatch))));
        }

        internal static void TextPatch(ref string value)
        {
            if (replacements.ContainsKey(value))
            {
                value = replacements[value];
            }
        }
    }
}