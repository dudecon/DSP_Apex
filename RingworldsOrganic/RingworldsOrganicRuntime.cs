namespace RingworldsOrganic
{
    internal static class RingworldsOrganicRuntime
    {
        internal static float BiomassLevel;
        internal static int SegmentCount;

        internal static void TickBiomass(PlanetFactory factory)
        {
            if (factory == null)
                return;

            int frames = factory.entityCount;
            SegmentCount = frames;
            if (!RingworldOrganicLogic.IsOrganicRing(frames))
                return;

            float floraBonus = 1.4f;
            BiomassLevel = RingworldOrganicLogic.OrganicBiomassYield(frames, floraBonus);
        }
    }
}