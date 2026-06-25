using System;

namespace ExoticStars
{
    public enum ApexExoticStarClass
    {
        None = 0,
        WhiteHole = 1,
        StrangeStar = 2,
        MonopoleStar = 3,
        Neutron = 4,
        BlackHole = 5
    }

    public static class ExoticStarLogic
    {
        public static ApexExoticStarClass Classify(StarData star)
        {
            if (star == null)
                return ApexExoticStarClass.None;

            if (star.type == EStarType.BlackHole)
                return star.luminosity > 2f ? ApexExoticStarClass.WhiteHole : ApexExoticStarClass.BlackHole;

            if (star.type == EStarType.NeutronStar)
                return star.luminosity > 1.5f ? ApexExoticStarClass.StrangeStar : ApexExoticStarClass.Neutron;

            if (star.type == EStarType.GiantStar && star.mass > 2.5f)
                return ApexExoticStarClass.MonopoleStar;

            return ApexExoticStarClass.None;
        }

        public static bool IsExotic(EStarType type) =>
            type == EStarType.NeutronStar || type == EStarType.BlackHole;

        public static bool IsExoticStar(StarData star) => Classify(star) != ApexExoticStarClass.None;

        public static bool DiscoverWithTelescope(int panelCount, float scanProgress) =>
            panelCount >= 8 && scanProgress >= 1f;

        public static float ScanProgressIncrement(int panelCount) =>
            panelCount <= 0 ? 0f : Math.Min(0.25f, panelCount * 0.02f);
    }
}