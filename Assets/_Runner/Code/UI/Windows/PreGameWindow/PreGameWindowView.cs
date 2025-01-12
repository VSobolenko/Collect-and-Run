using Game.GUI.Windows;
using Game.Inputs;
using NaughtyAttributes;
using Runner.UI.PreGameWindow.Components;
using TMPro;
using UnityEngine;

namespace Runner.UI.PreGameWindow
{
public class PreGameWindowView : WindowUI
{
    [SerializeField] private TextMeshProUGUI _levelCounter;
    public InputEventHandler inputEventHandler;
    public PreGameWindowButton[] windowButtons;

    public void SetLevelCounterText(int levelId) => _levelCounter.text = $"Level {levelId + 1}";

#if UNITY_EDITOR

    [ContextMenu("Collect window buttons"), Button]
    private void CollectWindowButtons()
    {
        windowButtons = GetComponentsInChildren<PreGameWindowButton>(true);

        UnityEditor.EditorUtility.SetDirty(this);
    }

#endif
}
}