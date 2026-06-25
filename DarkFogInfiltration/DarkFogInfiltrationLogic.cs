using System;

namespace DarkFogInfiltration
{
    public static class DarkFogInfiltrationLogic
    {
        public static float ConvertChance(int scrapLevel) => 0.05f + scrapLevel * 0.01f;

        public static float FogCounterRisk => 0.02f;

        public static float ApplyReciprocalRisk(float fogLevel, float infiltrationProgress) =>
            fogLevel + FogCounterRisk * (1f + infiltrationProgress);

        public static bool ShouldFogRetaliate(float fogLevel) => fogLevel >= 0.5f;

        public static int ConvertedAssets(float progress, int scrapLevel) =>
            progress >= 1f ? Math.Max(1, scrapLevel) : 0;
    }
}