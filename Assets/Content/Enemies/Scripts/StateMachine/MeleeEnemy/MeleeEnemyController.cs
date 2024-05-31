using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyController : EnemyController
{
    public override void SetDefaultState()
    {
        ChangeState(new MoveToTarget(Player.transform, new AttackPlayer()));
    }
}
