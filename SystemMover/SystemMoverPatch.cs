using HarmonyLib;

namespace SystemMover
{

    [HarmonyPatch(typeof(StarData), "Load")]
    internal static class SystemMoverPatch
    {
static void Postfix(StarData __instance) { SystemMoverRuntime.RegisterStar(__instance); }
    }

}
