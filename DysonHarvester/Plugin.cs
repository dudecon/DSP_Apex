using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using DspApex.Common;

namespace DysonHarvester
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

            Logger.LogInfo(
                $"{PluginInfo.PLUGIN_NAME} harmony patches applied ({patchedTypes} types ok, {failedTypes} failed).");
        }
    }

    public static class PluginInfo
    {
        public const string PLUGIN_GUID = "com.paulspooner.dsp.dysonharvester";
        public const string PLUGIN_NAME = "DysonHarvester";
        public const string PLUGIN_VERSION = "0.1.0";
    }
}