using System;
using System.Collections.Generic;
using Code.Core.Event;
using UnityEngine;

namespace Code.GamePlay
{
    public class Player3DMover : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] Transform cameraTransform;
        [SerializeField] CharacterController controller;

        private Vector2 _inputDir = Vector2.zero;
        private readonly List<IDisposable> _subscriptions = new List<IDisposable>();

        private void Awake()
        {
            if (cameraTransform == null)
                cameraTransform = Camera.main?.transform;
        }

        private void OnEnable()
        {
            var subscription = EventSystem.Instance.Subscribe<MoveInputEvent>(OnMoveInput);
            _subscriptions.Add(subscription);
        }

        private void OnDisable()
        {
            foreach (var subscription in _subscriptions)
                subscription.Dispose();
        }

        private void OnMoveInput(MoveInputEvent e)
        {
            _inputDir = e.Input;
        }

        private void Update()
        {
            if (cameraTransform == null) return;

            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            Vector3 worldMove = _inputDir.y * forward + _inputDir.x * right;
            controller.SimpleMove(worldMove * moveSpeed);

            if (worldMove.magnitude > 0.1f)
            {
                transform.forward = Vector3.Slerp(transform.forward, worldMove, 0.2f);
                
            }
        }
    }
}