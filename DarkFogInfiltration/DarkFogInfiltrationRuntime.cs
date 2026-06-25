namespace DarkFogInfiltration
{
    internal static class DarkFogInfiltrationRuntime
    {
        internal static int ScrapLevel;
        internal static float InfiltrationProgress;
        internal static float FogCounterLevel;
        internal static int ConvertedUnitCount;

        internal static void TickInfiltration(long gameTick)
        {
            if (gameTick % 3600 != 0)
                return;

            float convert = DarkFogInfiltrationLogic.ConvertChance(ScrapLevel);
            InfiltrationProgress += convert;
            FogCounterLevel = DarkFogInfiltrationLogic.ApplyReciprocalRisk(FogCounterLevel, InfiltrationProgress);

            if (InfiltrationProgress >= 1f)
            {
                ScrapLevel++;
                ConvertedUnitCount += DarkFogInfiltrationLogic.ConvertedAssets(InfiltrationProgress, ScrapLevel);
                InfiltrationProgress = 0f;
            }

            if (DarkFogInfiltrationLogic.ShouldFogRetaliate(FogCounterLevel))
                FogCounterLevel += DarkFogInfiltrationLogic.FogCounterRisk;
        }
    }
}