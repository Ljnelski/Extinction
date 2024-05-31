using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseEnemy : EnemyController
{

    public override void SetDefaultState()
    {
        ChangeState(new MoveToTarget(Player.transform, new AttackPlayer()));
    }
}
