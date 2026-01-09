using Code.UI;
using Code.Utils;

namespace Code.Scene
{
    public class LobbyScene : AScene
    {
        public override SceneType SceneType()
        {
            return Scene.SceneType.GameLobby;
        }

        public override void Enter()
        {
            UIUtil.OpenUI(UIType.Lobby);
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }
}