namespace MegaStructuresUI
{
    /// <summary>Pure allocation-to-stats math for the Orbital Command panel.</summary>
    public static class OrbitalCommandStatsLogic
    {
        public struct ComputedStats
        {
            public float PowerOutputMw;
            public float HarvestRate;
            public float TransmutationRate;
            public float BeamDamageRate;
        }

        public static ComputedStats ComputeStats(ModuleAllocation alloc)
        {
            if (alloc == null)
                return default;

            return new ComputedStats
            {
                PowerOutputMw = alloc.RingModules * 12f + alloc.StationModules * 8f + alloc.ShipModules * 6f,
                HarvestRate = alloc.RingModules * 1.5f + alloc.ProductionModules * 0.5f + alloc.ShipModules * 0.25f,
                TransmutationRate = alloc.WeaponFrames * 2.5f + alloc.ProductionModules * 0.25f,
                BeamDamageRate = alloc.WeaponFrames * 250f
            };
        }

        public static void ApplyComputedStats(ModuleAllocation alloc, ComputedStats stats)
        {
            if (alloc == null)
                return;

            alloc.PowerOutputMw = stats.PowerOutputMw;
            alloc.HarvestRate = stats.HarvestRate;
            alloc.TransmutationRate = stats.TransmutationRate;
            alloc.BeamDamageRate = stats.BeamDamageRate;
        }

        public static string FormatStatsText(ModuleAllocation alloc)
        {
            if (alloc == null)
                return string.Empty;

            var stats = ComputeStats(alloc);
            return
                $"Rings: {alloc.RingModules}  Stations: {alloc.StationModules}  Ships: {alloc.ShipModules}\n" +
                $"Weapons: {alloc.WeaponFrames}  Production: {alloc.ProductionModules}\n" +
                $"Layers: {alloc.SphereSections}  Power: {stats.PowerOutputMw:F1} MW\n" +
                $"Harvest: {stats.HarvestRate:F1}  Transmute: {stats.TransmutationRate:F1}\n" +
                $"Beams: {stats.BeamDamageRate:F0}  Total modules: {alloc.TotalModules}";
        }
    }
}