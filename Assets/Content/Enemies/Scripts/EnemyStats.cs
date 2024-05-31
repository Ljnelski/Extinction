using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Extinction/Enemy/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _health;

    public float AttackSpeed => _attackSpeed;
    public float Health => _health;
}
