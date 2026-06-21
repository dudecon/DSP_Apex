namespace RecipeRebalance
{
    internal static class RecipeUnlockService
    {
        internal static void SyncFromHistory(GameHistoryData history, string reason)
        {
            if (history == null || RecipeBootstrap.ModRecipeIds.Count == 0)
                return;

            int unlocked = 0;
            int missing = 0;

            foreach (var recipeId in RecipeBootstrap.ModRecipeIds)
            {
                if (history.RecipeUnlocked(recipeId))
                    continue;

                var recipe = LDB.recipes.Select(recipeId);
                if (recipe == null)
                {
                    missing++;
                    continue;
                }

                if (recipe.preTech != null && !history.TechState(recipe.preTech.ID).unlocked)
                    continue;

                history.UnlockRecipe(recipeId);
                unlocked++;
            }

            if (unlocked > 0)
            {
                Plugin.Log.LogInfo($"RecipeRebalance: unlocked {unlocked} mod recipes during {reason}.");
            }

            if (missing > 0)
            {
                Plugin.Log.LogWarning(
                    $"RecipeRebalance: {missing} mod recipes missing from LDB during {reason}.");
            }
        }
    }
}