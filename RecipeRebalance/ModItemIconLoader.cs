using System.IO;
using System.Reflection;
using BepInEx.Logging;
using UnityEngine;

namespace RecipeRebalance
{
    /// <summary>
    /// Loads optional per-item PNG icons from the plugin Icons/Item folder.
    /// Falls back to vanilla Resources paths set on ItemProto.IconPath.
    /// </summary>
    internal static class ModItemIconLoader
    {
        private const string IconFolder = "Icons/Item";

        private static readonly FieldInfo IconSpriteField = typeof(ItemProto).GetField(
            "_iconSprite",
            BindingFlags.Instance | BindingFlags.NonPublic);

        internal static void Apply(ManualLogSource logger)
        {
            TryApplyFileIcon(ApexIds.Helium, "helium.png", logger);
        }

        private static void TryApplyFileIcon(int itemId, string fileName, ManualLogSource logger)
        {
            if (!LDB.items.Exist(itemId))
                return;

            string path = Path.Combine(GetPluginDirectory(), IconFolder, fileName);
            if (!File.Exists(path))
                return;

            var sprite = LoadSprite(path);
            if (sprite == null)
            {
                logger.LogWarning($"RecipeRebalance: could not load icon sprite from {path}");
                return;
            }

            var item = LDB.items.Select(itemId);
            IconSpriteField?.SetValue(item, sprite);
            logger.LogInfo($"RecipeRebalance: applied file icon {fileName} to item {itemId}.");
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
            string location = typeof(ModItemIconLoader).Assembly.Location;
            return string.IsNullOrEmpty(location)
                ? string.Empty
                : Path.GetDirectoryName(location);
        }
    }
}