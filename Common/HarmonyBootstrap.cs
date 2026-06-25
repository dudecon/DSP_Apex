using System;
using System.Reflection;
using HarmonyLib;

namespace DspApex.Common
{
    /// <summary>Shared Harmony patch discovery for all DSP Apex mod packs.</summary>
    public static class HarmonyBootstrap
    {
        public static bool HasHarmonyPatches(Type type)
        {
            if (type.GetCustomAttributes(typeof(HarmonyPatch), true).Length > 0)
                return true;

            const BindingFlags flags =
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static
                | BindingFlags.Instance | BindingFlags.DeclaredOnly;

            foreach (var method in type.GetMethods(flags))
            {
                if (method.GetCustomAttributes(typeof(HarmonyPatch), true).Length > 0)
                    return true;
            }

            return false;
        }
    }
}