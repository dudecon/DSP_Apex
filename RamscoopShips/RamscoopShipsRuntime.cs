namespace RamscoopShips
{
    internal static class RamscoopShipsRuntime
    {
        internal static int AccumulatedScoopYield;
        internal static float TransitSpeedFactor = RamscoopLogic.SpeedPenalty;
        internal static float LastEffectiveSpeed = 1f;

        internal static void TickTransitHarvest(long gameTick)
        {
            if (gameTick % 300 != 0)
                return;

            const float baseSpeed = 100f;
            LastEffectiveSpeed = GetEffectiveSpeed(baseSpeed);
            float speedFactor = 1f - (LastEffectiveSpeed / baseSpeed);

            int baseYield = 10;
            int scoop = RamscoopLogic.ScoopYield(baseYield, speedFactor);
            AccumulatedScoopYield += scoop;
            TransitSpeedFactor = RamscoopLogic.SpeedPenalty;
        }

        internal static float GetEffectiveSpeed(float baseSpeed) =>
            baseSpeed * RamscoopLogic.TransitSpeedMultiplier(TransitSpeedFactor);
    }
}