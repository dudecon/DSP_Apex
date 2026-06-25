namespace OrbitalInfrastructure
{
    internal static class OrbitalInfrastructureBootstrap
    {
        static bool done;

        internal static void RegisterProtos()
        {
            if (done || LDB.items == null)
                return;

            InfrastructureProtoRegistry.RegisterItems();
            done = true;
        }
    }
}