using CodeBase.Data;
using CodeBase.Infrastructure.Factories;
using CodeBase.Services.Progress;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IProgressService _progressService;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IProgressService progressService, IGameFactory gameFactory)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
        }

        public PlayerProgress LoadProgress()
        {
            Debug.Log(PlayerPrefs.GetString(ProgressKey));
            return PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress savedProgress in _gameFactory.ProgressSavers)
                savedProgress.UpdateProgress(_progressService.PlayerProgress);

            PlayerPrefs.SetString(ProgressKey, _progressService.PlayerProgress.ToJson());
        }
    }
}
