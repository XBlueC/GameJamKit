namespace Code.Scene
{
    public abstract class AScene
    {
        public abstract SceneType SceneType();
        public abstract string GetName();
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }
}