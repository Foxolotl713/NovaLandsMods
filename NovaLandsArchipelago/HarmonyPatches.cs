using HarmonyLib;
using System;
using System.Reflection;
using Il2Cpp;

namespace NovaLandsArchipelago
{
    public class HarmonyPatches
    {
        public static Core core = Core.Instance;
        public static string[] Types = ["ResearchesList", "ResearchesList"];
        public static string[] Methods = ["OnResearched", "AddExtraContentResearch"];
        public static Type[] classes = [typeof(ResearchesList_OnResearched), typeof(ResearchesList_SetTechState)];
        public static void PatchAll()
        {
            // Create a new Harmony instance with a unique ID.
            var harmony = new HarmonyLib.Harmony("gg.archipelago.novalands");
            core.LoggerInstance.Msg("Patching all methods...");

            for (int i = 0; i < Types.Length; i++)
            {
                try
                {
                    var type = AccessTools.TypeByName(Types.GetValue(i).ToString());
                    if (type == null)
                    {
                        core.LoggerInstance.Msg($"{Types.GetValue(i)} type not found.");
                        return;
                    }

                    // Try to locate the AddItem method (any overload named "AddItem")
                    MethodInfo method = null;
                    foreach (var m in type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        if (m.Name == Methods.GetValue(i).ToString())
                        {
                            method = m;
                            break;
                        }
                    }

                    if (method == null)
                    {
                        core.LoggerInstance.Msg($"{Methods.GetValue(i)} method not found on {Types.GetValue(i)}.");
                        return;
                    }

                    // Get the patch class Type from the array
                    var patchClassType = classes.GetValue(i) as Type;
                    if (patchClassType == null)
                    {
                        core.LoggerInstance.Msg($"Patch class for index {i} not found or not a Type.");
                        return;
                    }

                    // Resolve Prefix and Postfix methods by name
                    var prefixMethod = patchClassType.GetMethod("Prefix", BindingFlags.Static | BindingFlags.Public);
                    var postfixMethod = patchClassType.GetMethod("Postfix", BindingFlags.Static | BindingFlags.Public);
                    var reverseMethod = patchClassType.GetMethod("ReversePatch", BindingFlags.Static | BindingFlags.Public);

                    HarmonyMethod prefix = prefixMethod != null ? new HarmonyMethod(prefixMethod) : null;
                    HarmonyMethod postfix = postfixMethod != null ? new HarmonyMethod(postfixMethod) : null;
                    HarmonyMethod reverse = reverseMethod != null ? new HarmonyMethod(reverseMethod) : null;

                    harmony.Patch(method, prefix, postfix);
                    harmony.CreateReversePatcher(method, reverse).Patch();
                }
                catch (Exception ex)
                {
                    core.LoggerInstance.Msg($"Exception while patching {Methods.GetValue(i)} on {Types.GetValue(i)}: {ex}");
                }
            }
        }
    }

    public static class ResearchesList_OnResearched
    {
        // Prefix runs before ResearchesList.OnResearched.
        // Use generic parameter names since exact signature may vary; Harmony will bind parameters by name/type when possible.
        public static bool isReversePatch = false;
        public static bool Prefix(object __instance, object descriptor)
        {
            if (isReversePatch)
            {
                Core.Instance.LoggerInstance.Msg("ResearchesList.OnResearched Prefix called from ReversePatch on " + descriptor);
                return false; // Run original method for reverse patch
            }
            try
            {
                string researchName = null;
                if (descriptor != null)
                {
                    var t = descriptor.GetType();
                    // Try property
                    var prop = t.GetProperty("researchName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (prop != null)
                    { 
                        researchName = prop.GetValue(descriptor)?.ToString();
                    }
                    else
                    {
                        throw new Exception("researchName property not found on descriptor.");
                    }
                }
                Core.Instance.CheckLocation(researchName);
                return false; // Skip original method
            }
            catch (Exception ex)
            {
                Core.Instance.LoggerInstance.Msg($"ResearchesList.OnResearched Postfix reflection error: {ex}");
                return true; // Run original method if there's an error
            }
        }

        // Postfix runs after ResearchesList.OnResearched.
        // descriptor is typed as object because the concrete type may not be available at compile-time.
        // Use reflection to read the researchName member safely to avoid CS1061.
        public static void Postfix(object __instance, object descriptor)
        {
            isReversePatch = false;
        }

        public static void ReversePatch(object __instance, object descriptor)
        {
            isReversePatch = true;
            Core.Instance.LoggerInstance.Msg("ResearchesList.OnResearched ReversePatch called on " + descriptor);
        }
    }
    public static class ResearchesList_SetTechState
    {
        // Prefix runs before ResearchesList.SetTechState.
        public static void Prefix(object __instance, object research)
        {
            Core.Instance.LoggerInstance.Msg("ResearchesList.SetTechState Prefix called!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        // Postfix runs after ResearchesList.SetTechState.
        public static void Postfix(object __instance, object research)
        {
            Core.Instance.LoggerInstance.Msg("ResearchesList.SetTechState Postfix called on " + research);
        }
        public static void ReversePatch(object __instance, object research)
        {
            Core.Instance.LoggerInstance.Msg("ResearchesList.SetTechState ReversePatch called on " + research);
        }
    }
}