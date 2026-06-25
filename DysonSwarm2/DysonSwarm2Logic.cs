using System;

namespace DysonSwarm2
{
    public enum SwarmRole
    {
        Power = 0,
        Collector = 1,
        Combat = 2,
        Sensor = 3
    }

    public static class DysonSwarm2Logic
    {
        public static float RoleMultiplier(SwarmRole role) => role switch
        {
            SwarmRole.Collector => 1.3f,
            SwarmRole.Combat => 0.8f,
            SwarmRole.Sensor => 1.1f,
            _ => 1f
        };

        public static SwarmRole AssignRoleForOrbitCount(int orbitCount) => orbitCount switch
        {
            < 2 => SwarmRole.Power,
            < 4 => SwarmRole.Collector,
            < 6 => SwarmRole.Sensor,
            _ => SwarmRole.Combat
        };
    }
}