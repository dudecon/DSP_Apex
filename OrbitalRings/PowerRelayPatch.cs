using HarmonyLib;

namespace OrbitalRings
{
    [HarmonyPatch(typeof(PowerGeneratorComponent), nameof(PowerGeneratorComponent.EtaCurrent_Gamma))]
    internal static class PowerRelayPatch
    {
        static void Postfix(PowerGeneratorComponent __instance, ref float __result)
        {
            if (__instance.id == 0 || !__instance.gamma)
                return;

            if (!OrbitalRingContext.TryResolveGeneratorPlanet(__instance, out int planetId))
                return;

            float relay = OrbitalRingRuntimeState.GetRelayBonus(planetId);
            __result = OrbitalRingLogic.ApplyRelayBonus(__result, relay);
        }
    }
}