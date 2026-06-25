using HarmonyLib;

namespace TerraformingReGreening
{
    [HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.BeforeGameTick))]
    internal static class TerraformingReGreeningPatch
    {
        static void Postfix(PlanetFactory __instance)
        {
            TerraformingReGreeningRuntime.ApplyFloraBonus(__instance);
        }
    }
}