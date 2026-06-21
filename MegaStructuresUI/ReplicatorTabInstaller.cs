using UnityEngine;
using UnityEngine.UI;

namespace MegaStructuresUI
{
    internal static class ReplicatorTabInstaller
    {
        private const float CompactScale = 0.82f;

        internal static void EnsureInstalled(UIReplicatorWindow window)
        {
            if (window == null)
                return;

            var state = GetOrAddState(window);
            if (state.Installed)
                return;

            Install(window, state);
            state.Installed = true;
        }

        internal static void WireTabClicks(UIReplicatorWindow window)
        {
            if (window == null)
                return;

            var state = GetOrAddState(window);
            EnsureInstalled(window);

            UnwireTabClicks(window);

            for (int i = 0; i < state.TabButtons.Count; i++)
            {
                var button = state.TabButtons[i];
                if (button == null)
                    continue;

                int tab = UiButtonUtil.GetData(button);
                if (tab <= ReplicatorTabConfig.VanillaTabCount)
                    continue;

                var handler = CreateClickHandler(window);
                UiButtonUtil.AddClickHandler(button, handler);
                state.ClickHandlers[button] = handler;
            }

            state.Wired = true;
            SyncTabHighlights(window, UiReplicatorUtil.GetCurrentType(window));
        }

        internal static void UnwireTabClicks(UIReplicatorWindow window)
        {
            if (window == null)
                return;

            var state = window.GetComponent<ReplicatorTabState>();
            if (state == null || !state.Wired)
                return;

            foreach (var entry in state.ClickHandlers)
            {
                if (entry.Key != null && entry.Value != null)
                    UiButtonUtil.RemoveClickHandler(entry.Key, entry.Value);
            }

            state.ClickHandlers.Clear();
            state.Wired = false;
        }

        internal static void SyncTabHighlights(UIReplicatorWindow window, int activeType)
        {
            if (window == null)
                return;

            var state = GetOrAddState(window);
            if (!state.Installed)
                return;

            for (int i = 0; i < state.TabButtons.Count; i++)
            {
                var button = state.TabButtons[i];
                if (button == null)
                    continue;

                int tab = UiButtonUtil.GetData(button);
                UiButtonUtil.SetHighlighted(button, tab == activeType);
                UiButtonUtil.SetInteractable(button, tab != activeType);
            }
        }

        private static System.Action<int> CreateClickHandler(UIReplicatorWindow window)
        {
            return UiReplicatorUtil.CreateTypeButtonClickHandler(window);
        }

        private static void Install(UIReplicatorWindow window, ReplicatorTabState state)
        {
            state.TabButtons.Clear();
            state.TabButtons.Add(window.typeButton1);
            state.TabButtons.Add(window.typeButton2);

            var template = window.typeButton2;
            if (template == null)
                return;

            var templateRect = template.transform as RectTransform;
            var parent = template.transform.parent;
            if (templateRect == null || parent == null)
                return;

            float step = GuessTabSpacing(window.typeButton1, templateRect);

            for (int tab = ReplicatorTabConfig.ApexTab; tab <= ReplicatorTabConfig.TotalTabCount; tab++)
            {
                var clone = Object.Instantiate(template.gameObject, parent);
                clone.name = $"typeButton{tab}";
                clone.transform.SetSiblingIndex(template.transform.GetSiblingIndex() + (tab - ReplicatorTabConfig.VanillaTabCount));

                var uiButton = clone.GetComponent<UIButton>();
                if (uiButton == null)
                    continue;

                UiButtonUtil.SetData(uiButton, tab);

                var rect = clone.transform as RectTransform;
                if (rect != null)
                {
                    rect.anchoredPosition = new Vector2(
                        templateRect.anchoredPosition.x + step * (tab - ReplicatorTabConfig.VanillaTabCount),
                        templateRect.anchoredPosition.y);
                    rect.localScale = templateRect.localScale * CompactScale;
                }

                SetTabLabel(clone, ReplicatorTabConfig.GetTabLabel(tab));
                state.TabButtons.Add(uiButton);
            }
        }

        private static float GuessTabSpacing(UIButton firstButton, RectTransform templateRect)
        {
            if (firstButton != null)
            {
                var firstRect = firstButton.transform as RectTransform;
                if (firstRect != null)
                    return templateRect.anchoredPosition.x - firstRect.anchoredPosition.x;
            }

            return templateRect.rect.width * CompactScale;
        }

        private static void SetTabLabel(GameObject buttonObject, string label)
        {
            var text = buttonObject.GetComponentInChildren<Text>(true);
            if (text != null)
                text.text = label;
        }

        private static ReplicatorTabState GetOrAddState(UIReplicatorWindow window)
        {
            var state = window.GetComponent<ReplicatorTabState>();
            if (state != null)
                return state;

            return window.gameObject.AddComponent<ReplicatorTabState>();
        }
    }
}