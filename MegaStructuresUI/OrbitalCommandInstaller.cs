using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MegaStructuresUI
{
    internal static class OrbitalCommandInstaller
    {
        private static readonly Dictionary<int, PanelView> PanelsByStar = new Dictionary<int, PanelView>();

        internal static void EnsureInstalled(UIDysonEditor editor)
        {
            if (editor == null || GameMain.localStar == null)
                return;

            int starId = GameMain.localStar.id;
            if (PanelsByStar.ContainsKey(starId))
            {
                RefreshStats(starId);
                return;
            }

            var root = editor.gameObject;
            if (root == null)
                return;

            var panel = new GameObject("ApexOrbitalCommandPanel", typeof(RectTransform));
            panel.transform.SetParent(root.transform, false);
            panel.transform.SetAsLastSibling();

            if (root.GetComponentInParent<Canvas>() == null)
            {
                var canvas = panel.AddComponent<Canvas>();
                canvas.overrideSorting = true;
                canvas.sortingOrder = 100;
                panel.AddComponent<GraphicRaycaster>();
            }

            var rect = panel.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.anchoredPosition = new Vector2(-20f, -20f);
            rect.sizeDelta = new Vector2(300f, 280f);

            var bg = panel.AddComponent<Image>();
            bg.color = new Color(0.05f, 0.08f, 0.12f, 0.9f);
            bg.raycastTarget = true;

            var titleGo = CreateText(panel.transform, "Title", "Orbital Command", 14, TextAnchor.UpperLeft);
            var titleRect = titleGo.GetComponent<RectTransform>();
            titleRect.anchorMin = new Vector2(0f, 1f);
            titleRect.anchorMax = new Vector2(1f, 1f);
            titleRect.pivot = new Vector2(0.5f, 1f);
            titleRect.anchoredPosition = new Vector2(0f, -8f);
            titleRect.sizeDelta = new Vector2(-16f, 24f);

            var statsGo = CreateText(panel.transform, "Stats", "", 11, TextAnchor.UpperLeft);
            var statsRect = statsGo.GetComponent<RectTransform>();
            statsRect.anchorMin = new Vector2(0f, 1f);
            statsRect.anchorMax = new Vector2(1f, 1f);
            statsRect.pivot = new Vector2(0.5f, 1f);
            statsRect.anchoredPosition = new Vector2(8f, -36f);
            statsRect.sizeDelta = new Vector2(-16f, 96f);
            var statsText = statsGo.GetComponent<Text>();

            CreateModuleButton(panel.transform, "Ring +", new Vector2(-12f, -112f), () => Allocate("ring", 1));
            CreateModuleButton(panel.transform, "Ring -", new Vector2(-142f, -112f), () => Allocate("ring", -1));
            CreateModuleButton(panel.transform, "Station +", new Vector2(-12f, -148f), () => Allocate("station", 1));
            CreateModuleButton(panel.transform, "Weapon +", new Vector2(-142f, -148f), () => Allocate("weapon", 1));
            CreateModuleButton(panel.transform, "Production +", new Vector2(-12f, -184f), () => Allocate("production", 1));
            CreateModuleButton(panel.transform, "Ship +", new Vector2(-142f, -184f), () => Allocate("ship", 1));
            CreateModuleButton(panel.transform, "Section +", new Vector2(-12f, -220f), () => Allocate("section", 1));

            var toggleBtn = CreateModuleButton(panel.transform, "Hide", new Vector2(-142f, -220f), () => TogglePanel(starId));
            var view = new PanelView
            {
                Root = panel,
                StatsText = statsText,
                ContentRoot = panel.transform,
                Visible = true
            };

            PanelsByStar[starId] = view;
            RefreshStats(starId);
            Plugin.Log.LogInfo($"OrbitalCommand: installed panel for star {starId}");
        }

        internal static void RefreshStats(int starId)
        {
            if (!PanelsByStar.TryGetValue(starId, out var view) || view.StatsText == null)
                return;

            var alloc = OrbitalCommandState.GetOrCreate(starId);
            OrbitalCommandStatsLogic.ApplyComputedStats(alloc, OrbitalCommandStatsLogic.ComputeStats(alloc));
            view.StatsText.text = OrbitalCommandStatsLogic.FormatStatsText(alloc);
        }

        private static void Allocate(string category, int delta)
        {
            if (GameMain.localStar == null)
                return;

            int starId = GameMain.localStar.id;
            OrbitalCommandState.AllocateModule(starId, category, delta);
            RefreshStats(starId);
            Plugin.Log.LogInfo($"OrbitalCommand: {category} {(delta > 0 ? "+" : "")}{delta}");
        }

        private static void TogglePanel(int starId)
        {
            if (!PanelsByStar.TryGetValue(starId, out var view) || view.ContentRoot == null)
                return;

            view.Visible = !view.Visible;
            for (int i = 0; i < view.ContentRoot.childCount; i++)
            {
                var child = view.ContentRoot.GetChild(i);
                if (child.name == "Title")
                    continue;
                child.gameObject.SetActive(view.Visible);
            }
        }

        private static GameObject CreateText(Transform parent, string name, string content, int fontSize, TextAnchor align)
        {
            var go = new GameObject(name, typeof(RectTransform));
            go.transform.SetParent(parent, false);
            var text = go.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = fontSize;
            text.color = Color.white;
            text.alignment = align;
            text.text = content;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            return go;
        }

        private static Button CreateModuleButton(Transform parent, string label, Vector2 anchoredPos, UnityEngine.Events.UnityAction onClick)
        {
            var go = new GameObject(label, typeof(RectTransform));
            go.transform.SetParent(parent, false);
            var img = go.AddComponent<Image>();
            img.color = new Color(0.15f, 0.25f, 0.35f, 0.95f);
            var btn = go.AddComponent<Button>();
            btn.targetGraphic = img;
            btn.onClick.AddListener(onClick);

            var textGo = new GameObject("Text", typeof(RectTransform));
            textGo.transform.SetParent(go.transform, false);
            var text = textGo.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = 12;
            text.color = Color.white;
            text.alignment = TextAnchor.MiddleCenter;
            text.text = label;

            var textRect = textGo.GetComponent<RectTransform>();
            textRect.anchorMin = Vector2.zero;
            textRect.anchorMax = Vector2.one;
            textRect.offsetMin = Vector2.zero;
            textRect.offsetMax = Vector2.zero;

            var rect = go.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.pivot = new Vector2(1f, 1f);
            rect.anchoredPosition = anchoredPos;
            rect.sizeDelta = new Vector2(120f, 28f);
            return btn;
        }

        private sealed class PanelView
        {
            internal GameObject Root;
            internal Text StatsText;
            internal Transform ContentRoot;
            internal bool Visible;
        }
    }
}