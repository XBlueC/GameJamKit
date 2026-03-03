using Code.Core.UI;
using Code.Utils;
using UnityEngine.UI;

namespace Code.UI.Windows
{
    public class PlayerHudWindow : UIBase
    {
        public Button settingButton;

        private void Start()
        {
            settingButton.onClick.AddListener(() => { UIHelper.OpenUI(UIType.Setting); });
        }
    }
}