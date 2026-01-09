using Code.UI;
using Code.UI.Core;

namespace Code.Utils
{
    public static class UIUtil
    {
        public static UIBase OpenUI(UIType uiType)
        {
            return UIManager.Instance.OpenUI(uiType);
        }

        public static void CloseUI(UIType uiType)
        {
            UIManager.Instance.CloseUI(uiType);
        }
    }
}