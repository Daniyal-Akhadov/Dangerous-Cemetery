namespace CodeBase.Infrastructure.States
{
    public interface IExitState
    {
        void Exit();
    }

    public interface IState : IExitState
    {
        void Enter();
    }

    public interface IPayloadState<TPayload> : IExitState
    {
        void Enter(TPayload payload);
    }
}