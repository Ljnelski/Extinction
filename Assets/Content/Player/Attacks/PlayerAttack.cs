using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAttack : MonoBehaviour
{
    [SerializeField] protected float _damage;
    [SerializeField] protected float _damageBroken;
    [SerializeField] protected float _staminaCost;
    [SerializeField] protected float _staminaCostBroken;

    [SerializeField] protected float _attackDuration;

    public float Damage => _damage;
    public float DamageBroken => _damageBroken;
    public float StaminaCost => _staminaCost;
    public float StaminaCostBroken => _staminaCostBroken;

    public float AttackDuration => _attackDuration;

    public abstract void ExecuteAttack(PlayerStats stats);
}
