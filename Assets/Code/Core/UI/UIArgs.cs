using System;

namespace Code.Core.UI
{
    public struct UIArgs
    {
        /// <summary>
        /// 用户自定义数据
        /// </summary>
        public object UserData;

        /// <summary>
        /// 打开UI完成回调
        /// </summary>
        public Action OnOpen;

        /// <summary>
        /// 关闭UI完成回调
        /// </summary>
        public Action OnClose;
    }
}