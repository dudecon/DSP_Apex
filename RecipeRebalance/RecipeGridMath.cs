namespace RecipeRebalance
{
    /// <summary>Pure grid index math for replicator tab placement (testable without LDB).</summary>
    public static class RecipeGridMath
    {
        public const int ApexTab = 3;
        public const int ColumnCount = 14;
        public const int RowCount = 8;

        public static int Encode(int tab, int row, int column) =>
            tab * 1000 + row * 100 + column;

        public static int DecodeTab(int gridIndex) => gridIndex / 1000;

        public static int DecodeRow(int gridIndex) => (gridIndex % 1000) / 100;

        public static int DecodeColumn(int gridIndex) => gridIndex % 100;

        /// <summary>Consecutive Apex-tab slot for recipe index 0..n-1 (row-major).</summary>
        public static int GetApexSlotAtIndex(int index)
        {
            int row = index / ColumnCount + 1;
            int column = index % ColumnCount + 1;
            return Encode(ApexTab, row, column);
        }
    }
}