using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseEnemy : EnemyStateInit
{
    public override void SetDefaultState(EnemyController ctrl)
    {
        ctrl.ChangeState(new Patrol());
    }
}
