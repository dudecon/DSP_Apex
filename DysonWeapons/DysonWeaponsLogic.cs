using System;

namespace DysonWeapons
{
    public static class DysonWeaponsLogic
    {
        public const int PolarGridMode = 1;

        public static bool IsFrameComplete(DysonFrame frame) =>
            frame != null && frame.id > 0 && DysonWeaponsLogicPure.IsFrameComplete(frame.spA, frame.spB, frame.spMax);

        public static bool IsOutermostLayer(DysonSphereLayer layer)
        {
            if (layer?.dysonSphere?.layersIdBased == null)
                return false;

            float radius = layer.orbitRadius;
            var layers = layer.dysonSphere.layersIdBased;
            for (int i = 0; i < layers.Length; i++)
            {
                var other = layers[i];
                if (other == null || other.id <= 0)
                    continue;

                if (other.orbitRadius > radius)
                    return false;
            }

            return true;
        }

        public static bool IsOuterPolarFrame(DysonSphereLayer layer, DysonFrame frame) =>
            layer != null
            && frame != null
            && IsFrameComplete(frame)
            && layer.drawingGridMode == PolarGridMode
            && IsOutermostLayer(layer);

        public static int CountWeaponFrames(DysonSphereLayer layer)
        {
            if (layer?.framePool == null)
                return 0;

            int count = 0;
            for (int i = 1; i < layer.frameCursor; i++)
            {
                var frame = layer.framePool[i];
                if (frame != null && IsOuterPolarFrame(layer, frame))
                    count++;
            }

            return count;
        }

        public static int TransmuteHydrogenToDeuterium(int hydrogen, long powerAvailable) =>
            DysonWeaponsLogicPure.TransmuteHydrogenToDeuterium(hydrogen, powerAvailable);

        public static float BeamDamagePerFrame(int frameCount) =>
            DysonWeaponsLogicPure.BeamDamagePerFrame(frameCount);
    }
}