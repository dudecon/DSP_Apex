using System;

namespace RamscoopShips
{
    public static class RamscoopLogic
    {
        public static float SpeedPenalty => 0.6f;
        public static float HarvestBonus => 2.5f;

        public static int ScoopYield(int baseYield, float speedFactor) =>
            (int)(baseYield * HarvestBonus * (1f - speedFactor));

        public static float TransitSpeedMultiplier(float speedFactor) =>
            Math.Max(0.2f, 1f - speedFactor);

        public static float HarvestMultiplier(float speedFactor) =>
            1f + HarvestBonus * speedFactor;
    }
}