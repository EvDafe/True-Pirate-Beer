﻿namespace CodeBase.Infrastructure.States
{
    internal class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        public GameLoopState(GameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}