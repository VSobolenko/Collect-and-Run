using Game.GUI.Windows;
using NaughtyAttributes;
using UnityEngine;
using WarehouseKeeper.UI.Windows;

namespace Runner.UI.SupportWindow
{
public class SupportWindowView : WindowUI
{
    public SupportWindowButton[] windowButtons;
    [SerializeField] private InformedText _informedText;

    public void ShowInformer(string text) => _informedText.Show(text);

#if UNITY_EDITOR

    [ContextMenu("Collect window buttons"), Button]
    private void CollectWindowButtons()
    {
        windowButtons = GetComponentsInChildren<SupportWindowButton>(true);

        UnityEditor.EditorUtility.SetDirty(this);
    }

#endif
}
}