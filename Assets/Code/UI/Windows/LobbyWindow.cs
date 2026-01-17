using Code.UI.Core;
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
            settingButton.onClick.AddListener(() => { UIManager.Instance.OpenUI(UIType.Setting); });
        }

        private void StartGame()
        {
            // todo
        }
    }
}