namespace ZeroGProduction
{
    internal static class ZeroGContext
    {
        internal static bool TryResolveAssemblerContext(
            AssemblerComponent assembler,
            out PlanetFactory factory,
            out int protoId,
            out bool hasRing,
            out bool hasStation)
        {
            factory = null;
            protoId = 0;
            hasRing = false;
            hasStation = false;

            if (assembler.id == 0)
                return false;

            var data = GameMain.data;
            if (data?.factories == null)
                return false;

            for (int f = 0; f < data.factoryCount; f++)
            {
                var candidate = data.factories[f];
                if (candidate?.factorySystem?.assemblerPool == null)
                    continue;

                var pool = candidate.factorySystem.assemblerPool;
                if (assembler.id <= 0 || assembler.id >= pool.Length)
                    continue;

                if (pool[assembler.id].id != assembler.id)
                    continue;

                factory = candidate;
                if (assembler.entityId > 0 && assembler.entityId < candidate.entityPool.Length)
                    protoId = candidate.entityPool[assembler.entityId].protoId;

                CountOrbitalStructures(candidate, out hasRing, out hasStation);
                return true;
            }

            return false;
        }

        private static void CountOrbitalStructures(PlanetFactory factory, out bool hasRing, out bool hasStation)
        {
            hasRing = false;
            hasStation = false;

            var power = factory.powerSystem;
            if (power?.genPool != null)
            {
                for (int i = 1; i < power.genCursor; i++)
                {
                    if (power.genPool[i].id != 0 && power.genPool[i].gamma)
                    {
                        hasRing = true;
                        break;
                    }
                }
            }

            var transport = factory.transport;
            if (transport?.stationPool != null)
            {
                for (int i = 1; i < transport.stationCursor; i++)
                {
                    var station = transport.stationPool[i];
                    if (station != null && station.isStellar)
                    {
                        hasStation = true;
                        break;
                    }
                }
            }
        }
    }
}