using HarmonyLib;

namespace DysonWeapons
{
    [HarmonyPatch(typeof(DysonSphereLayer), nameof(DysonSphereLayer.GameTick))]
    internal static class DysonWeaponsPatch
    {
        static void Postfix(DysonSphereLayer __instance, long gameTick)
        {
            if (gameTick % 120 != 0)
                return;

            DysonWeaponsRuntime.AccumulateBeamCharge(__instance);
            DysonWeaponsRuntime.TickTransmutation(__instance?.dysonSphere);
        }
    }
}