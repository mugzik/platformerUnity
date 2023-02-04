using System.Collections;
using UnityEngine;

using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GOBased;
using PixelCrew.Components;

namespace PixelCrew.Creatures.Mobs.AI
{
    public class ShootingTrapAI : MonoBehaviour
    {
        [SerializeField] private bool _invertScale;

        [Header("Melee Atack")]
        [SerializeField] private CheckCircleOverlap _meleeAttack;
        [SerializeField] private LayerCheck _meleeCanAttack;
        [SerializeField] private Cooldown _meleeAttackCooldown;

        [Header("Range Atack")]
        [SerializeField] private SpawnComponent _rangeAttack;
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private Cooldown _rangeAttackCooldown;

        private Animator _animator;
        private static readonly int MeleeAttackKey = Animator.StringToHash("melee-attack");
        private static readonly int RangeAttackKey = Animator.StringToHash("range-attack");

        private GameObject _target;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (_vision.IsTouchingLayer())
            {
                LookAtHero();

                if (_meleeCanAttack.IsTouchingLayer())
                {
                    if (_meleeAttackCooldown.IsReady)
                        MeleeAttack();
                }
                else if (_rangeAttackCooldown.IsReady)
                {
                    RangeAttack();
                }

            }
        }

        private void LookAtHero()
        {
            var direction = GetDirectionToTarget();
            var scaleInverter = _invertScale ? -1 : 1;

            if (direction.x != 0)
                transform.localScale = new Vector3(scaleInverter * direction.x / Mathf.Abs(direction.x), 1, 1);
        }

        private Vector3 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;

            return direction.normalized;
        }

        private void MeleeAttack()
        {
            _meleeAttackCooldown.Reset();
            _animator.SetTrigger(MeleeAttackKey);
        }

        private void RangeAttack()
        {
            _rangeAttackCooldown.Reset();
            _animator.SetTrigger(RangeAttackKey);
        }

        public void OnMeleeAttack()
        {
            _meleeAttack.Check();
        }

        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }
        public void OnHeroInVision(GameObject go)
        {
            _target = go;
        }
    }
}