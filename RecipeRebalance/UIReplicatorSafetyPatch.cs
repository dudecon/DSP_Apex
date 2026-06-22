using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace RecipeRebalance
{
    /// <summary>
    /// Mod items can have null makes until FindRecipes runs; vanilla UI assumes a non-null list.
    /// </summary>
    [HarmonyPatch(typeof(UIReplicatorWindow), "OnSelectedRecipeChange")]
    internal static class UIReplicatorSafetyPatch
    {
        private static readonly FieldInfo SelectedRecipeField = typeof(UIReplicatorWindow).GetField(
            "selectedRecipe",
            BindingFlags.Instance | BindingFlags.NonPublic);

        static void Prefix(UIReplicatorWindow __instance)
        {
            var selectedRecipe = SelectedRecipeField?.GetValue(__instance) as RecipeProto;
            if (selectedRecipe?.Results == null)
                return;

            for (int i = 0; i < selectedRecipe.Results.Length; i++)
            {
                var item = LDB.items.Select(selectedRecipe.Results[i]);
                EnsureMakesList(item);
            }

            if (selectedRecipe.Items == null)
                return;

            for (int i = 0; i < selectedRecipe.Items.Length; i++)
            {
                var item = LDB.items.Select(selectedRecipe.Items[i]);
                EnsureMakesList(item);
            }
        }

        private static void EnsureMakesList(ItemProto item)
        {
            if (item == null)
                return;

            if (item.makes == null)
                item.makes = new List<RecipeProto>();
        }
    }
}