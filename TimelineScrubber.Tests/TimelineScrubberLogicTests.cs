using Xunit;

namespace TimelineScrubber.Tests
{
    public class TimelineScrubberLogicTests
    {
        [Fact]
        public void InterpolateProduction_advances_by_rate_times_dt()
        {
            long result = TimelineScrubberLogic.InterpolateProduction(100, 10, 4f);
            Assert.Equal(140, result);
        }

        [Fact]
        public void CanRewind_respects_max_branch_depth()
        {
            Assert.True(TimelineScrubberLogic.CanRewind(0));
            Assert.True(TimelineScrubberLogic.CanRewind(2));
            Assert.False(TimelineScrubberLogic.CanRewind(3));
        }

        [Fact]
        public void AdvanceTick_adds_scale_when_fast_forward()
        {
            Assert.Equal(104, TimelineScrubberLogic.AdvanceTick(100, true));
            Assert.Equal(100, TimelineScrubberLogic.AdvanceTick(100, false));
        }

        [Fact]
        public void NextBranchDepth_increments_only_when_recording()
        {
            Assert.Equal(1, TimelineScrubberLogic.NextBranchDepth(0, true));
            Assert.Equal(0, TimelineScrubberLogic.NextBranchDepth(0, false));
            Assert.Equal(3, TimelineScrubberLogic.NextBranchDepth(2, true));
        }
    }
}