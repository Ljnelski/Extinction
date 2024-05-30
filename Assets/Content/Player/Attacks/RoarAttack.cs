using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoarAttack : PlayerAttackState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("RoarAttack");
    }

    public override void Run(PlayerInputRecorder playerInput)
    {
        ;
    }
}
