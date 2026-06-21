namespace RecipeRebalance
{
    internal static class TechBootstrap
    {
        internal static void UnlockRecipe(RecipeProto recipe)
        {
            if (recipe?.preTech == null)
                return;

            if (!AppendUnlockRecipe(recipe.preTech, recipe.ID))
                return;

            AppendUnlockRecipeProto(recipe.preTech, recipe);
        }

        private static void AppendUnlockRecipeProto(TechProto tech, RecipeProto recipe)
        {
            if (tech.unlockRecipeArray == null)
            {
                tech.unlockRecipeArray = new[] { recipe };
                return;
            }

            for (int i = 0; i < tech.unlockRecipeArray.Length; i++)
            {
                if (tech.unlockRecipeArray[i]?.ID == recipe.ID)
                    return;
            }

            var merged = new RecipeProto[tech.unlockRecipeArray.Length + 1];
            for (int i = 0; i < tech.unlockRecipeArray.Length; i++)
                merged[i] = tech.unlockRecipeArray[i];
            merged[tech.unlockRecipeArray.Length] = recipe;
            tech.unlockRecipeArray = merged;
        }

        private static bool AppendUnlockRecipe(TechProto tech, int recipeId)
        {
            if (tech.UnlockRecipes == null)
            {
                tech.UnlockRecipes = new[] { recipeId };
                return true;
            }

            for (int i = 0; i < tech.UnlockRecipes.Length; i++)
            {
                if (tech.UnlockRecipes[i] == recipeId)
                    return false;
            }

            var merged = new int[tech.UnlockRecipes.Length + 1];
            for (int i = 0; i < tech.UnlockRecipes.Length; i++)
                merged[i] = tech.UnlockRecipes[i];
            merged[tech.UnlockRecipes.Length] = recipeId;
            tech.UnlockRecipes = merged;
            return true;
        }
    }
}