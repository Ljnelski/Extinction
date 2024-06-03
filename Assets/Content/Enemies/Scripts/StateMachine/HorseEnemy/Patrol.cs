using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : EnemyState<EnemyController>
{
    Transform Target => PlayerReference.Instance.Get().transform;

    private Vector3 pointA;
    private Vector3 pointB;
    private bool movingToA = true;
    private float patrolTimer;
    private float patrolDuration;

    public override void Init(EnemyController enemy)
    {
        base.Init(enemy);
        _controller.NavAgent.stoppingDistance = 2f; // to avoid objects blocking
    }

    public override void Enter()
    {
        SetRandomPatrolPoints();
        _controller.NavAgent.destination = pointA;
        patrolDuration = Random.Range(5f, 15f);
        patrolTimer = patrolDuration;
    }

    private void SetRandomPatrolPoints()
    {
        //Vector3 direction = (_controller.transform.position - Target.position).normalized;

        var direction = Vector3.forward;
        float distanceFront = Random.Range(4f, 12f);
        float distanceSide = Random.Range(4f, 8f);

        pointA = Target.position + direction * distanceFront + Vector3.left * distanceSide;
        pointB = Target.position + direction * distanceFront + Vector3.right * distanceSide;
    }

    public override void Run()
    {
        if (!_controller.NavAgent.pathPending && _controller.NavAgent.remainingDistance < 2.5f)
        {
            // Switch to the next point
            movingToA = !movingToA;
            _controller.NavAgent.destination = movingToA ? pointA : pointB;
        }

        patrolTimer -= Time.deltaTime;
        if (patrolTimer <= 0f)
        {
            _controller.ChangeState(
                new MoveToTarget(
                    new AttackRanged(true)
                ).SetTarget(Target)
            );
        }
    }

    public override void Exit()
    {
        // Reset any patrol-specific settings if needed
    }
}