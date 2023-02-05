using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Animations;

namespace PixelCrew.Creatures.Mobs
{
    public class Totem : MonoBehaviour
    {
        public enum AnimatorType
        {
            DEFAULT,
            HEAD
        }

        [SerializeField] private AnimatorOverrideController _default;
        [SerializeField] private AnimatorOverrideController _head;
        [SerializeField] private bool isTopHead = false;
        [SerializeField] private TotemsController _totemController;

        private static readonly int RangeAttackKey = Animator.StringToHash("range-attack");

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            if (isTopHead) SwitchAnimator(AnimatorType.HEAD);
        }

        public void SwitchAnimator(AnimatorType type)
        {
            if (type == AnimatorType.HEAD)
            {
                _animator.runtimeAnimatorController = _head;
            }
            else _animator.runtimeAnimatorController = _default;
        }

        public void PerformAttack()
        {
            _animator.SetTrigger(RangeAttackKey);
        }

        private void OnDestroy()
        {
            _totemController.Remove(gameObject);
        }
    }
}