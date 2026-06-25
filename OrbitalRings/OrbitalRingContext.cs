namespace OrbitalRings
{
    internal static class OrbitalRingContext
    {
        internal static bool TryResolveGeneratorPlanet(PowerGeneratorComponent generator, out int planetId)
        {
            planetId = 0;
            if (generator.id == 0)
                return false;

            var data = GameMain.data;
            if (data?.factories == null)
                return false;

            for (int f = 0; f < data.factoryCount; f++)
            {
                var factory = data.factories[f];
                var pool = factory?.powerSystem?.genPool;
                if (pool == null || generator.id <= 0 || generator.id >= pool.Length)
                    continue;

                if (pool[generator.id].id != generator.id)
                    continue;

                if (factory.planet == null)
                    return false;

                planetId = factory.planet.id;
                return true;
            }

            return false;
        }

        internal static int ResolvePlanetIdFromMissile(GeneralMissile missile)
        {
            var galaxy = GameMain.data?.galaxy;
            if (galaxy == null)
                return 0;

            int astroId = missile.caster.astroId;
            if (astroId == 0)
                astroId = missile.nearAstroId;
            if (astroId == 0)
                return 0;

            var planet = galaxy.PlanetByAstroId(astroId);
            return planet?.id ?? 0;
        }
    }
}