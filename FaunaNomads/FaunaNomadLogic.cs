using System;

namespace FaunaNomads
{
    public static class FaunaNomadLogic
    {
        public static int HerdSize(int agents) => Math.Min(32, agents * 2);

        public static float NomadHarvestRate(int herd) => 1f + herd * 0.02f;

        public static float NomadRange(int herd) => 100f + herd * 15f;

        public static int NomadAgentsForDeficit(int resourceDeficit) =>
            resourceDeficit > 0 ? Math.Min(16, 1 + resourceDeficit / 200) : 0;
    }
}