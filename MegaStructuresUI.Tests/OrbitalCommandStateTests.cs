using MegaStructuresUI;
using Xunit;

namespace MegaStructuresUI.Tests
{
    public class OrbitalCommandStateTests
    {
        [Fact]
        public void AllocateModule_updates_ring_count_via_shipped_path()
        {
            const int starId = 1001;
            OrbitalCommandState.Reset(starId);

            OrbitalCommandState.AllocateModule(starId, "ring", 1);
            OrbitalCommandState.AllocateModule(starId, "ring", 1);
            OrbitalCommandState.AllocateModule(starId, "ring", -1);

            var alloc = OrbitalCommandState.GetOrCreate(starId);
            Assert.Equal(1, alloc.RingModules);
            Assert.Equal(1, alloc.TotalModules);
        }

        [Fact]
        public void TotalModules_sums_all_categories()
        {
            const int starId = 1002;
            OrbitalCommandState.Reset(starId);

            OrbitalCommandState.AllocateModule(starId, "ring", 2);
            OrbitalCommandState.AllocateModule(starId, "station", 1);
            OrbitalCommandState.AllocateModule(starId, "weapon", 3);
            OrbitalCommandState.AllocateModule(starId, "production", 4);

            var alloc = OrbitalCommandState.GetOrCreate(starId);
            alloc.SphereSections = 5;

            Assert.Equal(15, alloc.TotalModules);
            Assert.Equal(10, alloc.RingModules + alloc.StationModules + alloc.WeaponFrames + alloc.ProductionModules);
        }
    }
}