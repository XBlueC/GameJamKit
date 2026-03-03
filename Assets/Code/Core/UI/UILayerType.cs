namespace Code.Core.UI
{
    public enum UILayerType
    {
        /// <summary>
        /// 场景层
        /// </summary>
        SceneLayer = 0,

        /// <summary>
        /// 背景层
        /// </summary>
        Background = 1000,

        /// <summary>
        /// 普通界面
        /// </summary>
        Normal = 2000,

        /// <summary>
        /// 弹出窗口
        /// </summary>
        Popup = 3000,

        /// <summary>
        /// 即时提示
        /// </summary>
        Tips = 4000,

        /// <summary>
        /// 顶级界面
        /// </summary>
        Top = 5000
    }
}