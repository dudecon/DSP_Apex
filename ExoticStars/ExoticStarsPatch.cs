using HarmonyLib;

namespace ExoticStars
{
    [HarmonyPatch(typeof(StarData), nameof(StarData.Load))]
    internal static class ExoticStarsLoadPatch
    {
        static void Postfix(StarData __instance)
        {
            if (ExoticStarLogic.IsExotic(__instance.type) || ExoticStarLogic.IsExoticStar(__instance))
                ExoticStarsRuntime.MarkDiscovered(__instance.id);
        }
    }

    [HarmonyPatch(typeof(DysonSphere), nameof(DysonSphere.BeforeGameTick))]
    internal static class ExoticStarsTelescopePatch
    {
        static void Postfix(DysonSphere __instance, long gameTick)
        {
            if (gameTick % 600 != 0 || __instance?.starData == null)
                return;

            int panels = CountTelescopePanels(__instance);
            ExoticStarsRuntime.TickTelescopeScan(__instance.starData, panels);
        }

        static int CountTelescopePanels(DysonSphere sphere)
        {
            if (sphere?.layersIdBased == null)
                return 0;

            int panels = 0;
            var layers = sphere.layersIdBased;
            for (int i = 0; i < layers.Length; i++)
            {
                var layer = layers[i];
                if (layer == null || layer.id <= 0)
                    continue;

                panels += layer.shellCursor + layer.frameCursor;
            }

            return panels;
        }
    }
}