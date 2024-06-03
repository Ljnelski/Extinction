using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : PlayerState
{
    public override void Enter()
    {
        // Lock Movement, Play death animation
        _player.Animator.SetTrigger(_player.DeathTriggerID);
    }

    public override void Run(PlayerInputRecorder input)
    {

    }

    public override void Exit()
    {

    }
}
