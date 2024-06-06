using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDead : PlayerState
{
    public override void Enter()
    {
        // Lock Movement, Play death animation
        _player.Animator.SetTrigger(_player.DeathTriggerID);

    }

    public override void Run( )
    {

    }

    public override void Exit()
    {

    }


}
