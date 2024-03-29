﻿using System.Collections;
using UnityEngine;

namespace PixelCrew.Components.Health
{
    public class HealthChangerComponent : MonoBehaviour
    {
        [SerializeField] private int _value;

        public void SetValue(int value)
        {
            _value = value;
        }

        public void ChangeHealth(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            
            if (healthComponent != null)
            {
                healthComponent.ChangeHealth(_value);
            }
        }
    }
}