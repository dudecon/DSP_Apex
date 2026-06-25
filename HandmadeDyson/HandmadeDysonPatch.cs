using HarmonyLib;

namespace HandmadeDyson
{
    [HarmonyPatch(typeof(BuildModel), nameof(BuildModel.Open))]
    internal static class HandmadeDysonBuildOpenPatch
    {
        static void Postfix() => HandmadeDysonRuntime.EnableOrbitalBuild = true;
    }

    [HarmonyPatch(typeof(BuildModel), nameof(BuildModel.Close))]
    internal static class HandmadeDysonBuildClosePatch
    {
        static void Postfix() => HandmadeDysonRuntime.EnableOrbitalBuild = false;
    }

    [HarmonyPatch(typeof(BuildTool_Click), nameof(BuildTool_Click.UpdatePreviewModels))]
    internal static class HandmadeDysonPreviewPatch
    {
        static void Postfix(BuildTool_Click __instance)
        {
            if (!HandmadeDysonRuntime.EnableOrbitalBuild || __instance == null)
                return;

            int protoId = __instance.handItem != null ? __instance.handItem.ID : 0;
            if (!HandmadeDysonLogic.CanBuildOnOrbitalStructure(protoId))
                return;

            __instance.cursorValid = true;
        }
    }
}