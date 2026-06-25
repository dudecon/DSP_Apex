namespace RecipeRebalance
{
    /// <summary>Pure fusion-chain ordering and item constants (testable without LDB).</summary>
    public static class FusionChainCatalog
    {
        public const int StoneRecipeCount = 7;
        public const int FusionRecipeCount = 5;
        public const int TotalModRecipeCount = StoneRecipeCount + FusionRecipeCount;

        public const int HeliumPerFusionOutput = 1;
        public const int HeliumInputForEnergeticGraphite = 3;
        public const int EnergeticGraphiteInputForStone = 2;
        public const int StoneOutputFromGraphiteFusion = 5;

        public static bool IsStoneRecipeIndex(int recipeIndex) =>
            recipeIndex >= 0 && recipeIndex < StoneRecipeCount;

        public static bool IsFusionRecipeIndex(int recipeIndex) =>
            recipeIndex >= StoneRecipeCount && recipeIndex < TotalModRecipeCount;

        public static int GetFusionRecipeOrdinal(int recipeIndex) =>
            recipeIndex - StoneRecipeCount;
    }
}