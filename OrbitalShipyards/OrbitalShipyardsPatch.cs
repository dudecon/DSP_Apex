using HarmonyLib;

namespace OrbitalShipyards
{
    [HarmonyPatch(typeof(DysonSphere), nameof(DysonSphere.RocketGameTick))]
    internal static class OrbitalShipyardsRocketPatch
    {
        static void Postfix(DysonSphere __instance)
        {
            OrbitalShipyardsRuntime.BatchConstructRockets(__instance);
        }
    }

    [HarmonyPatch(typeof(DysonSphere), nameof(DysonSphere.BeforeGameTick))]
    internal static class OrbitalShipyardsRepairPatch
    {
        static void Postfix(DysonSphere __instance, long gameTick)
        {
            if (gameTick % 300 != 0 || __instance?.rocketPool == null)
                return;

            int damaged = 0;
            for (int i = 1; i < __instance.rocketCursor; i++)
            {
                DysonRocket rocket = __instance.rocketPool[i];
                if (rocket.id > 0 && rocket.nodeId == 0)
                    damaged++;
            }

            OrbitalShipyardsRuntime.TickRepair(damaged);
        }
    }
}