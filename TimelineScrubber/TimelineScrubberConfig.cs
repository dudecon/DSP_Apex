using BepInEx.Configuration;

namespace TimelineScrubber
{
    internal static class TimelineScrubberConfig
    {
        internal static ConfigEntry<bool> EnableFastForward;

        internal static void Bind(BepInEx.BaseUnityPlugin plugin)
        {
            EnableFastForward = plugin.Config.Bind(
                "TimelineScrubber",
                "EnableFastForward",
                true,
                "When enabled, advances dyson sphere ticks faster for timelapse extrapolation.");

            TimelineScrubberRuntime.FastForward = EnableFastForward.Value;
            EnableFastForward.SettingChanged += (_, __) =>
            {
                TimelineScrubberRuntime.FastForward = EnableFastForward.Value;
            };
        }
    }
}