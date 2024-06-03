using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : PlayerState
{
    public override void Enter()
    {
        // Lock Movement, Play death animation
        _player.Animator.SetBool(_player.DeathBoolID, true);
    }

    public override void Run(PlayerInputRecorder input)
    {
        throw new System.NotImplementedException();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException(); 
    }
}
