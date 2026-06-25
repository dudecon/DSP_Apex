namespace DysonWeapons
{
    internal static class DysonWeaponsRuntime
    {
        internal static long BeamCharge;
        internal static int TransmutedDeuterium;
        internal static int PendingHydrogen;

        internal static void AccumulateBeamCharge(DysonSphereLayer layer)
        {
            if (layer == null)
                return;

            int weaponFrames = DysonWeaponsLogic.CountWeaponFrames(layer);
            if (weaponFrames <= 0)
                return;

            BeamCharge += (long)DysonWeaponsLogic.BeamDamagePerFrame(weaponFrames);
        }

        internal static void TickTransmutation(DysonSphere sphere)
        {
            if (sphere?.starData == null)
                return;

            long power = sphere.energyGenCurrentTick;
            if (power <= 0)
                return;

            int hydrogen = ReadProductRegister(sphere, 1120);
            if (hydrogen <= 0)
                hydrogen = PendingHydrogen;

            int produced = DysonWeaponsLogic.TransmuteHydrogenToDeuterium(hydrogen, power);
            if (produced <= 0)
                return;

            TransmutedDeuterium += produced;
            PendingHydrogen = System.Math.Max(0, hydrogen - produced * 10);
        }

        private static int ReadProductRegister(DysonSphere sphere, int itemId)
        {
            var register = sphere.productRegister;
            if (register == null)
                return 0;

            var proto = LDB.items.Select(itemId);
            if (proto == null)
                return 0;

            int index = proto.index;
            if (index < 0 || index >= register.Length)
                return 0;

            return register[index];
        }
    }
}