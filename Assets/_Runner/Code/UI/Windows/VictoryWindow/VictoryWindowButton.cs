using Game.GUI.Windows.Components;
using NaughtyAttributes;
using UnityEngine;

namespace Runner.UI.VictoryWindow.Components
{
public enum VictoryWindowAction : byte
{
    Unknown = 0,
    OnClickGet = 1,
    OnClickGetSemicircleFortune = 2,
}

public class VictoryWindowButton : BaseButton<VictoryWindowAction>
{
    [ContextMenu("Editor simulate click"), Button]
    protected override void SimulateClick() => base.SimulateClick();

    [ContextMenu("Editor force validate"), Button]
    protected override void ForceValidate() => base.ForceValidate();
}
}