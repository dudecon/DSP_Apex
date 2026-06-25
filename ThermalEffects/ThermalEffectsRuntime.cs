namespace ThermalEffects
{
    internal static class ThermalEffectsRuntime
    {
        internal static float PlanetWasteHeat;

        internal static void TickHeat(float powerDraw, float planetTemp)
        {
            PlanetWasteHeat = ThermalEffectsLogic.AccumulateWasteHeat(PlanetWasteHeat, powerDraw, planetTemp);
            PlanetWasteHeat = ThermalEffectsLogic.DissipateWasteHeat(PlanetWasteHeat, planetTemp);
        }

        internal static float GetAssemblerFactor(float planetTemp)
        {
            float baseFactor = ThermalEffectsLogic.AssemblerSpeedFactor(planetTemp);
            if (PlanetWasteHeat > 1f)
                return baseFactor * 0.95f;

            return baseFactor;
        }
    }
}