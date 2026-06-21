using HarmonyLib;

namespace RecipeRebalance
{
    /// <summary>
    /// Register mod protos after vanilla LDB preload finishes (items, recipes, and techs are ready).
    /// </summary>
    [HarmonyPatch(typeof(VFPreload), "InvokeOnLoadWorkEnded")]
    internal static class VFPreloadPatch
    {
        private static bool registered;

        static void Postfix()
        {
            if (registered)
                return;

            try
            {
                RecipeBootstrap.Apply(Plugin.Log);
                if (RecipeBootstrap.BootstrapComplete)
                    registered = true;
                else
                    Plugin.Log.LogError("RecipeRebalance bootstrap did not complete.");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"RecipeRebalance bootstrap failed: {ex}");
            }
        }
    }
}