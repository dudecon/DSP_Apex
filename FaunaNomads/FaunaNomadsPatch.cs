using HarmonyLib;

namespace FaunaNomads
{
    [HarmonyPatch(typeof(DysonSphere), nameof(DysonSphere.BeforeGameTick))]
    internal static class FaunaNomadsPatch
    {
        static void Postfix(DysonSphere __instance, long gameTick)
        {
            if (gameTick % 600 != 0)
                return;

            int deficit = FaunaNomadsContext.ScanResourceDeficit();
            FaunaNomadsRuntime.SyncAgentsFromDeficit(deficit);
            FaunaNomadsRuntime.TickHerds();
        }
    }
}