using DspApex.Common;
using RecipeRebalance;
using Xunit;

namespace RecipeRebalance.Tests
{
    public class RecipeGridMathTests
    {
        [Theory]
        [InlineData(3, 1, 1, 3101)]
        [InlineData(3, 1, 14, 3114)]
        [InlineData(3, 2, 1, 3201)]
        [InlineData(3, 8, 14, 3814)]
        public void Encode_ProducesExpectedGridIndex(int tab, int row, int column, int expected)
        {
            Assert.Equal(expected, RecipeGridMath.Encode(tab, row, column));
            ModTestHelpers.AssertGridRoundTrip(expected, tab, row, column);
        }

        [Fact]
        public void Encode_Decode_RoundTrip_ApexTabSlots()
        {
            for (int i = 0; i < FusionChainCatalog.TotalModRecipeCount; i++)
            {
                int grid = RecipeGridMath.GetApexSlotAtIndex(i);
                Assert.Equal(RecipeGridMath.ApexTab, RecipeGridMath.DecodeTab(grid));
                Assert.InRange(RecipeGridMath.DecodeRow(grid), 1, RecipeGridMath.RowCount);
                Assert.InRange(RecipeGridMath.DecodeColumn(grid), 1, RecipeGridMath.ColumnCount);
            }
        }

        [Fact]
        public void GetApexSlotAtIndex_FirstSevenSlots_AreRowOneStoneRecipes()
        {
            for (int i = 0; i < FusionChainCatalog.StoneRecipeCount; i++)
            {
                int grid = RecipeGridMath.GetApexSlotAtIndex(i);
                Assert.Equal(3100 + i + 1, grid);
                Assert.True(FusionChainCatalog.IsStoneRecipeIndex(i));
            }
        }

        [Fact]
        public void GetApexSlotAtIndex_FusionRecipes_FollowStoneRow()
        {
            int heliumGrid = RecipeGridMath.GetApexSlotAtIndex(FusionChainCatalog.StoneRecipeCount);
            Assert.Equal(3108, heliumGrid);
            Assert.True(FusionChainCatalog.IsFusionRecipeIndex(FusionChainCatalog.StoneRecipeCount));
        }
    }
}