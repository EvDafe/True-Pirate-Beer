using CodeBase.Infrastructure.Factories;
using CodeBase.Services.Progress;
using System;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private const string SpawnPointTag = "PlayerSpawnPoint";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory, IProgressService progressService = null)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string payLoad)
        {
            _curtain.Show();
            _gameFactory.CleanUp();
            _sceneLoader.Load(payLoad, OnLoad);
        }

        private void OnLoad()
        {
            InitGameWorld();
            InformProgressReaders();
            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (var reader in _gameFactory.ProgressReaders)
                reader.LoadProgress(_progressService.PlayerProgress);
        }

        private void InitGameWorld()
        {
            var spawnPoint = GameObject.FindGameObjectWithTag(SpawnPointTag);
            var hero = _gameFactory.CreateHero(spawnPoint);
            _gameFactory.CreateHud(hero);
        }

        public void Exit() => 
            _curtain.Hide();
    }
}