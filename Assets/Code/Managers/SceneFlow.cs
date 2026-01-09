using System;
using System.Collections;
using System.Collections.Generic;
using Code.Core;
using Code.Core.Event;
using Code.GamePlay;
using Code.Scene;
using Code.UI.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Managers
{
    public class SceneFlow : SingletonMono<SceneFlow>
    {
        [SerializeField] private float minTime = 0.5f; // 最小时间

        public float sliderValue;

        private readonly Dictionary<SceneType, AScene> _scenes = new();
        private AScene _curScene;

        private bool _isLoading;
        private AScene _loadingScene;

        private AsyncOperation _operation;
        private float _time; // 当前时间，与最小时间连用

        private void Update()
        {
            if (_isLoading)
            {
                _time -= Time.deltaTime;
                var progress = _operation.progress * 10 / 9;
                var timeFlag = 1 - _time / minTime;
                sliderValue = timeFlag < progress ? timeFlag : progress;
                if (!(Math.Abs(sliderValue - 1) < 0.01))
                {
                    return;
                }

                _isLoading = false;
                EventSystem.Instance.Publish(new SceneLoadingEnd());

                _operation.allowSceneActivation = true;
                // gameObject.SetActive(false);
                _curScene = _loadingScene;
                _curScene.Enter();

                return;
            }

            _curScene.Update();
        }

        protected override void Initialize()
        {
            foreach (var type in typeof(AScene).Assembly.GetTypes())
            {
                if (type.BaseType != typeof(AScene))
                {
                    continue;
                }

                var scene = Activator.CreateInstance(type) as AScene;
                _scenes.TryAdd(scene.SceneType(), scene);
            }
        }

        public void StartLoadScene(SceneType sceneType)
        {
            if (!_scenes.TryGetValue(sceneType, out var targetScene))
            {
                return;
            }

            if (_curScene == null)
            {
                _curScene = targetScene;
                targetScene.Enter();
                return;
            }

            if (_isLoading || sceneType == _curScene.SceneType())
            {
                return;
            }

            _curScene.Exit();
            _loadingScene = targetScene;
            _isLoading = true;
            // gameObject.SetActive(true);
            _time = minTime;
            EventSystem.Instance.Publish(new SceneLoading());
            UIManager.Instance.OnSceneLoaded();

            StartCoroutine(LoadScene(targetScene.SceneType().ToString()));
        }

        public IEnumerator LoadScene(string sceneName)
        {
            _operation = SceneManager.LoadSceneAsync(sceneName);
            _operation.allowSceneActivation = false;

            yield return _operation;
        }
    }
}