using System.Reflection;
using BepInEx.Logging;
using UnityEngine;

namespace RecipeRebalance
{
    /// <summary>
    /// Vanilla itemIds/itemNames tables are fixed-length; re-running InitItemIds after
    /// registering mod items maps new IDs onto stale property names (e.g. 棱镜/Prism).
    /// </summary>
    internal static class ModItemHintUtil
    {
        private static readonly FieldInfo PropertyNameField = typeof(ItemProto).GetField(
            "<propertyName>k__BackingField",
            BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly FieldInfo PropertyIconField = typeof(ItemProto).GetField(
            "_propertyIconSprite",
            BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly FieldInfo PropertyIconSmallField = typeof(ItemProto).GetField(
            "_propertyIconSpriteSmall",
            BindingFlags.NonPublic | BindingFlags.Instance);

        private static bool logged;

        internal static void ApplyHeliumHints(ManualLogSource logger)
        {
            if (!LDB.items.Exist(ApexIds.Helium))
                return;

            var helium = LDB.items.Select(ApexIds.Helium);
            var template = LDB.items.Select(ApexIds.Deuterium);
            if (helium == null)
                return;

            ConfigureFluidHints(
                helium,
                template,
                propertyName: "Apex Helium",
                descFields: new[] { 1 });

            if (!logged)
            {
                logged = true;
                logger.LogInfo("RecipeRebalance: configured Helium item hint metadata.");
            }
        }

        private static void ConfigureFluidHints(
            ItemProto item,
            ItemProto template,
            string propertyName,
            int[] descFields)
        {
            if (item == null)
                return;

            PropertyNameField?.SetValue(item, propertyName);
            item.DescFields = descFields;

            if (template == null)
                return;

            if (string.IsNullOrEmpty(item.IconTag))
                item.IconTag = template.IconTag;

            PropertyIconField?.SetValue(item, PropertyIconField.GetValue(template));
            PropertyIconSmallField?.SetValue(item, PropertyIconSmallField.GetValue(template));
        }
    }
}