using System.Reflection;

namespace RecipeRebalance
{
    internal static class ProtoUtil
    {
        private static readonly MethodInfo FindRecipes = typeof(ItemProto).GetMethod(
            "FindRecipes",
            BindingFlags.Instance | BindingFlags.NonPublic);

        private static readonly MethodInfo ComputeRawMats = typeof(ItemProto).GetMethod(
            "ComputeRawMats",
            BindingFlags.Instance | BindingFlags.NonPublic);

        internal static void RefreshItem(ItemProto item)
        {
            if (item == null)
                return;

            FindRecipes?.Invoke(item, null);
            ComputeRawMats?.Invoke(item, null);
        }
    }
}