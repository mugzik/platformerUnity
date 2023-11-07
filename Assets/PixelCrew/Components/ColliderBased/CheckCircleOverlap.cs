using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

namespace PixelCrew.Components.ColliderBased
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        [SerializeField] private string[] _filterTags;
        [SerializeField] private LayerMask _filterMask;

        [SerializeField] private OnOverlapEvent _onOverlapEvenet;

        private const int MAX_OVERLAP_SIZE = 10;
        private readonly Collider2D[] _overlapResult = new Collider2D[MAX_OVERLAP_SIZE];

        public void Check()
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                    transform.position,
                    _radius,
                    _overlapResult,
                    _filterMask
                );

            var resultSize = Mathf.Min(size, MAX_OVERLAP_SIZE);

            for (int i = 0; i < resultSize; i++)
            {
                var rightTag = _filterTags.Any(tag => _overlapResult[i].gameObject.CompareTag(tag));
                if (rightTag || _filterTags.Length == 0)
                    _onOverlapEvenet?.Invoke(_overlapResult[i].gameObject);
            }

        }

        [System.Serializable]
        class OnOverlapEvent : UnityEvent<GameObject>
        {

        }

#if UNITY_EDITOR
        public void OnDrawGizmosSelected()
        {
            Handles.color = Utils.HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
#endif
    }
}