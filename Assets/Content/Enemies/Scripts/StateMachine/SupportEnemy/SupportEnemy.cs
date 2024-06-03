using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportEnemy : EnemyStateInit
{
    public override void SetDefaultState(EnemyController ctrl)
    {
        ctrl.ChangeState(new FindClosestEnemy(
            new MoveToTarget(new AttackRanged(true),
                (x) => x.TryGetComponent<EnemyController>(out var enemy) && !enemy.BubbleBuff),
                (x) => !x.BubbleBuff
            ));
    }
}
