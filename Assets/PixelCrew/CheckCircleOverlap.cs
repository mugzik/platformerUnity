using System.Collections;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace PixelCrew
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float _radius = 1f;
        [SerializeField] private string _filterTag;

        private const int MAX_OVERLAP_SIZE = 10;
        private readonly Collider2D[] _overlapResult = new Collider2D[MAX_OVERLAP_SIZE];

        public GameObject[] GetObjectsInRange()
        { 
            var size = Physics2D.OverlapCircleNonAlloc(
                    transform.position,
                    _radius,
                    _overlapResult
                );


            var resultSize = Mathf.Min(size, MAX_OVERLAP_SIZE);
            var result = new List<GameObject>();

            for (int i = 0; i < resultSize; i++ )
            {
                if (_overlapResult[i].gameObject.tag == _filterTag)
                    result.Add(_overlapResult[i].gameObject);
            }

            return result.ToArray();
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