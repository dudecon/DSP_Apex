using Xunit;

namespace ThermalEffects.Tests
{
    public class ThermalEffectsLogicTests
    {
        [Fact]
        public void LavaPlanet_ReducesRefineryEnergy()
        {
            Assert.Equal(0.85f, ThermalEffectsLogic.RefineryEnergyFactor(650f));
            Assert.Equal(1f, ThermalEffectsLogic.RefineryEnergyFactor(300f));
        }

        [Fact]
        public void Temperature_AffectsAssemblerSpeed()
        {
            Assert.True(ThermalEffectsLogic.AssemblerSpeedFactor(650f) < 0.9f);
            Assert.Equal(0.95f, ThermalEffectsLogic.AssemblerSpeedFactor(150f));
            Assert.Equal(1f, ThermalEffectsLogic.AssemblerSpeedFactor(300f));
        }

        [Fact]
        public void WasteHeat_accumulates_and_dissipates()
        {
            float hot = ThermalEffectsLogic.AccumulateWasteHeat(0f, 100f, 650f);
            Assert.True(hot > 0f);
            float cooler = ThermalEffectsLogic.DissipateWasteHeat(hot, 300f);
            Assert.True(cooler < hot);
        }
    }
}