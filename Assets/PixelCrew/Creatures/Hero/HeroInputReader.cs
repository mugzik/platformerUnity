using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Creatures.Hero.Hero _hero;
        private HeroInputActions _inputActions;

        public void OnHorizontalMovment(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero?.SetDirection(direction);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero?.Interact();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero?.Attack();
            }
        }

        public void OnRangeAttack(InputAction.CallbackContext context)
        {
            var ultimatePerformedTime = 1f;
            if (context.performed && context.duration >= ultimatePerformedTime)
            {
                _hero?.TrippleRangeAttack();
            }
            else if (context.performed && context.duration < ultimatePerformedTime)
            {
                _hero?.RangeAttack();
            }
        }

        private void Awake()
        {
            _inputActions = new HeroInputActions();

            _inputActions.Hero.Movment.performed += OnHorizontalMovment;
            _inputActions.Hero.Movment.canceled += OnHorizontalMovment;
            _inputActions.Hero.Interact.performed += OnInteract;
            _inputActions.Hero.Attack.performed += OnAttack;
            _inputActions.Hero.RangeAttack.performed += OnRangeAttack;
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDestroy()
        {
            _inputActions.Disable();
            _inputActions = null;
        }
    }
}