namespace OrbitalRings
{
    internal static class OrbitalRingCounter
    {
        internal static int CountRingFrames(PlanetFactory factory)
        {
            if (factory == null)
                return 0;

            int frames = 0;

            var transport = factory.transport;
            if (transport?.stationPool != null)
            {
                int limit = transport.stationCursor;
                for (int i = 1; i < limit; i++)
                {
                    var station = transport.stationPool[i];
                    if (station == null || station.id == 0)
                        continue;

                    if (station.isStellar)
                        frames++;
                }
            }

            var power = factory.powerSystem;
            if (power?.genPool != null)
            {
                int limit = power.genCursor;
                for (int i = 1; i < limit; i++)
                {
                    var gen = power.genPool[i];
                    if (gen.id == 0)
                        continue;

                    if (gen.gamma)
                        frames++;
                }
            }

            if (factory.entityPool != null)
            {
                int limit = factory.entityCursor;
                for (int i = 1; i < limit; i++)
                {
                    var entity = factory.entityPool[i];
                    if (entity.id == 0)
                        continue;

                    if (OrbitalRingIds.IsRingProto(entity.protoId))
                        frames++;
                }
            }

            return frames;
        }

        internal static int CountSwarmNodes(PlanetFactory factory)
        {
            if (factory == null)
                return 0;

            int swarms = 0;

            if (factory.transport?.stationPool != null)
            {
                int limit = factory.transport.stationCursor;
                for (int i = 1; i < limit; i++)
                {
                    var station = factory.transport.stationPool[i];
                    if (station == null || station.id == 0)
                        continue;

                    if (station.isCollector)
                        swarms++;
                }
            }

            if (factory.entityPool != null)
            {
                int limit = factory.entityCursor;
                for (int i = 1; i < limit; i++)
                {
                    var entity = factory.entityPool[i];
                    if (entity.id == 0)
                        continue;

                    if (OrbitalRingIds.IsSwarmProto(entity.protoId))
                        swarms++;
                }
            }

            return swarms;
        }
    }
}