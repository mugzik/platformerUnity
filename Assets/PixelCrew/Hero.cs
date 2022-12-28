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
        private bool _isGrounded;
        private bool _allowDoubleJump;
        private Collider2D[] _interactionResult = new Collider2D[1];

        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _damageJumpSpeed;
        [SerializeField] private float _interactRadius;
        [SerializeField] private float _powerFallSpeedLimit;
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private LayerMask _interactionLayer;
        [SerializeField] private Components.SpawnComponent _foorStepParticles;
        [SerializeField] private Components.SpawnComponent _jumpParticles;
        [SerializeField] private Components.SpawnComponent _fallParticles;
        [SerializeField] private ParticleSystem _hitParticles;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunningKey = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");

        // Private methods
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            _isGrounded = IsGrounded();
        }

        private void FixedUpdate()
        {
            var xVelocity = _direction.x * _speed;
            var yVelocity = CalculateYVelocity();

            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            _animator.SetFloat(VerticalVelocityKey, _rigidbody.velocity.y);
            _animator.SetBool(IsGroundKey, _isGrounded);
            _animator.SetBool(IsRunningKey, _direction.x != 0);

            UpdateSpriteDirection();
        }

        private float CalculateYVelocity()
        {
            var yVelocity = _rigidbody.velocity.y;

            var isJumpPressing = _direction.y > 0;

            if (_isGrounded)
            {
                if (yVelocity < _powerFallSpeedLimit)
                {
                    SpawnFallDust();
                }
                _allowDoubleJump = true;
            }
            if (isJumpPressing)
            {
                yVelocity = CalculateJumpVelocity(yVelocity);
            }
            else if (_rigidbody.velocity.y > 0)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        private float CalculateJumpVelocity(float yVelocity)
        {
            var isFalling = _rigidbody.velocity.y <= -0.5f;

            if (_isGrounded)
            {
                //Debug.Log("First jump");
                yVelocity = _jumpForce;
                //SpawnJumpDust();
            }
            else if (_allowDoubleJump && isFalling)
            {
                //Debug.Log("Second jump");
                yVelocity = _jumpForce;
                _allowDoubleJump = false;
                //SpawnJumpDust();
            }

            return yVelocity;
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
            if (_direction.x != 0) transform.localScale = new Vector3(_direction.x / Mathf.Abs(_direction.x), 1, 1);
        }

        // Public methods
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public void TakeDamage()
        {
            Debug.Log("Take Damage");
            _animator.SetTrigger(Hit);
            //_rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageJumpSpeed);
            _rigidbody.AddForce(Vector2.up * _damageJumpSpeed, ForceMode2D.Impulse);
            _allowDoubleJump = false;


            SpawnCoins(GetComponent<CoinCounter>().LoseCoins());
        }

        private void SpawnCoins( int value )
        {
            if (value <= 0) return;

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = value;
            _hitParticles.emission.SetBurst(0, burst);

            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
        }

        public void BattleRoar()
        {
            Debug.Log("ROOOOARRR!!!");
        }

        public void Interact()
        {
            var size = Physics2D.OverlapCircleNonAlloc(  
                    transform.position, _interactRadius, 
                    _interactionResult, _interactionLayer
                    );

            for (int i = 0; i < size; i++)
            {
                var interactable = _interactionResult[i].GetComponent<Components.interactComponent>();
                if (interactable != null)
                    interactable.Interact();
            }
        }

        // Spawn particles
        public void SpawnFootDust()
        {
            _foorStepParticles.Spawn();
        }

        public void SpawnJumpDust()
        {
            _jumpParticles.Spawn();
        }

        public void SpawnFallDust()
        {
            _fallParticles.Spawn();
        }
    }
}