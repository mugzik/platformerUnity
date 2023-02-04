using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Weapons
{
    public class Projectile : BaseProjectile
    {

        private void FixedUpdate()
        {
            var newPos = Rigedbody.position;

            newPos.x += Speed * Direction * Time.deltaTime;

            Rigedbody.MovePosition(newPos);
        }
    }
}