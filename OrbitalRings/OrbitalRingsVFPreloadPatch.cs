using HarmonyLib;

namespace OrbitalRings
{
    [HarmonyPatch(typeof(VFPreload), "InvokeOnLoadWorkEnded")]
    internal static class OrbitalRingsVFPreloadPatch
    {
        static void Postfix()
        {
            OrbitalRingsBootstrap.RegisterProtos();
        }
    }
}