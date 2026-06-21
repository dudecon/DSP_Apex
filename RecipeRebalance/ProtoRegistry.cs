using System.Collections.Generic;
using System.Reflection;

namespace RecipeRebalance
{
    /// <summary>
    /// Safely append mod protos to LDB tables (cannot use sparse set_Item IDs without resizing).
    /// </summary>
    internal static class ProtoRegistry
    {
        private static readonly FieldInfo ItemIndexField = typeof(ItemProto).GetField(
            "<index>k__BackingField",
            BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly FieldInfo RecipeIndexField = typeof(RecipeProto).GetField(
            "index",
            BindingFlags.Public | BindingFlags.Instance);

        internal static void AddItem(ItemProto item)
        {
            if (item == null || LDB.items.Exist(item.ID))
                return;

            int index = Append(LDB.items, item);
            SetBackingIndex(item, ItemIndexField, index);
        }

        internal static void AddRecipe(RecipeProto recipe)
        {
            if (recipe == null || LDB.recipes.Exist(recipe.ID))
                return;

            var savedPreTech = recipe.preTech;
            int index = Append(LDB.recipes, recipe);
            SetBackingIndex(recipe, RecipeIndexField, index);

            if (savedPreTech != null)
                recipe.preTech = savedPreTech;
            else if (recipe.preTech == null)
                recipe.FindPreTech();
        }

        private static int Append<T>(ProtoSet<T> protoSet, T proto) where T : Proto
        {
            var existing = protoSet.dataArray;
            int oldLength = existing?.Length ?? 0;
            protoSet.Init(oldLength + 1);

            for (int i = 0; i < oldLength; i++)
                protoSet.dataArray[i] = existing[i];

            protoSet.dataArray[oldLength] = proto;
            RebuildIndices(protoSet);
            return oldLength;
        }

        private static void RebuildIndices<T>(ProtoSet<T> protoSet) where T : Proto
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

            SetDataIndices(protoSet, dataIndices);
        }

        private static void SetDataIndices<T>(ProtoSet<T> protoSet, Dictionary<int, int> dataIndices) where T : Proto
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

            Plugin.Log?.LogError("RecipeRebalance: failed to set ProtoSet.dataIndices — LDB lookups will break.");
        }

        private static void SetBackingIndex(object target, FieldInfo field, int index)
        {
            field?.SetValue(target, index);
        }
    }
}