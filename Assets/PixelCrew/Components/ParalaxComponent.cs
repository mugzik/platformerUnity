using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components
{
    public class ParalaxComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _paralaxEffect;
        [SerializeField] private float _smuth;

        private Vector3 _startPos;
        private Vector3 _delta;

        private void Awake()
        {
            _startPos = transform.position;
            // keep delta between objects
            _delta = _target.position - _startPos;
            Debug.Log("_delta " + _delta.ToString());
        }

        private void LateUpdate()
        {
            var newPos = new Vector3(
                    _startPos.x + ( _target.position.x * _paralaxEffect ),
                    _target.position.y - _delta.y,
                    transform.position.z
                );

            transform.position = Vector3.Lerp( transform.position, newPos, _smuth );
        }
    }
}