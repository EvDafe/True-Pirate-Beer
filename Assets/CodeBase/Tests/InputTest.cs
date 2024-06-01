using CodeBase.Data;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.Progress;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Tests
{
    public class InputTest : MonoBehaviour, ISavedProgress
    {
        private FirstPersonController _player;
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _player = GetComponent<FirstPersonController>();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() != progress.WorldData.PositionOnLevel.LevelName)
                return;
            if (progress.WorldData.PositionOnLevel.Position == null)
                return;

            Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
            Warp(savedPosition);
        }

        public void Disactive()
        {
            _player.enabled = false;
            this.enabled = false;
        }

        private void Warp(Vector3Data to)
        {
            _player._controller.enabled = false;
            _player.transform.position = to.AsUnityVector().AddY(_player._controller.height);
            _player._controller.enabled = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel =
                new PositionOnLevel(transform.position.AsVectorData(), CurrentLevel());
        }

        private static string CurrentLevel() =>
            SceneManager.GetActiveScene().name;

        private void Update()
        {
            if (_inputService.IsAttack())
            {
                Debug.Log("Чипи чипа чапа чапа дуби дуби даба даба");
            } 
        }
    }
}
