using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private void Awake()
    {
        _inputActions = new HeroInputActions();

        _inputActions.Hero.Movment.performed    += OnHorizontalMovment;
        _inputActions.Hero.Movment.canceled     += OnHorizontalMovment;
        _inputActions.Hero.BattleRoar.performed += OnBattleRoar;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }
}