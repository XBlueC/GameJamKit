using System;
using System.Collections;
using System.Collections.Generic;
using Code.Core;
using Code.Core.Event;
using Code.Core.UI;
using Code.GamePlay;
using Code.Scene;
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
            _curScene?.Update();
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

            if (_isLoading || sceneType == _curScene?.SceneType())
            {
                return;
            }

            _curScene?.Exit();
            _loadingScene = targetScene;
            _isLoading = true;
            _time = minTime;
            EventSystem.Instance.Publish(new SceneLoadingStarted());
            UIManager.Instance.OnSceneLoaded();

            StartCoroutine(LoadScene(targetScene.GetName()));
        }

        private IEnumerator LoadScene(string sceneName)
        {
            _operation = SceneManager.LoadSceneAsync(sceneName);
            if (_operation == null)
            {
                yield break;
            }

            _operation.allowSceneActivation = false;
            sliderValue = 0;
            while (_time > 0)
            {
                yield return null;
                _time -= Time.deltaTime;
                var progress = _operation.progress * 10 / 9;
                var timeFlag = 1 - _time / minTime;
                sliderValue = timeFlag < progress ? timeFlag : progress;
            }

            sliderValue = 1;
            _curScene = _loadingScene;
            _curScene.Enter();
            _operation.allowSceneActivation = true;
            yield return null;
            _isLoading = false;
            EventSystem.Instance.Publish(new SceneLoadingCompleted());
        }
    }
}