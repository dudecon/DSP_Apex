using Xunit;

namespace FaunaNomads.Tests
{
    public class FaunaNomadsLogicTests
    {
        [Fact]
        public void NomadAgentsForDeficit_scales_with_shortage()
        {
            Assert.Equal(0, FaunaNomadLogic.NomadAgentsForDeficit(0));
            Assert.Equal(2, FaunaNomadLogic.NomadAgentsForDeficit(200));
            Assert.Equal(16, FaunaNomadLogic.NomadAgentsForDeficit(5000));
        }

        [Fact]
        public void NomadRange_grows_with_herd_size()
        {
            Assert.Equal(100f, FaunaNomadLogic.NomadRange(0));
            Assert.Equal(130f, FaunaNomadLogic.NomadRange(2));
        }
    }
}