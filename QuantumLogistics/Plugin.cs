using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using DspApex.Common;

namespace QuantumLogistics
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;

        private void Awake()
        {
            Log = Logger;
            Logger.LogInfo($"{PluginInfo.PLUGIN_NAME} v{PluginInfo.PLUGIN_VERSION} — Intra-system teleporters and interstellar stargates");

            var harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            int patched = 0;
            foreach (var type in typeof(Plugin).Assembly.GetTypes())
            {
                if (!HarmonyBootstrap.HasHarmonyPatches(type)) continue;
                try { harmony.CreateClassProcessor(type).Patch(); patched++; }
                catch (Exception ex) { Logger.LogError($"Patch failed {type.FullName}: {ex.Message}"); }
            }
            Logger.LogInfo($"{PluginInfo.PLUGIN_NAME} loaded ({patched} patch types).");
        }
    }

    public static class PluginInfo
    {
        public const string PLUGIN_GUID = "com.paulspooner.dsp.quantumlogistics";
        public const string PLUGIN_NAME = "QuantumLogistics";
        public const string PLUGIN_VERSION = "0.1.0";
    }
}
