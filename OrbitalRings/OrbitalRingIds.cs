namespace OrbitalRings
{
    internal static class OrbitalRingIds
    {
        public const int VanillaOrbitalCollector = 2103;
        public const int VanillaRayReceiver = 2203;

        public static int RingFrameModule;
        public static int SatelliteSwarmModule;

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

            RingFrameModule = ++max;
            SatelliteSwarmModule = ++max;
        }

        internal static bool IsRingProto(int protoId) =>
            protoId == RingFrameModule;

        internal static bool IsSwarmProto(int protoId) =>
            protoId == SatelliteSwarmModule;
    }
}