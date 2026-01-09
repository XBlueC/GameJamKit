// ReSharper disable InconsistentNaming

using System;
using UnityEngine;

namespace Code.UI.Core
{
    [Serializable]
    public class UIConfig
    {
        public UIType UIType;
        public bool IsModal;
        public bool DestroyOnClose;
        public UILayerType Layer;
        public GameObject Prefab;
        public string AssetPath;
    }
}