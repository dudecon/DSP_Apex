using Xunit;

namespace SelfPropagation.Tests
{
    public class SelfPropagationLogicTests
    {
        [Fact]
        public void ShouldExpand_when_deficit_exceeds_threshold()
        {
            Assert.False(SelfPropagationLogic.ShouldExpand(50));
            Assert.True(SelfPropagationLogic.ShouldExpand(150));
        }

        [Fact]
        public void BlueprintDemand_reduces_by_priority_factor()
        {
            Assert.Equal(50, SelfPropagationLogic.BlueprintDemand(150, 2));
            Assert.Equal(0, SelfPropagationLogic.BlueprintDemand(80, 2));
        }

        [Fact]
        public void ComputeTotalDeficit_sums_positive_entries()
        {
            Assert.Equal(300, SelfPropagationLogic.ComputeTotalDeficit(new[] { 100, 200, -5 }));
        }
    }
}