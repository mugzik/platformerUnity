using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Weapons
{
    public class BaseProjectile : MonoBehaviour
    {
        [SerializeField] private bool _invertX;
        [SerializeField] protected float Speed;

        protected Rigidbody2D Rigedbody;
        protected int Direction;

        protected virtual void Start()
        {
            var mod = _invertX ? -1 : 1;
            Direction = mod * transform.lossyScale.x > 0 ? 1 : -1;
            Rigedbody = GetComponent<Rigidbody2D>();
        }
    }
}