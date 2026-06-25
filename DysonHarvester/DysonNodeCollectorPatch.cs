using HarmonyLib;

namespace DysonHarvester
{
    [HarmonyPatch(typeof(DysonSphere), nameof(DysonSphere.UpdateProgress), typeof(DysonNode))]
    internal static class DysonNodeCollectorPatch
    {
        static void Postfix(DysonSphere __instance, DysonNode node)
        {
            if (__instance?.starData == null || !node.use)
                return;

            int innermost = HarvesterGameAdapter.GetInnermostActiveLayerId(__instance);
            NodeCollectorRegistry.SetActive(__instance.starData.index, node, innermost);
        }
    }
}