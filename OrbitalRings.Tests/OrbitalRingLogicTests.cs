using OrbitalRings;
using Xunit;

namespace OrbitalRings.Tests
{
    public class OrbitalRingLogicTests
    {
        [Fact]
        public void GetHarvestMultiplier_scales_with_swarm_count_on_gas_giant()
        {
            float m0 = OrbitalRingLogic.GetHarvestMultiplier(OrbitalRingLogic.PlanetKindGas, 0);
            float m3 = OrbitalRingLogic.GetHarvestMultiplier(OrbitalRingLogic.PlanetKindGas, 3);
            Xunit.Assert.Equal(1f, m0);
            Xunit.Assert.True(m3 > 1f);
        }

        [Fact]
        public void GetPowerRelayMultiplier_scales_on_rocky_planets()
        {
            float m0 = OrbitalRingLogic.GetPowerRelayMultiplier(OrbitalRingLogic.PlanetKindRocky, 0);
            float m4 = OrbitalRingLogic.GetPowerRelayMultiplier(OrbitalRingLogic.PlanetKindRocky, 4);
            Xunit.Assert.Equal(1f, m0);
            Xunit.Assert.True(m4 > 1f);
        }

        [Fact]
        public void GetSensorCoverage_caps_at_one()
        {
            Xunit.Assert.Equal(1f, OrbitalRingLogic.GetSensorCoverage(16));
            Xunit.Assert.Equal(0.25f, OrbitalRingLogic.GetSensorCoverage(2));
        }

        [Fact]
        public void GetSensorRangeMultiplier_scales_with_coverage()
        {
            Xunit.Assert.Equal(1f, OrbitalRingLogic.GetSensorRangeMultiplier(0f));
            Xunit.Assert.Equal(1.5f, OrbitalRingLogic.GetSensorRangeMultiplier(0.5f));
            Xunit.Assert.Equal(2f, OrbitalRingLogic.GetSensorRangeMultiplier(1f));
        }

        [Fact]
        public void ApplySensorSearchBonus_is_identity_when_coverage_zero()
        {
            Xunit.Assert.Equal(80f, OrbitalRingLogic.ApplySensorSearchBonus(80f, 0f));
        }
    }
}