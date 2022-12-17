using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        private HeroInputActions _inputActions;

        public void OnHorizontalMovment(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        public void OnBattleRoar(InputAction.CallbackContext context)
        {
            _hero.BattleRoar();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            Debug.Log("E Pressed");
            if (context.performed)
            {
                _hero.Interact();
            }
        }

        private void Awake()
        {
            _inputActions = new HeroInputActions();

            _inputActions.Hero.Movment.performed += OnHorizontalMovment;
            _inputActions.Hero.Movment.canceled += OnHorizontalMovment;
            _inputActions.Hero.BattleRoar.performed += OnBattleRoar;
            _inputActions.Hero.Interact.performed += OnInteract;
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }
    }
}