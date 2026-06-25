using DysonHarvester;
using Xunit;

namespace DysonHarvester.Tests
{
    public class HarvesterDepositLogicTests
    {
        [Fact]
        public void SelectTarget_prefers_collector_station()
        {
            var target = HarvesterDepositLogic.SelectTarget(true, true, true);
            Assert.Equal(HarvesterDepositLogic.DepositTarget.CollectorStation, target);
        }

        [Fact]
        public void SelectTarget_falls_back_to_factory_insert()
        {
            var target = HarvesterDepositLogic.SelectTarget(false, true, true);
            Assert.Equal(HarvesterDepositLogic.DepositTarget.FactoryInsert, target);
        }

        [Fact]
        public void SelectTarget_falls_back_to_player_package()
        {
            var target = HarvesterDepositLogic.SelectTarget(false, false, true);
            Assert.Equal(HarvesterDepositLogic.DepositTarget.PlayerPackage, target);
        }

        [Fact]
        public void AdvanceProgress_emits_pulse_when_threshold_reached()
        {
            int pulses = HarvesterDepositLogic.AdvanceProgress(55, 10, 60, out int remainder);
            Assert.Equal(1, pulses);
            Assert.Equal(5, remainder);
        }

        [Fact]
        public void IsCollectorStationCandidate_requires_collector_or_stellar()
        {
            Assert.True(HarvesterDepositLogic.IsCollectorStationCandidate(true, false, 1));
            Assert.True(HarvesterDepositLogic.IsCollectorStationCandidate(false, true, 1));
            Assert.False(HarvesterDepositLogic.IsCollectorStationCandidate(false, false, 1));
            Assert.False(HarvesterDepositLogic.IsCollectorStationCandidate(true, true, 0));
        }

        [Fact]
        public void IsLogisticsStationCandidate_requires_entity()
        {
            Assert.True(HarvesterDepositLogic.IsLogisticsStationCandidate(2, 10));
            Assert.False(HarvesterDepositLogic.IsLogisticsStationCandidate(2, 0));
            Assert.False(HarvesterDepositLogic.IsLogisticsStationCandidate(0, 10));
        }
    }
}