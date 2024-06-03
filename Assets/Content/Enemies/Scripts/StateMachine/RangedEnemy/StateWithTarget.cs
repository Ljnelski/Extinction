using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateWithTarget : EnemyState<EnemyController>
{
    public int randomId;
    protected Transform _target;
    
    public bool IsTargetInRange()
    {
        return Vector3.Distance(_controller.transform.position, _target.transform.position) <= _controller.AttackRadius + 20;
    }

    public StateWithTarget SetTarget(Transform target)
    {
        _target = target;
        //randomId = Random.Range(0, 99999);
        //Debug.Log($"setTarget {randomId}: {(bool)_target}");
        return this;
    }
}