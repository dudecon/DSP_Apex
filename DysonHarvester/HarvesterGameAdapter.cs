namespace DysonHarvester
{
    internal static class HarvesterGameAdapter
    {
        internal static HarvesterYieldLogic.StarClass MapStarType(EStarType starType)
        {
            switch (starType)
            {
                case EStarType.GiantStar: return HarvesterYieldLogic.StarClass.Giant;
                case EStarType.WhiteDwarf: return HarvesterYieldLogic.StarClass.WhiteDwarf;
                case EStarType.NeutronStar: return HarvesterYieldLogic.StarClass.Neutron;
                case EStarType.BlackHole: return HarvesterYieldLogic.StarClass.BlackHole;
                default: return HarvesterYieldLogic.StarClass.MainSequence;
            }
        }

        internal static bool IsNodeComplete(DysonNode node) =>
            HarvesterYieldLogic.IsNodeComplete(node.use, node.sp, node.spMax);

        internal static bool LayerHasCompletedNode(DysonSphereLayer layer)
        {
            if (layer?.nodePool == null || layer.nodeCursor <= 0)
                return false;

            for (int i = 1; i < layer.nodeCursor; i++)
            {
                if (IsNodeComplete(layer.nodePool[i]))
                    return true;
            }

            return false;
        }

        internal static int GetInnermostActiveLayerId(DysonSphere sphere)
        {
            if (sphere?.layersSorted == null || sphere.layerCount <= 0)
                return -1;

            int bestId = -1;
            float bestRadius = float.MaxValue;

            for (int i = 0; i < sphere.layerCount; i++)
            {
                var layer = sphere.layersSorted[i];
                if (layer == null || !LayerHasCompletedNode(layer))
                    continue;

                if (layer.orbitRadius < bestRadius)
                {
                    bestRadius = layer.orbitRadius;
                    bestId = layer.id;
                }
            }

            return bestId;
        }

        internal static bool IsNodeHarvestEligible(DysonNode node, int innermostLayerId) =>
            HarvesterYieldLogic.IsNodeHarvestEligible(
                node.use, node.layerId, innermostLayerId, node.sp, node.spMax);

        internal static int CountEligibleNodes(DysonSphere sphere, int innermostLayerId)
        {
            if (sphere?.layersIdBased == null || innermostLayerId < 0 || innermostLayerId >= sphere.layersIdBased.Length)
                return 0;

            var layer = sphere.layersIdBased[innermostLayerId];
            if (layer?.nodePool == null)
                return 0;

            int count = 0;
            for (int i = 1; i < layer.nodeCursor; i++)
            {
                if (IsNodeHarvestEligible(layer.nodePool[i], innermostLayerId))
                    count++;
            }

            return count;
        }
    }
}