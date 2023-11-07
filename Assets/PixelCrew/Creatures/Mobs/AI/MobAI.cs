using System.Collections;
using UnityEngine;
using UnityEngine.Events;

using PixelCrew.Components.ColliderBased;
using PixelCrew.Creatures.Mobs.Patrolling;

namespace PixelCrew.Creatures.Mobs.AI
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private float _agroPause = 0.5f;
        [SerializeField] private float _attackCooldown = 1f;

        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;
        [SerializeField] private UnityEvent _onAgro;
        [SerializeField] private UnityEvent _onMiss;
        [SerializeField] private GameObject _deadColliderGO;

        private static readonly int IsDeadKey = Animator.StringToHash("is-dead");

        private Creature _creature;
        private Animator _animator;
        private Coroutine _currentCoroutine;
        private GameObject _target;
        private Patrol _patrol;

        private bool _isDead = false;

        private void Awake()
        {
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }

        private IEnumerator GoToTarget()
        {
            while (_vision.IsTouchingLayer())
            {
                if (_canAttack.IsTouchingLayer())
                {
                    StartState(Atack());
                }
                else
                {
                    SetDirectionToTarget();
                }
                yield return null;
            }
            _creature.SetDirection(new Vector3(0, 0, 0));
            _onMiss?.Invoke();
            yield return new WaitForSeconds(_agroPause);
            StartState(_patrol.DoPatrol());
        }

        private IEnumerator AgroOnTarget()
        {
            _onAgro?.Invoke();
            _creature.SetDirection(new Vector3(0, 0, 0));
            LookAtHero();
            yield return new WaitForSeconds(_agroPause);
            StartState(GoToTarget());
        }

        private IEnumerator Atack()
        {
            while (_canAttack.IsTouchingLayer())
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCooldown);
            }

            StartState(GoToTarget());
        }

        private void StartState(IEnumerator coroutine)
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = StartCoroutine(coroutine);
        }

        private void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget();

            _creature.SetDirection(direction);
        }

        private Vector3 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;

            return direction.normalized;
        }

        private void LookAtHero()
        {
            var direction = GetDirectionToTarget();
            _creature.UpdateSpriteDirection(direction);
        }

        public void OnHeroInVision(GameObject go)
        {
            if (_isDead) return;


            _target = go;

            StartState(AgroOnTarget());
        }

        public void OnDie()
        {
            _isDead = true;
            _creature.SetDirection(Vector3.zero);

            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _animator.SetBool(IsDeadKey, _isDead);

            var collider = GetComponent<CapsuleCollider2D>();
            var deadCollider = _deadColliderGO?.GetComponent<Collider2D>();

            if (deadCollider != null)
            {
                collider.enabled = false;
                deadCollider.enabled = true;
            }
        }
    }
}