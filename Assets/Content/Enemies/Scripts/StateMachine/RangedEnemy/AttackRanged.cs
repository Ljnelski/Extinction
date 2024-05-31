using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class AttackRanged : EnemyState<EnemyController>
{
    private float _attackCoolDownTimer;
    private float _attackDurationTimer;

    private bool _isAttacking;

    private float _tempAttackDuration = 1f;

    private List<HitBox.HurtBoxHitData> _lastHits;

    Transform _target;

    public AttackRanged(Transform target)
    {
        _target = target;
    }

    public override void Init(EnemyController enemy)
    {
        base.Init(enemy);
        _lastHits = new List<HitBox.HurtBoxHitData>();
    }

    public override void Enter()
    {
        _controller.HitBox.HurtBoxEntered += AddHitObject;
    }

    public override void Run()
    {
        if (_target || Vector3.Distance(_controller.transform.position, _target.position) > _controller.AttackRadius)
        {
            _controller.SetDefaultState();
            return;
        }

        if (_attackCoolDownTimer >= _controller.Stats.AttackSpeed)
        {
            _attackCoolDownTimer = 0;
            _controller.projectileSpawner.Spawn(_target);
        }

        _attackCoolDownTimer += Time.deltaTime;
    }

    public override void Exit()
    {
        _attackCoolDownTimer = 0f;
        _attackCoolDownTimer = 0f;

        _isAttacking = false;

        _controller.HitBox.HurtBoxEntered -= AddHitObject;
    }

    private void AddHitObject(HitBox.HurtBoxHitData hitData)
    {
        _lastHits.Add(hitData);
    }
}



