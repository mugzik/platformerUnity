using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.Health;
using PixelCrew.Components;

namespace PixelCrew.Creatures.Hero
{
    public class Hero : Creature
    {
        private bool _allowDoubleJump;

        [SerializeField] private CheckCircleOverlap _interactionCheck;

        [SerializeField] private Cooldown _rangeAttackCooldown;
        [SerializeField] private float _powerFallSpeedLimit;
        [SerializeField] private LayerMask _interactionLayer;

        [Space] [Header("Particles")]
        [SerializeField] private ParticleSystem _hitParticles;

        [Space] [Header("Animators")]
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        private static readonly int RangeAttackKey = Animator.StringToHash("range-attack");

        private Model.GameSession _session;

        // Private methods
        private void Start()
        {
            _session = FindObjectOfType<Model.GameSession>();

            var health = GetComponent<HealthComponent>();
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

        public void PerformRangeAttack()
        {
            if (_session.Data.SwordsCount > 1)
           {
                _particles.Spawn("RangeAttack");
                _session.Data.SwordsCount -= 1;
            }
        }

        public void RangeAttack()
        {
            if (_session.Data.IsArmed && _rangeAttackCooldown.IsReady)
            {
                _animator.SetTrigger(RangeAttackKey);
                _rangeAttackCooldown.Reset();
            }
        }

        public IEnumerator PerformTrippleRangeAttack()
        {
            var attacksCount = 3;
            var attackDellay = 0.3f;

            while (attacksCount-- != 0)
            {
                PerformRangeAttack();
                yield return new WaitForSeconds(attackDellay);
            }
        }

        public void TrippleRangeAttack()
        {
            StartCoroutine(PerformTrippleRangeAttack());
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
            if (_session.Data.IsArmed)
            {
                _session.Data.SwordsCount += 1;
            }
            else
            {
                _session.Data.IsArmed = true;
                _animator.runtimeAnimatorController = _armed;
            }
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.HP = currentHealth;
        }

        public void OnCoinsChanged(int currentCoinsCount)
        {
            _session.Data.Coins = currentCoinsCount;
        }
    }
}