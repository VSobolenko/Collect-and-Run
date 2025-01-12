using Game.GUI.Windows;
using NaughtyAttributes;
using Runner.UI.VictoryWindow.Components;
using TMPro;
using UnityEngine;

namespace Runner.UI.VictoryWindow
{
public class VictoryWindowView : WindowUI
{
    [SerializeField] private TextMeshProUGUI _levelCounter;
    public VictoryWindowButton[] windowButtons;
    public void SetLevelCounterText(int levelId) => _levelCounter.text = $"Level {levelId + 1}";

#if UNITY_EDITOR

    [ContextMenu("Collect window buttons"), Button]
    private void CollectWindowButtons()
    {
        windowButtons = GetComponentsInChildren<VictoryWindowButton>(true);

        UnityEditor.EditorUtility.SetDirty(this);
    }

#endif
}
}