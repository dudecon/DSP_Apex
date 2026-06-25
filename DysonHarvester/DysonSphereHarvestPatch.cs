using HarmonyLib;

namespace DysonHarvester
{
    [HarmonyPatch(typeof(DysonSphere), nameof(DysonSphere.GameTick))]
    internal static class DysonSphereHarvestPatch
    {
        static void Postfix(DysonSphere __instance, long gameTick)
        {
            if (gameTick % HarvesterYieldLogic.TicksPerHarvestPulse != 0)
                return;

            HarvesterService.ApplyHarvestPulse(__instance);
        }
    }
}