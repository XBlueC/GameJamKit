using Code.Managers;
using Code.Scene;
using Code.UI.Core;
using UnityEditor;

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
            announcementButton.onClick.AddListener(() => { UIManager.Instance.OpenUI(UIType.Announcement); });
        }

        private void EnterGame()
        {
            SceneFlow.Instance.StartLoadScene(SceneType.GameLobby);
        }

        private void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif
        }
    }
}