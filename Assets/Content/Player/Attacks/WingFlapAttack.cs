using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingFlapAttack : PlayerAttack
{
    [SerializeField] private float _attackRadius;

    public Transform _sphereCastSource;

    public BodyPart _leftWing;
    public BodyPart _rightWing;

    public override void ExecuteAttack(PlayerStats stats)
    {
        Debug.Log("WING FLAP ATTACK");

        int extraStaminaCost = 0;
        if (_leftWing.IsBroken)
        {
            extraStaminaCost += 1;
        }
        if (_rightWing.IsBroken)
        {
            extraStaminaCost += 1;
        }

        stats.Stamina -= _staminaCost + _damageBroken * extraStaminaCost;

        Collider[] hitEnemies = Physics.OverlapSphere(_sphereCastSource.position, _attackRadius);

        List<Transform> enemiesToTheLeft = new List<Transform>();
        List<Transform> enemiesToTheRight = new List<Transform>();

        Vector2 dirRight = new Vector2(_sphereCastSource.right.x, _sphereCastSource.right.z);
        Vector2 posXZPlayer = new Vector2(_sphereCastSource.position.x, _sphereCastSource.position.z);

        foreach (Collider collider in hitEnemies)
        {
            ITarget indicator = collider.GetComponent<ITarget>();

            if(indicator == null) continue;

            indicator.DebugIndicateHit(Color.red);


            Vector2 posXZTarget = new Vector2(collider.transform.position.x, collider.transform.position.z);
            Vector2 dirToTarget = posXZPlayer - posXZTarget;

            // Target is to the right
            if (Vector2.Dot(dirRight, dirToTarget) < 0)
            {
                enemiesToTheLeft.Add(collider.transform);
                indicator.DebugIndicateHit(Color.red);
            }
            else
            {
                enemiesToTheRight.Add(collider.transform);
                indicator.DebugIndicateHit(Color.green);
            }
        }
    }
}
