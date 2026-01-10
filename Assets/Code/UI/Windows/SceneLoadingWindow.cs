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
            Subscribe<SceneLoadingStarted>(OnSceneLoadingStarted);
            Subscribe<SceneLoadingCompleted>(OnSceneLoadingCompleted);
        }

        public override void OnShow(UIArgs args)
        {
            root.SetActive(false);
        }

        private void OnSceneLoadingCompleted(SceneLoadingCompleted obj)
        {
            root.SetActive(false);
        }

        private void OnSceneLoadingStarted(SceneLoadingStarted obj)
        {
            slider.value = 0;
            root.SetActive(true);
            StopCoroutine(Loading());
            StartCoroutine(Loading());
        }

        private IEnumerator Loading()
        {
            rate.text = "0%";
            while (true)
            {
                yield return null;
                var sliderValue = SceneFlow.Instance.sliderValue;
                slider.value = sliderValue;
                var round = Math.Round(sliderValue * 100, 1);
                rate.text = $"{round}%";
                if (!(Math.Abs(sliderValue - 1) < 0.01)) continue;
                rate.text = "100%";
                break;
            }
        }
    }
}