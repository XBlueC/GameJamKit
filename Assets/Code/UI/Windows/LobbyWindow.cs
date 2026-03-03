using Code.Core.UI;
using Code.Utils;
using UnityEngine.UI;

namespace Code.UI.Windows
{
    public class LobbyWindow : UIBase
    {
        public Button startButton;
        public Button settingButton;

        private void Start()
        {
            startButton.onClick.AddListener(StartGame);
            settingButton.onClick.AddListener(() => { UIHelper.OpenUI(UIType.Setting); });
        }

        private void StartGame()
        {
            // todo
        }
    }
}