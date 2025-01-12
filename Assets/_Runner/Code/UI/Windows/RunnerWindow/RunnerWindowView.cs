using Game.GUI.Windows;
using Game.Inputs;
using NaughtyAttributes;
using Runner.UI.GameWindow.Components;
using TMPro;
using UnityEngine;

namespace Runner.UI.GameWindow
{
public class RunnerWindowView : WindowUI
{
    [SerializeField] private TextMeshProUGUI _levelCounter;
    public RunnerWindowButton[] windowButtons;
    public RectTransform lerpArea;
    public InputEventHandler inputEventHandler;

    public void SetLevelCounterText(int levelId) => _levelCounter.text = $"Level {levelId + 1}";

#if UNITY_EDITOR

    [ContextMenu("Collect window buttons"), Button]
    private void CollectWindowButtons()
    {
        windowButtons = GetComponentsInChildren<RunnerWindowButton>(true);

        UnityEditor.EditorUtility.SetDirty(this);
    }

#endif
}
}