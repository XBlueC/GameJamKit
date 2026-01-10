using UnityEngine;

namespace Code.GamePlay
{
    public struct SceneLoadingStarted
    {
    }

    public struct SceneLoadingCompleted
    {
    }

    public readonly struct MoveInputEvent
    {
        public readonly Vector2 Input;

        public MoveInputEvent(Vector2 input)
        {
            Input = input;
        }
    }

    public readonly struct JumpInputEvent
    {
    }

    public readonly struct PauseInputEvent
    {
    }
}