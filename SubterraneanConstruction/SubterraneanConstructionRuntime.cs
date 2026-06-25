namespace SubterraneanConstruction
{
    internal static class SubterraneanConstructionRuntime
    {
        internal static int CityLayers;
        internal static int MantleDepth;

        internal static void TickLayers(PlanetFactory factory, long gameTick)
        {
            if (factory == null || gameTick % 600 != 0)
                return;

            int entityDepth = factory.entityCount / 100;
            MantleDepth = System.Math.Max(MantleDepth, entityDepth);
            CityLayers = System.Math.Max(CityLayers, MantleDepth / 4);
        }

        internal static uint ApplyMiningBonus(uint baseYield, int veinCount) =>
            SubterraneanLogic.ApplyLayeredYield(baseYield, veinCount, MantleDepth, CityLayers);
    }
}