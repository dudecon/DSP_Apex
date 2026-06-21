using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace RecipeRebalance
{
    internal static class RecipeGridPatchSupport
    {
        internal const int ReplicatorSlotCount = 160;

        internal static IEnumerable<CodeInstruction> ExpandRowCap(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            int rowCap = RecipeGrid.ModSmeltRow;

            for (int i = 0; i < codes.Count - 1; i++)
            {
                if (!IsRowBoundCheck(codes[i], codes[i + 1]))
                    continue;

                codes[i] = new CodeInstruction(OpCodes.Ldc_I4, rowCap);
            }

            return codes;
        }

        internal static IEnumerable<CodeInstruction> ExpandMaterialRowCount(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);
            int rowCap = RecipeGrid.ModSmeltRow;

            for (int i = 0; i < codes.Count; i++)
            {
                if (!IsVanillaRowCap(codes[i]))
                    continue;

                codes[i] = new CodeInstruction(OpCodes.Ldc_R4, (float)rowCap);
            }

            return codes;
        }

        internal static IEnumerable<CodeInstruction> ExpandReplicatorArraySize(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            for (int i = 0; i < codes.Count; i++)
            {
                if (!IsVanillaSlotArraySize(codes[i]))
                    continue;

                codes[i] = new CodeInstruction(OpCodes.Ldc_I4, ReplicatorSlotCount);
            }

            return codes;
        }

        private static bool IsRowBoundCheck(CodeInstruction current, CodeInstruction next)
        {
            if (next.opcode != OpCodes.Bge && next.opcode != OpCodes.Bge_S
                && next.opcode != OpCodes.Bge_Un && next.opcode != OpCodes.Bge_Un_S)
            {
                return false;
            }

            return IsVanillaRowCap(current);
        }

        private static bool IsVanillaSlotArraySize(CodeInstruction instruction)
        {
            if (instruction.opcode == OpCodes.Ldc_I4_S && instruction.operand is sbyte svalue)
                return svalue == 120;

            if (instruction.opcode == OpCodes.Ldc_I4 && instruction.operand is int value)
                return value == 120;

            return false;
        }

        private static bool IsVanillaRowCap(CodeInstruction instruction)
        {
            if (instruction.opcode == OpCodes.Ldc_I4_8)
                return true;

            if (instruction.opcode == OpCodes.Ldc_I4 && instruction.operand is int value)
                return value == 8;

            if (instruction.opcode == OpCodes.Ldc_I4_S && instruction.operand is sbyte svalue)
                return svalue == 8;

            if (instruction.opcode == OpCodes.Ldc_R4 && instruction.operand is float fvalue)
                return Mathf.Approximately(fvalue, 8f);

            return false;
        }
    }
}