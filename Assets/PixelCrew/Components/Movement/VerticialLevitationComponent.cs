using System.Collections;
using UnityEngine;

namespace PixelCrew.Components.Movement
{
    public class VerticialLevitationComponent : MonoBehaviour
    {
        [SerializeField] float _frequency = 1;
        [SerializeField] float _amplitude = 1;
        [SerializeField] bool _randomize = false;

        private float _originalY;
        private float _seed = 0;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _originalY = _rigidbody.position.y;
            if (_randomize)
                _seed = Random.value * Mathf.PI * 2;
        }

        private void Update()
        {
            var position = _rigidbody.position;
            position.y = _originalY + Mathf.Sin(_seed + Time.time * _frequency) * _amplitude;

            _rigidbody.MovePosition(position);
        }
    }
}