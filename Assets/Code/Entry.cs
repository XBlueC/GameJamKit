using Code.Managers;
using Code.Scene;
using Code.UI;
using Code.UI.Core;
using UnityEngine;

namespace Code
{
    public class Entry : MonoBehaviour
    {
        private void Awake()
        {
        }

        private void Reset()
        {
        }

        public void Start()
        {
            UIManager.Instance.Init();
            SceneFlow.Instance.StartLoadScene(SceneType.Home);
            UIManager.Instance.OpenUI(UIType.Loader);
        }

        private void Update()
        {
        }

        private void FixedUpdate()
        {
        }

        private void LateUpdate()
        {
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }

        private void OnDestroy()
        {
        }


        private void OnApplicationFocus(bool hasFocus)
        {
        }

        private void OnApplicationPause(bool pauseStatus)
        {
        }

        private void OnApplicationQuit()
        {
        }
    }
}