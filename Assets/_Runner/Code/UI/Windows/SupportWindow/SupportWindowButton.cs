using Game.GUI.Windows.Components;
using NaughtyAttributes;
using UnityEngine;

namespace Runner.UI.SupportWindow
{
public enum SupportWindowAction : byte
{
    Unknown = 0,
    OnClickClose = 1,
    OnClickSimple = 2,
    OnClickIntermediate = 3,
    OnClickAdvanced = 4,
}

public class SupportWindowButton : BaseButton<SupportWindowAction>
{
    [ContextMenu("Editor simulate click"), Button]
    protected override void SimulateClick() => base.SimulateClick();

    [ContextMenu("Editor force validate"), Button]
    protected override void ForceValidate() => base.ForceValidate();
}
}