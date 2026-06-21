using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace MegaStructuresUI
{
    internal static class ReplicatorTabIconLoader
    {
        private const string IconFolder = "Icons/ReplicatorTabs";

        internal static void ApplyTabIcon(UIButton button, int tab)
        {
            if (button == null)
                return;

            string fileName = ReplicatorTabConfig.GetTabIconFileName(tab);
            if (string.IsNullOrEmpty(fileName))
                return;

            string path = Path.Combine(GetPluginDirectory(), IconFolder, fileName);
            if (!File.Exists(path))
            {
                Plugin.Log?.LogWarning($"MegaStructuresUI: tab icon not found at {path}");
                return;
            }

            Sprite sprite = LoadSprite(path);
            if (sprite == null)
                return;

            Image image = FindTabIconImage(button.gameObject);
            if (image == null)
            {
                Plugin.Log?.LogWarning($"MegaStructuresUI: no icon Image found on {button.gameObject.name}");
                return;
            }

            image.sprite = sprite;
        }

        private static Image FindTabIconImage(GameObject buttonObject)
        {
            Transform iconTransform = buttonObject.transform.Find("icon");
            if (iconTransform != null)
            {
                Image iconImage = iconTransform.GetComponent<Image>();
                if (iconImage != null)
                    return iconImage;
            }

            Image[] images = buttonObject.GetComponentsInChildren<Image>(true);
            for (int i = 0; i < images.Length; i++)
            {
                Image candidate = images[i];
                if (candidate != null && candidate.gameObject != buttonObject)
                    return candidate;
            }

            return null;
        }

        private static Sprite LoadSprite(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            var texture = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            if (!texture.LoadImage(bytes))
                return null;

            texture.filterMode = FilterMode.Bilinear;
            return Sprite.Create(
                texture,
                new Rect(0f, 0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                100f);
        }

        private static string GetPluginDirectory()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            return string.IsNullOrEmpty(location)
                ? string.Empty
                : Path.GetDirectoryName(location);
        }
    }
}