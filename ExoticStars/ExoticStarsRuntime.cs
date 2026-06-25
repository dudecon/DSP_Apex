using System.Collections.Generic;

namespace ExoticStars
{
    internal static class ExoticStarsRuntime
    {
        internal static readonly HashSet<int> DiscoveredStarIds = new HashSet<int>();
        internal static readonly Dictionary<int, float> TelescopeScanProgress = new Dictionary<int, float>();
        internal static int TelescopePanelCount;

        internal static void MarkDiscovered(int starId)
        {
            if (starId > 0)
                DiscoveredStarIds.Add(starId);
        }

        internal static void TickTelescopeScan(StarData star, int panelCount)
        {
            if (star == null)
                return;

            TelescopePanelCount = panelCount;
            if (!TelescopeScanProgress.TryGetValue(star.id, out float progress))
                progress = 0f;

            progress += ExoticStarLogic.ScanProgressIncrement(panelCount);
            TelescopeScanProgress[star.id] = progress;

            if (ExoticStarLogic.DiscoverWithTelescope(panelCount, progress) && ExoticStarLogic.IsExoticStar(star))
                MarkDiscovered(star.id);
        }
    }
}