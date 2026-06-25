using HarmonyLib;

namespace MegaStructuresUI
{
    [HarmonyPatch(typeof(UIDysonEditor), "_OnOpen")]
    internal static class OrbitalCommandPatch
    {
        static void Postfix(UIDysonEditor __instance)
        {
            OrbitalCommandPanel.OnDysonEditorOpened(__instance);
        }
    }
}