using Code.Core;
using UnityEngine;

namespace Code.Managers
{
    public class AssetsManager : SingletonMono<AssetsManager>
    {
        public static GameObject LoadPrefab(string path)
        {
            return Resources.Load<GameObject>(path);
        }

        public T LoadAsset<T>(string assetPath) where T : Object
        {
            if (string.IsNullOrEmpty(assetPath))
            {
                Debug.LogError("Asset path is null or empty!");
                return null;
            }

            return Resources.Load<T>(assetPath);
        }
    }
}