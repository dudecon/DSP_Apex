using System.Collections.Generic;

namespace MegaStructuresUI
{
    /// <summary>Tracks module allocation across sphere sections, rings, and stations.</summary>
    public static class OrbitalCommandState
    {
        public static readonly Dictionary<int, ModuleAllocation> Allocations = new Dictionary<int, ModuleAllocation>();

        public static ModuleAllocation GetOrCreate(int starId)
        {
            if (!Allocations.TryGetValue(starId, out var alloc))
            {
                alloc = new ModuleAllocation();
                Allocations[starId] = alloc;
            }

            return alloc;
        }

        public static void Reset(int starId)
        {
            Allocations.Remove(starId);
        }

        public static void AllocateModule(int starId, string category, int delta)
        {
            var alloc = GetOrCreate(starId);
            switch (category)
            {
                case "ring": alloc.RingModules = ClampNonNegative(alloc.RingModules + delta); break;
                case "station": alloc.StationModules = ClampNonNegative(alloc.StationModules + delta); break;
                case "ship": alloc.ShipModules = ClampNonNegative(alloc.ShipModules + delta); break;
                case "weapon": alloc.WeaponFrames = ClampNonNegative(alloc.WeaponFrames + delta); break;
                case "production": alloc.ProductionModules = ClampNonNegative(alloc.ProductionModules + delta); break;
                case "section": alloc.SphereSections = ClampNonNegative(alloc.SphereSections + delta); break;
            }

            OrbitalCommandStatsLogic.ApplyComputedStats(alloc, OrbitalCommandStatsLogic.ComputeStats(alloc));
        }

        private static int ClampNonNegative(int value) => value < 0 ? 0 : value;
    }

    public class ModuleAllocation
    {
        public int RingModules;
        public int StationModules;
        public int ShipModules;
        public int SphereSections;
        public int WeaponFrames;
        public int ProductionModules;
        public float PowerOutputMw;
        public float HarvestRate;
        public float TransmutationRate;
        public float BeamDamageRate;

        public int TotalModules =>
            RingModules + StationModules + ShipModules + SphereSections + WeaponFrames + ProductionModules;
    }
}