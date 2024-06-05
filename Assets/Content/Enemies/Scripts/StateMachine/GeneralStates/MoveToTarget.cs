using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTarget : StateWithTarget
{
    StateWithTarget attackState;
    System.Predicate<Transform> _filter;

    float spawnedAt;

    public MoveToTarget(StateWithTarget attackState, System.Predicate<Transform> filter = null)
    {
        this.attackState = attackState;
        this._filter = filter;
    }

    public override void Init(EnemyController enemy)
    {
        base.Init(enemy);
        _controller.NavAgent.stoppingDistance = _controller.AttackRadius;
    }

    public override void Enter()
    {
        //Debug.Log($"enter {randomId}: {(bool)_target}");
        // TODO Play Animation
        spawnedAt = Time.time;
        _controller.Animator.SetBool("moving", true);
    }

    public override void Run()
    {
        // this is to optimize and let some spawn animations play
        if (spawnedAt - Time.time > 0.2f) return;

        if (!_target)
        {
            _controller.SetDefaultState();
            return;
        }

        if (_filter != null && !_filter(_target))
        {
            _controller.SetDefaultState();
            return;
        }

        if (_target.position != _controller.NavAgent.nextPosition)
        {
            _controller.NavAgent.SetDestination(_target.position);
        }

        if (IsTargetInRange())
        {
            _controller.ChangeState(attackState.SetTarget(_target));
        }
    }

    public override void Exit()
    {
        _controller.Animator.SetBool("moving", false);

    }


}
