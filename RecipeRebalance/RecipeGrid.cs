using System.Collections.Generic;
using BepInEx.Logging;

namespace RecipeRebalance
{
    internal static class RecipeGrid
    {
        private const int SmelterTab = 1;

        /// <summary>Dedicated smelter matrix row for mod stone smelts (row IX — beyond vanilla rows I–VIII).</summary>
        internal const int ModSmeltRow = 9;

        internal static int Encode(int tab, int row, int column)
        {
            return tab * 1000 + row * 100 + column;
        }

        /// <summary>
        /// Returns consecutive slots on smelter tab row IX (e.g. 1901–1905).
        /// </summary>
        internal static int[] GetModSmeltSlots(int count, ManualLogSource logger)
        {
            var slots = new int[count];
            var collisions = new List<string>();

            for (int i = 0; i < count; i++)
            {
                int grid = Encode(SmelterTab, ModSmeltRow, i + 1);
                slots[i] = grid;

                var existing = FindRecipeAtGrid(grid);
                if (existing != null)
                    collisions.Add($"{existing.ID}@{grid}");
            }

            if (collisions.Count > 0)
            {
                logger.LogWarning(
                    $"RecipeRebalance: row IX smelt slots collide with existing recipes: [{string.Join(", ", collisions)}]");
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