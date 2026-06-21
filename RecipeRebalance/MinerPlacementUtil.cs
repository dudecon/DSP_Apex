using System.Collections.Generic;

namespace RecipeRebalance
{
    internal static class MinerPlacementUtil
    {
        internal static void AllowBareGroundMiners(BuildTool_Click tool)
        {
            if (tool == null)
                return;

            AllowBareGroundMiners(tool.buildPreviews);
            AllowBareGroundMiners(tool.actionBuild?.templatePreviews);
        }

        internal static void AllowBareGroundMiners(IList<BuildPreview> previews)
        {
            if (previews == null)
                return;

            for (int i = 0; i < previews.Count; i++)
                FixPreview(previews[i]);
        }

        private static void FixPreview(BuildPreview preview)
        {
            if (preview?.desc == null || !preview.desc.veinMiner)
                return;

            if (preview.condition == EBuildCondition.NeedResource
                || preview.condition == EBuildCondition.NeedSingleResource)
            {
                preview.condition = EBuildCondition.Ok;
            }
        }
    }
}