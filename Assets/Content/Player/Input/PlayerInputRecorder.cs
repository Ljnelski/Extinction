using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This Class Record Input from the PlayerInputActions
public class PlayerInputRecorder : MonoBehaviour
{
    private ExtinctionInputActions _inputActions;

    private Vector2 _lookDirection;

    private bool _primaryAttack;
    private bool _secondaryAttack;
    private bool _wingAttack;
    private bool _breathAttack;
    private bool _roar;

    public Vector2 LookDirection => _lookDirection;
    public bool PrimaryAttack => _primaryAttack;
    public bool SecondaryAttack => _secondaryAttack;
    public bool WingAttack => _wingAttack;
    public bool BreathAttack => _breathAttack;
    public bool Roar => _roar;

    public Action TestInputPressed;

    private void Awake()
    {
        _inputActions = new ExtinctionInputActions();

        _inputActions.Player.Turn.performed += OnTurn;
        _inputActions.Player.Turn.canceled += OnTurn;
        
        _inputActions.Player.PrimaryAttack.performed += OnPrimaryAttack;
        _inputActions.Player.PrimaryAttack.canceled += OnPrimaryAttack;

        _inputActions.Player.SecondaryAttack.performed += OnSecondaryAttack;
        _inputActions.Player.SecondaryAttack.canceled += OnSecondaryAttack;

        _inputActions.Player.WingAttack.performed += OnWingAttack;
        _inputActions.Player.WingAttack.canceled += OnWingAttack;

        _inputActions.Player.BreathAttack.performed += OnBreathAttack;
        _inputActions.Player.BreathAttack.canceled += OnBreathAttack;

        _inputActions.Player.Roar.performed += OnRoar;
        _inputActions.Player.Roar.canceled += OnRoar;
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }

    private void OnDestroy()
    {
        _inputActions.Player.Turn.performed -= OnTurn;
        _inputActions.Player.Turn.canceled -= OnTurn;

        _inputActions.Player.PrimaryAttack.performed -= OnPrimaryAttack;
        _inputActions.Player.PrimaryAttack.canceled -= OnPrimaryAttack;

        _inputActions.Player.PrimaryAttack.performed -= OnPrimaryAttack;
        _inputActions.Player.SecondaryAttack.canceled -= OnSecondaryAttack;

        _inputActions.Player.WingAttack.performed -= OnWingAttack;
        _inputActions.Player.WingAttack.canceled -= OnWingAttack;

        _inputActions.Player.BreathAttack.performed -= OnBreathAttack;
        _inputActions.Player.BreathAttack.canceled -= OnBreathAttack;

        _inputActions.Player.Roar.performed -= OnRoar;
        _inputActions.Player.Roar.canceled -= OnRoar;
    }

    private void OnTurn(InputAction.CallbackContext context)
    {
        _lookDirection = context.ReadValue<Vector2>();
    }

    private void OnPrimaryAttack(InputAction.CallbackContext context)
    {
        _primaryAttack = context.ReadValue<float>() > 0;

    }

    private void OnSecondaryAttack(InputAction.CallbackContext context)
    {
        _secondaryAttack = context.ReadValue<float>() > 0;

    }

    private void OnWingAttack(InputAction.CallbackContext context)
    {
        _wingAttack = context.ReadValue<float>() > 0;
    }

    private void OnBreathAttack(InputAction.CallbackContext context)
    {
        _breathAttack = context.ReadValue<float>() > 0;
    }

    private void OnRoar(InputAction.CallbackContext context)
    {
        _roar = context.ReadValue<float>() > 0;
    }

    private void OnTestInput(InputAction.CallbackContext context)
    {
        TestInputPressed?.Invoke();
    }
}
