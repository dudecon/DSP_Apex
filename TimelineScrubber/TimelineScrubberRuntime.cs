using System.Collections.Generic;

namespace TimelineScrubber
{
    internal static class TimelineScrubberRuntime
    {
        internal static bool FastForward;
        internal static int BranchDepth;
        internal static long LastProductionSnapshot;
        internal static long ExtrapolatedProduction;

        internal static readonly List<long> BranchTicks = new List<long>();

        internal static void RecordBranch(long gameTick)
        {
            bool canRecord = TimelineScrubberLogic.CanRewind(BranchDepth);
            if (canRecord)
                BranchTicks.Add(gameTick);

            BranchDepth = TimelineScrubberLogic.NextBranchDepth(BranchDepth, canRecord);
        }

        internal static long AdvanceGameTick(long gameTick)
        {
            if (FastForward)
                TickExtrapolation(gameTick, 1);

            return TimelineScrubberLogic.AdvanceTick(gameTick, FastForward);
        }

        internal static void TickExtrapolation(long gameTick, long productionRate)
        {
            if (!FastForward)
                return;

            ExtrapolatedProduction = TimelineScrubberLogic.InterpolateProduction(
                LastProductionSnapshot,
                productionRate,
                TimelineScrubberLogic.FastForwardScale);

            if (gameTick % 3600 == 0)
                LastProductionSnapshot = ExtrapolatedProduction;
        }
    }
}