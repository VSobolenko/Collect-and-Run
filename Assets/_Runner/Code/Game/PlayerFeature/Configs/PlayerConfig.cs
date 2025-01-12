using System;
using System.Collections;
using Runner.Common;
using Runner.Movements;
using UnityEngine;

namespace Runner.PlayerFeature.Configs
{
[CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = GameData.EditorName + "/Player config")]
public class PlayerConfig : ScriptableObject, IEnumerable
{
    public float roadSpeed;
    public float horizontalSpeed;
    public RotateToMoveDirection.Settings rotationSettings;
    public int defaultProgress;
    public int maxProgress;
    public PlayerStageConfig[] configs;

    public PlayerStageConfig this[int index] => configs[index];
    public IEnumerator GetEnumerator() => configs.GetEnumerator();

    public bool HasConfigByIndex(int index) => index < configs.Length;
}

[Serializable]
public class PlayerStageConfig
{
    public int debugIndex;
    public int requireProgress;
    public string viewKey;
    public Color progressBarColor;
}
}