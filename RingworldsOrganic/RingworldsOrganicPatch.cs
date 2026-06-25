using HarmonyLib;

namespace RingworldsOrganic
{
    [HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.BeforeGameTick))]
    internal static class RingworldsOrganicPatch
    {
        static void Postfix(PlanetFactory __instance)
        {
            RingworldsOrganicRuntime.TickBiomass(__instance);
        }
    }
}