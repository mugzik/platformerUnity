using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] private bool _invertScale;
        [SerializeField] private float _speed;
        [SerializeField] private float _damageVelocity;
        [SerializeField] private int _damage;
        [SerializeField] private LayerCheck _groundCheck;

        [SerializeField] private CheckCircleOverlap _attackRange;

        [SerializeField] protected float _jumpForce;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunningKey = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocityKey = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int AttackKey = Animator.StringToHash("attack");

        private Vector2 _direction;
        private Components.HealthChangerComponent _damageApplyer;

        protected Components.SpawnListComponent _particles;
        protected bool _isGrounded;
        protected Rigidbody2D _rigidbody;
        protected Animator _animator;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _particles = GetComponent<Components.SpawnListComponent>();

            _damageApplyer = gameObject.AddComponent<Components.HealthChangerComponent>();
            _damageApplyer.SetValue(-_damage);
        }

        protected virtual void Update()
        {
            _isGrounded = IsGrounded();
        }
        protected virtual void FixedUpdate()
        {
            var xVelocity = _direction.x * _speed;
            var yVelocity = CalculateYVelocity();

            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            _animator.SetFloat(VerticalVelocityKey, _rigidbody.velocity.y);
            _animator.SetBool(IsGroundKey, _isGrounded);
            _animator.SetBool(IsRunningKey, _direction.x != 0);

            UpdateSpriteDirection(_direction);
        }

        protected virtual float CalculateYVelocity()
        {
            var yVelocity = _rigidbody.velocity.y;

            var isJumpPressing = _direction.y > 0;

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

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {
            if (_isGrounded)
            {
                yVelocity = _jumpForce;
            }

            return yVelocity;
        }

        public void UpdateSpriteDirection(Vector3 direction)
        {
            var scaleInverter = _invertScale ? -1 : 1;

            if (direction.x != 0) 
                transform.localScale = new Vector3(scaleInverter * direction.x / Mathf.Abs(direction.x), 1, 1);
        }

        private bool IsGrounded()
        {
            // GroundCheck method
            // Return true if object 'hero' have 'bottom' collision with layers from _groundCheck._groundLayer
            // overwise return false. See LayerCheck class

            return _groundCheck.IsTouchingLayer();
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public virtual void TakeDamage()
        {
            _animator.SetTrigger(Hit);
            _rigidbody.AddForce(Vector2.up * _damageVelocity, ForceMode2D.Impulse);
        }

        public virtual void Attack()
        {
            _animator.SetTrigger(AttackKey);
        }

        public void PerformAttack()
        {
            //Depended from attack animation
            _attackRange.Check();
        }

        public void ApplyDamage(GameObject target)
        {
            _damageApplyer.ChangeHealth(target);
        }
    }
}