using Runner.Collectors;
using Runner.Collectors.Items.Negative.Types;
using Runner.Collectors.Items.Positive;
using UnityEngine;

namespace Runner.Levels
{
public class LevelsDirector
{
    private readonly LevelsFactory _levelsFactory;
    private readonly PickupsDirector _pickupsDirector;

    private LevelData _activeLevel;

    private int SaveLevelId
    {
        get => PlayerPrefs.GetInt("LevelId", 0);
        set => PlayerPrefs.SetInt("LevelId", value);
    }

    public LevelData ActiveLevel => _activeLevel;

    public LevelsDirector(LevelsFactory levelsFactory, PickupsDirector pickupsDirector)
    {
        _levelsFactory = levelsFactory;
        _pickupsDirector = pickupsDirector;
    }

    public LevelData LoadLevel()
    {
        var level = _levelsFactory.InstantiateLevel(SaveLevelId);
        _activeLevel = new LevelData
        {
            levelId = SaveLevelId,
            level = level,
            positiveItems = _pickupsDirector.SetupPositive(level),
            negativeItems = _pickupsDirector.SetupNegative(level),
        };

        return _activeLevel;
    }

    public void DestroyLevel()
    {
        foreach (var item in _activeLevel.negativeItems)
            item.Release();
        foreach (var item in _activeLevel.positiveItems)
            item.Release();
        _levelsFactory.DestroyLevel(_activeLevel.level);
        _activeLevel = null;
    }

    public void MarkActiveLevelComplete()
    {
        SaveLevelId = _activeLevel.levelId + 1;
    }
}

public class LevelData
{
    public int levelId;
    public Level level;
    public PositiveItem[] positiveItems;
    public NegativeItem[] negativeItems;
}
}