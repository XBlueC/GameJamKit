using System;
using System.Collections;
using Code.GamePlay;
using Code.Managers;
using Code.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows
{
    public class SceneLoadingWindow : UIBase
    {
        public GameObject root;
        public TMP_Text rate;
        public Slider slider;

        public override void OnInit()
        {
            Subscribe<SceneLoading>(OnSceneLoading);
            Subscribe<SceneLoadingEnd>(OnSceneLoadingEnd);
        }

        public override void OnShow(UIArgs args)
        {
            root.SetActive(false);
        }

        private void OnSceneLoadingEnd(SceneLoadingEnd obj)
        {
            root.SetActive(false);
        }

        private void OnSceneLoading(SceneLoading obj)
        {
            slider.value = 0;
            root.SetActive(true);
            StopCoroutine(Loading());
            StartCoroutine(Loading());
        }

        private IEnumerator Loading()
        {
            while (true)
            {
                yield return null;
                var sliderValue = SceneFlow.Instance.sliderValue;
                slider.value = sliderValue;
                var round = Math.Round(sliderValue * 100, 1);
                rate.text = $"{round}%";
                if (Math.Abs(sliderValue - 1) < 0.01)
                {
                    break;
                }
            }
        }
    }
}