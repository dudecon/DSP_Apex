using System.Collections.Generic;

namespace SystemMover
{
    internal static class SystemMoverRuntime
    {
        internal static readonly Dictionary<int, long> RelocationCosts = new Dictionary<int, long>();
        internal static readonly HashSet<int> RegisteredStars = new HashSet<int>();

        internal static void RegisterStar(StarData star)
        {
            if (star == null)
                return;

            bool isBlackHole = star.type == EStarType.BlackHole;
            RelocationCosts[star.id] = SystemMoverLogic.ComputeRelocationCost(star, isBlackHole);
            RegisteredStars.Add(star.id);
        }

        internal static bool TryGetRelocationCost(int starId, out long cost) =>
            RelocationCosts.TryGetValue(starId, out cost);

        internal static float GetAnchorEfficiency(StarData star)
        {
            if (star == null)
                return 1f;

            if (star.type == EStarType.BlackHole)
                return 0.25f;

            if (star.type == EStarType.NeutronStar)
                return 0.5f;

            return 1f;
        }
    }
}