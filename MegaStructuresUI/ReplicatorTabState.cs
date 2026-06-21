using System;
using System.Collections.Generic;
using UnityEngine;

namespace MegaStructuresUI
{
    internal sealed class ReplicatorTabState : MonoBehaviour
    {
        internal readonly List<UIButton> TabButtons = new List<UIButton>();
        internal readonly Dictionary<UIButton, Action<int>> ClickHandlers = new Dictionary<UIButton, Action<int>>();
        internal bool Installed;
        internal bool Wired;
    }
}