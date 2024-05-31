using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyController : EnemyController, IDamageAble
{
    [SerializeField] private EnemyAttackStats _attackStats;
    [SerializeField] public DebugColorChanger _colorChanger;

    private MoveToPlayer _moveToPlayerState;
    private AttackPlayer _attackPlayerState;
    private Die _dieState;

    private HitBox _hitBox;

    public EnemyAttackStats AttackStats => _attackStats;

    public MoveToPlayer MoveToPlayerState => _moveToPlayerState; 
    public AttackPlayer AttackPlayerState => _attackPlayerState; 
    public Die DieState => _dieState; 

    public HitBox HitBox => _hitBox;
    public float DistanceToPlayer => Vector3.Distance(Player.transform.position, transform.position);

    public void ApplyDamage(float amount)
    {
        TakeDamage(amount);
        _colorChanger.TriggerColorChange(Color.red, 0.5f);
    }

    public void ApplyEffect()
    {
    }

    public void DebugIndicateHit(Color color)
    {
    }

    protected override void Awake()
    {
        base.Awake();

        _moveToPlayerState = new MoveToPlayer(this);
        _attackPlayerState = new AttackPlayer(this);
        _dieState = new Die(this);

        _hitBox = GetComponentInChildren<HitBox>();

        SetFirstState(_moveToPlayerState);
    }    
}
