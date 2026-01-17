using Code.UI;
using Code.Utils;

namespace Code.Scene
{
    /// <summary>
    /// 3D第三人称示例场景
    /// </summary>
    public class Scene3DThirdPerson : AScene
    {
        public override SceneType SceneType()
        {
            return Scene.SceneType.Scene3DThirdPerson;
        }

        public override string GetName()
        {
            return "Scene_3D_ThirdPerson";
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