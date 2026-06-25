namespace TerraformingReGreening
{
    internal static class TerraformingReGreeningRuntime
    {
        internal static float AccumulatedOrganicBonus;
        internal static bool GeneticEngineeringActive;

        internal static void ApplyFloraBonus(PlanetFactory factory)
        {
            if (factory?.planet == null)
                return;

            bool supports = TerraformingLogic.SupportsFlora(factory.planet.type);
            AccumulatedOrganicBonus = TerraformingLogic.HarvestOrganicYield(supports, GeneticEngineeringActive);
        }

        internal static float GetYieldMultiplier(PlanetFactory factory)
        {
            if (factory?.planet == null)
                return 1f;

            return TerraformingLogic.HarvestOrganicYield(
                TerraformingLogic.SupportsFlora(factory.planet.type),
                GeneticEngineeringActive);
        }
    }
}