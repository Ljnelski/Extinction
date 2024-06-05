using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class FindClosestEnemy : EnemyState<EnemyController>
{
    float _cooldown = 1f;
    float _nextSearch = 0f;

    StateWithTarget _nextState;
    System.Predicate<EnemyController> _filter;

    public FindClosestEnemy(StateWithTarget nextState, System.Predicate<EnemyController> filter = null, float cooldown = 1f)
    {
        _cooldown = cooldown;
        _nextState = nextState;
        _filter = filter;
    }
    private Transform _playerPosition;


    public override void Enter()
    {
        
        if (_playerPosition == null)
        {
            _playerPosition = _controller.Player.transform;
        }
        
        _controller.animator.SetBool("moving", true);
        
        
        _nextSearch = Time.time + _cooldown;
    }

    public override void Run()
    {
        if (_controller.DistanceToPlayer > _controller.AttackRadius * 2)
        {
            _controller.NavAgent.SetDestination(_playerPosition.position);
        }

        else
        {

            if (Time.time < _nextSearch) return;

            _nextSearch = Time.time + _cooldown;

            GameObject closestEnemy = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Unit"))
            {
                if (obj == _controller.gameObject) continue;
                if (!obj.TryGetComponent<EnemyController>(out var enemy)) continue;
                if (enemy.ZeroHealth) continue;
                if (_filter != null && !_filter(enemy)) continue;

                float distance = Vector3.Distance(_controller.transform.position, obj.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = obj;
                }
            }

            if (closestEnemy)
            {
                _controller.ChangeState(_nextState.SetTarget(closestEnemy.transform));
            }
        }
    }

    public override void Exit()
    {
    }
}



