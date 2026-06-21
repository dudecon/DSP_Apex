using HarmonyLib;

namespace RecipeRebalance
{
    /// <summary>
    /// Allow vein miners on bare ground (no covered veins). Vanilla blocks with NeedResource.
    /// </summary>
    [HarmonyPatch(typeof(BuildTool_Click), nameof(BuildTool_Click.DeterminePreviews))]
    internal static class MinerPlacementDeterminePatch
    {
        static void Postfix(BuildTool_Click __instance)
        {
            MinerPlacementUtil.AllowBareGroundMiners(__instance);
        }
    }

    [HarmonyPatch(typeof(BuildTool_Click), nameof(BuildTool_Click.UpdatePreviewModelConditions))]
    internal static class MinerPlacementPreviewPatch
    {
        static void Postfix(BuildTool_Click __instance)
        {
            MinerPlacementUtil.AllowBareGroundMiners(__instance);
        }
    }

    [HarmonyPatch(typeof(BuildTool_Click), nameof(BuildTool_Click.CheckBuildConditions))]
    internal static class MinerPlacementCheckPatch
    {
        static void Prefix(BuildTool_Click __instance)
        {
            MinerPlacementUtil.AllowBareGroundMiners(__instance);
        }
    }
}