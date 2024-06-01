using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyStateInit
{
    public override void SetDefaultState(EnemyController ctrl)
    {
        ctrl.ChangeState(new MoveToTarget(
            new AttackRanged()
        ).SetTarget(ctrl.Player.transform));
    }
}
