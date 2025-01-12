using Game.GUI.Windows.Components;
using NaughtyAttributes;
using UnityEngine;

namespace Runner.UI.DefeatWindow.Components
{
public enum DefeatWindowAction : byte
{
    Unknown = 0,
    OnClickShop = 1,
    OnClickRetry = 2,
}

public class DefeatWindowButton : BaseButton<DefeatWindowAction>
{
    [ContextMenu("Editor simulate click"), Button]
    protected override void SimulateClick() => base.SimulateClick();

    [ContextMenu("Editor force validate"), Button]
    protected override void ForceValidate() => base.ForceValidate();
}
}