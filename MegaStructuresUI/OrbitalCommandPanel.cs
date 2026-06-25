namespace MegaStructuresUI
{
    /// <summary>Sphere production / orbital command interface helpers.</summary>
    internal static class OrbitalCommandPanel
    {
        internal static void OnDysonEditorOpened(UIDysonEditor editor)
        {
            if (editor == null || GameMain.localStar == null)
                return;

            OrbitalCommandInstaller.EnsureInstalled(editor);

            int starId = GameMain.localStar.id;
            var alloc = OrbitalCommandState.GetOrCreate(starId);
            var sphere = GameMain.data?.dysonSpheres?[GameMain.localStar.index];
            if (sphere == null)
                return;

            if (alloc.SphereSections <= 0)
                alloc.SphereSections = sphere.layerCount;

            OrbitalCommandStatsLogic.ApplyComputedStats(alloc, OrbitalCommandStatsLogic.ComputeStats(alloc));
            alloc.PowerOutputMw = System.Math.Max(alloc.PowerOutputMw, sphere.energyGenCurrentTick * 60e-6f);
            alloc.HarvestRate = System.Math.Max(alloc.HarvestRate, sphere.autoNodeCount * 0.5f);

            OrbitalCommandInstaller.RefreshStats(starId);

            Plugin.Log.LogInfo(
                $"OrbitalCommand: star {starId} — {alloc.TotalModules} modules, " +
                $"{alloc.SphereSections} layers, {alloc.PowerOutputMw:F1} MW/tick harvest {alloc.HarvestRate:F1}");
        }

        internal static void AllocateModule(int starId, string category, int delta) =>
            OrbitalCommandState.AllocateModule(starId, category, delta);
    }
}