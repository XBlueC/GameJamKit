using Code.Managers;
using Code.Scene;
using Code.UI.Core;

namespace Code.UI.Windows
{
    public class SettingWindow : UIBase
    {
        public UnityEngine.UI.Button backBtn;
        public UnityEngine.UI.Button closeBtn;

        public override void OnInit()
        {
            backBtn.onClick.AddListener(() => { SceneFlow.Instance.StartLoadScene(SceneType.Home); });
            closeBtn.onClick.AddListener(Close);
        }
    }
}