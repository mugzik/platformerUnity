using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Creatures.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _rigedbody;
        private int _direction;

        private void Start()
        {
            _direction = transform.lossyScale.x > 0 ? 1 : -1;
            _rigedbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            var newPos = _rigedbody.position;

            newPos.x += _speed * _direction * Time.deltaTime;

            _rigedbody.MovePosition(newPos);
        }
    }
}