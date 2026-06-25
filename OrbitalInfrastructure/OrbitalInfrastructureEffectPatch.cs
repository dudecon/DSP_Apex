using HarmonyLib;

namespace OrbitalInfrastructure
{
    [HarmonyPatch(typeof(PlanetTransport), nameof(PlanetTransport.RefreshStationTraffic))]
    internal static class OrbitalInfrastructureEffectPatch
    {
        static void Postfix(PlanetTransport __instance)
        {
            if (__instance?.stationPool == null)
                return;

            for (int i = 1; i < __instance.stationCursor; i++)
            {
                var station = __instance.stationPool[i];
                if (station == null || station.id == 0)
                    continue;

                int protoId = 0;
                var factory = __instance.planet?.factory;
                if (factory != null && station.entityId > 0 && station.entityId < factory.entityPool.Length)
                    protoId = factory.entityPool[station.entityId].protoId;

                if (protoId == InfrastructureIds.SpaceElevator)
                    station.storage[0].count = System.Math.Max(station.storage[0].count, OrbitalInfrastructureLogic.GetElevatorThroughput(4));

                if (protoId == InfrastructureIds.OrbitalStationModule)
                    station.energyMax = System.Math.Max(station.energyMax, OrbitalInfrastructureLogic.GetStationModuleSlots(station.storage.Length) * 1000L);
            }
        }
    }
}