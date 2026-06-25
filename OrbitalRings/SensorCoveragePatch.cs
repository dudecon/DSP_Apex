using HarmonyLib;

namespace OrbitalRings
{
    [HarmonyPatch(typeof(SkillSystem), nameof(SkillSystem.MissileSearchGroundTarget))]
    internal static class SensorCoveragePatch
    {
        static void Prefix(ref GeneralMissile missile, ref float searchRange)
        {
            int planetId = OrbitalRingContext.ResolvePlanetIdFromMissile(missile);
            if (planetId == 0)
                return;

            float coverage = OrbitalRingRuntimeState.GetSensorCoverage(planetId);
            searchRange = OrbitalRingLogic.ApplySensorSearchBonus(searchRange, coverage);
        }
    }
}