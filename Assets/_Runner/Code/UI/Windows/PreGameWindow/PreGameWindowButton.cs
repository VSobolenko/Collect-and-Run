using Game.GUI.Windows.Components;
using NaughtyAttributes;
using UnityEngine;

namespace Runner.UI.PreGameWindow.Components
{
public enum PreGameWindowAction : byte
{
    Unknown = 0,
    OnClickSettings = 1,
    OnClickShop = 2,
    OnClickDiscord = 3,
}

public class PreGameWindowButton : BaseButton<PreGameWindowAction>
{
    [ContextMenu("Editor simulate click"), Button]
    protected override void SimulateClick() => base.SimulateClick();

    [ContextMenu("Editor force validate"), Button]
    protected override void ForceValidate() => base.ForceValidate();
}
}