namespace Code.Core.UI
{
    public interface IUIBase
    {
        /// <summary>
        /// 界面初始化
        /// </summary>
        public void OnInit();

        /// <summary>
        /// 界面显示（每次激活时调用）
        /// </summary>
        public void OnShow(UIArgs args);

        /// <summary>
        /// 界面参数更新
        /// </summary>
        public void OnArgsUpdate(UIArgs args);

        /// <summary>
        /// 界面暂停（被其他界面覆盖）
        /// </summary>
        public void OnPause();

        /// <summary>
        /// 界面恢复（从暂停状态回到前台）
        /// </summary>
        public void OnResume();

        /// <summary>
        /// 界面关闭
        /// </summary>
        public void OnClose();
    }
}