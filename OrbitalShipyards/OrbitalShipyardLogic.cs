using System;

namespace OrbitalShipyards
{
    public static class OrbitalShipyardLogic
    {
        public static int BatchRocketCount(int shipyardLevel) => 4 + shipyardLevel * 2;

        public static float LaunchEnergyDiscount => 0.7f;

        public static int RepairThroughput(int shipyardLevel, int damagedHull) =>
            Math.Min(damagedHull, 10 + shipyardLevel * 5);

        public static float MassProductionDiscount(int batchCount) =>
            batchCount <= 0 ? 1f : Math.Max(0.5f, 1f - batchCount * 0.02f);
    }
}