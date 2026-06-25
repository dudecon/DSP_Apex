using System;

namespace DysonHarvester
{
    /// <summary>Pure yield and layer eligibility rules for Dyson node harvesting.</summary>
    public static class HarvesterYieldLogic
    {
        public const int TicksPerHarvestPulse = 60;

        public enum StarClass
        {
            MainSequence = 0,
            Giant = 1,
            WhiteDwarf = 2,
            Neutron = 3,
            BlackHole = 4
        }

        public struct HarvestYield
        {
            public int Hydrogen;
            public int Deuterium;
            public int FireIce;
            public int StrangeMatter;
            public int UnipolarMagnet;

            public bool HasAny =>
                Hydrogen > 0 || Deuterium > 0 || FireIce > 0 || StrangeMatter > 0 || UnipolarMagnet > 0;
        }

        public static bool IsNodeComplete(bool inUse, int sp, int spMax) =>
            inUse && spMax > 0 && sp >= spMax;

        public static bool IsNodeHarvestEligible(bool inUse, int nodeLayerId, int innermostLayerId, int sp, int spMax) =>
            innermostLayerId >= 0 && IsNodeComplete(inUse, sp, spMax) && nodeLayerId == innermostLayerId;

        public static HarvestYield CalculatePerNodeYield(StarClass starClass, float luminosity)
        {
            float lum = Math.Max(0.25f, Math.Min(luminosity, 8f));
            float scale = 1f + (lum - 1f) * 0.15f;

            int hydrogen;
            int deuterium;
            int fireIce;
            int strangeMatter;
            int unipolar;

            switch (starClass)
            {
                case StarClass.Giant:
                    hydrogen = 14;
                    deuterium = 1;
                    fireIce = 0;
                    strangeMatter = 0;
                    unipolar = 0;
                    break;
                case StarClass.WhiteDwarf:
                    hydrogen = 6;
                    deuterium = 4;
                    fireIce = 0;
                    strangeMatter = 0;
                    unipolar = 0;
                    break;
                case StarClass.Neutron:
                    hydrogen = 8;
                    deuterium = 3;
                    fireIce = 0;
                    strangeMatter = 1;
                    unipolar = 0;
                    break;
                case StarClass.BlackHole:
                    hydrogen = 10;
                    deuterium = 2;
                    fireIce = 1;
                    strangeMatter = 0;
                    unipolar = 1;
                    break;
                default:
                    hydrogen = 10;
                    deuterium = 1;
                    fireIce = 0;
                    strangeMatter = 0;
                    unipolar = 0;
                    break;
            }

            return new HarvestYield
            {
                Hydrogen = Math.Max(1, (int)Math.Round(hydrogen * scale)),
                Deuterium = Math.Max(0, (int)Math.Round(deuterium * scale)),
                FireIce = fireIce,
                StrangeMatter = strangeMatter,
                UnipolarMagnet = unipolar
            };
        }

        public static HarvestYield ScaleYield(HarvestYield perNode, int nodeCount)
        {
            if (nodeCount <= 0)
                return default;

            return new HarvestYield
            {
                Hydrogen = perNode.Hydrogen * nodeCount,
                Deuterium = perNode.Deuterium * nodeCount,
                FireIce = perNode.FireIce * nodeCount,
                StrangeMatter = perNode.StrangeMatter * nodeCount,
                UnipolarMagnet = perNode.UnipolarMagnet * nodeCount
            };
        }

        public static HarvestYield Add(HarvestYield a, HarvestYield b) =>
            new HarvestYield
            {
                Hydrogen = a.Hydrogen + b.Hydrogen,
                Deuterium = a.Deuterium + b.Deuterium,
                FireIce = a.FireIce + b.FireIce,
                StrangeMatter = a.StrangeMatter + b.StrangeMatter,
                UnipolarMagnet = a.UnipolarMagnet + b.UnipolarMagnet
            };

        public static HarvestYield AccumulatePulseDeposits(HarvestYield perNode, int pulses, HarvestYield accumulated)
        {
            if (pulses <= 0)
                return accumulated;

            for (int p = 0; p < pulses; p++)
                accumulated = Add(accumulated, perNode);

            return accumulated;
        }

        public static bool ShouldUpdateProductRegister(HarvestYield deposited) => deposited.HasAny;
    }
}