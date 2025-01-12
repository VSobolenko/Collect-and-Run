using Game.FSMCore.Machines;
using Runner.LoopFSM.States;
using VContainer;

namespace Runner.LoopFSM
{
public class GameFSM
{
    private readonly IObjectResolver _resolver;
    private LiteStateMachine _stateMachine;

    public GameFSM(IObjectResolver resolver)
    {
        _resolver = resolver;
    }

    public LiteStateMachine ActualFSM => _stateMachine;

    public void ConfigureFSM()
    {
        _stateMachine = new LiteStateMachine();
        _stateMachine.AddState(_resolver.Resolve<EntryPointState>())
                     .AddState(_resolver.Resolve<RunnerState>())
                     .AddState(_resolver.Resolve<DefeatState>())
                     .AddState(_resolver.Resolve<VictoryState>())
                     .AddState(_resolver.Resolve<DisposeState>())
                     .EnableLogger();
    }

    public void StartFSM()
    {
        _stateMachine.TransitTo<EntryPointState>();
    }

    public void DisposeActiveFsm()
    {
        _stateMachine.TransitTo<DisposeState>();
    }
}
}