using HarmonyLib;

namespace SelfPropagation
{
    [HarmonyPatch(typeof(GameData), "GameTick")]
    internal static class SelfPropagationPatch
    {
        static void Postfix(GameData __instance, long time)
        {
            if (time % 3600 != 0)
                return;

            ScanDeficits(__instance);
            SelfPropagationRuntime.TickAgents();
        }

        static void ScanDeficits(GameData gameData)
        {
            if (gameData?.mainPlayer?.package == null)
                return;

            var package = gameData.mainPlayer.package;
            int ironId = 1001;
            int copperId = 1002;
            int target = 1000;

            SelfPropagationRuntime.ReportDeficit(ironId, target - package.GetItemCount(ironId));
            SelfPropagationRuntime.ReportDeficit(copperId, target - package.GetItemCount(copperId));
        }
    }
}