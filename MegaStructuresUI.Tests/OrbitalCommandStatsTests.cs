using Xunit;

namespace MegaStructuresUI.Tests
{
    public class OrbitalCommandStatsTests
    {
        [Fact]
        public void ComputeStats_includes_transmutation_and_beams_from_weapons()
        {
            var alloc = new ModuleAllocation
            {
                WeaponFrames = 4,
                ProductionModules = 2,
                RingModules = 1,
                StationModules = 1,
                ShipModules = 2
            };

            var stats = OrbitalCommandStatsLogic.ComputeStats(alloc);

            Assert.Equal(10.5f, stats.TransmutationRate);
            Assert.Equal(1000f, stats.BeamDamageRate);
            Assert.Equal(12f + 8f + 12f, stats.PowerOutputMw);
        }

        [Fact]
        public void AllocateModule_updates_transmutation_via_shipped_state_path()
        {
            const int starId = 2001;
            OrbitalCommandState.Reset(starId);

            OrbitalCommandState.AllocateModule(starId, "weapon", 2);
            OrbitalCommandState.AllocateModule(starId, "production", 4);

            var alloc = OrbitalCommandState.GetOrCreate(starId);
            Assert.Equal(6f, alloc.TransmutationRate);
            Assert.Equal(500f, alloc.BeamDamageRate);
            Assert.Contains("Transmute: 6.0", OrbitalCommandStatsLogic.FormatStatsText(alloc));
            Assert.Contains("Beams: 500", OrbitalCommandStatsLogic.FormatStatsText(alloc));
        }
    }
}