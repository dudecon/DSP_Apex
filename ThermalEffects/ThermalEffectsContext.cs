namespace ThermalEffects
{
    internal static class ThermalEffectsContext
    {
        internal static bool TryResolvePlanetTemperature(AssemblerComponent assembler, out float temperature)
        {
            temperature = 300f;
            if (assembler.id == 0)
                return false;

            var data = GameMain.data;
            if (data?.factories == null)
                return false;

            for (int f = 0; f < data.factoryCount; f++)
            {
                var factory = data.factories[f];
                if (factory?.factorySystem?.assemblerPool == null)
                    continue;

                var pool = factory.factorySystem.assemblerPool;
                if (assembler.id <= 0 || assembler.id >= pool.Length)
                    continue;

                if (pool[assembler.id].id != assembler.id)
                    continue;

                var planet = factory.planet;
                if (planet == null)
                    return false;

                temperature = EstimatePlanetTemperature(planet);
                return true;
            }

            return false;
        }

        internal static bool TryResolvePlanetTemperature(MinerComponent miner, out float temperature)
        {
            temperature = 300f;
            if (miner.id == 0)
                return false;

            var data = GameMain.data;
            if (data?.factories == null)
                return false;

            for (int f = 0; f < data.factoryCount; f++)
            {
                var factory = data.factories[f];
                if (factory?.factorySystem?.minerPool == null)
                    continue;

                var pool = factory.factorySystem.minerPool;
                if (miner.id <= 0 || miner.id >= pool.Length)
                    continue;

                if (pool[miner.id].id != miner.id)
                    continue;

                var planet = factory.planet;
                if (planet == null)
                    return false;

                temperature = EstimatePlanetTemperature(planet);
                return true;
            }

            return false;
        }

        private static float EstimatePlanetTemperature(PlanetData planet)
        {
            if (planet.type == EPlanetType.Vocano)
                return 650f;

            if (planet.type == EPlanetType.Ice)
                return 150f;

            if (planet.type == EPlanetType.Desert)
                return 420f;

            return 300f;
        }
    }
}