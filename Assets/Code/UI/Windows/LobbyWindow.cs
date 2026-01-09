using Code.UI.Core;

namespace Code.UI.Windows
{
    public class LobbyWindow : UIBase
    {
        public UnityEngine.UI.Button startButton;
        public UnityEngine.UI.Button settingButton;

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