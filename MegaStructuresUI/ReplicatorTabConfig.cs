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
                case 5: return "Zero G";
                case 6: return "Weapons";
                case 7: return "Exotic";
                default: return $"Tab {tab}";
            }
        }

        internal static string GetTabIconFileName(int tab)
        {
            switch (tab)
            {
                case 3: return "tab3-apex.png";
                case 4: return "tab4-orbital.png";
                case 5: return "tab5-0g.png";
                case 6: return "tab6-weapons.png";
                case 7: return "tab7-exotic.png";
                default: return null;
            }
        }
    }
}