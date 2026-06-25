using HarmonyLib;

namespace RamscoopShips
{
    [HarmonyPatch(typeof(DysonSphere), nameof(DysonSphere.BeforeGameTick))]
    internal static class RamscoopShipsPatch
    {
        static void Postfix(DysonSphere __instance, long gameTick)
        {
            RamscoopShipsRuntime.TickTransitHarvest(gameTick);
        }
    }
}