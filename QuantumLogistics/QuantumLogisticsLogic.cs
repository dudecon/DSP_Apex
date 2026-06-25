using System;

namespace QuantumLogistics
{
    public static class QuantumLogisticsLogic
    {
        public const int WarperCostPerTeleport = 1;
        public const int StargateWarperCost = 50;
        public const int MinTowerLevelForTeleport = 3;
        public const int MinModulesForStargate = 8;

        public static bool CanTeleport(int towerLevel) => towerLevel >= MinTowerLevelForTeleport;

        public static bool CanBuildStargate(int orbitalModules) => orbitalModules >= MinModulesForStargate;

        public static int TeleportWarperCost(int itemCount) =>
            Math.Max(0, itemCount) * WarperCostPerTeleport;

        public static int StargateTransferCost(int itemCount) =>
            StargateWarperCost + TeleportWarperCost(itemCount);
    }
}