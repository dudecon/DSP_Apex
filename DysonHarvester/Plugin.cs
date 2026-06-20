using BepInEx;

namespace DysonHarvester
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo("DysonHarvester placeholder loaded. Real implementation coming later!");
        }
    }

    public static class PluginInfo
    {
        public const string PLUGIN_GUID = "com.paulspooner.dsp.dysonharvester";
        public const string PLUGIN_NAME = "DysonHarvester";
        public const string PLUGIN_VERSION = "0.0.1";
    }
}
