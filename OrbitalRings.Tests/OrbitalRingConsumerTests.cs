using OrbitalRings;
using Xunit;

namespace OrbitalRings.Tests
{
    public class OrbitalRingConsumerTests
    {
        [Fact]
        public void ApplyRelayBonus_matches_power_relay_patch_path()
        {
            const int planetId = 42;
            OrbitalRingRuntimeState.SetRelayBonus(planetId, 1.2f);

            float eta = OrbitalRingLogic.ApplyRelayBonus(
                0.5f,
                OrbitalRingRuntimeState.GetRelayBonus(planetId));

            Assert.Equal(0.6f, eta, 3);
        }

        [Fact]
        public void ApplyHarvestBonus_matches_station_collection_patch_path()
        {
            const int planetId = 7;
            OrbitalRingRuntimeState.SetHarvestBonus(planetId, 1.25f);

            float power = OrbitalRingLogic.ApplyHarvestBonus(
                100f,
                OrbitalRingRuntimeState.GetHarvestBonus(planetId));

            Assert.Equal(125f, power);
        }

        [Fact]
        public void ApplySensorSearchBonus_matches_missile_search_patch_path()
        {
            const int planetId = 99;
            OrbitalRingRuntimeState.SetSensorCoverage(planetId, 0.5f);

            float searchRange = OrbitalRingLogic.ApplySensorSearchBonus(
                80f,
                OrbitalRingRuntimeState.GetSensorCoverage(planetId));

            Assert.Equal(120f, searchRange);
        }
    }
}