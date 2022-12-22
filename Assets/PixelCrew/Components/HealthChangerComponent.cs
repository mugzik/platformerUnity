using System.Collections;
using UnityEngine;

namespace PixelCrew.Components
{
    public class HealthChangerComponent : MonoBehaviour
    {
        [SerializeField] private int _value;

        public void ChangeHealth(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            
            if (healthComponent != null)
            {
                healthComponent.ChanHealth(_value);
            }
        }
    }
}