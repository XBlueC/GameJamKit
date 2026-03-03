using System;
using System.Collections.Generic;
using Code.Core.Event;
using Code.Core.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Core.UI
{
    public abstract class UIBase : MonoBehaviour, IUIBase
    {
        public string UIName { get; private set; }
        private readonly List<IDisposable> _disposables = new();
        private UIArgs _args;

        public void Init(string uiName)
        {
            UIName = uiName;
        }

        public void SetArgs(UIArgs args)
        {
            _args = args;
        }

        private void Awake()
        {
            var canvas = gameObject.GetOrAddComponent<Canvas>();
            canvas.overrideSorting = false;
            gameObject.GetOrAddComponent<GraphicRaycaster>();
        }

        public void OnDestroy()
        {
            UnSubscribeAll();
        }

        public virtual void OnInit()
        {
        }

        public virtual void OnShow(UIArgs args)
        {
        }

        public virtual void OnArgsUpdate(UIArgs args)
        {
        }

        public virtual void OnPause()
        {
        }

        public virtual void OnResume()
        {
        }

        public virtual void OnClose()
        {
        }


        protected void Subscribe<T>(Action<T> action)
        {
            var disposable = EventSystem.Instance.Subscribe(action);
            _disposables.Add(disposable);
        }

        protected void UnSubscribe<T>(Action<T> action)
        {
            EventSystem.Instance.UnSubscribe(action);
        }

        protected void UnSubscribeAll()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _disposables.Clear();
        }

        protected void Close()
        {
            UIManager.Instance.CloseUI(UIName);
        }
    }
}