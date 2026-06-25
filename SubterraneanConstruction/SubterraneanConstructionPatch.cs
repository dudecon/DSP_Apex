using HarmonyLib;

namespace SubterraneanConstruction
{
    [HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.BeforeGameTick))]
    internal static class SubterraneanLayerTickPatch
    {
        static void Postfix(PlanetFactory __instance, long gameTick)
        {
            SubterraneanConstructionRuntime.TickLayers(__instance, gameTick);
        }
    }

    [HarmonyPatch(typeof(MinerComponent), nameof(MinerComponent.InternalUpdate))]
    internal static class SubterraneanMinerPatch
    {
        static void Postfix(MinerComponent __instance, ref uint __result)
        {
            __result = SubterraneanConstructionRuntime.ApplyMiningBonus(__result, __instance.veinCount);
        }
    }
}