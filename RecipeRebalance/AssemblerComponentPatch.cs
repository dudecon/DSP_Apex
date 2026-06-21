using HarmonyLib;

namespace RecipeRebalance
{
    /// <summary>
    /// Ensure mod recipes get valid execute data when assigned to smelters/assemblers.
    /// </summary>
    [HarmonyPatch(typeof(AssemblerComponent), nameof(AssemblerComponent.SetRecipe))]
    internal static class AssemblerComponentPatch
    {
        static void Postfix(AssemblerComponent __instance, int recpId)
        {
            if (recpId <= 0 || __instance.recipeExecuteData != null)
                return;

            if (RecipeProto.recipeExecuteData == null
                || !RecipeProto.recipeExecuteData.TryGetValue(recpId, out var data)
                || data == null)
            {
                Plugin.Log.LogWarning(
                    $"RecipeRebalance: SetRecipe({recpId}) left recipeExecuteData null.");
                return;
            }

            __instance.recipeExecuteData = data;
        }
    }
}