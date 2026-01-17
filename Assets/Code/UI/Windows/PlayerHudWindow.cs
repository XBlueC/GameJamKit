using Code.UI.Core;
using UnityEngine.UI;

namespace Code.UI.Windows
{
    public class PlayerHudWindow : UIBase
    {
        public Button settingButton;

        private void Start()
        {
            settingButton.onClick.AddListener(() => { UIManager.Instance.OpenUI(UIType.Setting); });
        }
    }
}