using HarmonyLib;

namespace MegaStructuresUI
{
    [HarmonyPatch(typeof(UIReplicatorWindow), "_OnCreate")]
    internal static class UIReplicatorOnCreateTabPatch
    {
        [HarmonyPostfix]
        internal static void Postfix(UIReplicatorWindow __instance)
        {
            ReplicatorTabInstaller.EnsureInstalled(__instance);
        }
    }

    [HarmonyPatch(typeof(UIReplicatorWindow), "_OnRegEvent")]
    internal static class UIReplicatorOnRegEventTabPatch
    {
        [HarmonyPostfix]
        internal static void Postfix(UIReplicatorWindow __instance)
        {
            ReplicatorTabInstaller.WireTabClicks(__instance);
        }
    }

    [HarmonyPatch(typeof(UIReplicatorWindow), "_OnUnregEvent")]
    internal static class UIReplicatorOnUnregEventTabPatch
    {
        [HarmonyPostfix]
        internal static void Postfix(UIReplicatorWindow __instance)
        {
            ReplicatorTabInstaller.UnwireTabClicks(__instance);
        }
    }

    [HarmonyPatch(typeof(UIReplicatorWindow), "_OnOpen")]
    internal static class UIReplicatorOnOpenTabPatch
    {
        [HarmonyPostfix]
        internal static void Postfix(UIReplicatorWindow __instance)
        {
            ReplicatorTabInstaller.EnsureInstalled(__instance);
            ReplicatorTabInstaller.SyncTabHighlights(__instance, UiReplicatorUtil.GetCurrentType(__instance));
        }
    }

    [HarmonyPatch(typeof(UIReplicatorWindow), "OnTypeButtonClick")]
    internal static class UIReplicatorOnTypeButtonClickTabPatch
    {
        [HarmonyPostfix]
        internal static void Postfix(UIReplicatorWindow __instance, int type)
        {
            ReplicatorTabInstaller.SyncTabHighlights(__instance, type);
        }
    }

    [HarmonyPatch(typeof(UIRecipePicker), "RefreshIcons")]
    internal static class UIRecipePickerRefreshIconsApexPatch
    {
        [HarmonyPostfix]
        internal static void Postfix(UIRecipePicker __instance)
        {
            RecipePickerApexBridge.InjectApexRecipes(__instance);
        }
    }
}