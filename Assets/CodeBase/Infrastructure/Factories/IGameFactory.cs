using CodeBase.Services;
using CodeBase.Services.Progress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        GameObject PlayerGameObject { get; }
        event Action PlayerCreated;
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressSavers { get; }

        void CleanUp();
        GameObject CreateHero(GameObject spawnPoint);
        void CreateHud(GameObject hero);
    }
}