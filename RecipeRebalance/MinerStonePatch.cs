using System.Collections.Generic;
using HarmonyLib;

namespace RecipeRebalance
{
    [HarmonyPatch(typeof(MinerComponent), nameof(MinerComponent.InternalUpdate))]
    internal static class MinerStonePatch
    {
        private static readonly Dictionary<int, int> ProgressByMinerId = new Dictionary<int, int>();

        // Produce stone at ~10% of a normal vein miner's throughput.
        private const int PeriodMultiplier = 10;

        static void Postfix(
            MinerComponent __instance,
            PlanetFactory factory,
            VeinData[] veinPool,
            float power,
            float miningRate,
            float miningSpeed,
            int[] productRegister,
            ref uint __result)
        {
            if (__instance.type != EMinerType.Vein)
                return;

            if (__instance.veinCount > 0)
                return;

            if (__instance.workstate == EWorkState.Idle || __instance.workstate == EWorkState.Lack)
                return;

            if (__instance.period <= 0)
                return;

            int threshold = __instance.period * PeriodMultiplier;
            if (threshold <= 0)
                return;

            if (!ProgressByMinerId.TryGetValue(__instance.id, out int progress))
                progress = 0;

            progress += __instance.speed > 0 ? __instance.speed : 1;
            if (progress < threshold)
            {
                ProgressByMinerId[__instance.id] = progress;
                return;
            }

            ProgressByMinerId[__instance.id] = progress - threshold;

            byte remain = 0;
            int inserted = factory.InsertInto(__instance.entityId, ApexIds.Stone, 1, 0, 0, out remain);
            if (inserted > 0)
                __result += (uint)inserted;
        }
    }
}