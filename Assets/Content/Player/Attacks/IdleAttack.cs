using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class IdleAttack : PlayerAttackState
{
    public override void StartAttack()
    {
    }

    public override void RunAttack(PlayerInputRecorder playerInput)
    {
        if (playerInput.PrimaryAttack && playerInput.SecondaryAttack && _player.BiteAttack.CanStart())
        {
            _player.SetAttack(_player.BiteAttack);
            Animator.SetTrigger(_player.BiteTriggerID);
        }
        else if (playerInput.PrimaryAttack && _player.ClawSwipeLeftAttack.CanStart())
        {
            _player.SetAttack(_player.ClawSwipeLeftAttack);
            Animator.SetTrigger(_player.LeftSwipeTriggerID);
        }
        else if (playerInput.SecondaryAttack && _player.ClawSwipeRightAttack.CanStart())
        {
            _player.SetAttack(_player.ClawSwipeLeftAttack);
            Animator.SetTrigger(_player.RightSwipeTriggerID);
        }
        else if (playerInput.WingAttack && _player.WingFlapleftAttack.CanStart() && _player.WingFlapRightAttack.CanStart())
        {
            _player.SetAttack(_player.WingFlapleftAttack);
            _player.SetAttack(_player.WingFlapRightAttack);
            Animator.SetTrigger(_player.WingFlapTriggerID);
        }
        else if (playerInput.BreathAttack && _player.BreathAttack.CanStart())
        {
            _player.SetAttack(_player.BreathAttack);
            Animator.SetBool(_player.BreathBoolID, true);
        }
        //else if (playerInput.Roar)
        //{
        //    _player.SetAttack(_player.RoarAttack);
        //}
    }

    public override bool ForceExit()
    {
        return false;
    }

    public override bool CanStart()
    {
        return true;
    }
}
