using Xunit;

namespace SubterraneanConstruction.Tests
{
    public class SubterraneanLogicTests
    {
        [Fact]
        public void ApplyLayeredYield_uses_efficiency_without_double_counting_mantle_bonus()
        {
            uint result = SubterraneanLogic.ApplyLayeredYield(100, 4, 10, 2);
            Assert.True(result > 100);
            Assert.Equal(100u, SubterraneanLogic.ApplyLayeredYield(100, 0, 0, 0));
        }

        [Fact]
        public void LayeredMineEfficiency_includes_depth_and_city_layers()
        {
            float shallow = SubterraneanLogic.LayeredMineEfficiency(2, 0);
            float deep = SubterraneanLogic.LayeredMineEfficiency(12, 3);
            Assert.True(deep > shallow);
        }
    }
}