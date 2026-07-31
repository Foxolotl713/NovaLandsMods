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
        public static Type ResearchesList = GetResearchesList();
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
        private static Type GetResearchesList()
        {
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == "Assembly-CSharp");
            if (assembly == null)
            {
                throw new DllNotFoundException($"Could not locate assembly: \"Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\"");
            }
            if (assembly.GetType("Il2Cpp.ResearchesList") != null)
            {
                return assembly.GetType("Il2Cpp.ResearchesList");
            }
            throw new NullReferenceException("Could not locate type: \"ResearchesList\"");
        }

        public static unsafe void Init()
        {

            Core.harmony.Patch(typeof(Text).GetMethod("set_text"), new HarmonyMethod(Core.GetMethod(nameof(TextPatch))));
            Core.harmony.Patch(typeof(TextMesh).GetMethod("set_text"), new HarmonyMethod(Core.GetMethod(nameof(TextPatch))));
            Core.harmony.Patch(TMP_Text.GetMethod("set_text"), new HarmonyMethod(Core.GetMethod(nameof(TextPatch))));
            Core.harmony.Patch(ResearchesList.GetMethod("OnResearched"), new HarmonyMethod(Core.GetMethod(nameof(ResearchesListPatch))));
        }

        internal static void TextPatch(ref string value)
        {
            if (replacements.ContainsKey(value))
            {
                value = replacements[value];
            }
        }
        internal static void ResearchesListPatch(ref ResearchDescriptor descriptor)
        {
            Core.Instance.LoggerInstance.Msg("a");
            var descriptorType = AccessTools.TypeByName("ResearchDescriptor") ?? AccessTools.TypeByName("ResearchesList+ResearchDescriptor");
            object desc = null;
            if (descriptorType != null)
            {
                try
                {
                    desc = Activator.CreateInstance(descriptorType);
                    var field = descriptorType.GetField("researchName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (field != null)
                        field.SetValue(desc, "MASS_PRODUCTION_I_NAME"); // set on created instance
                    else
                    {
                        var prop = descriptorType.GetProperty("researchName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        if (prop != null)
                            prop.SetValue(desc, "MASS_PRODUCTION_I_NAME");
                    }
                }
                catch (Exception ex)
                {
                    Core.Instance.LoggerInstance.Msg($"Failed to create Il2Cpp descriptor instance: {ex}");
                    desc = null;
                }
            }

            if (desc != null)
            {
                // explicit cast required
                descriptor = (ResearchDescriptor)desc;
            }
        }
    }
}