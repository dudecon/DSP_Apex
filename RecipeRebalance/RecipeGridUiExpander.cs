using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace RecipeRebalance
{
    /// <summary>
    /// Extends the recipe matrix from 8 vanilla rows (I–VIII) to 9 (adds row IX for mod smelts).
    /// </summary>
    internal static class RecipeGridUiExpander
    {
        internal const int ColumnCount = 14;
        internal const int VanillaRowCount = 8;
        internal const float DefaultCellSize = 46f;

        private const float RectUvScale = 0.06521739f;
        private const float RectHeightScale = 1.15f;
        private const float GridCellGap = 0.04f;
        private const string GridProperty = "_Grid";
        private const string RectProperty = "_Rect";

        private static readonly Dictionary<int, Vector2> OriginalSizeDeltas = new Dictionary<int, Vector2>();

        private static readonly FieldInfo PickerRowCount = Field(typeof(UIRecipePicker), "rowCount");
        private static readonly FieldInfo PickerColCount = Field(typeof(UIRecipePicker), "colCount");
        private static readonly FieldInfo PickerIconMat = Field(typeof(UIRecipePicker), "iconMat");
        private static readonly FieldInfo PickerGridSize = Field(typeof(UIRecipePicker), "kGridSize");

        private static readonly FieldInfo ReplicatorWindowRect = Field(typeof(UIReplicatorWindow), "windowRect");
        private static readonly FieldInfo ReplicatorColCount = Field(typeof(UIReplicatorWindow), "colCount");
        private static readonly FieldInfo ReplicatorRecipeRowCount = Field(typeof(UIReplicatorWindow), "recipeRowCount");
        private static readonly FieldInfo ReplicatorRecipeBgMat = Field(typeof(UIReplicatorWindow), "recipeBgMat");
        private static readonly FieldInfo ReplicatorRecipeIconMat = Field(typeof(UIReplicatorWindow), "recipeIconMat");
        private static readonly FieldInfo ReplicatorGridSize = Field(typeof(UIReplicatorWindow), "kGridSize");

        internal static void ApplyPicker(UIRecipePicker picker)
        {
            if (picker == null)
                return;

            int rows = RecipeGrid.ModSmeltRow;
            float cellSize = ReadNumericField(PickerGridSize, picker, DefaultCellSize);
            float extraHeight = (rows - VanillaRowCount) * cellSize;

            PickerColCount?.SetValue(picker, ColumnCount);
            PickerRowCount?.SetValue(picker, rows);

            ExtendHeightFromBaseline(picker.iconImage?.rectTransform, extraHeight);
            EnsureHighlightCellSize(picker.selImage?.rectTransform, cellSize);

            UpdateIconMaterial(picker.iconImage?.material, ColumnCount, rows);
            UpdateIconMaterial(PickerIconMat?.GetValue(picker) as Material, ColumnCount, rows);
        }

        internal static void ApplyReplicator(UIReplicatorWindow window)
        {
            if (window == null)
                return;

            int rows = RecipeGrid.ModSmeltRow;
            float cellSize = ReadNumericField(ReplicatorGridSize, window, DefaultCellSize);
            float extraHeight = (rows - VanillaRowCount) * cellSize;

            ReplicatorColCount?.SetValue(window, ColumnCount);
            ReplicatorRecipeRowCount?.SetValue(window, rows);

            var iconsRect = window.recipeIcons?.rectTransform;
            var bgRect = window.recipeBg?.rectTransform;

            ExtendHeightFromBaseline(iconsRect, extraHeight);
            ExtendHeightFromBaseline(bgRect, extraHeight);
            ExtendHeightFromBaseline(window.recipeGroup, extraHeight);
            ExtendHeightFromBaseline(ReplicatorWindowRect?.GetValue(window) as RectTransform, extraHeight);

            if (iconsRect != null && bgRect != null)
                bgRect.sizeDelta = iconsRect.sizeDelta;

            UpdateIconMaterial(window.recipeIcons?.material, ColumnCount, rows);
            UpdateIconMaterial(ReplicatorRecipeIconMat?.GetValue(window) as Material, ColumnCount, rows);
            UpdateGridMaterial(window.recipeBg?.material, ColumnCount, rows);
            UpdateGridMaterial(ReplicatorRecipeBgMat?.GetValue(window) as Material, ColumnCount, rows);
        }

        private static FieldInfo Field(System.Type type, string name)
        {
            return type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        private static float ReadNumericField(FieldInfo field, object instance, float fallback)
        {
            if (field == null || instance == null)
                return fallback;

            object value = field.GetValue(instance);
            if (value is int intValue)
                return intValue;

            if (value is float floatValue)
                return floatValue;

            return fallback;
        }

        private static void CaptureBaseline(RectTransform rect)
        {
            if (rect == null)
                return;

            int id = rect.GetInstanceID();
            if (!OriginalSizeDeltas.ContainsKey(id))
                OriginalSizeDeltas[id] = rect.sizeDelta;
        }

        private static void ExtendHeightFromBaseline(RectTransform rect, float extraHeight)
        {
            if (rect == null || extraHeight <= 0f)
                return;

            CaptureBaseline(rect);
            Vector2 original = OriginalSizeDeltas[rect.GetInstanceID()];
            rect.sizeDelta = new Vector2(original.x, original.y + extraHeight);
        }

        private static void EnsureHighlightCellSize(RectTransform rect, float cellSize)
        {
            if (rect == null)
                return;

            rect.sizeDelta = new Vector2(cellSize, cellSize);
        }

        private static void UpdateGridMaterial(Material material, int columns, int rows)
        {
            if (material == null || !material.HasProperty(GridProperty))
                return;

            material.SetVector(GridProperty, new Vector4(columns, rows, GridCellGap, GridCellGap));
        }

        private static void UpdateIconMaterial(Material material, int columns, int rows)
        {
            if (material == null)
                return;

            UpdateGridMaterial(material, columns, rows);

            if (material.HasProperty(RectProperty))
            {
                material.SetVector(
                    RectProperty,
                    new Vector4(RectUvScale, RectUvScale, RectHeightScale, RectHeightScale));
            }
        }
    }
}