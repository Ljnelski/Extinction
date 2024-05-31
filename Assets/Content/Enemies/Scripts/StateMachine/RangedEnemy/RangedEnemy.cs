using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyController
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void SetDefaultState()
    {
        base.SetDefaultState();
        ChangeState(new MoveToTarget(Player.transform, new AttackRanged(Player.transform)));
    }
}
