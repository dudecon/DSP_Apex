namespace RecipeRebalance
{
    /// <summary>Vanilla and DSP Apex proto IDs. Mod IDs are assigned at runtime from kMaxProtoId.</summary>
    internal static class ApexIds
    {
        // Vanilla items
        public const int IronOre = 1001;
        public const int CopperOre = 1002;
        public const int SiliconOre = 1003;
        public const int TitaniumOre = 1004;
        public const int Stone = 1005;
        public const int Coal = 1006;
        public const int KimberliteOre = 1013;
        public const int FractalSilicon = 1014;
        public const int Hydrogen = 1120;
        public const int Deuterium = 1121;
        public const int StrangeMatter = 1123;
        public const int UnipolarMagnet = 1126;
        public const int GravitonLens = 1208;
        public const int CrystalSilicon = 1115;
        public const int Diamond = 1118;
        public const int EnergeticGraphite = 1109;

        // DSP Apex items (assigned in AssignRuntimeIds)
        public static int Helium;

        // DSP Apex recipes (assigned in AssignRuntimeIds)
        public static int RecipeStoneToIronOre;
        public static int RecipeStoneToCopperOre;
        public static int RecipeStoneToSiliconOre;
        public static int RecipeStoneToTitaniumOre;
        public static int RecipeStoneToCoal;
        public static int RecipeStoneToKimberlite;
        public static int RecipeStoneToFractalSilicon;
        public static int RecipeHeliumToEnergeticGraphite;
        public static int RecipeEnergeticGraphiteToStone;
        public static int RecipeEnergeticGraphiteToStrangeMatter;
        public static int RecipeEnergeticGraphiteToUnipolarMagnet;

        internal static void AssignRuntimeIds(BepInEx.Logging.ManualLogSource logger)
        {
            int itemId = FindMaxId(LDB.items?.dataArray);
            Helium = ++itemId;

            if (Helium > ItemProto.kMaxProtoId)
            {
                throw new System.InvalidOperationException(
                    $"RecipeRebalance: mod item ID {Helium} exceeds ItemProto.kMaxProtoId ({ItemProto.kMaxProtoId}).");
            }

            int recipeId = FindMaxId(LDB.recipes?.dataArray);
            RecipeStoneToIronOre = ++recipeId;
            RecipeStoneToCopperOre = ++recipeId;
            RecipeStoneToSiliconOre = ++recipeId;
            RecipeStoneToTitaniumOre = ++recipeId;
            RecipeStoneToCoal = ++recipeId;
            RecipeStoneToKimberlite = ++recipeId;
            RecipeStoneToFractalSilicon = ++recipeId;
            RecipeHeliumToEnergeticGraphite = ++recipeId;
            RecipeEnergeticGraphiteToStone = ++recipeId;
            RecipeEnergeticGraphiteToStrangeMatter = ++recipeId;
            RecipeEnergeticGraphiteToUnipolarMagnet = ++recipeId;

            logger.LogInfo(
                $"RecipeRebalance: runtime IDs — item {Helium}; " +
                $"recipes {RecipeStoneToIronOre}–{RecipeEnergeticGraphiteToUnipolarMagnet}");
        }

        private static int FindMaxId(Proto[] protos)
        {
            int maxId = 0;
            if (protos == null)
                return maxId;

            for (int i = 0; i < protos.Length; i++)
            {
                var proto = protos[i];
                if (proto != null && proto.ID > maxId)
                    maxId = proto.ID;
            }

            return maxId;
        }
    }
}