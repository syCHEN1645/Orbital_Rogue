using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    public Vector2 MovementInput { get; private set; } 
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool[] AttackInputs { get; private set; }

    private void Start() 
    {
        playerInput = GetComponent<PlayerInput>();

        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];
    }

    public void OnMoveInput(InputAction.CallbackContext context) 
    {
        MovementInput = context.ReadValue<Vector2>();
        NormInputX = Mathf.RoundToInt(MovementInput.x);
        NormInputY = Mathf.RoundToInt(MovementInput.y);        
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.primary] = true;
        }
        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.primary] = false;
        }
    }

    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.secondary] = true;
        }
        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.secondary] = false;
        }
    }
}

public enum CombatInputs
{
    primary,
    secondary
}
