using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

using PixelCrew.Components;
using PixelCrew.Creatures.Mobs.AI;

namespace PixelCrew.Creatures.Mobs
{
    public class TotemsController : ShootingTrapAI
    {
        [SerializeField] private List<Totem> _totems;

        private int _nextAttackerInd = 0;

        protected override void RangeAttack()
        {
            _rangeAttackCooldown.Reset();
            _totems[_nextAttackerInd].PerformAttack();

            if (++_nextAttackerInd >= _totems.Count) _nextAttackerInd = 0;
        }

        public void Remove(GameObject go)
        {
            _nextAttackerInd = 0;
            _totems.Remove(go.GetComponent<Totem>());
        }
    }
}