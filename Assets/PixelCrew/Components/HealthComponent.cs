using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;

        public void ChanHealth(int value)
        {
            _health += value;

            if (value < 0) _onDamage?.Invoke();

            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }
    }
}