using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace MegaStructuresUI
{
    internal static class ReplicatorTabLabelUtil
    {
        private static readonly FieldInfo LocalizerTextField = typeof(Localizer).GetField(
            "text",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        private static readonly FieldInfo LocalizerStringKeyField = typeof(Localizer).GetField(
            "stringKey",
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        internal static void ApplyLabel(GameObject buttonObject, string label)
        {
            if (buttonObject == null || string.IsNullOrEmpty(label))
                return;

            Text labelText = DetachLocalizerText(buttonObject);
            if (labelText == null)
                labelText = FindLabelText(buttonObject);

            if (labelText != null)
                labelText.text = label;
        }

        internal static void RefreshModTabLabels(ReplicatorTabState state)
        {
            if (state == null)
                return;

            for (int i = 0; i < state.TabButtons.Count; i++)
            {
                var button = state.TabButtons[i];
                if (button == null)
                    continue;

                int tab = UiButtonUtil.GetData(button);
                if (tab <= ReplicatorTabConfig.VanillaTabCount)
                    continue;

                ApplyLabel(button.gameObject, ReplicatorTabConfig.GetTabLabel(tab));
            }
        }

        private static Text DetachLocalizerText(GameObject buttonObject)
        {
            Text labelText = null;
            var localizers = buttonObject.GetComponentsInChildren<Localizer>(true);
            for (int i = 0; i < localizers.Length; i++)
            {
                var localizer = localizers[i];
                if (localizer == null)
                    continue;

                if (LocalizerTextField?.GetValue(localizer) is Text text)
                    labelText = text;

                localizer.enabled = false;
                LocalizerStringKeyField?.SetValue(localizer, string.Empty);
                Object.Destroy(localizer);
            }

            return labelText;
        }

        private static Text FindLabelText(GameObject buttonObject)
        {
            Transform textTransform = buttonObject.transform.Find("text");
            if (textTransform != null)
            {
                Text namedText = textTransform.GetComponent<Text>();
                if (namedText != null)
                    return namedText;
            }

            return buttonObject.GetComponentInChildren<Text>(true);
        }
    }
}