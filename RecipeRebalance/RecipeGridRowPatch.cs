using System.Collections.Generic;
using HarmonyLib;

namespace RecipeRebalance
{
    [HarmonyPatch(typeof(UIRecipePicker), "RefreshIcons")]
    internal static class UIRecipePickerRefreshIconsPatch
    {
        [HarmonyTranspiler]
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return RecipeGridPatchSupport.ExpandRowCap(instructions);
        }

        [HarmonyPostfix]
        internal static void Postfix(UIRecipePicker __instance)
        {
            RecipeGridUiExpander.ApplyPicker(__instance);
        }
    }

    [HarmonyPatch(typeof(UIRecipePicker), "TestMouseIndex")]
    internal static class UIRecipePickerTestMouseIndexPatch
    {
        [HarmonyTranspiler]
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return RecipeGridPatchSupport.ExpandRowCap(instructions);
        }
    }

    [HarmonyPatch(typeof(UIRecipePicker), "SetMaterialProps")]
    internal static class UIRecipePickerSetMaterialPropsPatch
    {
        [HarmonyTranspiler]
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return RecipeGridPatchSupport.ExpandMaterialRowCount(instructions);
        }

        [HarmonyPostfix]
        internal static void Postfix(UIRecipePicker __instance)
        {
            RecipeGridUiExpander.ApplyPicker(__instance);
        }
    }

    [HarmonyPatch(typeof(UIRecipePicker), "_OnCreate")]
    internal static class UIRecipePickerOnCreatePatch
    {
        [HarmonyPostfix]
        internal static void Postfix(UIRecipePicker __instance)
        {
            RecipeGridUiExpander.ApplyPicker(__instance);
        }
    }

    [HarmonyPatch(typeof(UIRecipePicker), "_OnOpen")]
    internal static class UIRecipePickerOnOpenPatch
    {
        [HarmonyPostfix]
        internal static void Postfix(UIRecipePicker __instance)
        {
            RecipeGridUiExpander.ApplyPicker(__instance);
        }
    }

    [HarmonyPatch(typeof(UIReplicatorWindow), "RefreshRecipeIcons")]
    internal static class UIReplicatorRefreshRecipeIconsPatch
    {
        [HarmonyTranspiler]
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return RecipeGridPatchSupport.ExpandRowCap(instructions);
        }

        [HarmonyPostfix]
        internal static void Postfix(UIReplicatorWindow __instance)
        {
            RecipeGridUiExpander.ApplyReplicator(__instance);
        }
    }

    [HarmonyPatch(typeof(UIReplicatorWindow), "TestMouseRecipeIndex")]
    internal static class UIReplicatorTestMouseRecipeIndexPatch
    {
        [HarmonyTranspiler]
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return RecipeGridPatchSupport.ExpandRowCap(instructions);
        }
    }

    [HarmonyPatch(typeof(UIReplicatorWindow), "SetMaterialProps")]
    internal static class UIReplicatorSetMaterialPropsPatch
    {
        [HarmonyTranspiler]
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return RecipeGridPatchSupport.ExpandMaterialRowCount(instructions);
        }

        [HarmonyPostfix]
        internal static void Postfix(UIReplicatorWindow __instance)
        {
            RecipeGridUiExpander.ApplyReplicator(__instance);
        }
    }

    [HarmonyPatch(typeof(UIReplicatorWindow), "_OnCreate")]
    internal static class UIReplicatorOnCreatePatch
    {
        [HarmonyTranspiler]
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            return RecipeGridPatchSupport.ExpandReplicatorArraySize(instructions);
        }

        [HarmonyPostfix]
        internal static void Postfix(UIReplicatorWindow __instance)
        {
            RecipeGridUiExpander.ApplyReplicator(__instance);
        }
    }

    [HarmonyPatch(typeof(UIReplicatorWindow), "_OnOpen")]
    internal static class UIReplicatorOnOpenPatch
    {
        [HarmonyPostfix]
        internal static void Postfix(UIReplicatorWindow __instance)
        {
            RecipeGridUiExpander.ApplyReplicator(__instance);
        }
    }
}