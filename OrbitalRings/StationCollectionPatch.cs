using HarmonyLib;

namespace OrbitalRings
{
    [HarmonyPatch(typeof(StationComponent), nameof(StationComponent.UpdateCollection))]
    internal static class StationCollectionPatch
    {
        static void Prefix(StationComponent __instance, ref float power)
        {
            if (__instance.id == 0 || !__instance.isCollector || power < 0.01f)
                return;

            float bonus = OrbitalRingRuntimeState.GetHarvestBonus(__instance.planetId);
            power = OrbitalRingLogic.ApplyHarvestBonus(power, bonus);
        }
    }
}