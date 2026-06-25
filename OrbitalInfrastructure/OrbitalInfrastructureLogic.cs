using System;

namespace OrbitalInfrastructure
{
    public static class OrbitalInfrastructureLogic
    {
        public const int MaxStationModules = 64;
        public const int ElevatorThroughputTier = 4;

        public static bool CanBuildSpaceElevator(PlanetData planet) =>
            planet != null && planet.type != EPlanetType.Gas && planet.realRadius > 100f;

        public static int GetElevatorThroughput(int towerLevel) =>
            1000 * Math.Max(1, Math.Min(towerLevel, ElevatorThroughputTier));

        public static int GetStationModuleSlots(int builtModules) =>
            Math.Min(MaxStationModules, 4 + builtModules * 2);
    }

}
