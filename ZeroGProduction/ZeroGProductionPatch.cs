using HarmonyLib;

namespace ZeroGProduction
{
    [HarmonyPatch(typeof(VFPreload), "InvokeOnLoadWorkEnded")]
    internal static class ZeroGProtoPatch
    {
        static void Postfix() => ZeroGProtoBootstrap.RegisterBuildings();
    }

    [HarmonyPatch(typeof(AssemblerComponent), nameof(AssemblerComponent.InternalUpdate))]
    internal static class ZeroGProductionPatch
    {
        static void Postfix(AssemblerComponent __instance, float power, ref uint __result)
        {
            if (__instance.id == 0 || power < 0.1f || __result == 0)
                return;

            if (!ZeroGProductionLogic.ShouldApplyBonus(__instance, out float bonus))
                return;

            __result = (uint)ZeroGProductionLogic.ApplyYieldBonus((int)__result, bonus);
        }
    }
}