using Game.FSMCore.States;
using Runner.Levels;
using Runner.PlayerFeature;

namespace Runner.LoopFSM.States
{
public class DisposeState : QuiteState
{
    private readonly LevelsDirector _levelsDirector;
    private readonly PlayerDirector _playerDirector;

    public DisposeState(LevelsDirector levelsDirector, PlayerDirector playerDirector)
    {
        _levelsDirector = levelsDirector;
        _playerDirector = playerDirector;
    }

    protected override void OnStateActivated()
    {
        _playerDirector.Destroy();
        _levelsDirector.DestroyLevel();
    }
}
}