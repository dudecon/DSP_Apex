using HarmonyLib;

namespace RecipeRebalance
{
    [HarmonyPatch(typeof(GameHistoryData), nameof(GameHistoryData.Import))]
    internal static class GameHistoryImportPatch
    {
        static void Postfix(GameHistoryData __instance)
        {
            RecipeUnlockService.SyncFromHistory(__instance, "history import");
        }
    }

    [HarmonyPatch(typeof(GameHistoryData), nameof(GameHistoryData.NotifyTechUnlock))]
    internal static class GameHistoryTechUnlockPatch
    {
        static void Postfix(GameHistoryData __instance, int _techId, int _level, bool _unlockedDirect)
        {
            RecipeUnlockService.SyncFromHistory(__instance, $"tech unlock {_techId}");
        }
    }
}