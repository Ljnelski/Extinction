using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileActionBubble : MonoBehaviour, IProjectileAction
{
    public void Apply(Transform target)
    {
        if (!target.TryGetComponent<EnemyController>(out var enemy)) return;

        enemy.BubbleBuff = true;
    }
}