using HarmonyLib;

namespace TimelineScrubber
{
    [HarmonyPatch(typeof(DysonSphere), nameof(DysonSphere.BeforeGameTick))]
    internal static class TimelineScrubberPatch
    {
        static void Prefix(ref long gameTick)
        {
            if (!TimelineScrubberRuntime.FastForward)
                return;

            gameTick = TimelineScrubberRuntime.AdvanceGameTick(gameTick);
        }
    }

    [HarmonyPatch(typeof(GameData), "GameTick")]
    internal static class TimelineScrubberBranchPatch
    {
        static void Postfix(GameData __instance, long time)
        {
            if (time % 216000 != 0)
                return;

            TimelineScrubberRuntime.RecordBranch(time);
        }
    }
}