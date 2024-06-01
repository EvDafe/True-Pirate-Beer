using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Player;
using CodeBase.Services.Progress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public event Action PlayerCreated;

        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public List<ISavedProgress> ProgressSavers { get; } = new();

        public GameObject PlayerGameObject { get; private set; }

        public GameFactory(IAssetProvider assets) =>
            _assets = assets;

        public void CreateHud(GameObject hero)
        {
            var hud = InstatiateRigister(AssetPath.HudPrefabPath);
            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<PlayerHealth>());
        }

        public GameObject CreateHero(GameObject spawnPoint)
        {
            PlayerGameObject = InstatiateRigister(AssetPath.PlayerPrefabPath, spawnPoint.transform.position);
            PlayerCreated?.Invoke();
            return PlayerGameObject;
        }

        private GameObject InstatiateRigister(string prefabPath, Vector3 position)
        {
            var instance = _assets.Instantiate<GameObject>(prefabPath, position);
            RegisterProgressWatchers(instance);
            return instance;
        }

        private GameObject InstatiateRigister(string prefabPath)
        {
            var instance = _assets.Instantiate<GameObject>(prefabPath);
            RegisterProgressWatchers(instance);
            return instance;
        }

        private void RegisterProgressWatchers(GameObject instance)
        {
            foreach (ISavedProgressReader progressReader in instance.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressSaver)
                ProgressSavers.Add(progressSaver);

            ProgressReaders.Add(progressReader);
        }

        public void CleanUp()
        {
            ProgressReaders.Clear();
            ProgressSavers.Clear();
        }
    }
}
