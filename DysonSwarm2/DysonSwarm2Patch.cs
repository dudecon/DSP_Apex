using HarmonyLib;

namespace DysonSwarm2
{

    [HarmonyPatch(typeof(DysonSwarm), "GameTick")]
    internal static class DysonSwarm2Patch
    {
static void Postfix(DysonSwarm __instance, long gameTick) { DysonSwarm2Runtime.TickSwarmRoles(__instance, gameTick); }
    }

}
