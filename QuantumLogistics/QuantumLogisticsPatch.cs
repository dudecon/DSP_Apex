using HarmonyLib;

namespace QuantumLogistics
{
    [HarmonyPatch(typeof(StorageComponent), nameof(StorageComponent.AddItem))]
    internal static class QuantumLogisticsTowerPatch
    {
        static void Prefix(StorageComponent __instance, int count, ref int inc)
        {
            if (count <= 0 || !QuantumLogisticsContext.IsLogisticsTowerStorage(__instance))
                return;

            if (!QuantumLogisticsRuntime.TryTeleport(QuantumLogisticsLogic.MinTowerLevelForTeleport, count, out int warperCost))
                return;

            inc = System.Math.Max(inc, warperCost);
        }
    }

    [HarmonyPatch(typeof(DysonSphere), nameof(DysonSphere.BeforeGameTick))]
    internal static class QuantumLogisticsStargatePatch
    {
        static void Postfix(DysonSphere __instance, long gameTick)
        {
            if (gameTick % 600 != 0 || __instance == null)
                return;

            int modules = QuantumLogisticsContext.CountOrbitalModules(__instance);
            if (!QuantumLogisticsLogic.CanBuildStargate(modules))
                return;

            if (QuantumLogisticsRuntime.StargateCount == 0)
                QuantumLogisticsRuntime.RegisterStargate();

            int transferBatch = __instance.rocketCursor > 0 ? 1 : 0;
            if (transferBatch > 0)
                QuantumLogisticsRuntime.TryStargateTransfer(modules, transferBatch, out _);
        }
    }
}