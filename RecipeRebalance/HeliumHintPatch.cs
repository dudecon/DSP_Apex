using HarmonyLib;

namespace RecipeRebalance
{
    [HarmonyPatch(typeof(ItemProto), nameof(ItemProto.Preload))]
    internal static class HeliumHintPatch
    {
        static void Postfix(ItemProto __instance)
        {
            if (__instance == null || __instance.ID != ApexIds.Helium || ApexIds.Helium == 0)
                return;

            ModItemHintUtil.ApplyHeliumHints(Plugin.Log);
        }
    }
}