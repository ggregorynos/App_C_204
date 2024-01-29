namespace CodeHub.StateMachine
{
    public interface IState: IExitableState
    {
        void Enter();
    }
}
