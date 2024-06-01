using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : EnemyState<EnemyController>
{
    private float _poolTimer;
    private const float TIME_UNTIL_POOL = 5f;

    public override void Enter()
    {
        // Change Color To black
        //_controller._colorChanger?.TriggerColorChange(Color.black, 0.2f);
        _controller.NavAgent.isStopped = true;
        _controller.transform.localScale = new Vector3(1, 0.2f, 1);
        Object.Destroy(_controller.gameObject, 3f);
    }

    public override void Run()
    {
        if(_poolTimer >= TIME_UNTIL_POOL)
        {
            // Pool Self
        }

        _poolTimer += Time.fixedDeltaTime;
    }

    public override void Exit()
    {
        _poolTimer = 0;
    }

}
