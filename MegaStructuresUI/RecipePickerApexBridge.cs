using System.Reflection;
using RecipeRebalance;

namespace MegaStructuresUI
{
    /// <summary>
    /// Shows Apex-tab recipes in assembler pickers (filter by ERecipeType) even though their GridIndex tab is III.
    /// </summary>
    internal static class RecipePickerApexBridge
    {
        private static readonly FieldInfo IndexArrayField = typeof(UIRecipePicker).GetField(
            "indexArray",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        private static readonly FieldInfo ProtoArrayField = typeof(UIRecipePicker).GetField(
            "protoArray",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        private static readonly FieldInfo FilterField = typeof(UIRecipePicker).GetField(
            "filter",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        internal static void InjectApexRecipes(UIRecipePicker picker)
        {
            if (picker == null || RecipeRebalanceApi.ModRecipeIds.Count == 0)
                return;

            var indexArray = IndexArrayField?.GetValue(picker) as uint[];
            var protoArray = ProtoArrayField?.GetValue(picker) as RecipeProto[];
            if (indexArray == null || protoArray == null)
                return;

            var filterValue = FilterField?.GetValue(picker);
            int filter = filterValue is ERecipeType recipeFilter ? (int)recipeFilter : 0;
            var history = GameMain.history;
            var iconSet = GameMain.iconSet;
            if (history == null || iconSet == null)
                return;

            foreach (var recipeId in RecipeRebalanceApi.ModRecipeIds)
            {
                var recipe = LDB.recipes.Select(recipeId);
                if (recipe == null || !RecipeRebalanceApi.IsApexGridRecipe(recipe))
                    continue;

                if (filter != 0 && (int)recipe.Type != filter)
                    continue;

                if (!history.RecipeUnlocked(recipeId))
                    continue;

                if (!RecipeRebalanceApi.TryGetSlotIndex(recipe.GridIndex, out int slot))
                    continue;

                if (slot < 0 || slot >= indexArray.Length || slot >= protoArray.Length)
                    continue;

                if (protoArray[slot] != null)
                    continue;

                indexArray[slot] = iconSet.recipeIconIndex[recipe.ID];
                protoArray[slot] = recipe;
            }
        }
    }
}