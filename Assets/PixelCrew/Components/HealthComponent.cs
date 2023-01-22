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
        [SerializeField] private HealthChangeEvent _onHealthChanged;


        public void ChangeHealth(int value)
        {
            _health += value;
            _onHealthChanged?.Invoke(_health);

            if (value < 0) _onDamage?.Invoke();
            if (_health <= 0)
            {
                _onDie?.Invoke();
                enabled = false;
            }
        }

        public void SetHealth(int health)
        {
            _health = health;
        }

        [System.Serializable]
        class HealthChangeEvent : UnityEvent<int>
        {

        }
    }
}