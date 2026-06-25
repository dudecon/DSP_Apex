using System.Collections.Generic;

namespace SelfPropagation
{
    internal static class SelfPropagationRuntime
    {
        internal static int ActiveAgents;
        internal static readonly Dictionary<int, int> ResourceDeficits = new Dictionary<int, int>();

        internal static void TickAgents()
        {
            int totalDeficit = 0;
            foreach (var kv in ResourceDeficits)
                totalDeficit += kv.Value;

            if (!SelfPropagationLogic.ShouldExpand(totalDeficit))
                return;

            if (ActiveAgents >= SelfPropagationLogic.MaxAgents)
                return;

            ActiveAgents++;
            int priority = SelfPropagationLogic.BlueprintPriority(totalDeficit);
            ResourceDeficits[priority] = System.Math.Max(0, totalDeficit - priority * 50);
        }

        internal static void ReportDeficit(int itemId, int deficit)
        {
            if (deficit <= 0)
                ResourceDeficits.Remove(itemId);
            else
                ResourceDeficits[itemId] = deficit;
        }
    }
}