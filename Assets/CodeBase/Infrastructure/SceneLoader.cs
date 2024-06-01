using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string name, Action onLoad = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoad));
        private IEnumerator LoadScene(string name, Action onLoad = null)
        {
            if(SceneManager.GetActiveScene().name == name)
            {
                onLoad?.Invoke();
                yield break;
            }

            AsyncOperation wait = SceneManager.LoadSceneAsync(name);
            yield return new WaitUntil(() => wait.isDone);

            onLoad?.Invoke();
        }
    }
}

