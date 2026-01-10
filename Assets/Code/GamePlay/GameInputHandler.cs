using Code.Core.Event;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.GamePlay
{
    public class GameInputHandler : MonoBehaviour
    {
        private InputSystem_Actions _inputActions;

        private void OnEnable()
        {
            _inputActions = new InputSystem_Actions();

            _inputActions.Player.Move.performed += OnMove;
            _inputActions.Player.Move.canceled += _ => PublishMove(Vector2.zero);
            _inputActions.Player.Jump.performed += _ => EventSystem.Instance.Publish(new JumpInputEvent());
            // _inputActions.Player.Pause.performed += _ => EventSystem.Instance.Publish(new PauseInputEvent());

            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions?.Disable();
            _inputActions?.Dispose();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            var inputDir = context.ReadValue<Vector2>();
            PublishMove(inputDir);
        }

        private void PublishMove(Vector2 inputDir)
        {
            EventSystem.Instance.Publish(new MoveInputEvent(inputDir));
        }
    }
}