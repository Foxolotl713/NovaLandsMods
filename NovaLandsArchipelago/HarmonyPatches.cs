using HarmonyLib;
using Il2Cpp;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace NovaLandsArchipelago
{
    internal static class Patches
    {
        public static Type TMP_Text = GetTMP_TextDynamic();
        public static Assembly assembly = GetAssembly();
        public static Type ResearchPopupTechCard = assembly.GetType("Il2Cpp.ResearchPopupTechCard");
        public static Type ResearchPopup = assembly.GetType("Il2Cpp.ResearchPopup");
        public static Type MoschillarBoss = assembly.GetType("Il2Cpp.MoschillarBoss");
        public static Type DrameleonBoss = assembly.GetType("Il2Cpp.DrameleonBoss");
        public static Type TunasaBoss = assembly.GetType("Il2Cpp.TunasaBoss");
        public static Type MuseumAreaController = assembly.GetType("Il2Cpp.MuseumAreaController");
        public static List<string> researches = new();
        public static Dictionary<string, string> researchReplacements = new()
        {
            {"MASS_PRODUCTION_I_NAME", "Mass Production I"},
            {"MASS_PRODUCTION_II_NAME", "Mass Production II"},
            {"JETPACK_I_NAME", "Jetpack"},
            {"3D_PRINTING_I_NAME", "Explorer Needs I"},
            {"AUTOMATION_I_NAME", "Automation I"},
            {"AUTOMATION_II_NAME", "Automation II"},
            {"CONTAINERS_I_NAME", "Deposits I"},
            {"CONTAINERS_II_NAME", "Deposits II"},
            {"ENERGY_RIFLE_I_NAME", "Energy Rifle"},
            {"EXPLORER_NEEDS_II_NAME", "Explorer Needs II"},
            {"ADVANCED_PRODUCTION_I_NAME", "Advanced Production I"},
            {"FARMING_I_NAME", "Farming I"},
            {"FARMING_II_NAME", "Farming II"},
            {"ADVANCED_PRODUCTION_II_NAME", "Advanced Production II"},
            {"RANCHING_I_NAME", "Ranching I"},
            {"MODULES_I_NAME", "Modules I"},
            {"RANCHING_II_NAME", "Ranching II"},
            {"RANCHING_III_NAME", "Ranching III"},
            {"EXPLORER_NEEDS_III_NAME", "Explorer Needs III"},
            {"POWER_III_NAME", "Power III"},
            {"ADVANCED_PRODUCTION_III_NAME", "Advanced Production III"},
            {"LIQUIDS_I_NAME", "Liquids I"},
            {"COMPLEX_PRODUCTION_I_NAME", "Complex Production I"},
            {"FARMING_III_NAME", "Farming III"},
            {"SHIELD_ARMOR_NAME", "Suit Armor"},
            {"EXPLORER_NEEDS_IV_NAME", "Explorer Needs IV"},
            {"SUPERHARD_MINERALS_NAME", "Superhard Minerals"},
            {"COMPLEX_PRODUCTION_II_NAME", "Complex Production II"},
            {"GLASS_WORKS_NAME", "Glass Works"},
            {"SUPERCOMPUTER_RESEARCH_NAME", "Supercomputer"},
            {"DRONES_NAME", "Mass Transport"},
            {"NUCLEAR_TECH_NAME", "Nuclear Tech"},
            {"HYPERCOMPUTER_NAME", "Hypercomputer"}
        };
        public static Dictionary<string, string> replacements = new()
        {
            /*{"OPTIONS", "Wanna change something?"},
            {"EXIT", "DON'T LEAVE ME!!" },
            {"PLAY", "Get your head in the game!" },
            {"EXTRAS", "Cool Skins and stuff" },
            {"CREDITS", "Meet the team!" }*/
        };
        private static Assembly GetAssembly()
        {
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == "Assembly-CSharp");
            if (assembly == null)
            {
                throw new DllNotFoundException($"Could not not not locate assembly: \"Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\"");
            }
            return assembly;
        }
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

            //Core.harmony.Patch(typeof(Text).GetMethod("set_text"), new HarmonyMethod(Core.GetMethod(nameof(TextPatch))));
            //Core.harmony.Patch(typeof(TextMesh).GetMethod("set_text"), new HarmonyMethod(Core.GetMethod(nameof(TextPatch))));
            //Core.harmony.Patch(TMP_Text.GetMethod("set_text"), new HarmonyMethod(Core.GetMethod(nameof(TextPatch))));
            Core.harmony.Patch(ResearchPopup.GetMethod("OnClickResearch"), new HarmonyMethod(Core.GetMethod(nameof(ResearchPopupPatch))));
            Core.harmony.Patch(ResearchPopupTechCard.GetMethod("SetState"), new HarmonyMethod(Core.GetMethod(nameof(ResearchPopupTechCardPatch))));
            Core.harmony.Patch(MoschillarBoss.GetMethod("SpawnDeathLoot"), new HarmonyMethod(Core.GetMethod(nameof(MoschillarBossPatch))));
            Core.harmony.Patch(DrameleonBoss.GetMethod("SpawnDeathLoot"), new HarmonyMethod(Core.GetMethod(nameof(DrameleonBossPatch))));
            Core.harmony.Patch(TunasaBoss.GetMethod("SpawnDeathLoot"), new HarmonyMethod(Core.GetMethod(nameof(TunasaBossPatch))));
            Core.harmony.Patch(MuseumAreaController.GetMethod("IsAllDioramasComplete"), new HarmonyMethod(Core.GetMethod(nameof(MuseumAreaControllerPatch))));
        }

        internal static void TextPatch(ref string value)
        {
            if (replacements.ContainsKey(value))
            {
                value = replacements[value];
            }
        }
        internal static bool ResearchPopupPatch(Il2Cpp.ResearchPopup __instance)
        {
            if (__instance == null)
            {
                throw new NullReferenceException("ResearchPopup instance is null");
            }
            if (__instance.currentSelectedResearchDescriptor == null)
            {
                throw new NullReferenceException("currentSelectedResearchDescriptor is null");
            }
            if (__instance.currentSelectedResearchDescriptor.ResearchName == null)
            {
                throw new NullReferenceException("ResearchName is null");
            }
            string name = __instance.currentSelectedResearchDescriptor.ResearchName;
            Core.Instance.CheckLocation(name);
            if (researches.Contains(researchReplacements[name]))
            {
                return true;
            }
            return false;
        }
        internal static void ResearchPopupTechCardPatch(ref Il2Cpp.ResearchState state, ref Il2Cpp.ResearchPopupTechCard __instance)
        {
            if (state == Il2Cpp.ResearchState.LOCKED)
            {
                state =Il2Cpp.ResearchState.UNLOCKED;
            }
        }
        internal static void MoschillarBossPatch()
        {
            Core.Instance.CheckLocation("Moschillar");
            Core.Instance.Goal(2);
        }
        internal static void DrameleonBossPatch()
        {
            Core.Instance.CheckLocation("Drameleon");
            Core.Instance.Goal(3);
        }
        internal static void TunasaBossPatch()
        {
            Core.Instance.CheckLocation("Tunasa");
            Core.Instance.Goal(4);
        }
        internal static void MuseumAreaControllerPatch(ref bool __result)
        {
            if (__result)
            {
                Core.Instance.Goal(1);
            }
        }
    }
}