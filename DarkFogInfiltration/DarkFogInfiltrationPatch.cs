using HarmonyLib;

namespace DarkFogInfiltration
{

    [HarmonyPatch(typeof(GameData), "GameTick")]
    internal static class DarkFogInfiltrationPatch
    {
static void Postfix(GameData __instance, long time) { DarkFogInfiltrationRuntime.TickInfiltration(time); }
    }

}
