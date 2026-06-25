using System;

namespace RingworldsOrganic
{
    public static class RingworldOrganicLogic
    {
        public const int OrganicRingFrameThreshold = 12;

        public static float BiomassMultiplier(int segmentCount) =>
            1f + segmentCount * 0.08f;

        public static bool IsOrganicRing(int frames) => frames >= OrganicRingFrameThreshold;

        public static float OrganicBiomassYield(int frames, float floraBonus) =>
            IsOrganicRing(frames) ? frames * floraBonus * 0.1f : 0f;
    }
}