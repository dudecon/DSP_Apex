using System.Collections.Generic;

namespace RecipeRebalance
{
    /// <summary>Public surface for other DSP Apex mods (e.g. MegaStructuresUI).</summary>
    public static class RecipeRebalanceApi
    {
        public const int ApexTab = RecipeGrid.ApexTab;
        public const int TotalReplicatorTabs = 7;

        public static IReadOnlyList<int> ModRecipeIds => RecipeBootstrap.ModRecipeIds;

        public static bool IsModRecipe(int recipeId) => RecipeBootstrap.ModRecipeIds.Contains(recipeId);

        public static bool IsApexGridRecipe(RecipeProto recipe)
        {
            return recipe != null && RecipeGrid.DecodeTab(recipe.GridIndex) == ApexTab;
        }

        public static bool TryGetSlotIndex(int gridIndex, out int slot)
        {
            int row = RecipeGrid.DecodeRow(gridIndex) - 1;
            int column = RecipeGrid.DecodeColumn(gridIndex) - 1;

            if (row < 0 || column < 0 || row >= RecipeGrid.RowCount || column >= RecipeGrid.ColumnCount)
            {
                slot = -1;
                return false;
            }

            slot = row * RecipeGrid.ColumnCount + column;
            return true;
        }
    }
}