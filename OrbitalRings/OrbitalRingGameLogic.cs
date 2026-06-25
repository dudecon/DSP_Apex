namespace OrbitalRings
{
    internal static class OrbitalRingGameLogic
    {
        internal static int MapPlanetKind(EPlanetType type)
        {
            if (type == EPlanetType.Gas)
                return OrbitalRingLogic.PlanetKindGas;
            if (type == EPlanetType.None)
                return OrbitalRingLogic.PlanetKindNone;
            return OrbitalRingLogic.PlanetKindRocky;
        }

        internal static float GetPowerRelayMultiplier(PlanetData planet, int ringFrameCount) =>
            planet == null
                ? 1f
                : OrbitalRingLogic.GetPowerRelayMultiplier(MapPlanetKind(planet.type), ringFrameCount);

        internal static float GetHarvestMultiplier(PlanetData planet, int swarmCount) =>
            planet == null
                ? 1f
                : OrbitalRingLogic.GetHarvestMultiplier(MapPlanetKind(planet.type), swarmCount);
    }
}