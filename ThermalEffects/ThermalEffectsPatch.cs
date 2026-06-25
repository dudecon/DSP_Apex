using HarmonyLib;

namespace ThermalEffects
{
    [HarmonyPatch(typeof(AssemblerComponent), nameof(AssemblerComponent.InternalUpdate))]
    internal static class ThermalEffectsAssemblerPatch
    {
        static void Prefix(AssemblerComponent __instance, ref float power)
        {
            if (!ThermalEffectsContext.TryResolvePlanetTemperature(__instance, out float temp))
                temp = 300f;

            ThermalEffectsRuntime.TickHeat(power, temp);
            power *= ThermalEffectsRuntime.GetAssemblerFactor(temp);
        }
    }

    [HarmonyPatch(typeof(MinerComponent), nameof(MinerComponent.InternalUpdate))]
    internal static class ThermalEffectsRefineryPatch
    {
        static void Prefix(MinerComponent __instance, ref float power)
        {
            if (!ThermalEffectsContext.TryResolvePlanetTemperature(__instance, out float temp))
                temp = 300f;

            power *= ThermalEffectsLogic.RefineryEnergyFactor(temp);
        }
    }
}