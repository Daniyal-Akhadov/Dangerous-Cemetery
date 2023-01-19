using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public void LoadLevel(string name, Action onLoaded = null) => 
            _coroutineRunner.StartCoroutine(Load(name, onLoaded));

        private static IEnumerator Load(string name, Action onLoad = null)
        {
            if (SceneManager.GetActiveScene().name == name)
            {
                onLoad?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);

            while (waitNextScene.isDone == false)
            {
                yield return null;
            }

            onLoad?.Invoke();
        }
    }
}