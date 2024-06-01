using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Factories;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.Progress;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string BootSceneName = "BootStrapScene";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(BootSceneName, EnterLoadLevel);
        }

        private void EnterLoadLevel() => 
            _gameStateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            _services.RigisterSingle<IAssetProvider>(new AssetProvider());
            _services.RigisterSingle(InputService());
            _services.RigisterSingle<IProgressService>(new ProgressService());
            _services.RigisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IAssetProvider>()));
            _services.RigisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IProgressService>(), _services.Single<IGameFactory>()));
        }

        private IInputService InputService()
        {
             return new PCInputService();
        }

        public void Exit()
        {

        }
    }
}
