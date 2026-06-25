namespace TerraformingReGreening
{
    public static class TerraformingLogic
    {
        public static float OrganicYieldBonus = 1.4f;
        public static float GeneticEngineeringBonus = 1.25f;

        public static bool SupportsFlora(EPlanetType type) =>
            type == EPlanetType.Ocean || type == EPlanetType.Desert;

        public static float HarvestOrganicYield(bool supportsFlora, bool engineered) =>
            supportsFlora ? OrganicYieldBonus * (engineered ? GeneticEngineeringBonus : 1f) : 1f;
    }
}