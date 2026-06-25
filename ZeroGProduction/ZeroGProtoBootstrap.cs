using System.Collections.Generic;
using System.Reflection;

namespace ZeroGProduction
{
    internal static class ZeroGProtoBootstrap
    {
        private static readonly FieldInfo ItemIndexField = typeof(ItemProto).GetField(
            "<index>k__BackingField",
            BindingFlags.NonPublic | BindingFlags.Instance);

        internal static void RegisterBuildings()
        {
            if (LDB.items == null || ZeroGBuildingIds.ZeroGRefinery != 0)
                return;

            ZeroGBuildingIds.AssignRuntimeIds();
            CloneBuilding(ZeroGBuildingIds.ZeroGRefinery, 2302, "Apex 0G Refinery", "Orbital refinery with high-efficiency megastructure parts.");
            CloneBuilding(ZeroGBuildingIds.ZeroGAssembler, 2303, "Apex 0G Assembler", "Orbital assembler with advanced yields and byproducts.");
            Plugin.Log.LogInfo($"ZeroGProduction: registered buildings {ZeroGBuildingIds.ZeroGRefinery}, {ZeroGBuildingIds.ZeroGAssembler}");
        }

        private static void CloneBuilding(int id, int templateId, string name, string description)
        {
            var template = LDB.items.Select(templateId);
            if (template == null)
                return;

            var item = new ItemProto
            {
                ID = id,
                Name = name,
                Description = description,
                Type = template.Type,
                StackSize = template.StackSize,
                IsEntity = true,
                CanBuild = true,
                BuildInGas = true,
                IconPath = template.IconPath,
                GridIndex = template.GridIndex,
                ModelIndex = template.ModelIndex,
                ModelCount = template.ModelCount,
                HpMax = template.HpMax,
                prefabDesc = template.prefabDesc
            };

            var protoSet = LDB.items;
            var existing = protoSet.dataArray;
            int oldLength = existing?.Length ?? 0;
            protoSet.Init(oldLength + 1);
            for (int i = 0; i < oldLength; i++)
                protoSet.dataArray[i] = existing[i];
            protoSet.dataArray[oldLength] = item;
            ItemIndexField?.SetValue(item, oldLength);
        }
    }
}