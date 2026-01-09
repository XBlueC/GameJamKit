using Code.UI.Core;

namespace Code.UI.Windows
{
    public class AnnouncementWindow : UIBase
    {
        public UnityEngine.UI.Button closeBtn;

        public override void OnInit()
        {
            closeBtn.onClick.AddListener(Close);
        }
    }
}