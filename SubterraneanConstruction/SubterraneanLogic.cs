using System;

namespace SubterraneanConstruction
{
    public static class SubterraneanLogic
    {
        public static int MantleDepthBonus(int layer) => layer * 5;

        public static float DeepMineMultiplier(int depth) => 1f + depth * 0.05f;

        public static int EcumenopolisLayerBonus(int cityLayers) => cityLayers * 8;

        public static float LayeredMineEfficiency(int veinCount, int cityLayers) =>
            DeepMineMultiplier(veinCount) + cityLayers * 0.03f;

        public static uint ApplyLayeredYield(uint baseYield, int veinCount, int mantleDepth, int cityLayers)
        {
            float multiplier = LayeredMineEfficiency(veinCount + mantleDepth, cityLayers);
            int flatBonus = EcumenopolisLayerBonus(cityLayers);
            int scaled = (int)(baseYield * (multiplier - 1f)) + flatBonus;
            return baseYield + (uint)Math.Max(0, scaled);
        }
    }
}