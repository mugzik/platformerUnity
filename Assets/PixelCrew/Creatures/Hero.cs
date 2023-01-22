using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

namespace PixelCrew.Creatures
{
    public class Hero : Creature
    {
        private bool _allowDoubleJump;

        [SerializeField] private CheckCircleOverlap _interactionCheck;

        [SerializeField] private float _powerFallSpeedLimit;
        [SerializeField] private LayerMask _interactionLayer;

        [Space] [Header("Particles")]
        [SerializeField] private ParticleSystem _hitParticles;

        [Space] [Header("Animators")]
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        private Model.GameSession _session;

        // Private methods
        private void Start()
        {
            _session = FindObjectOfType<Model.GameSession>();

            var health = GetComponent<Components.HealthComponent>();
            health.SetHealth(_session.Data.HP);

            UpdateHeroWeapon();
        }

        private void OnDestroy()
        {
            _session = null;
        }

        protected override float CalculateYVelocity()
        {
            var yVelocity = base._rigidbody.velocity.y;

            if (_isGrounded)
            {
                if (yVelocity < _powerFallSpeedLimit)
                {
                    _particles.Spawn("Fall");
                }
                _allowDoubleJump = true;
            }

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            var isFalling = _rigidbody.velocity.y <= -0.5f;

            yVelocity = base.CalculateJumpVelocity(yVelocity);

            if (_allowDoubleJump && isFalling)
            {
                _allowDoubleJump = false;
                yVelocity = _jumpForce;
            }

            return yVelocity;
        }

        private void UpdateHeroWeapon()
        {
            _animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _disarmed;
        }

        // Public methods
        public override void TakeDamage()
        {
            base.TakeDamage();

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

        public void Interact()
        {
            _interactionCheck.Check();
        }

        public override void Attack()
        {
            if (_session.Data.IsArmed)
            {
                base.Attack();
            }
        }

        public void ArmHero()
        {
            _session.Data.IsArmed = true;
            _animator.runtimeAnimatorController = _armed;
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.HP = currentHealth;
        }

        public void OnCoinsChanged(int currentCoinsCount)
        {
            _session.Data.Coins = currentCoinsCount;
        }

        // Spawn particles
        //public void SpawnFootDust()
        //{
        //    _foorStepParticles.Spawn();
        //}

        //public void SpawnJumpDust()
        //{
        //    _jumpParticles.Spawn();
        //}

        //public void SpawnFallDust()
        //{
        //    _fallParticles.Spawn();
        //}

        //public void SpawnAttackEffect()
        //{
        //    _attackParticles.Spawn();
        //}

    }
}