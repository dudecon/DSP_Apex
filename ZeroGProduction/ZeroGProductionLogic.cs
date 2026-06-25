using System;

namespace ZeroGProduction
{
    public static class ZeroGProductionLogic
    {
        public const float RefineryEfficiencyBonus = 1.35f;
        public const float AssemblerYieldBonus = 1.20f;

        public static int ApplyYieldBonus(int baseCount, float bonus) =>
            Math.Max(1, (int)Math.Round(baseCount * bonus));

        public static float GetOrbitalBonus(bool hasRing, bool hasStation) =>
            1f + (hasRing ? 0.1f : 0f) + (hasStation ? 0.1f : 0f);

        public static bool ShouldApplyBonus(
            AssemblerComponent assembler,
            out float totalBonus)
        {
            totalBonus = 1f;
            if (!ZeroGContext.TryResolveAssemblerContext(
                    assembler,
                    out _,
                    out int protoId,
                    out bool hasRing,
                    out bool hasStation))
                return false;

            bool isZeroG = ZeroGBuildingIds.IsZeroGProto(protoId);
            bool orbital = hasRing || hasStation;
            if (!isZeroG && !orbital)
                return false;

            totalBonus = isZeroG ? RefineryEfficiencyBonus : AssemblerYieldBonus;
            totalBonus *= GetOrbitalBonus(hasRing, hasStation);
            return totalBonus > 1f;
        }
    }
}