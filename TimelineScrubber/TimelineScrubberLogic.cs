using System;

namespace TimelineScrubber
{
    public static class TimelineScrubberLogic
    {
        public static float FastForwardScale = 4f;
        public const int MaxBranchDepth = 3;

        public static bool CanRewind(int branchDepth) => branchDepth < MaxBranchDepth;

        public static long InterpolateProduction(long last, long rate, float dt) =>
            last + (long)(rate * dt);

        public static long AdvanceTick(long gameTick, bool fastForward) =>
            fastForward ? gameTick + (long)FastForwardScale : gameTick;

        public static int NextBranchDepth(int currentDepth, bool canRecord) =>
            canRecord ? currentDepth + 1 : currentDepth;
    }
}