// ReSharper disable InconsistentNaming

using System;
using UnityEngine;

namespace Code.UI.Core
{
    /// <summary>
    /// UI 配置
    /// </summary>
    [Serializable]
    public class UIConfig
    {
        /// <summary>
        /// UI 类型
        /// </summary>
        public UIType UIType;

        /// <summary>
        /// 是否为模态窗口（弹出时会遮挡背景并禁止与其它UI交互）。
        /// 例如：确认对话框、登录弹窗应设为 true；
        /// 背包、地图等可与其他界面共存的面板应设为 false。
        /// </summary>
        public bool IsModal;

        /// <summary>
        /// 关闭时是否销毁（目前都会销毁）
        /// </summary>
        public bool DestroyOnClose;

        /// <summary>
        /// 层级
        /// </summary>
        public UILayerType Layer = UILayerType.Normal;

        /// <summary>
        /// 预制体
        /// </summary>
        public GameObject Prefab;

        /// <summary>
        /// 资源路径，暂时无用
        /// </summary>
        public string AssetPath;
    }
}