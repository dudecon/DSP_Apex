using System;
using System.Reflection;
using UnityEngine.UI;

namespace MegaStructuresUI
{
    internal static class UiButtonUtil
    {
        private static readonly FieldInfo DataField = typeof(UIButton).GetField(
            "data",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        private static readonly FieldInfo HighlightedField = typeof(UIButton).GetField(
            "highlighted",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        private static readonly FieldInfo ButtonField = typeof(UIButton).GetField(
            "button",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        private static readonly MethodInfo AddOnClick = typeof(UIButton).GetMethod(
            "add_onClick",
            BindingFlags.Instance | BindingFlags.Public);

        private static readonly MethodInfo RemoveOnClick = typeof(UIButton).GetMethod(
            "remove_onClick",
            BindingFlags.Instance | BindingFlags.Public);

        internal static int GetData(UIButton button)
        {
            if (button == null || DataField == null)
                return 0;

            object value = DataField.GetValue(button);
            return value is int intValue ? intValue : 0;
        }

        internal static void SetData(UIButton button, int data)
        {
            DataField?.SetValue(button, data);
        }

        internal static void SetHighlighted(UIButton button, bool highlighted)
        {
            HighlightedField?.SetValue(button, highlighted);
        }

        internal static Button GetButton(UIButton button)
        {
            return ButtonField?.GetValue(button) as Button;
        }

        internal static void SetInteractable(UIButton button, bool interactable)
        {
            var unityButton = GetButton(button);
            if (unityButton != null)
                unityButton.interactable = interactable;
        }

        internal static void AddClickHandler(UIButton button, Action<int> handler)
        {
            if (button == null || handler == null || AddOnClick == null)
                return;

            AddOnClick.Invoke(button, new object[] { handler });
        }

        internal static void RemoveClickHandler(UIButton button, Action<int> handler)
        {
            if (button == null || handler == null || RemoveOnClick == null)
                return;

            RemoveOnClick.Invoke(button, new object[] { handler });
        }
    }
}