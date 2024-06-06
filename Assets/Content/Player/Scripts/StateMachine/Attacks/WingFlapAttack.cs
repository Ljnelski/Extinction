using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WingFlapAttack : PlayerAttackState
{
    [SerializeField] private float _attackRadius;
    [SerializeField] private Transform _sphereCastSource;
    [SerializeField] private WingHemisphere _wingAttackDirection;
    [SerializeField] private LayerMask layermask;

    public override void Enter()
    {
        base.Enter();
        Animator.SetTrigger(_player.WingFlapTriggerID);
    }

    public override void Run( )
    {
        base.Run();

        _player.Rotate();

        Collider[] hitEnemies = Physics.OverlapSphere(_sphereCastSource.position, _attackRadius, layermask);

        Vector2 dirHemisphere = GetHemiSphereDirection();

        Vector2 posXZPlayer = new Vector2(_sphereCastSource.position.x, _sphereCastSource.position.z);

        foreach (Collider collider in hitEnemies)
        {
            IDamageAble indicator = collider.GetComponent<IDamageAble>();

            if (indicator == null) continue;

           // indicator.DebugIndicateHit(Color.red);

            Vector2 posXZTarget = new Vector2(collider.transform.position.x, collider.transform.position.z);
            Vector2 dirToTarget = posXZPlayer - posXZTarget;
            // Target is to the right

            if (Vector2.Dot(dirHemisphere, dirToTarget) > 0)
            {
                //enemies.Add(collider.transform);
                //indicator.DebugIndicateHit(GetHemisphereColor());
            }

            //Vector3 warpDirection = new Vector3(posXZTarget.x + dirToTarget.normalized.x * 2, posXZTarget.y + 5, +dirToTarget.normalized.y * 2);
            if (collider.GetComponentInParent<NavMeshAgent>() != null)
            {
                StartCoroutine(Stun(collider, collider.GetComponentInParent<NavMeshAgent>()));
            }
        }

        if (_player.Stats.Health <= 0)
        {
            _player.SetState(_player.Dead);
        }
    }

    private Vector2 GetHemiSphereDirection()
    {
        switch (_wingAttackDirection)
        {
            case WingHemisphere.Left:
                return new Vector2(-_sphereCastSource.right.x, _sphereCastSource.right.z);
            case WingHemisphere.Right:
                return new Vector2(_sphereCastSource.right.x, _sphereCastSource.right.z);
            default:
                return Vector2.zero;
        }
    }
    private Color GetHemisphereColor()
    {
        switch (_wingAttackDirection)
        {
            case WingHemisphere.Left:
                return Color.red;
            case WingHemisphere.Right:
                return Color.cyan;
            default:
                return Color.magenta;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Vector2 vector = GetHemiSphereDirection();
        Gizmos.DrawLine(_sphereCastSource.position, _sphereCastSource.position + new Vector3(vector.x, 0, vector.y));
    }

    public override void Activate()
    {
        ;
    }

    public override void Deactivate()
    {

    }

    private enum WingHemisphere
    {
        Left,
        Right,
    }
    
    IEnumerator Stun(Collider collider, NavMeshAgent agent)
    {
        // NavMeshAgent agent = collider.GetComponentInParent<NavMeshAgent>();
        //collider.GetComponent<Animator>().SetTrigger("stunned");
        agent.Move(Vector3.Lerp(collider.transform.position, collider.transform.forward * -1, (Time.time / 2)));
                
        agent.isStopped = true;
        
        yield return new WaitForSeconds(2);
        if (agent != null) agent.isStopped = false;
    }
}

