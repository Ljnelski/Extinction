using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoarAttack : PlayerAttackState
{
    public override void StartAttack()
    {
        base.StartAttack();
        Debug.Log("RoarAttack");
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
