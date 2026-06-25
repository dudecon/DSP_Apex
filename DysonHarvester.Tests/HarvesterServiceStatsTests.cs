using DysonHarvester;
using Xunit;

namespace DysonHarvester.Tests
{
    public class HarvesterServiceStatsTests
    {
        [Fact]
        public void ShouldUpdateProductRegister_false_when_no_pulses()
        {
            var deposited = default(HarvesterYieldLogic.HarvestYield);
            Assert.False(HarvesterYieldLogic.ShouldUpdateProductRegister(deposited));
        }

        [Fact]
        public void AccumulatePulseDeposits_skipped_for_zero_pulses()
        {
            var perNode = HarvesterYieldLogic.CalculatePerNodeYield(
                HarvesterYieldLogic.StarClass.MainSequence, 1f);
            var deposited = default(HarvesterYieldLogic.HarvestYield);

            var result = HarvesterYieldLogic.AccumulatePulseDeposits(perNode, 0, deposited);

            Assert.False(result.HasAny);
            Assert.False(HarvesterYieldLogic.ShouldUpdateProductRegister(result));
        }

        [Fact]
        public void AccumulatePulseDeposits_matches_per_pulse_not_node_cursor()
        {
            var perNode = HarvesterYieldLogic.CalculatePerNodeYield(
                HarvesterYieldLogic.StarClass.MainSequence, 1f);
            var deposited = default(HarvesterYieldLogic.HarvestYield);

            deposited = HarvesterYieldLogic.AccumulatePulseDeposits(perNode, 2, deposited);

            Assert.True(HarvesterYieldLogic.ShouldUpdateProductRegister(deposited));
            Assert.Equal(perNode.Hydrogen * 2, deposited.Hydrogen);
            Assert.Equal(perNode.Deuterium * 2, deposited.Deuterium);
        }

        [Fact]
        public void Pulse_loop_uses_shipped_accumulation_path()
        {
            var perNode = HarvesterYieldLogic.CalculatePerNodeYield(
                HarvesterYieldLogic.StarClass.MainSequence, 1f);
            var deposited = default(HarvesterYieldLogic.HarvestYield);

            int pulses = HarvesterDepositLogic.AdvanceProgress(55, 10, 60, out _);
            deposited = HarvesterYieldLogic.AccumulatePulseDeposits(perNode, pulses, deposited);

            pulses = HarvesterDepositLogic.AdvanceProgress(10, 5, 60, out _);
            deposited = HarvesterYieldLogic.AccumulatePulseDeposits(perNode, pulses, deposited);

            pulses = HarvesterDepositLogic.AdvanceProgress(58, 10, 60, out _);
            deposited = HarvesterYieldLogic.AccumulatePulseDeposits(perNode, pulses, deposited);

            Assert.True(HarvesterYieldLogic.ShouldUpdateProductRegister(deposited));
            Assert.Equal(perNode.Hydrogen * 2, deposited.Hydrogen);
        }
    }
}