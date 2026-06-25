using System;

namespace DspApex.Common
{
    /// <summary>Pure proto ID sequencing helpers shared across suite packs.</summary>
    public static class ProtoIdAllocator
    {
        public static int FindMaxId(int[] ids)
        {
            if (ids == null || ids.Length == 0)
                return 0;

            int maxId = 0;
            for (int i = 0; i < ids.Length; i++)
            {
                if (ids[i] > maxId)
                    maxId = ids[i];
            }

            return maxId;
        }

        public static int[] AssignSequentialIds(int startAfter, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            var ids = new int[count];
            int next = startAfter;
            for (int i = 0; i < count; i++)
                ids[i] = ++next;

            return ids;
        }
    }
}