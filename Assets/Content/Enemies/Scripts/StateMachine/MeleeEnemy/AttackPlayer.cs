using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class AttackPlayer : StateWithTarget
{
    private float _attackCoolDownTimer;
    private float _attackDurationTimer;

    private bool _isAttacking;

    private float _tempAttackDuration = 1f;

    private List<HitBox.HurtBoxHitData> _lastHits;

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
        if (_isAttacking)
        {
            if (_attackDurationTimer >= _tempAttackDuration)
            {
                _attackDurationTimer = 0f;
                //_controller.HitBox.Deactivate();
                _isAttacking = false;
            }

            _attackDurationTimer += Time.fixedDeltaTime;

            //foreach (var hurtBox in _lastHits)
            //{
            //    hurtBox.Damagable?.ApplyDamage(_controller.AttackStats.DamageToBodyPart);
            //    hurtBox.Breakable?.DoBreakDamage(_controller.AttackStats.DamageToHealth);
            //}

            _lastHits.Clear();
        }
        else
        {
            if (_attackCoolDownTimer >= _controller.Stats.AttackSpeed)
            {
                _attackCoolDownTimer = 0;
                //_controller.HitBox.Activate();
                
                // hard Code the Attack the the player
                _controller.Player.GetComponent<PlayerController>().AttackedByEnemy(
                    _controller.AttackStats.DamageToHealth,
                    _controller.AttackStats.DamageToBodyPart,
                    _controller.transform.position);

                _isAttacking = true;
            }

            _attackCoolDownTimer += Time.fixedDeltaTime;

            if (_controller.DistanceToPlayer > _controller.AttackRadius)
            {
                _controller.SetDefaultState();
            }
        }
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



