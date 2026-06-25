using System;

namespace DspApex.Common
{
    /// <summary>Game-type adapters for proto ID extraction (no registration side effects).</summary>
    public static class ProtoGameAdapter
    {
        public static int[] ExtractProtoIds(Proto[] protos)
        {
            if (protos == null || protos.Length == 0)
                return Array.Empty<int>();

            var ids = new int[protos.Length];
            int count = 0;
            for (int i = 0; i < protos.Length; i++)
            {
                var proto = protos[i];
                if (proto == null)
                    continue;

                ids[count++] = proto.ID;
            }

            if (count == 0)
                return Array.Empty<int>();

            if (count == ids.Length)
                return ids;

            var trimmed = new int[count];
            Array.Copy(ids, trimmed, count);
            return trimmed;
        }

        public static int FindMaxProtoId(Proto[] protos) =>
            ProtoIdAllocator.FindMaxId(ExtractProtoIds(protos));
    }
}