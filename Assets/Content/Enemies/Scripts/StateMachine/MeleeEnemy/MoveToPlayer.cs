using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPlayer : EnemyState<MeleeEnemyController>
{
    private Transform _playerPosition;
    private float _distanceToPlayer;

    public MoveToPlayer(MeleeEnemyController controller) : base(controller)
    {
        _controller.NavAgent.stoppingDistance = _controller.AttackRadius;        
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
        if(_controller.ZeroHealth)
        {
            _controller.ChangeState(_controller.DieState);
            return;
        }

        _controller.NavAgent.SetDestination(_playerPosition.position);

        if (_controller.DistanceToPlayer <= _controller.AttackRadius) 
        {
            _controller.ChangeState(_controller.AttackPlayerState);
        }        
    }

    public override void Exit()
    {
        ;
    }

   
}
