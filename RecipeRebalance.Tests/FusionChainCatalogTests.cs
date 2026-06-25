using DspApex.Common;
using RecipeRebalance;
using Xunit;

namespace RecipeRebalance.Tests
{
    public class FusionChainCatalogTests
    {
        [Fact]
        public void TotalModRecipeCount_MatchesStonePlusFusion()
        {
            Assert.Equal(
                FusionChainCatalog.StoneRecipeCount + FusionChainCatalog.FusionRecipeCount,
                FusionChainCatalog.TotalModRecipeCount);
        }

        [Fact]
        public void AssignSequentialIds_AfterMax_ProducesTwelveContiguousRecipeIds()
        {
            int maxRecipeId = 2500;
            var ids = ProtoIdAllocator.AssignSequentialIds(maxRecipeId, FusionChainCatalog.TotalModRecipeCount);

            Assert.Equal(FusionChainCatalog.TotalModRecipeCount, ids.Length);
            Assert.Equal(maxRecipeId + 1, ids[0]);
            Assert.Equal(maxRecipeId + FusionChainCatalog.TotalModRecipeCount, ids[ids.Length - 1]);

            for (int i = 1; i < ids.Length; i++)
                Assert.Equal(ids[i - 1] + 1, ids[i]);
        }

        [Fact]
        public void FusionChainIndices_AreAfterStoneRecipes()
        {
            int firstFusion = FusionChainCatalog.StoneRecipeCount;
            Assert.True(FusionChainCatalog.IsFusionRecipeIndex(firstFusion));
            Assert.False(FusionChainCatalog.IsStoneRecipeIndex(firstFusion));
            Assert.Equal(0, FusionChainCatalog.GetFusionRecipeOrdinal(firstFusion));
        }

        [Fact]
        public void FusionChainConstants_MatchRegisteredRecipeRatios()
        {
            Assert.Equal(3, FusionChainCatalog.HeliumInputForEnergeticGraphite);
            Assert.Equal(2, FusionChainCatalog.EnergeticGraphiteInputForStone);
            Assert.Equal(5, FusionChainCatalog.StoneOutputFromGraphiteFusion);
            Assert.Equal(1, FusionChainCatalog.HeliumPerFusionOutput);
        }

        [Fact]
        public void FindMaxId_SelectsHighestFromArray()
        {
            var ids = new[] { 100, 2500, 42, 2500, 7 };
            Assert.Equal(2500, ProtoIdAllocator.FindMaxId(ids));
        }
    }
}