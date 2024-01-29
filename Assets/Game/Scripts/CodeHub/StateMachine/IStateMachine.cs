namespace CodeHub.StateMachine
{
    public interface IStateMachine
    {
        void EnterState<TState>() where TState : IExitableState;
    }
}
