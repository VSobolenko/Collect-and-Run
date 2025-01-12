using Runner.LoopFSM;
using VContainer.Unity;

namespace Runner.EntryPoints
{
public class GameEntryPoint : IStartable
{
    private GameFSM _fsm;

    public GameEntryPoint(GameFSM fsm)
    {
        _fsm = fsm;
    }

    public void Start()
    {
        _fsm.ConfigureFSM();
        _fsm.StartFSM();
    }
}
}