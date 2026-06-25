namespace FaunaNomads
{
    internal static class FaunaNomadsRuntime
    {
        internal static int AgentCount;
        internal static int HerdCount;
        internal static float HarvestRate = 1f;
        internal static float NomadRange;

        internal static void SyncAgentsFromDeficit(int resourceDeficit)
        {
            int targetAgents = FaunaNomadLogic.NomadAgentsForDeficit(resourceDeficit);
            while (AgentCount < targetAgents)
                RegisterAgent();
        }

        internal static void TickHerds()
        {
            HerdCount = FaunaNomadLogic.HerdSize(AgentCount);
            HarvestRate = FaunaNomadLogic.NomadHarvestRate(HerdCount);
            NomadRange = FaunaNomadLogic.NomadRange(HerdCount);
        }

        internal static void RegisterAgent() => AgentCount++;
    }
}