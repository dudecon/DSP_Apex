using System;

namespace OrbitalRings
{
    public static class OrbitalRingLogic
    {
        public const float RockyRelayBonus = 1.15f;
        public const float GasGiantHarvestBonus = 1.25f;

        public const int PlanetKindNone = 0;
        public const int PlanetKindRocky = 1;
        public const int PlanetKindGas = 2;

        public static float GetPowerRelayMultiplier(int planetKind, int ringFrameCount)
        {
            if (planetKind != PlanetKindRocky || ringFrameCount <= 0)
                return 1f;

            return 1f + (RockyRelayBonus - 1f) * Math.Min(ringFrameCount, 8) / 8f;
        }

        public static float GetHarvestMultiplier(int planetKind, int swarmCount)
        {
            if (planetKind != PlanetKindGas || swarmCount <= 0)
                return 1f;

            return 1f + (GasGiantHarvestBonus - 1f) * Math.Min(swarmCount, 6) / 6f;
        }

        public static float GetSensorCoverage(int ringCount) => Math.Min(1f, ringCount * 0.125f);

        public static float GetSensorRangeMultiplier(float coverage) =>
            coverage <= 0f ? 1f : 1f + coverage;

        public static float ApplyRelayBonus(float eta, float relayMultiplier) =>
            relayMultiplier > 1f ? eta * relayMultiplier : eta;

        public static float ApplyHarvestBonus(float power, float harvestMultiplier) =>
            harvestMultiplier > 1f ? power * harvestMultiplier : power;

        public static float ApplySensorSearchBonus(float searchRange, float coverage) =>
            coverage > 0f ? searchRange * GetSensorRangeMultiplier(coverage) : searchRange;
    }
}