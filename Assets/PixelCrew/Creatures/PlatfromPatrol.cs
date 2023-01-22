using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures
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