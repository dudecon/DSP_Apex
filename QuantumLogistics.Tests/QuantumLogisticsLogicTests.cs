using Xunit;

namespace QuantumLogistics.Tests
{
    public class QuantumLogisticsLogicTests
    {
        [Fact]
        public void CanTeleport_requires_tower_level()
        {
            Assert.False(QuantumLogisticsLogic.CanTeleport(2));
            Assert.True(QuantumLogisticsLogic.CanTeleport(3));
        }

        [Fact]
        public void StargateTransfer_includes_base_and_per_item_cost()
        {
            Assert.Equal(52, QuantumLogisticsLogic.StargateTransferCost(2));
        }

        [Fact]
        public void CanBuildStargate_requires_orbital_modules()
        {
            Assert.False(QuantumLogisticsLogic.CanBuildStargate(4));
            Assert.True(QuantumLogisticsLogic.CanBuildStargate(8));
        }
    }
}