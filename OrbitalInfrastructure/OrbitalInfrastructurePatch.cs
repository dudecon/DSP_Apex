using HarmonyLib;

namespace OrbitalInfrastructure
{
    [HarmonyPatch(typeof(VFPreload), "InvokeOnLoadWorkEnded")]
    internal static class OrbitalInfrastructurePatch
    {
        static void Postfix()
        {
            OrbitalInfrastructureBootstrap.RegisterProtos();
        }
    }
}