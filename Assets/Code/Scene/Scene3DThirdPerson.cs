using UnityEngine;

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
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public override void Exit()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public override void Update()
        {
        }
    }
}