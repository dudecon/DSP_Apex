using HarmonyLib;

namespace OrbitalRings
{
    [HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.BeforeGameTick))]
    internal static class OrbitalRingsPatch
    {
        static void Postfix(PlanetFactory __instance)
        {
            var planet = __instance?.planet;
            if (planet == null)
                return;

            int frames = OrbitalRingCounter.CountRingFrames(__instance);
            int swarms = OrbitalRingCounter.CountSwarmNodes(__instance);

            OrbitalRingRuntimeState.SetRelayBonus(
                planet.id,
                OrbitalRingGameLogic.GetPowerRelayMultiplier(planet, frames));

            OrbitalRingRuntimeState.SetHarvestBonus(
                planet.id,
                OrbitalRingGameLogic.GetHarvestMultiplier(planet, swarms));

            OrbitalRingRuntimeState.SetSensorCoverage(
                planet.id,
                OrbitalRingLogic.GetSensorCoverage(frames + swarms));
        }
    }
}