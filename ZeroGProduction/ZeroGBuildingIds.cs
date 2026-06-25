namespace ZeroGProduction
{
    internal static class ZeroGBuildingIds
    {
        public static int ZeroGRefinery;
        public static int ZeroGAssembler;

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

            ZeroGRefinery = ++max;
            ZeroGAssembler = ++max;
        }

        internal static bool IsZeroGProto(int protoId) =>
            protoId == ZeroGRefinery || protoId == ZeroGAssembler;
    }
}