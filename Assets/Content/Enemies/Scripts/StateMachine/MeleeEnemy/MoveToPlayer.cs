using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPlayer : EnemyState<EnemyController>
{
    private Transform _playerPosition;
    EnemyState<EnemyController> attackState;

    public MoveToPlayer(EnemyState<EnemyController> attackState)
    {
        this.attackState = attackState;
    }

    public override void Init(EnemyController enemy)
    {
        base.Init(enemy);

        _controller.NavAgent.stoppingDistance = _controller.Player.transform.localScale.x *
            _controller.Player.transform.Find("EnemyStopDistance")
            .GetComponent<CapsuleCollider>().radius + _controller.NavAgent.radius;
    }


    public override void Enter()
    {
        if(_playerPosition == null)
        {
            _playerPosition = _controller.Player.transform;
        }

        // TODO Play Animation
        _controller.NavAgent.SetDestination(_playerPosition.position);
    }

    public override void Run()
    {
        _controller.NavAgent.SetDestination(_playerPosition.position);

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
