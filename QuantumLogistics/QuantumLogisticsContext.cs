namespace QuantumLogistics
{
    internal static class QuantumLogisticsContext
    {
        public const int PlanetaryLogisticsTowerProto = 2101;
        public const int InterstellarLogisticsProto = 2102;

        internal static bool IsLogisticsTowerStorage(StorageComponent storage)
        {
            if (storage.id == 0 || storage.grids == null || storage.grids.Length < 4)
                return false;

            var factory = ResolveFactory(storage);
            if (factory == null || storage.entityId <= 0 || storage.entityId >= factory.entityPool.Length)
                return false;

            int protoId = factory.entityPool[storage.entityId].protoId;
            return protoId == PlanetaryLogisticsTowerProto || protoId == InterstellarLogisticsProto;
        }

        internal static int CountOrbitalModules(DysonSphere sphere)
        {
            if (sphere?.layersIdBased == null)
                return 0;

            int modules = 0;
            var layers = sphere.layersIdBased;
            for (int i = 0; i < layers.Length; i++)
            {
                var layer = layers[i];
                if (layer == null || layer.id <= 0)
                    continue;

                modules += layer.shellCursor + layer.frameCursor;
            }

            return modules;
        }

        private static PlanetFactory ResolveFactory(StorageComponent storage)
        {
            var data = GameMain.data;
            if (data?.factories == null)
                return null;

            for (int f = 0; f < data.factoryCount; f++)
            {
                var factory = data.factories[f];
                if (factory?.factoryStorage?.storagePool == null)
                    continue;

                var pool = factory.factoryStorage.storagePool;
                if (storage.id <= 0 || storage.id >= pool.Length)
                    continue;

                if (pool[storage.id].id == storage.id)
                    return factory;
            }

            return null;
        }
    }
}