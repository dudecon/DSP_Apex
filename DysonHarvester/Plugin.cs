using BepInEx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DysonHarvester
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // Super basic example: add text to the main menu
            SceneManager.sceneLoaded += OnSceneLoaded;
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded! (Basic main menu text test)");
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Try on scene load, and also use a delayed check for reliability
            AddBasicMenuText();

            // Retry after a short delay in case UI loads late
            StartCoroutine(DelayedAddText());
        }

        private System.Collections.IEnumerator DelayedAddText()
        {
            yield return new WaitForSeconds(1.0f);
            AddBasicMenuText();
        }

        private void AddBasicMenuText()
        {
            // Only add once
            if (GameObject.Find("BasicModMenuText") != null)
                return;

            // Find a suitable Canvas (DSP main menu uses UI Root / Canvas)
            Canvas canvas = null;
            var root = GameObject.Find("UI Root");
            if (root != null)
            {
                canvas = root.GetComponentInChildren<Canvas>();
            }
            if (canvas == null)
            {
                Canvas[] canvases = GameObject.FindObjectsOfType<Canvas>();
                foreach (var c in canvases)
                {
                    if (c.gameObject.activeInHierarchy && c.enabled)
                    {
                        canvas = c;
                        break;
                    }
                }
            }

            if (canvas == null)
            {
                canvas = GameObject.FindObjectOfType<Canvas>();
            }

            if (canvas == null)
            {
                return; // UI not ready yet
            }

            // Create a simple text object
            GameObject textGO = new GameObject("BasicModMenuText");
            textGO.transform.SetParent(canvas.transform, false);

            Text text = textGO.AddComponent<Text>();
            text.text = "DSP Apex - Basic Test Mod";
            text.fontSize = 24;
            text.color = new Color(0.3f, 0.85f, 1f);

            // Use an existing text's font for better look
            Text sourceText = GameObject.FindObjectOfType<Text>();
            if (sourceText != null && sourceText.font != null)
            {
                text.font = sourceText.font;
            }
            else
            {
                text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            }

            // Bottom-left position on the main menu
            RectTransform rt = textGO.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0f, 0f);
            rt.anchorMax = new Vector2(0f, 0f);
            rt.pivot = new Vector2(0f, 0f);
            rt.anchoredPosition = new Vector2(50f, 60f);

            Logger.LogInfo("Added basic text to main menu!");
        }
    }

    public static class PluginInfo
    {
        public const string PLUGIN_GUID = "com.paulspooner.dsp.basicmenutest";
        public const string PLUGIN_NAME = "Basic Menu Text Test";
        public const string PLUGIN_VERSION = "0.0.1";
    }
}
