using Code.UI;
using Code.Utils;

namespace Code.Scene
{
    public class LoginScene : AScene
    {
        public override SceneType SceneType()
        {
            return Scene.SceneType.Home;
        }

        public override void Enter()
        {
            UIUtil.OpenUI(UIType.Launcher);
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }
}