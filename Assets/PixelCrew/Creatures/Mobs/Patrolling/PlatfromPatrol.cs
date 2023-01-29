using System.Collections;
using UnityEngine;

using PixelCrew.Components.ColliderBased;

namespace PixelCrew.Creatures.Mobs.Patrolling
{
    public class PlatfromPatrol : Patrol
    {
        [SerializeField] private LayerCheck _endOfPlatfromChecker;

        private Creature _creature;
        private Vector3 _currentDirection = new Vector3(1, 0, 0);

        public void Awake()
        {
            _creature = GetComponent<Creature>();
        }

        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if(!_endOfPlatfromChecker.IsTouchingLayer())
                {
                    ChangeDirection();
                }
                yield return null;
            }
        }

        private void ChangeDirection()
        {
            _currentDirection *= -1;
            _creature.SetDirection(_currentDirection);
        }
    }
}