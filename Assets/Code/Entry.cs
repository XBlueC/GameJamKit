using Code.Core.Event;
using Code.Managers;
using Code.Scene;
using Code.UI;
using Code.UI.Core;
using UnityEngine;

namespace Code
{
    public class Entry : MonoBehaviour
    {
        public void Start()
        {
            EventSystem.Instance.OnError += Debug.LogError;
            SceneFlow.Instance.StartLoadScene(SceneType.Home);
            UIManager.Instance.OpenUI(UIType.Loader);
        }
    }
}