using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingFlapAttack : PlayerAttackState
{
    [SerializeField] private float _attackRadius;
    [SerializeField] private Transform _sphereCastSource;
    [SerializeField] private WingHemisphere _wingAttackDirection;

    public override void Enter()
    {
        base.Enter();
    }

    public override void Run(PlayerInputRecorder playerInput)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(_sphereCastSource.position, _attackRadius);

        Vector2 dirHemisphere = GetHemiSphereDirection();

        Vector2 posXZPlayer = new Vector2(_sphereCastSource.position.x, _sphereCastSource.position.z);

        foreach (Collider collider in hitEnemies)
        {
            ITarget indicator = collider.GetComponent<ITarget>();

            if (indicator == null) continue;

            indicator.DebugIndicateHit(Color.red);

            Vector2 posXZTarget = new Vector2(collider.transform.position.x, collider.transform.position.z);
            Vector2 dirToTarget = posXZPlayer - posXZTarget;

            // Target is to the right
            if (Vector2.Dot(dirHemisphere, dirToTarget) > 0)
            {
                //enemies.Add(collider.transform);
                indicator.DebugIndicateHit(GetHemisphereColor());
            }
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

    private enum WingHemisphere
    {
        Left,
        Right,
    }
}
