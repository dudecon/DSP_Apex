using System.Reflection;

namespace RecipeRebalance
{
    internal static class ProtoUtil
    {
        private static readonly FieldInfo ItemIndexField = typeof(ItemProto).GetField(
            "<index>k__BackingField",
            BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly MethodInfo FindRecipes = typeof(ItemProto).GetMethod(
            "FindRecipes",
            BindingFlags.Instance | BindingFlags.NonPublic);

        private static readonly MethodInfo ComputeRawMats = typeof(ItemProto).GetMethod(
            "ComputeRawMats",
            BindingFlags.Instance | BindingFlags.NonPublic);

        private static readonly MethodInfo Preload = typeof(ItemProto).GetMethod(
            "Preload",
            BindingFlags.Instance | BindingFlags.Public);

        internal static void PreloadItem(ItemProto item)
        {
            if (item == null || Preload == null)
                return;

            int index = ItemIndexField != null
                ? (int)ItemIndexField.GetValue(item)
                : 0;
            Preload.Invoke(item, new object[] { index });
        }

        internal static void RefreshItem(ItemProto item)
        {
            if (item == null)
                return;

            FindRecipes?.Invoke(item, null);
            ComputeRawMats?.Invoke(item, null);
        }
    }
}