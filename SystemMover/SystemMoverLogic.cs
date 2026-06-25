using System;

namespace SystemMover
{public static class SystemMoverLogic { public const long BaseWarpCost = 1_000_000; public static long ComputeRelocationCost(StarData star, bool isBlackHole) { float factor = isBlackHole ? 0.25f : 1f; return (long)(BaseWarpCost * star.mass * factor); } public static bool CanRelocate(StarData star, int warperCount) => star != null && warperCount >= 100; }
}
