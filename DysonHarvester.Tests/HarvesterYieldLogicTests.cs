using DysonHarvester;
using Xunit;

namespace DysonHarvester.Tests
{
    public class HarvesterYieldLogicTests
    {
        [Fact]
        public void CalculatePerNodeYield_MainSeqStar_produces_hydrogen_and_deuterium()
        {
            var yield = HarvesterYieldLogic.CalculatePerNodeYield(
                HarvesterYieldLogic.StarClass.MainSequence,
                1f);

            Assert.True(yield.Hydrogen > 0);
            Assert.True(yield.Deuterium >= 0);
            Assert.Equal(0, yield.FireIce);
        }

        [Fact]
        public void CalculatePerNodeYield_BlackHole_includes_exotic_materials()
        {
            var yield = HarvesterYieldLogic.CalculatePerNodeYield(
                HarvesterYieldLogic.StarClass.BlackHole,
                1f);

            Assert.True(yield.FireIce > 0);
            Assert.True(yield.UnipolarMagnet > 0);
        }

        [Fact]
        public void IsNodeHarvestEligible_requires_innermost_layer_and_completion()
        {
            Assert.True(HarvesterYieldLogic.IsNodeHarvestEligible(true, 2, 2, 100, 100));
            Assert.False(HarvesterYieldLogic.IsNodeHarvestEligible(true, 2, 3, 100, 100));
            Assert.False(HarvesterYieldLogic.IsNodeHarvestEligible(true, 2, 2, 50, 100));
        }

        [Fact]
        public void ScaleYield_multiplies_by_node_count()
        {
            var perNode = new HarvesterYieldLogic.HarvestYield { Hydrogen = 10, Deuterium = 2 };
            var total = HarvesterYieldLogic.ScaleYield(perNode, 3);
            Assert.Equal(30, total.Hydrogen);
            Assert.Equal(6, total.Deuterium);
        }
    }
}