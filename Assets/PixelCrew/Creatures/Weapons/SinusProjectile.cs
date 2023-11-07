using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Weapons
{
    public class SinusProjectile : BaseProjectile
    {
        [SerializeField] float _frequency = 1;
        [SerializeField] float _amplitude = 1;

        private float _originalY;
        private float _time = 0;

        protected override void Start()
        {
            base.Start();
            _originalY = Rigedbody.position.y;
        }

        private void FixedUpdate()
        {
            var position = Rigedbody.position;
            position.x += Direction * Speed * Time.deltaTime;
            position.y = _originalY + Mathf.Sin(_time * _frequency) * _amplitude;


            Rigedbody.MovePosition(position);
            _time += Time.fixedDeltaTime;
        }
    }
}