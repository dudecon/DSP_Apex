namespace FaunaNomads
{
    internal static class FaunaNomadsContext
    {
        internal static int ScanResourceDeficit()
        {
            var data = GameMain.data;
            if (data?.mainPlayer?.package == null)
                return 0;

            var package = data.mainPlayer.package;
            int ironId = 1001;
            int copperId = 1002;
            int target = 800;

            int ironDeficit = target - package.GetItemCount(ironId);
            int copperDeficit = target - package.GetItemCount(copperId);
            return System.Math.Max(0, ironDeficit) + System.Math.Max(0, copperDeficit);
        }
    }
}