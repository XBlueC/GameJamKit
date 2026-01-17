using Code.UI;
using Code.Utils;

namespace Code.Scene
{
    /// <summary>
    /// 2D平台示例场景
    /// </summary>
    public class Scene2DPlatformer : AScene
    {
        public override SceneType SceneType()
        {
            return Scene.SceneType.Scene2DPlatformer;
        }

        public override string GetName()
        {
            return "Scene_2D_Platformer";
        }

        public override void Enter()
        {
            UIUtil.OpenUI(UIType.PlayerHud);
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }
}