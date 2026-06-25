namespace QuantumLogistics
{
    internal static class QuantumLogisticsRuntime
    {
        internal static int TeleportCount;
        internal static int WarperSpent;
        internal static int StargateCount;
        internal static int StargateTransfers;

        internal static bool TryTeleport(int towerLevel, int itemCount, out int warperCost)
        {
            warperCost = 0;
            if (!QuantumLogisticsLogic.CanTeleport(towerLevel) || itemCount <= 0)
                return false;

            warperCost = QuantumLogisticsLogic.TeleportWarperCost(itemCount);
            TeleportCount += itemCount;
            WarperSpent += warperCost;
            return true;
        }

        internal static bool TryStargateTransfer(int orbitalModules, int itemCount, out int warperCost)
        {
            warperCost = 0;
            if (!QuantumLogisticsLogic.CanBuildStargate(orbitalModules) || itemCount <= 0)
                return false;

            warperCost = QuantumLogisticsLogic.StargateTransferCost(itemCount);
            StargateTransfers += itemCount;
            WarperSpent += warperCost;
            return true;
        }

        internal static void RegisterStargate() => StargateCount++;
    }
}