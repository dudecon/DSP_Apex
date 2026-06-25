namespace OrbitalShipyards
{
    internal static class OrbitalShipyardsRuntime
    {
        internal static int ShipyardLevel = 1;
        internal static int BatchedRockets;
        internal static int RepairedHull;

        internal static void BatchConstructRockets(DysonSphere sphere)
        {
            if (sphere == null)
                return;

            int batch = OrbitalShipyardLogic.BatchRocketCount(ShipyardLevel);
            BatchedRockets += batch;

            float discount = OrbitalShipyardLogic.LaunchEnergyDiscount;
            if (sphere.rocketCursor > 0 && discount < 1f)
                sphere.rocketCapacity = System.Math.Max(sphere.rocketCapacity, sphere.rocketCursor + batch);
        }

        internal static void TickRepair(int damagedHull)
        {
            if (damagedHull <= 0)
                return;

            int repaired = OrbitalShipyardLogic.RepairThroughput(ShipyardLevel, damagedHull);
            RepairedHull += repaired;
        }
    }
}