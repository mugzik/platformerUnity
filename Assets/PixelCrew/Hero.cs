using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew
{
    public class Hero : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private Vector2 _direction;
        private Animator _animator;
        private SpriteRenderer _sprite;

        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private LayerCheck _groundCheck;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunningKey = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");

        // Private methods
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            var isJumping = _direction.y > 0;
            var isGrounded = IsGrounded();

            _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);

            if (isJumping)
            {
                if (isGrounded)
                {
                    _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }
            }
            else if (_rigidbody.velocity.y > 0)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
            }

            _animator.SetFloat(VerticalVelocityKey, _rigidbody.velocity.y);
            _animator.SetBool(IsGroundKey, isGrounded);
            _animator.SetBool(IsRunningKey, _direction.x != 0);

            UpdateSpriteDirection();
        }

        private bool IsGrounded()
        {
            // GroundCheck method
            // Return true if object 'hero' have 'bottom' collision with layers from _groundCheck._groundLayer
            // overwise return false. See LayerCheck class

            return _groundCheck.IsTouchingLayer();
        }

        private void UpdateSpriteDirection()
        {
            if (_direction.x != 0) _sprite.flipX = _direction.x < 0;
        }

        // Public methods
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void BattleRoar()
        {
            Debug.Log("ROOOOARRR!!!");
        }

    }
}