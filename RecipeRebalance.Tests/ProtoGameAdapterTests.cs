using DspApex.Common;
using RecipeRebalance;
using Xunit;

namespace RecipeRebalance.Tests
{
    public class ProtoGameAdapterTests
    {
        [Fact]
        public void AssignSequentialIds_WithModTestHelpers_AreContiguous()
        {
            var ids = ProtoIdAllocator.AssignSequentialIds(2500, FusionChainCatalog.TotalModRecipeCount);
            ModTestHelpers.AssertContiguousIds(ids, 2501);
        }

        [Fact]
        public void ModTestHelpers_AssertGridRoundTrip_MatchesRecipeGridMath()
        {
            int grid = RecipeGridMath.Encode(3, 2, 5);
            ModTestHelpers.AssertGridRoundTrip(grid, 3, 2, 5);
            Assert.Equal(3205, grid);
        }
    }
}