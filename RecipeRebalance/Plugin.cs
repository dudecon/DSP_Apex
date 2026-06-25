using System;
using BepInEx;
using BepInEx.Logging;
using DspApex.Common;
using HarmonyLib;

namespace RecipeRebalance
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;
            Logger.LogInfo($"{PluginInfo.PLUGIN_NAME} v{PluginInfo.PLUGIN_VERSION} loading...");

            var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            int patchedTypes = 0;
            int failedTypes = 0;

            foreach (var type in typeof(Plugin).Assembly.GetTypes())
            {
                if (!HarmonyBootstrap.HasHarmonyPatches(type))
                    continue;

                try
                {
                    harmony.CreateClassProcessor(type).Patch();
                    patchedTypes++;
                }
                catch (Exception ex)
                {
                    failedTypes++;
                    Logger.LogError($"Failed to patch {type.FullName}: {ex.Message}");
                }
            }

            int methodCount = 0;
            foreach (var _ in harmony.GetPatchedMethods())
                methodCount++;

            Logger.LogInfo(
                $"{PluginInfo.PLUGIN_NAME} harmony patches applied ({methodCount} methods, {patchedTypes} types ok, {failedTypes} types failed).");
        }

    }

    public static class PluginInfo
    {
        public const string PLUGIN_GUID = "com.paulspooner.dsp.reciperebalance";
        public const string PLUGIN_NAME = "RecipeRebalance";
        public const string PLUGIN_VERSION = "1.0.0";
    }
}