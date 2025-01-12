using Game.GUI.Windows;
using NaughtyAttributes;
using Runner.UI.DefeatWindow.Components;
using UnityEngine;

namespace Runner.UI.DefeatWindow
{
public class DefeatWindowView : WindowUI
{
    public DefeatWindowButton[] windowButtons;

#if UNITY_EDITOR

    [ContextMenu("Collect window buttons"), Button]
    private void CollectWindowButtons()
    {
        windowButtons = GetComponentsInChildren<DefeatWindowButton>(true);

        UnityEditor.EditorUtility.SetDirty(this);
    }

#endif
}
}