using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyController : EnemyStateInit
{
    public override void SetDefaultState(EnemyController ctrl)
    {
        ctrl.ChangeState(new MoveToTarget(new AttackPlayer()).SetTarget(ctrl.Player.transform));
    }
}
