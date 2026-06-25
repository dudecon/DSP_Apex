using System.Collections.Generic;
using System.Reflection;
using BepInEx.Logging;

namespace DspApex.Common
{
    /// <summary>Shared LDB proto append helpers used across suite packs.</summary>
    public static class ProtoRegistry
    {
        private static readonly FieldInfo ItemIndexField = typeof(ItemProto).GetField(
            "<index>k__BackingField",
            BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly FieldInfo RecipeIndexField = typeof(RecipeProto).GetField(
            "index",
            BindingFlags.Public | BindingFlags.Instance);

        public static void AddItem(ItemProto item, ManualLogSource log = null)
        {
            if (item == null || LDB.items.Exist(item.ID))
                return;

            int index = Append(LDB.items, item, log);
            SetBackingIndex(item, ItemIndexField, index);
        }

        public static void AddRecipe(RecipeProto recipe, ManualLogSource log = null)
        {
            if (recipe == null || LDB.recipes.Exist(recipe.ID))
                return;

            var savedPreTech = recipe.preTech;
            int index = Append(LDB.recipes, recipe, log);
            SetBackingIndex(recipe, RecipeIndexField, index);

            if (savedPreTech != null)
                recipe.preTech = savedPreTech;
            else if (recipe.preTech == null)
                recipe.FindPreTech();
        }

        public static void AppendItem(ItemProto item, ManualLogSource log = null)
        {
            if (item == null || LDB.items.Exist(item.ID))
                return;

            var protoSet = LDB.items;
            var existing = protoSet.dataArray;
            int oldLength = existing?.Length ?? 0;
            protoSet.Init(oldLength + 1);

            for (int i = 0; i < oldLength; i++)
                protoSet.dataArray[i] = existing[i];

            protoSet.dataArray[oldLength] = item;
            ItemIndexField?.SetValue(item, oldLength);
            RebuildItemIndices(protoSet);
        }

        private static int Append<T>(ProtoSet<T> protoSet, T proto, ManualLogSource log) where T : Proto
        {
            var existing = protoSet.dataArray;
            int oldLength = existing?.Length ?? 0;
            protoSet.Init(oldLength + 1);

            for (int i = 0; i < oldLength; i++)
                protoSet.dataArray[i] = existing[i];

            protoSet.dataArray[oldLength] = proto;
            RebuildIndices(protoSet, log);
            return oldLength;
        }

        private static void RebuildItemIndices(ProtoSet<ItemProto> protoSet)
        {
            var dataIndices = new Dictionary<int, int>();
            var array = protoSet.dataArray;
            for (int i = 0; i < array.Length; i++)
            {
                var entry = array[i];
                if (entry != null)
                    dataIndices[entry.ID] = i;
            }

            SetDataIndices(protoSet, dataIndices, null);
        }

        private static void RebuildIndices<T>(ProtoSet<T> protoSet, ManualLogSource log) where T : Proto
        {
            var dataIndices = new Dictionary<int, int>();
            var array = protoSet.dataArray;
            for (int i = 0; i < array.Length; i++)
            {
                var entry = array[i];
                if (entry == null)
                    continue;

                entry.sid = entry.SID;
                dataIndices[entry.ID] = i;
            }

            SetDataIndices(protoSet, dataIndices, log);
        }

        private static void SetDataIndices<T>(
            ProtoSet<T> protoSet,
            Dictionary<int, int> dataIndices,
            ManualLogSource log) where T : Proto
        {
            for (var type = protoSet.GetType(); type != null; type = type.BaseType)
            {
                var field = type.GetField(
                    "dataIndices",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                if (field == null)
                    continue;

                field.SetValue(protoSet, dataIndices);
                return;
            }

            log?.LogError("DspApex.Common.ProtoRegistry: failed to set ProtoSet.dataIndices.");
        }

        private static void SetBackingIndex(object target, FieldInfo field, int index) =>
            field?.SetValue(target, index);
    }
}