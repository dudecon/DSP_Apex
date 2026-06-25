namespace DysonHarvester
{
    internal static class HarvesterDepositService
    {
        internal static int DepositItem(DysonSphere sphere, int itemId, int count)
        {
            if (sphere == null || count <= 0)
                return 0;

            var gameData = sphere.gameData;
            if (gameData == null)
                return 0;

            return HarvesterDepositLogic.ExecuteDepositRoute(
                () => TryDepositCollectorStation(gameData, sphere.starData, itemId, count),
                () => TryDepositLogisticsStation(gameData, sphere.starData, itemId, count),
                () => TryDepositPlayerPackage(itemId, count));
        }

        private static int TryDepositCollectorStation(GameData gameData, StarData star, int itemId, int count)
        {
            if (star?.planets == null)
                return 0;

            for (int p = 0; p < star.planetCount; p++)
            {
                var planet = star.planets[p];
                if (planet?.factory?.transport == null)
                    continue;

                var transport = planet.factory.transport;
                int limit = transport.stationCursor;
                var pool = transport.stationPool;
                if (pool == null)
                    continue;

                for (int i = 1; i < limit; i++)
                {
                    var station = pool[i];
                    if (station == null || station.id == 0)
                        continue;

                    if (!HarvesterDepositLogic.IsCollectorStationCandidate(
                            station.isCollector,
                            station.isStellar,
                            station.id))
                        continue;

                    int added = station.AddItem(itemId, count, 0);
                    if (added > 0)
                        return added;
                }
            }

            return 0;
        }

        private static int TryDepositLogisticsStation(GameData gameData, StarData star, int itemId, int count)
        {
            if (star?.planets == null)
                return 0;

            for (int p = 0; p < star.planetCount; p++)
            {
                var planet = star.planets[p];
                if (planet?.factory?.transport == null)
                    continue;

                var transport = planet.factory.transport;
                int limit = transport.stationCursor;
                var pool = transport.stationPool;
                if (pool == null)
                    continue;

                for (int i = 1; i < limit; i++)
                {
                    var station = pool[i];
                    if (station == null || station.id == 0)
                        continue;

                    if (!HarvesterDepositLogic.IsLogisticsStationCandidate(station.id, station.entityId))
                        continue;

                    int added = station.AddItem(itemId, count, 0);
                    if (added > 0)
                        return added;
                }
            }

            return 0;
        }

        private static int TryDepositPlayerPackage(int itemId, int count)
        {
            var player = GameMain.mainPlayer;
            if (player == null)
                return 0;

            int added = player.TryAddItemToPackage(itemId, count, 0, false, 0, false);
            return added > 0 ? added : 0;
        }
    }
}