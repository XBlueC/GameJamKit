using Code.Core.UI;
using Code.Managers;
using Code.Scene;
using Code.Utils;
using UnityEngine.UI;

namespace Code.UI.Windows
{
    public class LauncherWindow : UIBase
    {
        public Button startButton;
        public Button exitButton;
        public Button aboutButton;

        public override void OnInit()
        {
            startButton.onClick.AddListener(EnterGame);
            exitButton.onClick.AddListener(Quit);
            aboutButton.onClick.AddListener(() => { UIHelper.OpenUI(UIType.About); });
        }

        private void EnterGame()
        {
            SceneFlow.Instance.StartLoadScene(SceneType.Scene3DThirdPerson);
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