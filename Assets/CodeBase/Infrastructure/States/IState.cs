namespace CodeBase.Infrastructure.States
{
    public interface IState : IExitableState
    {
        public void Enter();
    }

    public interface IExitableState
    {
        public void Exit();
    }

    public interface IPayLoadedState<TPayLoad> : IExitableState
    {
        public void Enter(TPayLoad payLoad);
    }
}