using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace NovaLandsMod
{
    internal static class Patches
    {
        public static Assembly assembly = GetAssembly();

        private static Assembly GetAssembly()
        {
            Assembly assembly1 = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.GetName().Name == "Assembly-CSharp");
            if (assembly1 != null)
            {
                return assembly1;
            }
            throw new NullReferenceException("Could not locate main assembly");
        }

        public static unsafe void Init()
        {

        }
    }
}