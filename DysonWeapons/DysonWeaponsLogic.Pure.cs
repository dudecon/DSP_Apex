namespace DysonWeapons
{
    /// <summary>Game-agnostic weapon formulas testable without Unity/DSP assemblies.</summary>
    public static class DysonWeaponsLogicPure
    {
        public static int TransmuteHydrogenToDeuterium(int hydrogen, long powerAvailable) =>
            powerAvailable > 1_000_000 ? hydrogen / 10 : 0;

        public static float BeamDamagePerFrame(int frameCount) => frameCount * 250f;

        public static bool IsFrameComplete(int spA, int spB, int spMax) =>
            spMax > 0 && spA + spB >= spMax;
    }
}