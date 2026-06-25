namespace DysonHarvester
{
    internal static class HarvesterService
    {
        internal static void ApplyHarvestPulse(DysonSphere sphere)
        {
            if (sphere?.starData == null || sphere.gameData == null)
                return;

            int starIndex = sphere.starData.index;
            int innermost = HarvesterGameAdapter.GetInnermostActiveLayerId(sphere);
            if (innermost < 0)
                return;

            NodeCollectorRegistry.ClearInactive(starIndex, innermost);

            var layer = sphere.layersIdBased[innermost];
            if (layer?.nodePool == null)
                return;

            var perNode = HarvesterYieldLogic.CalculatePerNodeYield(
                HarvesterGameAdapter.MapStarType(sphere.starData.type),
                sphere.starData.luminosity);

            int threshold = HarvesterDepositLogic.TicksPerNodeDeposit;
            var deposited = default(HarvesterYieldLogic.HarvestYield);

            for (int i = 1; i < layer.nodeCursor; i++)
            {
                var node = layer.nodePool[i];
                if (!HarvesterGameAdapter.IsNodeHarvestEligible(node, innermost))
                    continue;

                NodeCollectorRegistry.SetActive(starIndex, node, innermost);
                if (!NodeCollectorRegistry.TryGet(starIndex, node.id, out var entry))
                    continue;

                int pulses = HarvesterDepositLogic.AdvanceProgress(
                    entry.Progress,
                    node.sp > 0 ? node.sp : 1,
                    threshold,
                    out int remainder);

                NodeCollectorRegistry.SetProgress(starIndex, node.id, remainder);
                if (pulses <= 0)
                    continue;

                for (int p = 0; p < pulses; p++)
                    DepositNodeYield(sphere, perNode);

                deposited = HarvesterYieldLogic.AccumulatePulseDeposits(perNode, pulses, deposited);
            }

            var register = sphere.productRegister;
            if (register != null && HarvesterYieldLogic.ShouldUpdateProductRegister(deposited))
                UpdateStatistics(register, deposited);
        }

        private static void DepositNodeYield(DysonSphere sphere, HarvesterYieldLogic.HarvestYield yield)
        {
            HarvesterDepositService.DepositItem(sphere, GameIds.Hydrogen, yield.Hydrogen);
            HarvesterDepositService.DepositItem(sphere, GameIds.Deuterium, yield.Deuterium);
            HarvesterDepositService.DepositItem(sphere, GameIds.FireIce, yield.FireIce);
            HarvesterDepositService.DepositItem(sphere, GameIds.StrangeMatter, yield.StrangeMatter);
            HarvesterDepositService.DepositItem(sphere, GameIds.UnipolarMagnet, yield.UnipolarMagnet);
        }

        private static void UpdateStatistics(int[] register, HarvesterYieldLogic.HarvestYield yield)
        {
            AddToRegister(register, GameIds.Hydrogen, yield.Hydrogen);
            AddToRegister(register, GameIds.Deuterium, yield.Deuterium);
            AddToRegister(register, GameIds.FireIce, yield.FireIce);
            AddToRegister(register, GameIds.StrangeMatter, yield.StrangeMatter);
            AddToRegister(register, GameIds.UnipolarMagnet, yield.UnipolarMagnet);
        }

        private static void AddToRegister(int[] register, int itemId, int amount)
        {
            if (amount <= 0)
                return;

            var proto = LDB.items.Select(itemId);
            if (proto == null)
                return;

            int index = proto.index;
            if (index < 0 || index >= register.Length)
                return;

            register[index] += amount;
        }
    }
}