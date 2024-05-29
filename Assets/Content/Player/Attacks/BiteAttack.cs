using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteAttack : PlayerAttackState
{
    public override void StartAttack()
    {
        base.StartAttack();
        Debug.Log("Bite Attack");
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
