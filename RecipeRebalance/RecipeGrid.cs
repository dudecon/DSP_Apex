using System.Collections.Generic;
using BepInEx.Logging;

namespace RecipeRebalance
{
    internal static class RecipeGrid
    {
        /// <summary>Replicator tab III — dedicated DSP Apex recipe matrix.</summary>
        internal const int ApexTab = 3;

        internal const int ColumnCount = 14;
        internal const int RowCount = 8;

        internal static int Encode(int tab, int row, int column)
        {
            return tab * 1000 + row * 100 + column;
        }

        /// <summary>
        /// Returns consecutive slots on the Apex tab (e.g. 3101, 3102, … 3114, 3201, …).
        /// </summary>
        internal static int[] GetApexTabSlots(int count, ManualLogSource logger)
        {
            var slots = new int[count];
            var collisions = new List<string>();

            for (int i = 0; i < count; i++)
            {
                int row = i / ColumnCount + 1;
                int column = i % ColumnCount + 1;
                int grid = Encode(ApexTab, row, column);
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

        internal static int DecodeTab(int gridIndex) => gridIndex / 1000;

        internal static int DecodeRow(int gridIndex) => (gridIndex % 1000) / 100;

        internal static int DecodeColumn(int gridIndex) => gridIndex % 100;
    }
}