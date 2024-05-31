using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : EnemyState<MeleeEnemyController>
{
    private float _poolTimer;
    private const float TIME_UNTIL_POOL = 5f;

    public Die(MeleeEnemyController enemy) : base(enemy)
    {
        ;
    }

    public override void Enter()
    {
        // Change Color To black
        _controller._colorChanger.TriggerColorChange(Color.black, 0.2f);
        _controller.NavAgent.isStopped = true;
    }

    public override void Run()
    {
        if(_poolTimer >= TIME_UNTIL_POOL)
        {
            // Pool Self
        }

        _poolTimer += Time.deltaTime;
    }

    public override void Exit()
    {
        _poolTimer = 0;
    }

}
