namespace OrbitalRings
{
    internal static class OrbitalRingRuntimeState
    {
        static readonly System.Collections.Generic.Dictionary<int, float> Relay = new System.Collections.Generic.Dictionary<int, float>();
        static readonly System.Collections.Generic.Dictionary<int, float> Harvest = new System.Collections.Generic.Dictionary<int, float>();
        static readonly System.Collections.Generic.Dictionary<int, float> Sensor = new System.Collections.Generic.Dictionary<int, float>();

        internal static void SetRelayBonus(int planetId, float v) => Relay[planetId] = v;
        internal static void SetHarvestBonus(int planetId, float v) => Harvest[planetId] = v;
        internal static void SetSensorCoverage(int planetId, float v) => Sensor[planetId] = v;

        internal static float GetRelayBonus(int planetId) =>
            Relay.TryGetValue(planetId, out var v) ? v : 1f;

        internal static float GetHarvestBonus(int planetId) =>
            Harvest.TryGetValue(planetId, out var v) ? v : 1f;

        internal static float GetSensorCoverage(int planetId) =>
            Sensor.TryGetValue(planetId, out var v) ? v : 0f;
    }
}