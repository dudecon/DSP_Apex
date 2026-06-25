using System;

namespace ThermalEffects
{
    public static class ThermalEffectsLogic
    {
        public const float HotThreshold = 500f;
        public const float ColdThreshold = 200f;

        public static float RefineryEnergyFactor(float planetTemp) =>
            planetTemp > HotThreshold ? 0.85f : 1f;

        public static float AssemblerSpeedFactor(float planetTemp)
        {
            if (planetTemp > HotThreshold)
                return Math.Max(0.5f, 0.9f - WasteHeatSlowdown(planetTemp));

            if (planetTemp < ColdThreshold)
                return 0.95f;

            return 1f;
        }

        public static float WasteHeatSlowdown(float planetTemp) =>
            planetTemp > HotThreshold ? Math.Min(0.4f, (planetTemp - HotThreshold) * 0.001f) : 0f;

        public static float AccumulateWasteHeat(float current, float powerDraw, float planetTemp)
        {
            float gain = powerDraw * 0.001f;
            if (planetTemp > HotThreshold)
                gain *= 1.5f;

            return current + gain;
        }

        public static float DissipateWasteHeat(float current, float planetTemp) =>
            Math.Max(0f, current - (planetTemp > HotThreshold ? 0.02f : 0.05f));
    }
}