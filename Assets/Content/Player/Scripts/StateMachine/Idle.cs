using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Idle : PlayerState
{
    public override void Enter()
    {
        Debug.Log("Idle State Entered");
    }

    public override void Run(PlayerInputRecorder playerInput)
    {
        if (playerInput.PrimaryAttack && playerInput.SecondaryAttack)
        {
            if (_player.BiteAttack.CanStart())
            {
                _player.SetAttack(_player.BiteAttack);
            }
        }
        else if (playerInput.PrimaryAttack)
        {
            if (_player.ClawSwipeLeftAttack.CanStart())
            {
                _player.SetAttack(_player.ClawSwipeLeftAttack);
            }
        }
        else if (playerInput.SecondaryAttack)
        {
            if (_player.ClawSwipeRightAttack.CanStart())
            {
                _player.SetAttack(_player.ClawSwipeLeftAttack);
            }
        }
        else if (playerInput.WingAttack)
        {
            if (_player.WingFlapleftAttack.CanStart() && _player.WingFlapRightAttack.CanStart())
            {
                _player.SetAttack(_player.WingFlapleftAttack);
                _player.SetAttack(_player.WingFlapRightAttack);
            }
        }
        else if (playerInput.BreathAttack)
        {
            if (_player.BreathAttack.CanStart())
            {
                Debug.Log("BreathingAttackInputConditionsMet");
                _player.SetAttack(_player.BreathAttack);
            }
        }
    }

    public override void Exit()
    {
        ;
    }
}
