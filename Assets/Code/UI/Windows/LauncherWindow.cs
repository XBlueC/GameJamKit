using Code.Managers;
using Code.Scene;
using Code.UI.Core;

namespace Code.UI.Windows
{
    public class LauncherWindow : UIBase
    {
        public UnityEngine.UI.Button startButton;
        public UnityEngine.UI.Button exitButton;
        public UnityEngine.UI.Button announcementButton;

        public override void OnInit()
        {
            startButton.onClick.AddListener(EnterGame);
            exitButton.onClick.AddListener(Quit);
            announcementButton.onClick.AddListener(() => { UIManager.Instance.OpenUI(UIType.About); });
        }

        private void EnterGame()
        {
            SceneFlow.Instance.StartLoadScene(SceneType.GameLobby);
        }

        private void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            UnityEngine.Application.Quit();
#endif
        }
    }
}