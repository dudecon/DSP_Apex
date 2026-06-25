namespace OrbitalInfrastructure
{
    internal static class InfrastructureIds
    {
        public const int VanillaPlanetaryLogistics = 2101;
        public const int VanillaOrbitalCollector = 2103;

        public static int SpaceElevator;
        public static int SatelliteSwarm;
        public static int OrbitalStationModule;

        internal static void AssignRuntimeIds()
        {
            int max = 0;
            var items = LDB.items?.dataArray;
            if (items != null)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    var item = items[i];
                    if (item != null && item.ID > max)
                        max = item.ID;
                }
            }

            SpaceElevator = ++max;
            SatelliteSwarm = ++max;
            OrbitalStationModule = ++max;
        }
    }
}