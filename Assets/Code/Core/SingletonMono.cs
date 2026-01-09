using UnityEngine;

namespace Code.Core
{
    public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        private static readonly object _lock = new();
        private static T _instance;

        protected SingletonMono()
        {
        }

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance != null)
                    {
                        return _instance;
                    }

                    _instance = FindAnyObjectByType<T>();
                    if (_instance != null)
                    {
                        Debug.Log($"Found existing instance of {typeof(T)}");
                        return _instance;
                    }

                    var o = new GameObject();
                    _instance = o.AddComponent<T>();
                    o.name = typeof(T).Name;
                    DontDestroyOnLoad(o);
                    Debug.Log($"Created new instance of {typeof(T)}");
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Debug.LogWarning($"Duplicate instance of {typeof(T)} detected. Destroying duplicate.");
                Destroy(gameObject);
                return;
            }

            _instance = this as T;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"Instance of {typeof(T)} initialized.");
            Initialize();
        }

        public void OnDestroy()
        {
            Debug.Log($"Instance of {typeof(T)} destroyed.");
            _instance = null;
        }

        protected virtual void Initialize()
        {
            // Custom initialization logic here
        }

        public static void DestroyInstance()
        {
            if (_instance != null)
            {
                Destroy(_instance.gameObject);
                _instance = null;
            }
        }
    }
}