using System;

namespace SelfPropagation
{
    public static class SelfPropagationLogic
    {
        public static int MaxAgents = 16;

        public static bool ShouldExpand(int resourceDeficit) => resourceDeficit > 100;

        public static int BlueprintPriority(int demand) => Math.Min(10, demand / 50);

        public static int ComputeTotalDeficit(int[] deficits)
        {
            if (deficits == null || deficits.Length == 0)
                return 0;

            int total = 0;
            for (int i = 0; i < deficits.Length; i++)
                total += Math.Max(0, deficits[i]);

            return total;
        }

        public static int BlueprintDemand(int itemDeficit, int priority) =>
            Math.Max(0, itemDeficit - priority * 50);
    }
}