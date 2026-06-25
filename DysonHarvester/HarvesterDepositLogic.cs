using System;

namespace DysonHarvester
{
    /// <summary>Pure deposit routing for node harvest output.</summary>
    public static class HarvesterDepositLogic
    {
        public enum DepositTarget
        {
            None = 0,
            CollectorStation = 1,
            FactoryInsert = 2,
            PlayerPackage = 3
        }

        public const int TicksPerNodeDeposit = 60;

        public static DepositTarget SelectTarget(bool hasCollectorStation, bool hasFactoryEntity, bool hasPlayer)
        {
            if (hasCollectorStation)
                return DepositTarget.CollectorStation;
            if (hasFactoryEntity)
                return DepositTarget.FactoryInsert;
            if (hasPlayer)
                return DepositTarget.PlayerPackage;
            return DepositTarget.None;
        }

        public static bool ShouldDeposit(int progress, int threshold) =>
            threshold > 0 && progress >= threshold;

        public static int AdvanceProgress(int progress, int speed, int threshold, out int remainder)
        {
            progress += Math.Max(1, speed);
            if (progress < threshold)
            {
                remainder = progress;
                return 0;
            }

            remainder = progress - threshold;
            return 1;
        }

        public static bool IsCollectorStationCandidate(bool isCollector, bool isStellar, int stationId) =>
            stationId > 0 && (isCollector || isStellar);

        public static bool IsLogisticsStationCandidate(int stationId, int entityId) =>
            stationId > 0 && entityId > 0;

        public static int ExecuteDepositRoute(Func<int> tryCollector, Func<int> tryLogistics, Func<int> tryPlayer)
        {
            int deposited = tryCollector();
            if (deposited > 0)
                return deposited;

            deposited = tryLogistics();
            if (deposited > 0)
                return deposited;

            return tryPlayer();
        }
    }
}