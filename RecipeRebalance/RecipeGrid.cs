using System.Collections.Generic;
using BepInEx.Logging;

namespace RecipeRebalance
{
    internal static class RecipeGrid
    {
        /// <summary>Replicator tab III — dedicated DSP Apex recipe matrix.</summary>
        internal const int ApexTab = RecipeGridMath.ApexTab;

        internal const int ColumnCount = RecipeGridMath.ColumnCount;
        internal const int RowCount = RecipeGridMath.RowCount;

        internal static int Encode(int tab, int row, int column) =>
            RecipeGridMath.Encode(tab, row, column);

        /// <summary>
        /// Returns consecutive slots on the Apex tab (e.g. 3101, 3102, … 3114, 3201, …).
        /// </summary>
        internal static int[] GetApexTabSlots(int count, ManualLogSource logger)
        {
            var slots = new int[count];
            var collisions = new List<string>();

            for (int i = 0; i < count; i++)
            {
                int grid = RecipeGridMath.GetApexSlotAtIndex(i);
                slots[i] = grid;

                var existing = FindRecipeAtGrid(grid);
                if (existing != null)
                    collisions.Add($"{existing.ID}@{grid}");
            }

            if (collisions.Count > 0)
            {
                logger.LogWarning(
                    $"RecipeRebalance: Apex tab slots collide with existing recipes: [{string.Join(", ", collisions)}]");
            }

            return slots;
        }

        private static RecipeProto FindRecipeAtGrid(int gridIndex)
        {
            var recipes = LDB.recipes?.dataArray;
            if (recipes == null)
                return null;

            for (int i = 0; i < recipes.Length; i++)
            {
                var recipe = recipes[i];
                if (recipe != null && recipe.GridIndex == gridIndex)
                    return recipe;
            }

            return null;
        }

        internal static int DecodeTab(int gridIndex) => RecipeGridMath.DecodeTab(gridIndex);

        internal static int DecodeRow(int gridIndex) => RecipeGridMath.DecodeRow(gridIndex);

        internal static int DecodeColumn(int gridIndex) => RecipeGridMath.DecodeColumn(gridIndex);
    }
}