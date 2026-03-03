using Code.Core.UI;
using Code.UI;

namespace Code.Utils
{
    public static class UIHelper
    {
        public static UIBase OpenUI(UIType uiType)
        {
            return UIManager.Instance.OpenUI(uiType.ToString());
        }

        public static void CloseUI(UIType uiType)
        {
            UIManager.Instance.CloseUI(uiType.ToString());
        }
    }
}