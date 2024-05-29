using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAttack : PlayerAttackState
{
    public override void StartAttack()
    {
        base.StartAttack();
    }    

    public override void RunAttack(PlayerInputRecorder playerInput)
    {
        ;
    }

    public override bool ForceExit()
    {
        return false;
    }
}
