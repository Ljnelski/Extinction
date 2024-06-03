using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoarAttack : PlayerAttackState
{
    public override void Activate()
    {

    }

    public override void Deactivate()
    {

    }

    public override void Enter()
    {
        base.Enter();
        Animator.SetTrigger(_player.WingFlapTriggerID);
        Debug.Log("RoarAttack");
    }

    public override void Run(PlayerInputRecorder playerInput)
    {
        base.Run(playerInput);

        if (_player.Stats.Health <= 0)
        {
            _player.SetState(_player.Dead);
        }
    }
}
