using System.Collections.Generic;
using System.Reflection;
using BindingFlags = System.Reflection.BindingFlags;

namespace OrbitalRings
{
    internal static class OrbitalRingProtoRegistry
    {
        private static readonly FieldInfo ItemIndexField = typeof(ItemProto).GetField(
            "<index>k__BackingField",
            BindingFlags.NonPublic | BindingFlags.Instance);

        internal static void RegisterItems()
        {
            OrbitalRingIds.AssignRuntimeIds();

            CloneEntityItem(
                OrbitalRingIds.RingFrameModule,
                OrbitalRingIds.VanillaRayReceiver,
                "Apex Orbital Ring Frame",
                "Planetary ring strut module; relays power to ray receivers on the dark side.");

            CloneEntityItem(
                OrbitalRingIds.SatelliteSwarmModule,
                OrbitalRingIds.VanillaOrbitalCollector,
                "Apex Satellite Swarm Node",
                "Microsat swarm node for atmospheric harvest and missile sensor coverage.");

            Plugin.Log.LogInfo(
                $"OrbitalRings: registered items {OrbitalRingIds.RingFrameModule}, " +
                $"{OrbitalRingIds.SatelliteSwarmModule}");
        }

        private static void CloneEntityItem(int newId, int templateId, string name, string description)
        {
            if (LDB.items.Exist(newId))
                return;

            var template = LDB.items.Select(templateId);
            if (template == null)
            {
                Plugin.Log.LogWarning($"OrbitalRings: template item {templateId} missing.");
                return;
            }

            var item = new ItemProto
            {
                ID = newId,
                Name = name,
                Description = description,
                Type = template.Type,
                StackSize = template.StackSize,
                IsFluid = template.IsFluid,
                IsEntity = true,
                CanBuild = true,
                BuildInGas = template.BuildInGas,
                IconPath = template.IconPath,
                GridIndex = template.GridIndex,
                ModelIndex = template.ModelIndex,
                ModelCount = template.ModelCount,
                HpMax = template.HpMax,
                prefabDesc = template.prefabDesc
            };

            AppendItem(item);
        }

        private static void AppendItem(ItemProto item)
        {
            var protoSet = LDB.items;
            var existing = protoSet.dataArray;
            int oldLength = existing?.Length ?? 0;
            protoSet.Init(oldLength + 1);

            for (int i = 0; i < oldLength; i++)
                protoSet.dataArray[i] = existing[i];

            protoSet.dataArray[oldLength] = item;
            ItemIndexField?.SetValue(item, oldLength);

            var dataIndices = new Dictionary<int, int>();
            for (int i = 0; i < protoSet.dataArray.Length; i++)
            {
                var entry = protoSet.dataArray[i];
                if (entry != null)
                    dataIndices[entry.ID] = i;
            }

            var field = typeof(ProtoSet<ItemProto>).GetField(
                "dataIndices",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            field?.SetValue(protoSet, dataIndices);
        }
    }
}