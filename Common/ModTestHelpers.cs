using System;

namespace DspApex.Common
{
    /// <summary>Shared assertion helpers for suite unit test projects.</summary>
    public static class ModTestHelpers
    {
        public static void AssertContiguousIds(int[] ids, int expectedStart)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            for (int i = 0; i < ids.Length; i++)
            {
                if (ids[i] != expectedStart + i)
                {
                    throw new InvalidOperationException(
                        $"Expected id {expectedStart + i} at index {i}, got {ids[i]}.");
                }
            }
        }

        public static void AssertGridRoundTrip(int gridIndex, int tab, int row, int column)
        {
            if (gridIndex / 1000 != tab)
                throw new InvalidOperationException($"Tab mismatch for grid {gridIndex}.");

            if ((gridIndex % 1000) / 100 != row)
                throw new InvalidOperationException($"Row mismatch for grid {gridIndex}.");

            if (gridIndex % 100 != column)
                throw new InvalidOperationException($"Column mismatch for grid {gridIndex}.");
        }
    }
}