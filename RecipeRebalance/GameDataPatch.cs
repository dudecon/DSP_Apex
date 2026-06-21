using HarmonyLib;

namespace RecipeRebalance
{
    [HarmonyPatch(typeof(GameData), nameof(GameData.Import))]
    internal static class GameDataImportPatch
    {
        static void Postfix(GameData __instance)
        {
            RecipeUnlockService.SyncFromHistory(__instance.history, "game data import");
        }
    }

    [HarmonyPatch(typeof(GameData), nameof(GameData.OnActivePlanetLoaded))]
    internal static class GameDataPlanetLoadedPatch
    {
        static void Postfix(GameData __instance)
        {
            RecipeUnlockService.SyncFromHistory(__instance.history, "planet loaded");
        }
    }
}