namespace Code.Core
{
    public class Singleton<T> where T : class, new()
    {
        private static T _instance;
        private static readonly object _lock = new();

        protected Singleton()
        {
        }

        public static T Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                lock (_lock)
                {
                    _instance ??= new T();
                }

                return _instance;
            }
        }

        public static void Reset()
        {
            _instance = null;
        }
    }
}