using Code.UI;
using Code.Utils;

namespace Code.Scene
{
    public class GameScene : AScene
    {
        public override SceneType SceneType()
        {
            return Scene.SceneType.GameScene;
        }

        public override string GetName()
        {
            return nameof(Scene.SceneType.GameScene);
        }

        public override void Enter()
        {
            UIHelper.OpenUI(UIType.PlayerHud);
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }
}