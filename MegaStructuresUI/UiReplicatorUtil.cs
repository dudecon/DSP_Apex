using System;
using System.Reflection;

namespace MegaStructuresUI
{
    internal static class UiReplicatorUtil
    {
        private static readonly FieldInfo CurrentTypeField = typeof(UIReplicatorWindow).GetField(
            "currentType",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        private static readonly MethodInfo OnTypeButtonClickMethod = typeof(UIReplicatorWindow).GetMethod(
            "OnTypeButtonClick",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        internal static int GetCurrentType(UIReplicatorWindow window)
        {
            if (window == null || CurrentTypeField == null)
                return 1;

            object value = CurrentTypeField.GetValue(window);
            return value is int intValue ? intValue : 1;
        }

        internal static Action<int> CreateTypeButtonClickHandler(UIReplicatorWindow window)
        {
            if (window == null || OnTypeButtonClickMethod == null)
                return _ => { };

            return tab => OnTypeButtonClickMethod.Invoke(window, new object[] { tab });
        }
    }
}