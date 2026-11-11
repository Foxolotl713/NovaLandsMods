using HarmonyLib;
using Il2Cpp;
using Il2CppTMPro;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using static Il2Cpp.DialoguePopup;

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
        public static Type PlayerUpgradesList = assembly.GetType("Il2Cpp.PlayerUpgradesList");
        public static Type GemOverclockBuilding = assembly.GetType("Il2Cpp.GemOverclockBuilding");
        public static Type WorldArea = assembly.GetType("Il2Cpp.WorldArea");
        public static Type CheatsPopup = assembly.GetType("Il2Cpp.CheatsPopup");
        public static List<string> researches = new();
        public static Dictionary<string, string> researchReplacements = new()
        {
            {"MASS_PRODUCTION_I_NAME", "mass production i"},
            {"MASS_PRODUCTION_II_NAME", "mass production ii"},
            {"JETPACK_I_NAME", "jetpack"},
            {"3D_PRINTING_I_NAME", "explorer needs i"},
            {"AUTOMATION_I_NAME", "automation i"},
            {"AUTOMATION_II_NAME", "automation ii"},
            {"CONTAINERS_I_NAME", "deposits i"},
            {"CONTAINERS_II_NAME", "deposits ii"},
            {"ENERGY_RIFLE_I_NAME", "energy rifle"},
            {"EXPLORER_NEEDS_II_NAME", "explorer needs ii"},
            {"ADVANCED_PRODUCTION_I_NAME", "advanced production i"},
            {"FARMING_I_NAME", "farming i"},
            {"FARMING_II_NAME", "farming ii"},
            {"ADVANCED_PRODUCTION_II_NAME", "advanced production ii"},
            {"RANCHING_I_NAME", "ranching i"},
            {"MODULES_I_NAME", "modules i"},
            {"RANCHING_II_NAME", "ranching ii"},
            {"RANCHING_III_NAME", "ranching iii"},
            {"EXPLORER_NEEDS_III_NAME", "explorer needs iii"},
            {"POWER_III_NAME", "power iii"},
            {"POWER_I_NAME", "power i"},
            {"POWER_II_NAME", "power ii"},
            {"OVERCLOCKIN_I_NAME", "overclocking i"},
            {"OVERCLOCKIN_II_NAME", "overclocking ii"},
            {"ADVANCED_PRODUCTION_III_NAME", "advanced production iii"},
            {"LIQUIDS_I_NAME", "liquids i"},
            {"COMPLEX_PRODUCTION_I_NAME", "complex production i"},
            {"FARMING_III_NAME", "farming iii"},
            {"SHIELD_ARMOR_NAME", "suit armor"},
            {"EXPLORER_NEEDS_IV_NAME", "explorer needs iv"},
            {"SUPERHARD_MINERALS_NAME", "superhard minerals"},
            {"COMPLEX_PRODUCTION_II_NAME", "complex production ii"},
            {"GLASS_WORKS_NAME", "glass works"},
            {"SUPERCOMPUTER_RESEARCH_NAME", "supercomputer"},
            {"DRONES_NAME", "mass transport"},
            {"NUCLEAR_TECH_NAME", "nuclear tech"},
            {"HYPERCOMPUTER_NAME", "hypercomputer"}
        };
        public static Dictionary<string, string> replacements = new()
        {
            /*{"OPTIONS", "Wanna change something?"},
            {"EXIT", "DON'T LEAVE ME!!" },
            {"PLAY", "Get your head in the game!" },
            {"EXTRAS", "Cool Skins and stuff" },
            {"CREDITS", "Meet the team!" }*/
        };
        public static Dictionary<string, int> biomeLights = new()
        {
            {"TestGrass", 0},
            {"TestRocks", 0},
            {"Desert", 0},
            {"TestSwamp", 0},
            {"ForestBiome", 0},
            {"Snow", 0},
            {"Vulcanic", 0},
            {"RadioactiveBiome", 0},
        };
        public static Dictionary<string, string> biomes = new()
        {
            {"TestGrass", "MG"},
            {"TestRocks", "MR"},
            {"Desert", "MD"},
            {"TestSwamp", "MS"},
            {"ForestBiome", "MF"},
            {"Snow", "MSn"},
            {"Vulcanic", "MM"},
            {"RadioactiveBiome", "MB"},
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

            Core.harmony.Patch(typeof(Text).GetMethod("set_text"), new HarmonyMethod(Core.GetMethod(nameof(TextPatch))));
            Core.harmony.Patch(typeof(TextMesh).GetMethod("set_text"), new HarmonyMethod(Core.GetMethod(nameof(TextPatch))));
            Core.harmony.Patch(TMP_Text.GetMethod("set_text"), new HarmonyMethod(Core.GetMethod(nameof(TextPatch))));
            Core.harmony.Patch(ResearchPopup.GetMethod("OnClickResearch"), new HarmonyMethod(Core.GetMethod(nameof(ResearchPopupPatch))));
            Core.harmony.Patch(ResearchPopupTechCard.GetMethod("SetState"), new HarmonyMethod(Core.GetMethod(nameof(ResearchPopupTechCardPatch))));
            Core.harmony.Patch(MoschillarBoss.GetMethod("SpawnDeathLoot"), new HarmonyMethod(Core.GetMethod(nameof(MoschillarBossPatch))));
            Core.harmony.Patch(DrameleonBoss.GetMethod("SpawnDeathLoot"), new HarmonyMethod(Core.GetMethod(nameof(DrameleonBossPatch))));
            Core.harmony.Patch(TunasaBoss.GetMethod("SpawnDeathLoot"), new HarmonyMethod(Core.GetMethod(nameof(TunasaBossPatch))));
            Core.harmony.Patch(MuseumAreaController.GetMethod("UpdateDioramaState", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(Il2Cpp.WorldBiome), typeof(int), typeof(bool) }, null), new HarmonyMethod(Core.GetMethod(nameof(MuseumAreaControllerPatch))));
            Core.harmony.Patch(PlayerUpgradesList.GetMethod("SetTechState"), new HarmonyMethod(Core.GetMethod(nameof(PlayerUpgradesListPatch))));
            //Core.harmony.Patch(GemOverclockBuilding.GetMethod("GetBonusPerGroup"), new HarmonyMethod(Core.GetMethod(nameof(GemOverclockBuildingPatch))));
            Core.harmony.Patch(WorldArea.GetMethod("HasBotAntenna"), new HarmonyMethod(Core.GetMethod(nameof(WorldAreaPatch))));
            Core.harmony.Patch(CheatsPopup.GetMethod("CheatSpawnSingleResource"), new HarmonyMethod(Core.GetMethod(nameof(CheatsPopupPatch))));
        }
        internal static void TextPatch(ref string value)
        {
            if (researchReplacements.Values.Contains(value.ToLower()))
            {
                if (researches.Contains(value.ToLower()))
                {
                    value = $"<color=green>{value}</color>";
                }
                else
                {
                    value = $"<color=red>{value}</color>";
                }
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
                state = Il2Cpp.ResearchState.UNLOCKED;
            }
        }
        internal static void PlayerUpgradesListPatch(ref Il2Cpp.ResearchState state)
        {
            Core.Instance.LoggerInstance.Msg($"Original PlayerUpgradesList Patch result: {state}");
            state = Il2Cpp.ResearchState.UNLOCKED;
        }
        internal static void GemOverclockBuildingPatch(ref float __result)
        {
            Core.Instance.LoggerInstance.Msg($"Original GemOverclockBuilding Patch result: {__result}");
            __result *= 10f;
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
        internal static void MuseumAreaControllerPatch(ref Il2Cpp.WorldBiome biome, ref int numberOfLights)
        {
            biomeLights[biome.BiomeType.ToString()] = numberOfLights;
            if (biomeLights[biome.BiomeType.ToString()] > 0)
            {
                Core.Instance.CheckLocation(biomes[biome.BiomeType.ToString()] + "1");
            }
            if (biomeLights[biome.BiomeType.ToString()] > 1)
            {
                Core.Instance.CheckLocation(biomes[biome.BiomeType.ToString()] + "2");
            }
            if (biomeLights[biome.BiomeType.ToString()] > 2)
            {
                Core.Instance.CheckLocation(biomes[biome.BiomeType.ToString()] + "3");
            }
            if (biomeLights[biome.BiomeType.ToString()] > 3)
            {
                Core.Instance.CheckLocation(biomes[biome.BiomeType.ToString()] + "4");
            }
            if (biomeLights[biome.BiomeType.ToString()] > 4)
            {
                Core.Instance.CheckLocation(biomes[biome.BiomeType.ToString()] + "5");
            }
            int sum = 0;
            for (int i = 0; i < 8; i++)
            {
                sum += biomeLights[biomeLights.Keys.ElementAt(i)];
            }
            if (sum >= Core.Instance.museumGoal)
            {
                Core.Instance.Goal(1);
            }
        }
        internal static void WorldAreaPatch(ref bool __result)
        {
            __result = true;
        }
        internal static void CheatsPopupPatch(ref Il2Cpp.CheatsPopup __instance)
        { 
            __instance.RaiseCancellation();
        }
    }
}