using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class IdleAttack : PlayerAttackState
{
    public override void Run(PlayerInputRecorder playerInput)
    {
        if (playerInput.PrimaryAttack && playerInput.SecondaryAttack)
        {
            if (_player.BiteAttack.CanStart())
            {
                _player.SetAttack(_player.BiteAttack);
                Animator.SetTrigger(_player.BiteTriggerID);
            }
        }
        else if (playerInput.PrimaryAttack)
        {
            if (_player.ClawSwipeLeftAttack.CanStart())
            {
                _player.SetAttack(_player.ClawSwipeLeftAttack);
                Animator.SetTrigger(_player.LeftSwipeTriggerID);
            }
        }
        else if (playerInput.SecondaryAttack)
        {
            if (_player.ClawSwipeRightAttack.CanStart())
            {
                _player.SetAttack(_player.ClawSwipeLeftAttack);
                Animator.SetTrigger(_player.RightSwipeTriggerID);
            }
        }
        else if (playerInput.WingAttack)
        {
            if (_player.WingFlapleftAttack.CanStart() && _player.WingFlapRightAttack.CanStart())
            {
                _player.SetAttack(_player.WingFlapleftAttack);
                _player.SetAttack(_player.WingFlapRightAttack);
                Animator.SetTrigger(_player.WingFlapTriggerID);
            }
        }
        else if (playerInput.BreathAttack)
        {
            if (_player.BreathAttack.CanStart())
            {
                Debug.Log("BreathingAttackInputConditionsMet");
                _player.SetAttack(_player.BreathAttack);
                Animator.SetBool(_player.BreathBoolID, true);
            }
        }
    }
}
