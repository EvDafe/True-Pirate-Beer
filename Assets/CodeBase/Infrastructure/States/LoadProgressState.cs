using CodeBase.Data;
using CodeBase.Services.Progress;
using CodeBase.Services.SaveLoad;
using System.Reflection;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string InitLevel = "Test";
        private readonly GameStateMachine _stateMachine;
        private readonly IProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, IProgressService progressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrCreateNew();
            _stateMachine.Enter<LoadLevelState, string>(_progressService.PlayerProgress.WorldData.PositionOnLevel.LevelName);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrCreateNew() => 
            _progressService.PlayerProgress = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(InitLevel);
            progress.PlayerState.MaxHealth = 100;
            progress.PlayerState.CurrentHealth = progress.PlayerState.MaxHealth;
            return progress;
        }
    }
}