using System.Collections.Generic;

namespace DysonHarvester
{
    internal static class NodeCollectorRegistry
    {
        internal struct CollectorEntry
        {
            public int LayerId;
            public bool Active;
            public int Progress;
        }

        static readonly Dictionary<long, CollectorEntry> Entries = new Dictionary<long, CollectorEntry>();

        internal static long Key(int starIndex, int nodeId) =>
            ((long)starIndex << 32) | (uint)nodeId;

        internal static void SetActive(int starIndex, DysonNode node, int innermostLayerId)
        {
            if (!node.use)
                return;

            long key = Key(starIndex, node.id);
            bool active = HarvesterYieldLogic.IsNodeHarvestEligible(
                node.use, node.layerId, innermostLayerId, node.sp, node.spMax);

            if (!Entries.TryGetValue(key, out var entry))
                entry = new CollectorEntry();

            entry.LayerId = node.layerId;
            entry.Active = active;
            Entries[key] = entry;
        }

        internal static void ClearInactive(int starIndex, int innermostLayerId)
        {
            var remove = new List<long>();
            foreach (var pair in Entries)
            {
                if ((pair.Key >> 32) != (uint)starIndex)
                    continue;
                if (pair.Value.LayerId != innermostLayerId)
                    remove.Add(pair.Key);
            }

            for (int i = 0; i < remove.Count; i++)
                Entries.Remove(remove[i]);
        }

        internal static bool TryGet(int starIndex, int nodeId, out CollectorEntry entry)
        {
            return Entries.TryGetValue(Key(starIndex, nodeId), out entry);
        }

        internal static void SetProgress(int starIndex, int nodeId, int progress)
        {
            long key = Key(starIndex, nodeId);
            if (!Entries.TryGetValue(key, out var entry))
                return;

            entry.Progress = progress;
            Entries[key] = entry;
        }
    }
}