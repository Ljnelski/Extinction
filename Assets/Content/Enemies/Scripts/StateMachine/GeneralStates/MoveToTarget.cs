using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : EnemyState<EnemyController>
{
    private Transform _target;
    EnemyState<EnemyController> attackState;

    public MoveToTarget(Transform target, EnemyState<EnemyController> attackState)
    {
        this.attackState = attackState;
        _target = target;
    }

    public override void Init(EnemyController enemy)
    {
        base.Init(enemy);
        _controller.NavAgent.stoppingDistance = _controller.AttackRadius;
    }

    public override void Enter()
    {
        // TODO Play Animation
        _controller.NavAgent.SetDestination(_target.position);
    }

    public override void Run()
    {
        if(!_target)
        {
            // maybe set some iddle state here
            return;
        }

        _controller.NavAgent.SetDestination(_target.position);

        if (_controller.DistanceToPlayer <= _controller.AttackRadius)
        {
            _controller.ChangeState(attackState);
        }
    }

    public override void Exit()
    {
        ;
    }


}
