namespace MegaStructuresUI
{
    internal static class ReplicatorTabConfig
    {
        internal const int VanillaTabCount = 2;
        internal const int TotalTabCount = 7;
        internal const int ApexTab = 3;
        internal const int FirstReservedTab = 4;

        internal static string GetTabLabel(int tab)
        {
            switch (tab)
            {
                case 3: return "Apex";
                case 4: return "Orbital";
                case 5: return "0G";
                case 6: return "Weapons";
                case 7: return "Exotic";
                default: return $"Tab {tab}";
            }
        }
    }
}