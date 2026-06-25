namespace DysonSwarm2
{
    internal static class DysonSwarm2Runtime
    {
        internal static SwarmRole ActiveRole = SwarmRole.Power;
        internal static float LastRoleMultiplier = 1f;

        internal static void TickSwarmRoles(DysonSwarm swarm, long gameTick)
        {
            if (swarm == null || gameTick % 60 != 0)
                return;

            int orbitCount = swarm.orbitCursor;
            ActiveRole = DysonSwarm2Logic.AssignRoleForOrbitCount(orbitCount);
            LastRoleMultiplier = DysonSwarm2Logic.RoleMultiplier(ActiveRole);
        }
    }
}