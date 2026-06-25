using Xunit;

namespace DysonWeapons.Tests
{
    public class DysonWeaponsLogicTests
    {
        [Fact]
        public void TransmuteHydrogenToDeuterium_RequiresPower()
        {
            Assert.Equal(0, DysonWeaponsLogicPure.TransmuteHydrogenToDeuterium(100, 500_000));
            Assert.Equal(5, DysonWeaponsLogicPure.TransmuteHydrogenToDeuterium(50, 2_000_000));
        }

        [Fact]
        public void BeamDamageScalesWithFrameCount()
        {
            Assert.Equal(500f, DysonWeaponsLogicPure.BeamDamagePerFrame(2));
            Assert.Equal(0f, DysonWeaponsLogicPure.BeamDamagePerFrame(0));
        }

        [Fact]
        public void IsFrameComplete_WhenSpSumMeetsMax()
        {
            Assert.True(DysonWeaponsLogicPure.IsFrameComplete(5, 5, 10));
            Assert.False(DysonWeaponsLogicPure.IsFrameComplete(3, 3, 10));
        }
    }
}