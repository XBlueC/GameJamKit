using System;
using System.Collections.Generic;
using Code.Core.Event;
using UnityEngine;

namespace Code.GamePlay
{
    public class Player2DMover : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] Rigidbody2D rb;
        private readonly List<IDisposable> _subscriptions = new List<IDisposable>();

        private Vector2 _moveDir = Vector2.zero;

        private void OnEnable()
        {
            var subscription = EventSystem.Instance.Subscribe<MoveInputEvent>(OnMove);
            _subscriptions.Add(subscription);
        }

        private void OnDisable()
        {
            foreach (var subscription in _subscriptions)
                subscription.Dispose();
        }

        private void OnMove(MoveInputEvent e)
        {
            _moveDir = e.Input;
        }

        private void FixedUpdate()
        {
            rb.velocity = new Vector2(_moveDir.x * moveSpeed, rb.velocity.y);

            if (_moveDir.x > 0.1f) transform.localScale = Vector3.one;
            else if (_moveDir.x < -0.1f) transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}