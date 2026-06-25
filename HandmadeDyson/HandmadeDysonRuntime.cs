namespace HandmadeDyson
{
    internal static class HandmadeDysonRuntime
    {
        internal static bool EnableOrbitalBuild;

        internal static bool IsOrbitalBuildAllowed(int protoId) =>
            EnableOrbitalBuild && HandmadeDysonLogic.CanBuildOnOrbitalStructure(protoId);

        internal static float GetEffectiveBuildRange(float baseRange) =>
            EnableOrbitalBuild ? baseRange * HandmadeDysonLogic.BuildRangeBonus : baseRange;
    }
}